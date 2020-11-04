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
            actions.Start(); //ВВЕСТИ КОЛ-ВО РЫБ И ТД
        }
    }

    abstract class Fish
    {
        public double X;
        public double Y;
        public double Z;
        public double weight;
        public double ruddCount;
        public double howManyTimesPikeHungry;
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
        private List<Fish> _fishes = new List<Fish>();
        private int _border = 50; // ГРАНИЦА
        private double _pikeSpeed = 5;
        private double _ruddSpeed = 2;
        private int _pikeTimesToDie = 2;
        private int _ruddTimesToBorn = 2;

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
            foreach (Fish f in _fishes.ToList())
            {
                if ((f.GetType()).ToString() == "River.Rudd") // условие для красноперок(когда две красноперки рядом 2 шага, то рождается новая)
                {
                    foreach (Fish r in _fishes.ToList())
                    {
                        if ((r.GetType()).ToString() == "River.Rudd")
                        {
                            double distanceBetweenRudds = Math.Sqrt((f.X - r.X) * (f.X - r.X) + (f.Y - r.Y) * (f.Y - r.Y) + (f.Z - r.Z) * (f.Z - r.Z));
                            if (distanceBetweenRudds == 0)
                            {
                                break;
                            }
                            else if (distanceBetweenRudds < ruddBornDistance)
                            {
                                ++r.ruddCount;
                                ++f.ruddCount;
                            }

                            if (r.ruddCount == _ruddTimesToBorn && f.ruddCount == _ruddTimesToBorn)
                            {
                                AddRudd();
                                Console.WriteLine("Rudd was born");
                                r.ruddCount = 0;
                                f.ruddCount = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void PikesEatRudds(double pikeEatDistance)
        {
            foreach (Fish f in _fishes.ToList())
            {
                if ((f.GetType()).ToString() == "River.Pike")  // условие для щук(когда съедает красноперку, то вес увеличивается, если не ела 2 шага, то умирает)
                {
                    foreach (Fish r in _fishes.ToList())
                    {
                        if ((r.GetType()).ToString() == "River.Rudd")
                        {
                            double distanceBetweenPandR = Math.Sqrt((f.X - r.X) * (f.X - r.X) + (f.Y - r.Y) * (f.Y - r.Y) + (f.Z - r.Z) * (f.Z - r.Z));
                            if (distanceBetweenPandR < pikeEatDistance)
                            {
                                _fishes.Remove(r);
                                Console.WriteLine("Rudd died");
                                ++f.weight;
                                break;
                            }
                            else
                            {
                                ++f.howManyTimesPikeHungry;
                                break;
                            }
                        }
                    }
                    if (f.howManyTimesPikeHungry == _pikeTimesToDie)
                    {
                        _fishes.Remove(f);
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
                    Random random = new Random();
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
                    Random random = new Random();
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
            Random random = new Random();
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
            Random random = new Random();
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

