
using System;
using System.Collections.Generic;
using System.Linq;
using MyStore.Client.Common;
using Subscription.Dto;

namespace MyStore.Client.Console
{
    class Program
    {
        private static WebApiService _webApiService;
        private static Dictionary<Guid, Guid> _parcelSubscriptions = new Dictionary<Guid, Guid>();
        private static Guid _userId;

        static void Main(string[] args)
        {
            _webApiService = new WebApiService("http://localhost:3400", "application/json");
            _userId = _webApiService.Login().Result;

            while (true)
            {
                System.Console.WriteLine("Command:");
                System.Console.WriteLine("1.Register a parcel");
                System.Console.WriteLine("2.Check a parcel");

                var command = System.Console.ReadLine();

                ProcessingCommand(command);
            }
        }

        private static void ProcessingCommand(string command)
        {
            switch (command)
            {
                case "1":
                {
                    System.Console.WriteLine("Enter Tracking Number:");
                    var trackingNumber = System.Console.ReadLine();
                    var parcelId = _webApiService.FindOrCreateParcelAsync("Emms", trackingNumber).Result;

                    var subId = _webApiService.SubscribeParcelTrackingAsync(new SubscriptionDto
                    {
                        SubscribeeId = parcelId,
                        SubscriberId = _userId,
                    }).Result;

                    if (subId == Guid.Empty)
                        System.Console.WriteLine("Subscription Failed");
                    else
                    {
                        System.Console.WriteLine("Parcel {0} subscribed.", parcelId);
                        _parcelSubscriptions.Add(subId, parcelId);
                    }
                }
                    break;
                case "2":
                {
                    System.Console.WriteLine("Choose a Parcel:");
                    foreach (var parcelGuid in _parcelSubscriptions)
                    {
                        System.Console.WriteLine(parcelGuid.Value);
                    }
                    var index = Int32.Parse(System.Console.ReadLine());
                    var parcelId = _parcelSubscriptions.ToList()[index].Value;
                    var parcelStatus = _webApiService.GetParcelStatusAsync(parcelId).Result;
                    System.Console.WriteLine(parcelStatus);
                }
                    break;
            }
        }
    }
}
