using System;

namespace App
{
    public static class Extensions
    {
        public static void WriteLine(this ConsoleColor color, object value)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        public static void PressAnyKeyToContinue(this ConsoleColor color)
        {
            color.WriteLine("Press any key to continue ..");
            Console.ReadKey();
        }

        public static void PressAnyKeyToExit(this ConsoleColor color)
        {
            color.WriteLine("Press any key to exit ..");
            Console.ReadKey();
        }
    }
}
