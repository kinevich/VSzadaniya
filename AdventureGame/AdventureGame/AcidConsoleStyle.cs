using System;

namespace Adventure
{
    class AcidConsoleStyle : IConsoleStyleFactory
    {
        public ConsoleColor SetBackgroundColor()
        {
            return ConsoleColor.Yellow;
        }

        public ConsoleColor SetForegroundColor()
        {
            return ConsoleColor.Red;
        }
    }
}
