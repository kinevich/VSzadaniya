using System;

namespace River
{
    class Pair : IEquatable<Pair>
    {
        public Rudd R1 { get; set; }

        public Rudd R2 { get; set; }

        public int Count { get; set; }

        public Pair(Rudd r1, Rudd r2)
        {
            R1 = r1; R2 = r2;
        }

        public bool Equals(Pair other)
        {
            if (other == null)
                return false;

            if (other.R1 == R1 && other.R2 == R2)
                return true;

            if (other.R1 == R2 && other.R2 == R1)
                return true;

            return false;
        }
    }
}


