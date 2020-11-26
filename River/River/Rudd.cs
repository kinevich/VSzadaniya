using System;
using System.Collections.Generic;
using System.Linq;

namespace River
{
    class Rudd : Fish
    {
        public Rudd(River river, List<Pair> pairs) : base(river)
        { 
            Speed = 2;
            _pairs = pairs;
        }

        private List<Pair> _pairs;

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

                    foreach (Pair pair in _pairs.ToList())
                    {
                        if (pair.Equals(p))
                        {
                            ++pair.Count;
                        }

                        if (pair.Count == _timesToBorn)
                        {
                            Console.WriteLine("Rudd was born");
                            pair.Count = 0;
                        }
                    }
                    if (_pairs.All(pair => !pair.Equals(p)))
                        _pairs.Add(p);
                }
            }
        }         
    }
}


