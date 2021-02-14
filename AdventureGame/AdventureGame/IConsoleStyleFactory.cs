using System;

namespace Adventure
{
    interface IConsoleStyleFactory
    {
        ConsoleColor SetForegroundColor();

        ConsoleColor SetBackgroundColor();
    }
}
