using System;
using System.Text;

namespace Messenger
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.OutputEncoding = Encoding.UTF8;

            var vova = new Messenger("vova");
            var dima = new Messenger("dima");

            TelecomStation.Connect(vova, dima);

            vova.Send("Димон чемпион! ФСБ");

            Console.ReadKey();
        }
    }
}
