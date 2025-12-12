using System.Globalization;

namespace AdventOfCode
{
    class Program
    {
        private const int year = 2025;

        static void Main()
        {
            var singleDay = 0;
            var ignoreSlowDays = false;

            var dayClasses = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.Namespace == $"AdventOfCode.Year{year}")
                .Where(t => typeof(IDay).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .OrderBy(t => t.Name, StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering))
                .Skip(singleDay-1)
                .Take(singleDay > 0 ? 1 : 99);

            var start = DateTime.Now;

            foreach (var day in dayClasses)
            {
                Console.WriteLine($"{day.Name}:");

                // Ignore SlowDay Days if running all
                if (singleDay == 0 && ignoreSlowDays && day.CustomAttributes.Any(a => a.AttributeType.Name == "SlowDay"))
                {
                    Console.WriteLine("Skipping slow day...");
                }
                else
                {
                    IDay? dayRunner = Activator.CreateInstance(day) as IDay;
                    dayRunner?.Run();
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Total time: {(DateTime.Now - start).TotalMilliseconds}ms");
        }
    }
}