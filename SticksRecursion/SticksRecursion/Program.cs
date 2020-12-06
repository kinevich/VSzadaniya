using System;

namespace SticksRecursion
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(20);
            game.Play();
        }
    }

    class Game
    {
        public Game (double numberOfSticks)
        {
            _numberOfSticks = numberOfSticks;
        }

        Random random = new Random();

        bool _keepworking = true;

        bool _masterWin = false;

        double _numberOfSticks; 

        public void Play ()
        {
            if (!_keepworking)
                return;

            int yourStep = YourStep();

            if (!_keepworking)
                return;

            MasterStep(yourStep);
        }

        private int YourStep()
        {
            int yourStep;
            while (true)
            {
                Console.Write($"Number of sticks = {_numberOfSticks}. Enter number:");
                yourStep = Convert.ToInt32(Console.ReadLine());
                if ((yourStep == 1 || yourStep == 2 || yourStep == 3) && yourStep < _numberOfSticks)
                {
                    break;
                }
            }

            Step(yourStep, "Your");

            return yourStep;
        }

        private void MasterStep(int yourStep)
        {
            if (_masterWin)
            {
                Step(4 - yourStep, "Master");
                Play();
            }
            else if (LosingPosition()) // 1, 5, 9, 13, 17, ..., 4n + 1; 
            {
                Step(random.Next(1, 4), "Master");
                Play();
            }
            else 
            {
                _masterWin = true;
                if ((_numberOfSticks - 2) % 4 == 0) // 2, 6, 10, 14, 18, .., 4n + 2;
                {
                    Step(1, "Master");
                    Play();
                }
                else if ((_numberOfSticks - 3) % 4 == 0) // 3, 7, 11, 15, 19, .., 4n + 3;
                {
                    Step(2, "Master");
                    Play();
                }
                else if ((_numberOfSticks - 4) % 4 == 0) // 4, 8, 12, 16, 20, .., 4n + 4;
                {
                    Step(3, "Master");
                    Play();
                }
            }
        }

        private void Step(double step, string player)
        {
            _numberOfSticks -= step;
            Console.WriteLine($"{player} step = {step}. Number of sticks = {_numberOfSticks}.");

            if (LosingPosition() && player == "Master") // 1, 5, 9, 13, 17, ..., 4n + 1;
            {
                _masterWin = true;
            } 

            if (_numberOfSticks == 1)
            {
                if (player == "Your")
                    Console.WriteLine("You win!");
                Console.WriteLine("Master win!");
                _keepworking = false;
            }

        }

        private bool LosingPosition()
        {
            return (_numberOfSticks - 1) % 4 == 0;
        }

    }
}
