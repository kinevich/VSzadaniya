using System;
using System.Collections.Generic;
using System.Linq;

namespace StatePattern
{
    class ShowHelper
    {
        public static void ShowValidPhoneActions(List<Action> list)
        {
            foreach (var action in list)
            {
                Console.WriteLine($"{list.IndexOf(action) + 1}){StringSplitter(action.Method.Name)}");
            }

            int choice;
            do
            {
                Console.WriteLine("Enter number:");
                int.TryParse(Console.ReadLine(), out choice);
            }
            while (!list.Any(i => list.IndexOf(i) == choice - 1));

            Console.WriteLine();

            list[choice - 1]();
        }

        public static string StringSplitter(string stringtosplit)
        {
            string words = string.Empty;
            if (!string.IsNullOrEmpty(stringtosplit))
            {
                foreach (char ch in stringtosplit)
                {
                    if (char.IsLower(ch))
                    {
                        words += ch.ToString();
                    }
                    else
                    {
                        if (stringtosplit.IndexOf(ch) == 0)
                            words += ch;
                        else
                            words += " " + ch.ToString().ToLower();
                    }
                }
                return words;
            }
            else
                return string.Empty;
        }
    }
}

