using System;

namespace MyStore.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var processor = new StoreProcessor())
            {
                processor.Start();
                Console.WriteLine("Worker started");
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();

                processor.Stop();
            }
        }
    }
}
