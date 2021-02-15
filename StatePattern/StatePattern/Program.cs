using System;

namespace StatePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
        }
    }

    enum PhoneState
    {
        Normal,
        Connection,
        Connected,
        InStandBy
    }

    class Phone
    {
        public PhoneState State { get; private set; }

        public Phone()
        {
            State = PhoneState.Normal;
        }

        public void Call()
        {
            if (State == PhoneState.Normal)
            {
                State = PhoneState.Connection;

                int choice;
                do
                {
                    Console.WriteLine("1)Pick up\n2)Hang up");
                    int.TryParse(Console.ReadLine(), out choice);
                }
                while (choice != 1 && choice != 2);

                switch (choice)
                {
                    case 1:
                        PickUp();
                        break;
                    case 2:
                        HangUp();
                        break;
                }
            }

            if (State == PhoneState.Connected)
            {
                State = PhoneState.Connection;

                int choice;
                do
                {
                    Console.WriteLine("1)Hang up\n2)Stand by on");
                    int.TryParse(Console.ReadLine(), out choice);
                }
                while (choice != 1 && choice != 2 && choice != 3);

                switch (choice)
                {
                    case 1:
                        PickUp();
                        break;
                    case 2:
                        HangUp();
                        break;
                    case 3:
                        StandByOn();
                        break;
                }
            }
        }

        public void PickUp()
        {
            if (State == PhoneState.Normal)
            {
                State = PhoneState.Connection;

                int choice;
                do
                {
                    Console.WriteLine("1)Pick up\n2)Hang up");
                    int.TryParse(Console.ReadLine(), out choice);
                }
                while (choice != 1 && choice != 2);

                switch (choice)
                {
                    case 1:
                        PickUp();
                        break;
                    case 2:
                        HangUp();
                        break;
                }
            }

            if (State == PhoneState.Connected)
            {
                State = PhoneState.Connected;

                int choice;
                do
                {
                    Console.WriteLine("1)Stand by on\n2)Hang up");
                    int.TryParse(Console.ReadLine(), out choice);
                }
                while (choice != 1 && choice != 2);

                switch (choice)
                {
                    case 1:
                        PickUp();
                        break;
                    case 2:
                        HangUp();
                        break;
                }
            }
        }

        public void HangUp()
        {

        }

        public void StandByOn()
        {

        }

        public void StandByOff()
        {

        }
    }
}
