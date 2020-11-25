using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

namespace River
{
    abstract class Fish
    {
        protected River River;

        private double _x;

        private double _y;

        private double _z;

        public double Speed { get; protected set; }

        private Random _random = new Random();

        public Fish(River river)
        {
            River = river;
            _x = _random.Next(River.Border);
            _y = _random.Next(River.Border);
            _z = _random.Next(River.Border);
        }

        public virtual void Move()
        {
            if (_random.Next(2) == 0)
            {
                Speed *= -1;
            }

            ChangeCoordinates();
        }

        private void ChangeCoordinates()
        {
            _x += Speed;
            _y += Speed;
            _z += Speed;
            _x = BorderCheck(_x);
            _y = BorderCheck(_y);
            _z = BorderCheck(_z);
        }

        private double BorderCheck(double fishCoordinate)
        {
            if (fishCoordinate > River.Border)
            {
                return fishCoordinate - Speed;
            }
            else if (fishCoordinate < 0)
            {
                return 0;
            }
            else return fishCoordinate;
        }
        
        protected double DistanceBetweenFishes(Fish f1, Fish f2)
        {
            return Math.Sqrt((f1._x - f2._x) * (f1._x - f2._x) + (f1._y - f2._y) * (f1._y - f2._y) + (f1._z - f2._z) * (f1._z - f2._z));
        }
    } 
        
    class Rudd : Fish
    {
        public Rudd(River river, Pairs pairs) : base(river)
        { 
            Speed = 2;
            _pairs = pairs;
        }

        private Pairs _pairs;

        private const double _bornDist = 5;

        private const int _timesToBorn = 2;

        public override void Move()
        {
            base.Move();
            foreach (Fish f in River.Fishes.ToList())
            {
                if (f is Rudd r && r != this && DistanceBetweenFishes(this, r) < _bornDist)
                {
                    Pair p = new Pair(this, r);

                    if (_pairs.List == null)
                        _pairs.List.Add(p);
                    else
                    {
                        bool inList = false;;
                        foreach (Pair pair in _pairs.List.ToList())
                        {
                            if (pair.Equals(p))
                            {
                                ++pair.Count;
                                inList = true;
                            }
                                
                            if (pair.Count == _timesToBorn)
                            {
                                Console.WriteLine("Rudd was born");
                                pair.Count = 0;
                            }
                        }
                        if (!inList)
                            _pairs.List.Add(p);
                    }
                }
            }
        }         
    }
    
    class Pair : IEquatable<Pair>
    {
        public Rudd R1 { get; private set; }

        public Rudd R2 { get; private set; }

        public int Count { get; set; }

        public Pair(Rudd r1, Rudd r2)
        {
            R1 = r1; R2 = r2;
        }

        public bool Equals(Pair other)
        {
            if (other == null)
                return false;

            if (other.R1 == R1 && other.R2 == R2 && other != this)
                return true;

            if (other.R1 == R2 && other.R2 == R1 && other != this)
                return true;

            return false;
        }
    }

    class Pairs
    {
        public List<Pair> List = new List<Pair>();
    }

    class Pike : Fish
    {
        public Pike(River river) : base(river) { Speed = 5; }

        private int _timesHungry;

        private double _weight;

        private bool _hungry = true;

        private const double _pikeEatDist = 5;

        private const int _timesToDie = 2;

        public override void Move()
        {
            base.Move();
            foreach (Fish f in River.Fishes.ToList())
            {
                if (f is Rudd r && DistanceBetweenFishes(this, r) < _pikeEatDist)
                {
                    River.Fishes.Remove(r);
                    ++_weight;
                    _timesHungry = 0;
                    Console.WriteLine("Rudd died");
                    _hungry = false;
                    break;                    
                }
            }
            if (_hungry)
                ++_timesHungry;
            if (_timesHungry == _timesToDie)
            {
                River.Fishes.Remove(this);
                Console.WriteLine("Pike died");
            }
            _hungry = true;
        }
    }

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

    class Program
    {
        static void Main(string[] args)
        {
            River river = new River();
            river.Start();
        }
    }
}


