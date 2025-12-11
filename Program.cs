using System.Globalization;

namespace AdventOfCode
{
    class Program
    {
        private const int year = 2025;

        static void Main()
        {
            var singleDay = 0;

            var dayClasses = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.Namespace == $"AdventOfCode.Year{year}")
                .Where(t => typeof(IDay).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                // Ignore SlowDay Days if running all
                .Where(t => singleDay != 0 || !t.CustomAttributes.Any(a => a.AttributeType.Name != "SlowDay"))
                .OrderBy(t => t.Name, StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering))
                .Skip(singleDay-1)
                .Take(singleDay > 0 ? 1 : 99);

            foreach (var day in dayClasses)
            {
                Console.WriteLine($"{day.Name}:");
                IDay? dayRunner = Activator.CreateInstance(day) as IDay;
                dayRunner?.Run();
                Console.WriteLine();
            }
        }
    }
}