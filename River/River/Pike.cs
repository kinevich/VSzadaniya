using System;
using System.Linq;

namespace River
{
    class Pike : Fish
    {
        public Pike(River river) : base(river) { Speed = 5; }

        private int _timesHungry;

        private double _weight;

        private bool _hungry = true;

        private const double PikeEatDist = 5;

        private const int TimesToDie = 2;

        public override void Move()
        {
            base.Move();
            foreach (Fish f in River.Fishes.ToList())
            {
                if (f is Rudd r && DistanceBetweenFishes(this, r) < PikeEatDist)
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
            if (_timesHungry == TimesToDie)
            {
                River.Fishes.Remove(this);
                Console.WriteLine($"Pike died. Weight = {_weight}");
            }
            _hungry = true;
        }
    }
}


