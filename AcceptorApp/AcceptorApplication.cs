using QuickFix;

namespace AcceptorApp
{
    class AcceptorApplication : IApplication
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
        public void ForceLogout(SessionID sessionID)
        {
            Console.WriteLine($"[Acceptor] Forcefully logging off session: {sessionID}");
            Session session = Session.LookupSession(sessionID);
            if (session != null)
            {
                session.Logout("Forced logout by Acceptor");
                //session.Disconnect("Forced logout by Acceptor");
                //session.Dispose();
            }
            else
            {
                Console.WriteLine($"[Acceptor] Session not found: {sessionID}");
            }
        }

        public void ForceLogIn(SessionID sessionID)
        {
            Console.WriteLine($"[Acceptor] Forcefully logging In session: {sessionID}");
            Session session = Session.LookupSession(sessionID);
            if (session != null)
            {
                session.Logon();
            }
            else
            {
                Console.WriteLine($"[Acceptor] Session not found: {sessionID}");
            }
        }
    }
}
