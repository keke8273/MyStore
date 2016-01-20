using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParcelTracking.Parsers;
using System.Threading.Tasks;

namespace ParcelTracking
{
    public interface IInterpreter
    {
        State Translate(string message);

        string Name { get; }
    }
}
