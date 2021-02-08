namespace Messenger
{
    interface IMessenger
    {
        string Name { get; set; }

        void Connect(IMessenger to);

        void Send(string message);

        void OnMessage(IMessenger sender, string message);
    }
}