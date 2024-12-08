using QuickFix;
using QuickFix.Logger;
using QuickFix.Store;

namespace AcceptorApp
{

    internal class Program
    {
        static void Main(string[] args)
        {
            SessionSettings settings = new SessionSettings("acceptor.cfg");
            var myApp = new AcceptorApplication();
            IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
            ILogFactory logFactory = new FileLogFactory(settings);
            ThreadedSocketAcceptor acceptor = new ThreadedSocketAcceptor(myApp, storeFactory, settings, logFactory);

            
            bool running = true;
            Console.WriteLine("FIX Protocol Manager");
            Console.WriteLine("1: Start Acceptor");
            Console.WriteLine("2: Stop Acceptor");
            while (running)
            {
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": // Start Acceptor
                        if (!acceptor.IsLoggedOn)
                        {
                            acceptor.Start();
                            Console.WriteLine("Acceptor started.");
                        }
                        else
                        {
                            Console.WriteLine("Acceptor is already running.");
                        }
                        break;

                    case "2": // Stop Acceptor
                        if (acceptor.IsLoggedOn)
                        {
                            acceptor.Stop();
                            Console.WriteLine("Acceptor stopped.");
                        }
                        else
                        {
                            Console.WriteLine("Acceptor is not running.");
                        }
                        break;
                    case "3": // Log off Acceptor
                        if (acceptor.IsLoggedOn)
                        {
                            myApp.ForceLogout(acceptor.GetSessionIDs().FirstOrDefault());
                            Console.WriteLine("Acceptor Logged Off.");
                        }
                        else
                        {
                            Console.WriteLine("Acceptor is not running.");
                        }
                        break;
                    case "4": // Log In Acceptor
                        if (!acceptor.IsLoggedOn)
                        {
                            myApp.ForceLogIn(acceptor.GetSessionIDs().FirstOrDefault());
                            Console.WriteLine("Acceptor Logged In.");
                        }
                        else
                        {
                            Console.WriteLine("Acceptor is not running.");
                        }
                        break;
                    case "0": // Exit
                        if (acceptor.IsLoggedOn)
                        {
                            acceptor.Stop();
                            Console.WriteLine("Acceptor stopped.");
                        }
                        Console.WriteLine("Exiting...");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }

            //acceptor.Start();
            //Console.WriteLine("Acceptor started.");
            //Console.ReadLine();
            //acceptor.Stop();
            //Console.WriteLine("Acceptor stopped.");
        }
    }
}
