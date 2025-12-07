using System.Globalization;

namespace AdventOfCode
{
    class Program
    {
        private const int year = 2025;

        static void Main()
        {

            var dayClasses = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.Namespace == $"AdventOfCode.Year{year}")
                .Where(t => typeof(IDay).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .OrderBy(t => t.Name, StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering));

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