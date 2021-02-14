using System;

namespace Adventure
{
    class GreenConsoleStyle : IConsoleStyleFactory
    {
        public ConsoleColor SetBackgroundColor()
        {
            return ConsoleColor.Green;
        }

        public ConsoleColor SetForegroundColor()
        {
            return ConsoleColor.DarkGreen;
        }
    }
}
