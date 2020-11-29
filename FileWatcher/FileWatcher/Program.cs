using System;
using System.IO;

namespace FileWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemWatcher watcher = new FileSystemWatcher(@"E:\Test");

            watcher.EnableRaisingEvents = true;

            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;

            Console.ReadLine();
        }

        private static void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File {e.Name} created");
        }

        private static void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File {e.Name} deleted");
        }

        private static void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"File {e.OldName} renamed to {e.Name}");
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File {e.Name} changed");
        }
    }
}
