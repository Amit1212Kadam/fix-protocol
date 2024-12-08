using QuickFix;
using QuickFix.Fields;

namespace InitiatorApp
{
    class InitiatorApplication : IApplication
    {
        public void FromAdmin(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"[FromAdmin] {message}");
            string msgType = message.Header.GetString(Tags.MsgType);

            if (msgType == MsgType.HEARTBEAT)
            {
                Console.WriteLine($"[Initiator] Received heartbeat from {sessionID}");
            }
        }

        public void FromApp(QuickFix.Message message, SessionID sessionID)
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

        public void ToAdmin(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"[ToAdmin] {message}");
            string msgType = message.Header.GetString(Tags.MsgType);

            if (msgType == MsgType.HEARTBEAT)
            {
                Console.WriteLine($"[Initiator] Sending heartbeat from {sessionID}");
            }
        }

        public void ToApp(QuickFix.Message message, SessionID sessionID)
        {
            Console.WriteLine($"[ToApp] {message}");
        }

        public void SendNewOrder(SessionID sessionID)
        {
            try
            {
                QuickFix.FIX44.NewOrderSingle newOrder = new QuickFix.FIX44.NewOrderSingle(
                    new ClOrdID("12345"),
                    new Symbol("AAPL"),
                    new Side(Side.BUY),
                    new TransactTime(DateTime.Now),
                    new OrdType(OrdType.MARKET)
                );
                // Stock Symbol
                newOrder.Set(new OrderQty(100));         // Quantity
                newOrder.Set(new Price(150.00m));        // Price

                Session session = Session.LookupSession(sessionID);
                var isavailable = session.IsLoggedOn;

                if (isavailable)
                    session.Send(newOrder);
                // Send the message
                //if (Session.SendToTarget(newOrder, sessionID))
                //{
                //    Console.WriteLine($"[Initiator] New Order sent: {newOrder}");
                //}
                //else
                //{
                //    Console.WriteLine($"[Initiator] Failed to send New Order: {newOrder}");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Initiator] Error sending New Order: {ex.Message}");
            }
        }
    }
}
