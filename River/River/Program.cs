using System;
using System.Collections.Generic;
using System.Linq;

namespace River
{
    abstract class Fish
    {
        protected River Riv;
        private double X;
        private double Y;
        private double Z;
        public double Speed { get; protected set; }
        Random random = new Random();
        public Fish(River river)
        {
            Riv = river;
            X = random.Next(River.Border);
            Y = random.Next(River.Border);
            Z = random.Next(River.Border);
        }

        public void Move()
        {
            int caseSwitch = random.Next(2);
            switch (caseSwitch)
            {
                case 0:
                    ChangeCoordinatesPositive();
                    break;
                case 1:
                    ChangeCoordinatesNegative();
                    break;
            }
        }
        private void ChangeCoordinatesNegative()
        {
            X -= Speed;
            Y -= Speed;
            Z -= Speed;
            BorderCheck(ref X);
            BorderCheck(ref Y);
            BorderCheck(ref Z);
        }

        private void ChangeCoordinatesPositive()
        {
            X += Speed;
            Y += Speed;
            Z += Speed;
            BorderCheck(ref X);
            BorderCheck(ref Y);
            BorderCheck(ref Z);
        }

        private void BorderCheck(ref double fishCoordinate)
        {
            if (fishCoordinate > River.Border)
            {
                fishCoordinate -= Speed;
            }
            else if (fishCoordinate < 0)
            {
                fishCoordinate -= fishCoordinate;
            }
        }
        
        protected double DistBetwFishes(Fish f1, Fish f2)
        {
            return Math.Sqrt((f1.X - f2.X) * (f1.X - f2.X) + (f1.Y - f2.Y) * (f1.Y - f2.Y) + (f1.Z - f2.Z) * (f1.Z - f2.Z));
        }
    } 
        
    class Rudd : Fish
    {
        public Rudd(River river) : base(river)
        { 
            Speed = 2;
        }
        private int _timesTogether;
        public void Born(double bornDist, int timesToBorn, Pairs pairs)
        {
            foreach (Fish f in Riv.Fishes.ToList())
            {
                if (f is Rudd r)
                {
                    if(DistBetwFishes(this, r) < bornDist)
                    {
                        pairs.list.Add(new Pair(this, r));
                    }
                }
            }
            foreach (Pair pair1 in pairs.list.ToList())
            {
                foreach (Pair pair2 in pairs.list.ToList())
                {
                    if (pair1.R1 == pair2.R2 && pair1.R2 == pair2.R1)
                        pairs.list.Remove(pair2);
                }
            }
            foreach (Pair pair1 in pairs.list.ToList())
            {
                int i = 0;
                foreach (Pair pair2 in pairs.list.ToList())
                {
                    if (pair1.R1 == pair2.R1 && pair1.R2 == pair2.R2)
                        ++i;
                }
                if (i == timesToBorn)
                    Riv.Fishes.Add(new Rudd(Riv));
            }

        }

         
    }
    
    class Pair
    {
        public Rudd R1 { get; private set; }
        public Rudd R2 { get; private set; }
        public Pair(Rudd r1, Rudd r2)
        {
            R1 = r1; R2 = r2;
        }
    }

    class Pairs
    {
        public List<Pair> list = new List<Pair>();
    }

    class Pike : Fish
    {
        public Pike(River river) : base(river) { Speed = 5; }
        private int _timesHungry;
        private double _weight;
        private bool _hungry = true;

        public void Eat(double pikeEatDist, int timesToDie)
        {
            foreach (Fish f in Riv.Fishes.ToList())
            {
                if (f is Rudd r)
                {
                    if (DistBetwFishes(this, r) < pikeEatDist)
                    {
                        Riv.Fishes.Remove(r);
                        ++_weight;
                        _timesHungry = 0;
                        Console.WriteLine("Rudd died");
                        _hungry = false;
                        break;
                    }
                }
            }
            if (_hungry)
                ++_timesHungry;
            if (_timesHungry == timesToDie)
            {
                Riv.Fishes.Remove(this);
                Console.WriteLine("Pike died");
            }
            _hungry = true;
        }
    }

    class River
    {
        public static int Border = 45;  // ГРАНИЦА
        public static int NumberOfFishes = 50;  // КОЛ-ВО РЫБ

        public List<Fish> Fishes = new List<Fish>();
        Random random = new Random();
        Pairs pairs = new Pairs();

        public void Start()
        {
            CreateFishes();
            for (int sec = 0; sec < 20; ++sec)
            {
                foreach(Fish f in Fishes.ToList())
                {
                    f.Move();
                }
                foreach(Fish f in Fishes.ToList())
                {
                    switch (f)
                    {
                        case Pike p:
                            p.Eat(1, 3);
                            break;
                        case Rudd r:
                            r.Born(1, 3, pairs);
                            break;
                    }
                }
            }
        }
        private void CreateFishes()
        {
            for (int i = 0; i < NumberOfFishes; ++i)
            {
                int c = random.Next(2);
                if (c == 0)
                {
                    Fishes.Add(new Rudd(this));
                }
                else
                {
                    Fishes.Add(new Pike(this));
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            River river = new River();
            river.Start();
        }

    }
}


