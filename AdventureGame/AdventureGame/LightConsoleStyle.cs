using System;

namespace Adventure
{
    class LightConsoleStyle : IConsoleStyleFactory
    {
        public ConsoleColor SetBackgroundColor()
        {
            return ConsoleColor.White;
        }

        public ConsoleColor SetForegroundColor()
        {
            return ConsoleColor.Gray;
        }
    }
}
