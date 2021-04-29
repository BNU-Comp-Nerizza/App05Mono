using System;

namespace App05Mono
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new DvDGame())
                game.Run();
        }
    }
}
