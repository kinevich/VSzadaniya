using System;

namespace Adventure
{
    class GrayConsoleStyle : IConsoleStyleFactory
    {
        public ConsoleColor SetBackgroundColor()
        {
            return ConsoleColor.DarkGray;
        }

        public ConsoleColor SetForegroundColor()
        {
            return ConsoleColor.Gray;
        }
    }
}
