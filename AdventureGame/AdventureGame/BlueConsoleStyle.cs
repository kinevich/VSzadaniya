using System;

namespace Adventure
{
    class BlueConsoleStyle : IConsoleStyleFactory
    {
        public ConsoleColor SetBackgroundColor()
        {
            return ConsoleColor.DarkBlue;
        }

        public ConsoleColor SetForegroundColor()
        {
            return ConsoleColor.Blue;
        }
    }
}
