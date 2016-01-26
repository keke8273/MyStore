using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Utils;
using ParcelTracking.Contacts.Commands;
using ParcelTracking.Dto;
using ParcelTracking.ReadModel;

namespace MyStore.Server.WebApi.Controllers
{
    [RoutePrefix("api/parcel")]
    public class ParcelController : ApiController
    {
        private readonly IParcelStatusDao _parcelStatusDao;
        private readonly ICommandBus _commandBus;

        public ParcelController(IParcelStatusDao parcelStatusDao, ICommandBus commandBus)
        {
            _parcelStatusDao = parcelStatusDao;
            _commandBus = commandBus;
        }

        [Route("{id:guid}")]
        [HttpGet]
        [ResponseType(typeof (ParcelStatusDto))]
        public IHttpActionResult GetParcelStatus(Guid id)
        {
            var parcelStatus = _parcelStatusDao.GetParcel(id);

            if (parcelStatus == null)
                return NotFound();

            return Ok(Mapper.Map<ParcelStatus, ParcelStatusDto>(parcelStatus));
        }

        [Route("{expressProvider}/{trackingNumber}")]
        [HttpGet]
        [ResponseType(typeof (Guid))]
        public IHttpActionResult FindParcel(string expressProvider, string trackingNumber)
        {
            //todo:: should i look for parcel or parcelStatus
            var parcelStatus =
                _parcelStatusDao.FindParcelByExpressProvider(expressProvider)
                    .FirstOrDefault(p => p.TrackingNumber == trackingNumber);

            if (parcelStatus != null)
                return Ok(parcelStatus.Id);

            return NotFound();
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof (Guid))]
        public IHttpActionResult CreateParcel(ParcelDto dto)
        {
            var parcelId = GuidUtil.NewSequentialId();
            var createParcelCommand = new CreateParcel(parcelId)
            {
                ExpressProvider = dto.ExpressionProvider,
                TrackingNumber = dto.TrackingNumber,
            };

            _commandBus.Send(createParcelCommand);

            return Ok(parcelId);
        }
    }
}