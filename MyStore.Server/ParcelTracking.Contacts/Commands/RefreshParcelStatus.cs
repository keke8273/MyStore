using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.Contacts.Commands
{
    public class RefreshParcelStatus : ParcelCommand
    {
        public Guid ParcelId { get; set; }
    }
}
