using System;
using System.Collections.Generic;
using System.Linq;

namespace StatePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var phone = new Phone();
            phone.Call();
        }
    }

    enum PhoneState
    {
        Normal,
        Connection,
        Connected,
        InStandBy
    }

    class Phone
    {
        public PhoneState State { get; private set; }

        public Phone()
        {
            State = PhoneState.Normal;
        }

        public void Call()
        {
            ShowValidActionsByCondition(PhoneState.Normal, PhoneState.Connection, new List<Action> { PickUp, HangUp });
            ShowValidActionsByCondition(PhoneState.Connected, PhoneState.Connection, new List<Action> { StandByOn, HangUp });
        }

        public void PickUp()
        {
            ShowValidActionsByCondition(PhoneState.Connection, PhoneState.Connected, new List<Action> { HangUp, Call });
        }

        public void HangUp()
        {
            ShowValidActionsByCondition(PhoneState.Connection, PhoneState.Normal, new List<Action> { Call });
            ShowValidActionsByCondition(PhoneState.Connected, PhoneState.Normal, new List<Action> { Call });
            ShowValidActionsByCondition(PhoneState.InStandBy, PhoneState.Connection, new List<Action> { HangUp });
        }

        public void StandByOn()
        {
            ShowValidActionsByCondition(PhoneState.Connection, PhoneState.InStandBy, new List<Action> { HangUp, StandByOff });
        }

        public void StandByOff()
        {
            ShowValidActionsByCondition(PhoneState.InStandBy, PhoneState.InStandBy, new List<Action> { HangUp, StandByOff });
        }

        private void ShowValidActionsByCondition(PhoneState conditionalPhoneState, PhoneState phoneStateToSet, 
                                                 List<Action> list)
        {
            if (State == conditionalPhoneState)
            {
                State = phoneStateToSet;

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

                list[choice - 1]();
            }
        }

        private static string StringSplitter(string stringtosplit)
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
