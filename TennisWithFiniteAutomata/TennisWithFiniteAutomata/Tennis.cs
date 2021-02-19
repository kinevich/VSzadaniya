using Stateless;
using System;

namespace TennisWithFiniteAutomata
{
    class Tennis
    {
        StateMachine<State, Trigger> machine = new StateMachine<State, Trigger>(State.Love);

        public Tennis()
        {
            machine.Configure(State.Love)
                .Permit(Trigger.Server, State.FifteenLove)
                .Permit(Trigger.Opponent, State.LoveFifteen);

            machine.Configure(State.FifteenLove)
                .Permit(Trigger.Server, State.ThirtyLove)
                .Permit(Trigger.Opponent, State.FifteenAll);

            machine.Configure(State.LoveFifteen)
                .Permit(Trigger.Server, State.FifteenAll)
                .Permit(Trigger.Opponent, State.LoveThirty);

            machine.Configure(State.ThirtyLove)
                .Permit(Trigger.Server, State.FortyLove)
                .Permit(Trigger.Opponent, State.ThirtyFifteen);

            machine.Configure(State.FifteenAll)
                .Permit(Trigger.Server, State.ThirtyFifteen)
                .Permit(Trigger.Opponent, State.FifteenThirty);

            machine.Configure(State.LoveThirty)
                .Permit(Trigger.Server, State.FifteenThirty)
                .Permit(Trigger.Opponent, State.LoveForty);

            machine.Configure(State.FortyLove)
                .Permit(Trigger.Server, State.ServerWins)
                .Permit(Trigger.Opponent, State.FortyFifteen);

            machine.Configure(State.ThirtyFifteen)
                .Permit(Trigger.Server, State.FortyFifteen)
                .Permit(Trigger.Opponent, State.ThirtyAll);

            machine.Configure(State.FifteenThirty)
                .Permit(Trigger.Server, State.ThirtyAll)
                .Permit(Trigger.Opponent, State.FifteenForty);

            machine.Configure(State.LoveForty)
                .Permit(Trigger.Server, State.FifteenForty)
                .Permit(Trigger.Opponent, State.OppWins);

            machine.Configure(State.ServerWins)
                .Permit(Trigger.Server, State.Dead)
                .Permit(Trigger.Opponent, State.Dead);

            machine.Configure(State.FortyFifteen)
                .Permit(Trigger.Server, State.ServerWins)
                .Permit(Trigger.Opponent, State.FortyThirty);

            machine.Configure(State.ThirtyAll)
                .Permit(Trigger.Server, State.FortyThirty)
                .Permit(Trigger.Opponent, State.ThirtyForty);

            machine.Configure(State.FifteenForty)
                .Permit(Trigger.Server, State.ThirtyForty)
                .Permit(Trigger.Opponent, State.OppWins);

            machine.Configure(State.FortyThirty)
                .Permit(Trigger.Server, State.ServerWins)
                .Permit(Trigger.Opponent, State.Deuce);

            machine.Configure(State.ThirtyForty)
                .Permit(Trigger.Server, State.Deuce)
                .Permit(Trigger.Opponent, State.OppWins);

            machine.Configure(State.AdIn)
                .Permit(Trigger.Server, State.ServerWins)
                .Permit(Trigger.Opponent, State.Deuce);

            machine.Configure(State.Deuce)
                .Permit(Trigger.Server, State.AdIn)
                .Permit(Trigger.Opponent, State.AdOut);

            machine.Configure(State.AdOut)
                .Permit(Trigger.Server, State.Deuce)
                .Permit(Trigger.Opponent, State.OppWins);

            machine.Configure(State.Dead)
                .Ignore(Trigger.Server)
                .Ignore(Trigger.Opponent);
        }

        private void ServerWinsGame()
        {
            machine.Fire(Trigger.Server);
            Console.WriteLine(machine.State);
        }

        private void OpponentWinsGame()
        {
            machine.Fire(Trigger.Opponent);
            Console.WriteLine(machine.State);
        }

        public void Play()
        {
            while (true)
            {
                string winningPlayer;
                do
                {
                    Console.WriteLine($"s or o?");
                    winningPlayer = Console.ReadLine();
                }
                while (winningPlayer != "s" && winningPlayer != "o");

                if (winningPlayer == "s")
                    ServerWinsGame();
                else
                    OpponentWinsGame();

                Console.WriteLine();
            }
        }
    }
}
