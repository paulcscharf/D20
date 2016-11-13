using System;
using System.Linq;
using D20;

namespace D20Roller
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                foreach (var s in args)
                {
                    roll(s);
                }
                return;
            }

            startRoller();
        }

        private static void startRoller()
        {
            while (true)
            {
                var line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                    break;

                roll(line);
            }
        }

        private static void roll(string line)
        {
            try
            {
                var rollable = line.Rollable();
                Console.WriteLine(
                    $@"parsed string as: {rollable}
values: {rollable.MinValue} to {rollable.MaxValue} (average: {rollable.Average})"
                );
                var rolls = Enumerable.Range(0, 10).Select(i => rollable.Roll());
                Console.WriteLine($"example rolls: {string.Join(", ", rolls)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}