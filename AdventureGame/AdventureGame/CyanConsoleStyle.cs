using System;

namespace Adventure
{
    class CyanConsoleStyle : IConsoleStyleFactory
    {
        public ConsoleColor SetBackgroundColor()
        {
            return ConsoleColor.DarkCyan;
        }

        public ConsoleColor SetForegroundColor()
        {
            return ConsoleColor.Cyan;
        }
    }
}
