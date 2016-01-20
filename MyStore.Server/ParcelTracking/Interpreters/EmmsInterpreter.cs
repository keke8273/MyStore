using ParcelTracking.Contacts.Commands;
using ParcelTracking.Contacts.Events;
using ParcelTracking.Events;
using ParcelTracking.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelTracking.Interpreters
{
    public class EmmsInterpreter : IInterpreter
    {
        private const string _name = "Emms";

        public State Translate(string message)
        {

        }


        public string Name
        {
            get { throw new NotImplementedException(); }
        }
    }
}
