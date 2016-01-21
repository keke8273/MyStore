using ParcelTracking.Contacts;

namespace ParcelTracking
{
    public interface IInterpreter
    {
        ParcelState Translate(string message);

        string Name { get; }
    }
}
