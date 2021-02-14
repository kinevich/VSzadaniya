using System;

namespace Adventure
{
    class MagentaConsoleStyle : IConsoleStyleFactory
    {
        public ConsoleColor SetBackgroundColor()
        {
            return ConsoleColor.DarkMagenta;
        }

        public ConsoleColor SetForegroundColor()
        {
            return ConsoleColor.Magenta;
        }
    }
}
