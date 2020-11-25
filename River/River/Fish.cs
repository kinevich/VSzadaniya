using System;

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
}


