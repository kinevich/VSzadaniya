using System;
using System.Xml.Serialization;

namespace Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            TheBeginning();
        }

        private static void ChooseColorStyle()
        {
            int choice = 0;
            while (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5 && choice != 6 && choice != 7)
            {
                Console.WriteLine("Enter number:\n1)Light console style\n2)Green console style\n3)Gray console style" +
                                  "\n4)Cyan console style\n5)Magenta console style\n6)Blue console style" +
                                  "\n7)Acid console style");
                int.TryParse(Console.ReadLine(), out choice);
            }

            switch (choice)
            {
                case 1:
                    SetConsoleColors(new LightConsoleStyle());
                    break;
                case 2:
                    SetConsoleColors(new GreenConsoleStyle());
                    break;
                case 3:
                    SetConsoleColors(new GrayConsoleStyle());
                    break;
                case 4:
                    SetConsoleColors(new CyanConsoleStyle());
                    break;
                case 5:
                    SetConsoleColors(new MagentaConsoleStyle());
                    break;
                case 6:
                    SetConsoleColors(new BlueConsoleStyle());
                    break;
                case 7:
                    SetConsoleColors(new AcidConsoleStyle());
                    break;
            }
        }

        private static void SetConsoleColors(IConsoleStyleFactory factory)
        {
            var consoleStyle = new ConsoleStyle(factory);
            Console.BackgroundColor = consoleStyle.BackgroundColor;
            Console.ForegroundColor = consoleStyle.ForegroundColor;
            Console.Clear();
        }

        public static void TheBeginning()
        {
            ChooseColorStyle();

            Console.WriteLine("Добро пожаловать в симулятор водителя.\n" +
                "Нажми 'Enter', чтобы начать.");
            Console.ReadLine();
            Console.Clear();
            First();
        }

        public static void First()
        {
            ChooseColorStyle();

            int choice = 0;
            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("Ты не нарушал правила дорожного движени, но тебя пытается остановить ГАИшник.\n" +
                "Выбери один вариант.\n" +
                "1. Не останавливаться и ехать дальше.\n" +
                "2. Остановиться сразу.");
                Console.Write("Твой выбор: ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            Console.Clear();

            switch (choice)
            {
                case 1:
                    {
                        Second();
                        break;
                    }
                case 2:
                    {
                        Third();
                        break;
                    }
            }
        }

        public static void Second()
        {
            ChooseColorStyle();

            int choice = 0;
            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("За тобой погналась машина с включенными мигалками.");
                Console.WriteLine("ГАИшник говорит в громкоговоритель, чтобы ты остановился.Остановишься?");
                Console.Write("Твой ответ: ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            if (choice == 1)
                Third();
            else
                BadTheEnd();
        }

        public static void Third()
        {
            ChooseColorStyle();

            int choice = 0;
            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("К машине подходит ГАИшник и просит предьявить документы.\n" +
                "1. Предьявить документы.\n" +
                "2. Не предъявлять.");
                Console.Write("Твои действия.(напиши номер пункта): ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            Console.Clear();

            if (choice == 1)
                TheEnd();
            else
                GoodTheEnd();
        }

        public static void BadTheEnd()
        {
            ChooseColorStyle();

            Console.Clear();
            Console.WriteLine("Тебе выпишут штраф, за то, что не остановился.");
            Console.WriteLine("Чтобы попробовать ещё раз, нажми 'Enter'.");
            Console.ReadLine();
            Console.Clear();
            TheBeginning();
        }

        public static void TheEnd()
        {
            ChooseColorStyle();

            Console.WriteLine("ГАИшники узнают твои данные и на тебя напишут постановление, которое ты можешь обжаловать в суде.");
            Console.WriteLine("Чтобы попробовать ещё раз, нажми 'Enter'.");
            Console.ReadLine();
            Console.Clear();
            TheBeginning();
        }

        public static void GoodTheEnd()
        {
            ChooseColorStyle();

            Console.WriteLine("ГАИшники не узнают твои данные и в итоге не смогут написать постановление.");
            Console.WriteLine("Чтобы попробовать ещё раз, нажми 'Enter'.");
            Console.ReadLine();
            Console.Clear();
            TheBeginning();
        }
    }
}
