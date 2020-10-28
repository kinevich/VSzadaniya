using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Tennis
{
    class Player
    {
        public string Name { get; set; }
        public int Points { get; set; }
    }

    class Player1 : Player
    {
        public Player1 (string name)
        {
            Name = name;
        }
    }

    class Player2 : Player
    {
        public Player2 (string name)
        {
            Name = name;
        }
    }

    class Actions
    {
        public string PlayOneGame(Player player1, Player player2)
        {
            Player[] players = new Player[] { player1, player2 };        
            var random = new Random();                                  
            int index = random.Next(players.Length);                    //выбираем рандомного пользователя
            if (players[index] == player1)
            {
                ++player1.Points;
            }
            else
            {
                ++player2.Points;                    
            }                                          //добавляем очко этому пользователю

            return Game(player1, player2);            
        }
                                                            
        private string Game(Player player1, Player player2)
        {
            Console.Write("Press random key to continue");
            Console.ReadLine();
            ClearLine();
            if (player1.Points >= 3 && player2.Points >= 3 && Math.Abs(player1.Points - player2.Points) < 2)
            {
                if (player1.Points == player2.Points)
                {
                    return "Deuce";                 // тайм брейк
                }
                else if (player1.Points > player2.Points)
                {
                    return $"Advantage {player1.Name}";    
                }
                else                                       // у какого игрока преимещуство во время тайм брейка
                {
                    return $"Advantage {player2.Name}";
                }                    
            }
                            
            if (player1.Points < 4 && player2.Points < 4)
            {
                return $"{player1.Points}-{player2.Points}";        // счет
            }
               
            if (player1.Points >= 4)
            { 
                player1.Points = 0;
                player2.Points = 0;                 // обнуление результатов при выигрыше сета

                return $"{player1.Name} win set";
            }
            else
            {
                player1.Points = 0;
                player2.Points = 0;  

                return $"{player2.Name} win set";
            }                
        }
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));       // метод удаляет одну линию
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }

    

    class Program
    {
        static void Main(string[] args)
        {
            Player1 mike = new Player1("Mike");
            Player2 kyle = new Player2("Kyle");
            Actions actions = new Actions();
            for (int i = 0; i < 100; ++i)
            {
                Console.WriteLine(actions.PlayOneGame(mike, kyle));
            }
        }
    }
}
