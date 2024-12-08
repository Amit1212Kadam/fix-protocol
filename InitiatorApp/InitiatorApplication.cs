using QuickFix;

namespace InitiatorApp
{
    class InitiatorApplication : IApplication
    {
        public void FromAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine($"[FromAdmin] {message}");
        }

        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine($"[FromApp] {message}");
        }

        public void OnCreate(SessionID sessionID)
        {
            Console.WriteLine($"[OnCreate] Session created: {sessionID}");
        }

        public void OnLogon(SessionID sessionID)
        {
            Console.WriteLine($"[OnLogon] Logon successful: {sessionID}");
        }

        public void OnLogout(SessionID sessionID)
        {
            Console.WriteLine($"[OnLogout] Logout occurred: {sessionID}");
        }

        public void ToAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine($"[ToAdmin] {message}");
        }

        public void ToApp(Message message, SessionID sessionID)
        {
            Console.WriteLine($"[ToApp] {message}");
        }
    }
}
