using System;
using System.Linq;

namespace River
{
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

                    bool inList = false; 
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


