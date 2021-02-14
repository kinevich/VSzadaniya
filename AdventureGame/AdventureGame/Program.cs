using System;
using System.Xml.Serialization;

namespace Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            ChooseColorStyle();
            TheBeginning();
        }

        private static void ChooseColorStyle()
        {
            int choice = 0;
            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("Enter number:\n1)Light console style\n2)Green console style");
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
            Console.WriteLine("Добро пожаловать в симулятор водителя.\n" +
                "Нажми 'Enter', чтобы начать.");
            Console.ReadLine();
            Console.Clear();
            First();
        }

        public static void First()
        {
            string choice;
            Console.WriteLine("Ты не нарушал правила дорожного движени, но тебя пытается остановить ГАИшник.\n" +
                "Выбери один вариант.\n" +
                "1. Не останавливаться и ехать дальше.\n" +
                "2. Остановиться сразу.");
            Console.Write("Твой выбор: ");
            choice = Console.ReadLine().ToLower();
            Console.Clear();

            switch (choice)
            {
                case "1":
                case "не останавливаться":
                case "ехать дальше":
                    {

                        Second();
                        break;
                    }
                case "2":
                case "остановиться":
                case "остановиться сразу":
                    {

                        Third();
                        break;
                    }
            }
        }

        public static void Second()
        {
            Console.WriteLine("За тобой погналась машина с включенными мигалками.");
            Random rand = new Random();
            string[] secOptions = { "ГАИшник говорит в громкоговоритель, чтобы ты остановился.",
                "ГАИшник говорит в громкоговоритель, чтобы ты немедленно остановился." };
            int randomNumber = rand.Next(0, 2);
            string secText = secOptions[randomNumber];

            string secChoice;

            Console.WriteLine(secText + "\nОстановишься ли ты?");
            Console.Write("Твой ответ: ");
            secChoice = Console.ReadLine().ToLower();

            if (secChoice == "да" || secChoice == "остановлюсь")
            {
                Third();
            }
            else if (secChoice == "нет" || secChoice == "не остановлюсь")
            {
                BadTheEnd();
            }
        }

        public static void Third()
        {
            Console.Clear();

            Console.WriteLine("К машине подходит ГАИшник и просит предьявить документы.\n" +
                "1. Предьявить документы.\n" +
                "2. Не предъявлять.");
            Console.Write("Твои действия.(напиши номер пункта): ");

            int.TryParse(Console.ReadLine(), out int choice);

            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("Ты ввел несуществущий номер пункта.");
                Console.Write("Введи ещё раз: ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            if (choice == 1)
            {
                TheEnd();
            }
            else
            {
                GoodTheEnd();
            }
        }

        public static void BadTheEnd()
        {
            Console.Clear();
            Console.WriteLine("Тебе выпишут штраф, за то, что не остановился.");
            Console.WriteLine("Чтобы попробовать ещё раз, нажми 'Enter'.");
            Console.ReadLine();
            Console.Clear();
            TheBeginning();
        }

        public static void TheEnd()
        {
            Console.Clear();
            Console.WriteLine("ГАИшники узнают твои данные и на тебя напишут постановление, которое ты можешь обжаловать в суде.");
            Console.WriteLine("Чтобы попробовать ещё раз, нажми 'Enter'.");
            Console.ReadLine();
            Console.Clear();
            TheBeginning();
        }

        public static void GoodTheEnd()
        {
            Console.Clear();
            Console.WriteLine("ГАИшники не узнают твои данные и в итоге не смогут написать постановление.");
            Console.WriteLine("Чтобы попробовать ещё раз, нажми 'Enter'.");
            Console.ReadLine();
            Console.Clear();
            TheBeginning();
        }
    }
}
