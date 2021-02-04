using System;

namespace Hearts
{
    class Program
    {
        static void Main(string[] args)
        {
            var rockfeller = new Rockefeller();

            Console.WriteLine("Human heart - 1; Plastic heart - 2; Tube heart - 3");
            Console.Write("Enter number: ");
            int num = int.Parse(Console.ReadLine());

            switch (num)
            {
                case 1:
                    rockfeller.InstallHeart(new HumanHeart());
                    rockfeller.ShowStatus();
                    break;
                case 2:
                    rockfeller.InstallHeart(new PlasticHeart());
                    rockfeller.ShowStatus();
                    break;
                case 3:
                    rockfeller.InstallHeart(new TubeHeart());
                    rockfeller.ShowStatus();
                    break;
            }               
        }
    }

    class Rockefeller 
    {
        private IHeart _heart;

        public void InstallHeart(IHeart heart)
        {
            _heart = heart;
            _heart.Connect();
        }
        
        public void ShowStatus()
        {
            Console.WriteLine(_heart.GetStatus());
        }
    }

    interface IHeart
    {
        void Connect();

        string GetStatus();

        double HeartRate { get; }
    }

    class HumanHeart : IHeart
    {
        public string PreviousOwnerName { get; }

        public double HeartRate { get; }

        public HumanHeart()
        {
            HeartRate = 5;
            PreviousOwnerName = "Bill Gates";
        }

        public void Connect()
        {
            Console.WriteLine("Human heart inserted.");
        }

        public string GetStatus()
        {
            return $"Heart rate: {HeartRate}; Previous owner name: {PreviousOwnerName}.";
        }
    }

    class PlasticHeart : IHeart
    {
        public Guid SerialNumber { get; }

        public double HeartRate { get; }

        public PlasticHeart()
        {
            HeartRate = 6;
            SerialNumber = Guid.NewGuid();
        }

        public void Connect()
        {
            Console.WriteLine("Plastic heart inserted.");
        }

        public string GetStatus()
        {
            return $"Heart rate: {HeartRate}; Serial number {SerialNumber}.";
        }
    }

    class TubeHeart : IHeart
    {
        public decimal Price { get; }

        public double HeartRate { get; }

        public TubeHeart()
        {
            HeartRate = 7;
            Price = 333;
        }

        public void Connect()
        {
            Console.WriteLine("Tube heart inserted.");
        }

        public string GetStatus()
        {
            return $"Heart rate: {HeartRate}; Price {Price}.";
        }
    }
}



