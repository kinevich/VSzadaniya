using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace River
{
    class River
    {
        public static int Border = 50;  // ГРАНИЦА

        public static int NumberOfFishes = 100;  // КОЛ-ВО РЫБ

        public List<Fish> Fishes = new List<Fish>();

        private Random _random = new Random();

        private Pairs _pairs = new Pairs();

        public void Start()
        {
            CreateFishes();
            for (int sec = 0; sec < 20; ++sec)
            {
                foreach(Fish f in Fishes.ToList())
                {
                    f.Move();
                }
                Thread.Sleep(100);
            }
        }
        private void CreateFishes()
        {
            for (int i = 0; i < NumberOfFishes; ++i)
            {
                int c = _random.Next(2);
                if (c == 0)
                {
                    Fishes.Add(new Rudd(this, _pairs));
                }
                else
                {
                    Fishes.Add(new Pike(this));
                }
            }
        }

    }
}


