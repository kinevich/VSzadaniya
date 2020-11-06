using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace River
{
    class Program
    {
        static void Main(string[] args)
        {
            Actions actions = new Actions();
            actions.Start(50, 30, 0.1); //ВВЕСТИ КОЛ-ВО РЫБ И ТД
        }
    }

    abstract class Fish
    {
        public double X;
        public double Y;
        public double Z;
        public double Weight;
        public double RuddCount;
        public double HowManyTimesPikeHungry;
    }

    class Rudd : Fish
    {
        public Rudd(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    class Pike : Fish
    {
        public Pike(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        } 
    }

    class Actions
    {
        List<Fish> _fishes = new List<Fish>();
        int _border = 100; // ГРАНИЦА
        double _pikeSpeed = 5;
        double _ruddSpeed = 2;
        int _pikeTimesToDie = 2;
        int _ruddTimesToBorn = 2;
        Random random = new Random();
        
        public void Start(int numberOfFishes, double pikeEatDistance, double ruddBornDistance)
        {
            CreateFishes(numberOfFishes);

            for (int sec = 0; sec < 10; ++sec)
            {
                ChangeCoordinates();
                PikesEatRudds(pikeEatDistance);
                RuddsBorn(ruddBornDistance);
                Thread.Sleep(100);
            }
        }

        private void RuddsBorn(double ruddBornDistance)
        {
            foreach (Fish r1 in _fishes.ToList())
            {
                if ((r1.GetType()).ToString() == "River.Rudd") // условие для красноперок(когда две красноперки рядом 2 шага, то рождается новая)
                {
                    foreach (Fish r2 in _fishes.ToList())
                    {
                        if ((r2.GetType()).ToString() == "River.Rudd")
                        {
                            if (DistBetwFishes(r1, r2) == 0)
                            {
                                break;
                            }
                            else if (DistBetwFishes(r1, r2) < ruddBornDistance)
                            {
                                ++r1.RuddCount;
                                ++r2.RuddCount;
                            }

                            if (r1.RuddCount == _ruddTimesToBorn && r2.RuddCount == _ruddTimesToBorn)
                            {
                                AddRudd();
                                Console.WriteLine("Rudd was born");
                                r1.RuddCount = 0;
                                r2.RuddCount = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private double DistBetwFishes(Fish f1, Fish f2)
        {
            return Math.Sqrt((f1.X - f2.X) * (f1.X - f2.X) + (f1.Y - f2.Y) * (f1.Y - f2.Y) + (f1.Z - f2.Z) * (f1.Z - f2.Z));
        }

        private void PikesEatRudds(double pikeEatDistance)
        {
            foreach (Fish p in _fishes.ToList())
            {
                if ((p.GetType()).ToString() == "River.Pike")  // условие для щук(когда съедает красноперку, то вес увеличивается, если не ела 2 шага, то умирает)
                {
                    foreach (Fish r in _fishes.ToList())
                    {
                        if ((r.GetType()).ToString() == "River.Rudd")
                        {
                            if (DistBetwFishes(p, r) < pikeEatDistance)
                            {
                                _fishes.Remove(r);
                                Console.WriteLine("Rudd died");
                                ++p.Weight;
                                p.HowManyTimesPikeHungry = 0;
                                break;
                            }
                            else
                            {
                                ++p.HowManyTimesPikeHungry;
                                break;
                            }
                        }
                    }
                    if (p.HowManyTimesPikeHungry == _pikeTimesToDie)
                    {
                        _fishes.Remove(p);
                        Console.WriteLine("Pike died");
                    }
                }
            }
        }
        private void ChangeCoordinates()
        {
            foreach (Fish f in _fishes)      // меняет координаты 
            {
                if ((f.GetType()).ToString() == "River.Pike") // у щук
                {
                    int caseSwitch = random.Next(2);
                    switch (caseSwitch)
                    {
                        case 0:
                            ChangeCoordinatesPositive(ref f.X, ref f.Y, ref f.Z, _pikeSpeed);
                            break;
                        case 1:
                            ChangeCoordinatesNegative(ref f.X, ref f.Y, ref f.Z, _pikeSpeed);
                            break;
                    }
                }
                else if ((f.GetType()).ToString() == "River.Rudd") //у красноперок
                {
                    int caseSwitch = random.Next(2);
                    switch (caseSwitch)
                    {
                        case 0:
                            ChangeCoordinatesPositive(ref f.X, ref f.Y, ref f.Z, _ruddSpeed);
                            break;
                        case 1:
                            ChangeCoordinatesNegative(ref f.X, ref f.Y, ref f.Z, _ruddSpeed);
                            break;
                    }
                }
            }
        }

        private void CreateFishes(int numberOfFishes)
        {
            for (int i = 0; i < numberOfFishes; ++i)
            {
                RuddOrPike();
            } 
        }        

        private void RuddOrPike()
        {
            int i = random.Next(2);
            if (i == 0)
            {
                AddRudd();
            }
            else
            {
                AddPike();
            }
        }

        private void AddRudd()
        {
            double[] array = randomXYZ();
            Rudd rudd = new Rudd(array[0], array[1], array[2]);
            _fishes.Add(rudd);
        }

        private void AddPike()
        {
            double[] array = randomXYZ();
            Pike pike = new Pike(array[0], array[1], array[2]);
            _fishes.Add(pike);
        }

        private double[] randomXYZ()
        {
            double[] array = new double[3];
            array[0] = random.Next(_border);
            array[1] = random.Next(_border);
            array[2] = random.Next(_border);
            return array;
        }

        private void ChangeCoordinatesNegative(ref double fishX, ref double fishY, ref double fishZ, double fishSpeed)
        {
            fishX -= fishSpeed;
            fishY -= fishSpeed;
            fishZ -= fishSpeed;
            BorderCondition(ref fishX, fishSpeed);
            BorderCondition(ref fishY, fishSpeed);
            BorderCondition(ref fishZ, fishSpeed);
        }

        private void ChangeCoordinatesPositive(ref double fishX, ref double fishY, ref double fishZ, double fishSpeed)
        {
            fishX += fishSpeed;
            fishY += fishSpeed;
            fishZ += fishSpeed;
            BorderCondition(ref fishX, fishSpeed);
            BorderCondition(ref fishY, fishSpeed);
            BorderCondition(ref fishZ, fishSpeed);
        }

        private void BorderCondition (ref double fishCoordinate, double fishSpeed)
        {
            if (Math.Abs(fishCoordinate) > _border)
            {
                if (fishCoordinate > 0)
                {
                    fishCoordinate -= fishSpeed;
                }
                else
                {
                    fishCoordinate += fishSpeed;
                }
            }
        }
    }
    
}

