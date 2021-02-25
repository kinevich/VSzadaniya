using Stateless;
using System;

namespace TennisWithFiniteAutomata
{
    class Tennis
    {
        StateMachine<State, Trigger> _machine = new StateMachine<State, Trigger>(State.Love);

        public Tennis()
        {
            _machine.Configure(State.Love)
                .Permit(Trigger.Server, State.FifteenLove)
                .Permit(Trigger.Opponent, State.LoveFifteen);

            _machine.Configure(State.FifteenLove)
                .Permit(Trigger.Server, State.ThirtyLove)
                .Permit(Trigger.Opponent, State.FifteenAll);

            _machine.Configure(State.LoveFifteen)
                .Permit(Trigger.Server, State.FifteenAll)
                .Permit(Trigger.Opponent, State.LoveThirty);

            _machine.Configure(State.ThirtyLove)
                .Permit(Trigger.Server, State.FortyLove)
                .Permit(Trigger.Opponent, State.ThirtyFifteen);

            _machine.Configure(State.FifteenAll)
                .Permit(Trigger.Server, State.ThirtyFifteen)
                .Permit(Trigger.Opponent, State.FifteenThirty);

            _machine.Configure(State.LoveThirty)
                .Permit(Trigger.Server, State.FifteenThirty)
                .Permit(Trigger.Opponent, State.LoveForty);

            _machine.Configure(State.FortyLove)
                .Permit(Trigger.Server, State.ServerWins)
                .Permit(Trigger.Opponent, State.FortyFifteen);

            _machine.Configure(State.ThirtyFifteen)
                .Permit(Trigger.Server, State.FortyFifteen)
                .Permit(Trigger.Opponent, State.ThirtyAll);

            _machine.Configure(State.FifteenThirty)
                .Permit(Trigger.Server, State.ThirtyAll)
                .Permit(Trigger.Opponent, State.FifteenForty);

            _machine.Configure(State.LoveForty)
                .Permit(Trigger.Server, State.FifteenForty)
                .Permit(Trigger.Opponent, State.OppWins);

            _machine.Configure(State.ServerWins)
                .Permit(Trigger.Server, State.Dead)
                .Permit(Trigger.Opponent, State.Dead);

            _machine.Configure(State.FortyFifteen)
                .Permit(Trigger.Server, State.ServerWins)
                .Permit(Trigger.Opponent, State.FortyThirty);

            _machine.Configure(State.ThirtyAll)
                .Permit(Trigger.Server, State.FortyThirty)
                .Permit(Trigger.Opponent, State.ThirtyForty);

            _machine.Configure(State.FifteenForty)
                .Permit(Trigger.Server, State.ThirtyForty)
                .Permit(Trigger.Opponent, State.OppWins);

            _machine.Configure(State.FortyThirty)
                .Permit(Trigger.Server, State.ServerWins)
                .Permit(Trigger.Opponent, State.Deuce);

            _machine.Configure(State.ThirtyForty)
                .Permit(Trigger.Server, State.Deuce)
                .Permit(Trigger.Opponent, State.OppWins);

            _machine.Configure(State.AdIn)
                .Permit(Trigger.Server, State.ServerWins)
                .Permit(Trigger.Opponent, State.Deuce);

            _machine.Configure(State.Deuce)
                .Permit(Trigger.Server, State.AdIn)
                .Permit(Trigger.Opponent, State.AdOut);

            _machine.Configure(State.AdOut)
                .Permit(Trigger.Server, State.Deuce)
                .Permit(Trigger.Opponent, State.OppWins);

            _machine.Configure(State.Dead)
                .Ignore(Trigger.Server)
                .Ignore(Trigger.Opponent);
        }

        private void ServerWinsGame()
        {
            _machine.Fire(Trigger.Server);
            Console.WriteLine(_machine.State);
        }

        private void OpponentWinsGame()
        {
            _machine.Fire(Trigger.Opponent);
            Console.WriteLine(_machine.State);
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
