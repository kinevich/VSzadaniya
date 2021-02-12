using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    class UncleMajorMessenger : IMessenger
    {
        List<string> _bannedWords = new List<string> {"ФСБ", "ГРУ", "КГБ", "МВД"};
            
        public string Name { get; set; }

        private IMessenger _to { get; set; }

        public void Connect(IMessenger to)
        {
            _to = to;
        }

        public void OnMessage(IMessenger sender, string message)
        {
            _to.OnMessage(sender, message);

            if (_bannedWords.Any(s => message.Contains(s)))
            {
                Console.WriteLine("За вами уже выехали.");
            }
        }

        public void Send(string message) { }
    }
}
