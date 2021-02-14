using System;

namespace Adventure
{
    class ConsoleStyle
    {
        public ConsoleColor BackgroundColor;

        public ConsoleColor ForegroundColor;

        public ConsoleStyle(IConsoleStyleFactory factory)
        {
            BackgroundColor = factory.SetBackgroundColor();
            ForegroundColor = factory.SetForegroundColor();
        }
    }
}
