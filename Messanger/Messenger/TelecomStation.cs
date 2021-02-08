namespace Messenger
{
    class TelecomStation
    {
        public static void Connect(IMessenger from, IMessenger to)
        {
            var uncle = new UncleMajorMessenger();
            uncle.Connect(to);
            from.Connect(uncle);
        }
    }
}