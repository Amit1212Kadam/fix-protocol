using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;
using QuickFix.Transport;

namespace InitiatorApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SessionSettings settings = new SessionSettings("initiator.cfg");
            IApplication myApp = new InitiatorApplication();
            IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
            ILogFactory logFactory = new FileLogFactory(settings);
            SocketInitiator initiator = new SocketInitiator(myApp, storeFactory, settings, logFactory);

            bool running = true;
            Console.WriteLine("FIX Protocol Manager");
            Console.WriteLine("1: Start Initiator");
            Console.WriteLine("2: Stop Initiator");

            while (running)
            {
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": // Start Initiator
                        if (!initiator.IsLoggedOn)
                        {
                            initiator.Start();
                            Console.WriteLine("Initiator started.");
                        }
                        else
                        {
                            Console.WriteLine("Initiator is already running.");
                        }
                        break;

                    case "2": // Stop Initiator
                        if (initiator.IsLoggedOn)
                        {
                            initiator.Stop();
                            Console.WriteLine("Initiator stopped.");
                        }
                        else
                        {
                            Console.WriteLine("Initiator is not running.");
                        }
                        break;

                    case "0": // Exit
                        if (initiator.IsLoggedOn)
                        {
                            initiator.Stop();
                            Console.WriteLine("Initiator stopped.");
                        }
                        Console.WriteLine("Exiting...");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
            //initiator.Start();
            //Console.WriteLine("Initiator started.");
            //Console.ReadLine();
            //initiator.Stop();
            //Console.WriteLine("Initiator stopped.");
        }
    }
}
