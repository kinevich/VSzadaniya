using System;

namespace Messenger
{
    class Messenger : IMessenger
    {
        public Messenger(string name)
        {
            Name = name;
        }

        private IMessenger _to { get; set; }

        public string Name { get; set; }

        public void Connect(IMessenger to)
        {
            _to = to;
        }

        public void Send(string message)
        {
            _to.OnMessage(this, message);
        }

        public void OnMessage(IMessenger sender, string message)
        {
            Console.WriteLine(Name + " received message from " + sender.Name + ": " + message);
        }
    }
}