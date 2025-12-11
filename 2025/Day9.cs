//https://adventofcode.com/2025/day/9
namespace AdventOfCode.Year2025
{
    using Coordinate = (int x, int y);

    class Day9 : IDay
    {
        private List<Coordinate> redTiles = [];
        private static Dictionary<int, int> compressedXMap = [];
        private static Dictionary<int, int> compressedYMap = [];
        private static List<(int x, int y)> compressedPositions = [];

        public void Run()
        {
            LoadData();

            var start = DateTime.Now;

            // 4738108384
            Console.WriteLine($"Part 1 answer: {Part1()}");
            // 1513792010
            Console.WriteLine($"Part 2 answer: {Part2()}");

            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        private long Part1()
        {
            // Create all combinations of pairs of red tiles in a queue ordered by largest area to smallest
            var rectangles = EnqueueRedTilePairsByLargestArea();

            return rectangles.Dequeue().area;
        }

        private long Part2()
        {
            #region From https://github.com/TimN1987/AdventOfCode/blob/main/CSharp/AoC2025/Days/Day9.cs

            BuildCompressedPositions();

            HashSet<(int, int)> edge = ScanEdges();
            var rectangles = GetCompressedRectangles();

            // Changed from github source to use PriorityQueue
            do
            {
                (var area, var corner1, var corner2) = rectangles.Dequeue();

                if (CheckRectangle2(corner1, corner2, edge))
                    return area;
            } while (rectangles.Count > 0);

            #endregion

            return 0;
        }

        private PriorityQueue<(Coordinate corner1, Coordinate corner2, long area), long> EnqueueRedTilePairsByLargestArea()
        {
            // Create all combinations of pairs of red tiles in a queue ordered by largest area to smallest
            // max-heap PriorityQueue using custom Comparer
            var rectangles = new PriorityQueue<(Coordinate corner1, Coordinate corner2, long area), long>(Comparer<long>.Create((a, b) => b.CompareTo(a)));

            for (var a = 0; a < redTiles.Count - 1; a++)
            {
                for (var b = a + 1; b < redTiles.Count; b++)
                {
                    var area = RectangleArea(redTiles[a], redTiles[b]);
                    rectangles.Enqueue((redTiles[a], redTiles[b], area), area);
                }
            }

            return rectangles;
        }

        private long RectangleArea(Coordinate corner1, Coordinate corner2) =>
            // Area (|x2 - x1| + 1) * (|y2 - y1| + 1)
            (AbsLong(corner2.x - corner1.x) + 1) * (AbsLong(corner2.y - corner1.y) + 1);

        private long AbsLong(long value) => (value + (value >> 63)) ^ (value >> 63);


        #region From https://github.com/TimN1987/AdventOfCode/blob/main/CSharp/AoC2025/Days/Day9.cs

        private void BuildCompressedPositions()
        {
            var uniqueX = redTiles.Select(t => t.x).Distinct().OrderBy(x => x).ToList();
            var uniqueY = redTiles.Select(t => t.y).Distinct().OrderBy(y => y).ToList();

            compressedXMap = uniqueX.Select((v, i) => new { v, i }).ToDictionary(x => x.v, x => x.i);
            compressedYMap = uniqueY.Select((v, i) => new { v, i }).ToDictionary(y => y.v, y => y.i);

            compressedPositions = redTiles
                .Select(p => (compressedXMap[p.x], compressedYMap[p.y]))
                .ToList();
        }

        private HashSet<(int, int)> ScanEdges()
        {
            var edge = new HashSet<(int, int)>();
            var coords = compressedPositions.ToList();
            var prev = coords[0];
            coords.Add(prev);

            for (int i = 1; i < coords.Count; i++)
            {
                var current = coords[i];
                int minX = Math.Min(prev.x, current.x);
                int maxX = Math.Max(prev.x, current.x);
                int minY = Math.Min(prev.y, current.y);
                int maxY = Math.Max(prev.y, current.y);

                if (prev.x == current.x)
                {
                    for (int j = minY; j <= maxY; j++)
                        edge.Add((prev.x, j));
                }
                else if (prev.y == current.y)
                {
                    for (int j = minX; j <= maxX; j++)
                        edge.Add((j, prev.y));
                }

                prev = current;
            }

            return edge;
        }

        private PriorityQueue<(long area, (int x, int y) corner1, (int x, int y) corner2), long> GetCompressedRectangles()
        {
            // Changed from github source to use PriorityQueue
            int n = compressedPositions.Count;
            // max-heap PriorityQueue using custom Comparer
            var rectangles = new PriorityQueue<(long area, (int x, int y), (int x, int y)), long>(Comparer<long>.Create((a, b) => b.CompareTo(a)));

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    var area = RectangleArea(redTiles[i], redTiles[j]);

                    rectangles.Enqueue((area, compressedPositions[i], compressedPositions[j]), area);
                }
            }

            return rectangles;
        }

        private bool CheckRectangle2((int x, int y) a, (int x, int y) b, HashSet<(int, int)> edge)
        {
            int minX = Math.Min(a.x, b.x);
            int maxX = Math.Max(a.x, b.x);
            int minY = Math.Min(a.y, b.y);
            int maxY = Math.Max(a.y, b.y);

            for (int x = minX + 1; x < maxX; x++)
            {
                if (edge.Contains((x, minY + 1)) || edge.Contains((x, maxY - 1)))
                    return false;
            }

            for (int y = minY + 1; y < maxY; y++)
            {
                if (edge.Contains((minX + 1, y)) || edge.Contains((maxX - 1, y)))
                    return false;
            }

            return true;
        }
        #endregion

        private void LoadData()
        {
            //redTiles.Add((7, 1));
            //redTiles.Add((11, 1));
            //redTiles.Add((11, 7));
            //redTiles.Add((9, 7));
            //redTiles.Add((9, 5));
            //redTiles.Add((2, 5));
            //redTiles.Add((2, 3));
            //redTiles.Add((7, 3));

            //return;

            redTiles.Add((98220, 50383));
            redTiles.Add((98220, 51611));
            redTiles.Add((98394, 51611));
            redTiles.Add((98394, 52815));
            redTiles.Add((97964, 52815));
            redTiles.Add((97964, 54005));
            redTiles.Add((97594, 54005));
            redTiles.Add((97594, 55224));
            redTiles.Add((97608, 55224));
            redTiles.Add((97608, 56483));
            redTiles.Add((97863, 56483));
            redTiles.Add((97863, 57566));
            redTiles.Add((96897, 57566));
            redTiles.Add((96897, 58822));
            redTiles.Add((97062, 58822));
            redTiles.Add((97062, 59947));
            redTiles.Add((96526, 59947));
            redTiles.Add((96526, 61107));
            redTiles.Add((96191, 61107));
            redTiles.Add((96191, 62379));
            redTiles.Add((96290, 62379));
            redTiles.Add((96290, 63533));
            redTiles.Add((95913, 63533));
            redTiles.Add((95913, 64738));
            redTiles.Add((95698, 64738));
            redTiles.Add((95698, 65885));
            redTiles.Add((95293, 65885));
            redTiles.Add((95293, 67009));
            redTiles.Add((94828, 67009));
            redTiles.Add((94828, 68073));
            redTiles.Add((94220, 68073));
            redTiles.Add((94220, 69435));
            redTiles.Add((94313, 69435));
            redTiles.Add((94313, 70174));
            redTiles.Add((93002, 70174));
            redTiles.Add((93002, 71616));
            redTiles.Add((93196, 71616));
            redTiles.Add((93196, 72602));
            redTiles.Add((92443, 72602));
            redTiles.Add((92443, 73462));
            redTiles.Add((91489, 73462));
            redTiles.Add((91489, 74876));
            redTiles.Add((91500, 74876));
            redTiles.Add((91500, 75874));
            redTiles.Add((90784, 75874));
            redTiles.Add((90784, 76501));
            redTiles.Add((89523, 76501));
            redTiles.Add((89523, 77604));
            redTiles.Add((88995, 77604));
            redTiles.Add((88995, 78801));
            redTiles.Add((88574, 78801));
            redTiles.Add((88574, 79650));
            redTiles.Add((87681, 79650));
            redTiles.Add((87681, 80801));
            redTiles.Add((87167, 80801));
            redTiles.Add((87167, 81809));
            redTiles.Add((86461, 81809));
            redTiles.Add((86461, 82630));
            redTiles.Add((85544, 82630));
            redTiles.Add((85544, 83027));
            redTiles.Add((84196, 83027));
            redTiles.Add((84196, 84452));
            redTiles.Add((83908, 84452));
            redTiles.Add((83908, 84937));
            redTiles.Add((82685, 84937));
            redTiles.Add((82685, 85741));
            redTiles.Add((81777, 85741));
            redTiles.Add((81777, 86619));
            redTiles.Add((80934, 86619));
            redTiles.Add((80934, 87768));
            redTiles.Add((80299, 87768));
            redTiles.Add((80299, 88349));
            redTiles.Add((79199, 88349));
            redTiles.Add((79199, 88541));
            redTiles.Add((77832, 88541));
            redTiles.Add((77832, 89980));
            redTiles.Add((77357, 89980));
            redTiles.Add((77357, 90494));
            redTiles.Add((76228, 90494));
            redTiles.Add((76228, 90566));
            redTiles.Add((74839, 90566));
            redTiles.Add((74839, 91519));
            redTiles.Add((73998, 91519));
            redTiles.Add((73998, 92314));
            redTiles.Add((73047, 92314));
            redTiles.Add((73047, 92444));
            redTiles.Add((71743, 92444));
            redTiles.Add((71743, 93461));
            redTiles.Add((70891, 93461));
            redTiles.Add((70891, 93816));
            redTiles.Add((69711, 93816));
            redTiles.Add((69711, 93931));
            redTiles.Add((68439, 93931));
            redTiles.Add((68439, 94554));
            redTiles.Add((67387, 94554));
            redTiles.Add((67387, 95135));
            redTiles.Add((66308, 95135));
            redTiles.Add((66308, 95641));
            redTiles.Add((65196, 95641));
            redTiles.Add((65196, 95506));
            redTiles.Add((63880, 95506));
            redTiles.Add((63880, 96329));
            redTiles.Add((62858, 96329));
            redTiles.Add((62858, 96763));
            redTiles.Add((61711, 96763));
            redTiles.Add((61711, 96562));
            redTiles.Add((60415, 96562));
            redTiles.Add((60415, 97182));
            redTiles.Add((59305, 97182));
            redTiles.Add((59305, 97788));
            redTiles.Add((58173, 97788));
            redTiles.Add((58173, 97501));
            redTiles.Add((56890, 97501));
            redTiles.Add((56890, 98040));
            redTiles.Add((55730, 98040));
            redTiles.Add((55730, 98269));
            redTiles.Add((54520, 98269));
            redTiles.Add((54520, 97753));
            redTiles.Add((53254, 97753));
            redTiles.Add((53254, 98440));
            redTiles.Add((52070, 98440));
            redTiles.Add((52070, 97834));
            redTiles.Add((50831, 97834));
            redTiles.Add((50831, 97766));
            redTiles.Add((49619, 97766));
            redTiles.Add((49619, 98209));
            redTiles.Add((48394, 98209));
            redTiles.Add((48394, 98038));
            redTiles.Add((47180, 98038));
            redTiles.Add((47180, 97341));
            redTiles.Add((46015, 97341));
            redTiles.Add((46015, 97712));
            redTiles.Add((44764, 97712));
            redTiles.Add((44764, 97144));
            redTiles.Add((43614, 97144));
            redTiles.Add((43614, 97019));
            redTiles.Add((42413, 97019));
            redTiles.Add((42413, 96935));
            redTiles.Add((41201, 96935));
            redTiles.Add((41201, 96646));
            redTiles.Add((40026, 96646));
            redTiles.Add((40026, 96658));
            redTiles.Add((38780, 96658));
            redTiles.Add((38780, 96087));
            redTiles.Add((37674, 96087));
            redTiles.Add((37674, 96044));
            redTiles.Add((36427, 96044));
            redTiles.Add((36427, 95949));
            redTiles.Add((35180, 95949));
            redTiles.Add((35180, 95401));
            redTiles.Add((34076, 95401));
            redTiles.Add((34076, 95034));
            redTiles.Add((32912, 95034));
            redTiles.Add((32912, 94348));
            redTiles.Add((31874, 94348));
            redTiles.Add((31874, 94127));
            redTiles.Add((30646, 94127));
            redTiles.Add((30646, 93188));
            redTiles.Add((29738, 93188));
            redTiles.Add((29738, 92766));
            redTiles.Add((28597, 92766));
            redTiles.Add((28597, 92326));
            redTiles.Add((27459, 92326));
            redTiles.Add((27459, 91509));
            redTiles.Add((26526, 91509));
            redTiles.Add((26526, 90860));
            redTiles.Add((25507, 90860));
            redTiles.Add((25507, 90226));
            redTiles.Add((24479, 90226));
            redTiles.Add((24479, 89993));
            redTiles.Add((23183, 89993));
            redTiles.Add((23183, 89175));
            redTiles.Add((22267, 89175));
            redTiles.Add((22267, 88771));
            redTiles.Add((21052, 88771));
            redTiles.Add((21052, 88082));
            redTiles.Add((20033, 88082));
            redTiles.Add((20033, 86875));
            redTiles.Add((19440, 86875));
            redTiles.Add((19440, 86039));
            redTiles.Add((18559, 86039));
            redTiles.Add((18559, 85150));
            redTiles.Add((17730, 85150));
            redTiles.Add((17730, 84769));
            redTiles.Add((16418, 84769));
            redTiles.Add((16418, 83845));
            redTiles.Add((15611, 83845));
            redTiles.Add((15611, 82569));
            redTiles.Add((15186, 82569));
            redTiles.Add((15186, 81626));
            redTiles.Add((14428, 81626));
            redTiles.Add((14428, 81005));
            redTiles.Add((13295, 81005));
            redTiles.Add((13295, 79912));
            redTiles.Add((12713, 79912));
            redTiles.Add((12713, 78929));
            redTiles.Add((12005, 78929));
            redTiles.Add((12005, 78138));
            redTiles.Add((11033, 78138));
            redTiles.Add((11033, 77077));
            redTiles.Add((10428, 77077));
            redTiles.Add((10428, 76034));
            redTiles.Add((9804, 76034));
            redTiles.Add((9804, 75127));
            redTiles.Add((8963, 75127));
            redTiles.Add((8963, 74103));
            redTiles.Add((8298, 74103));
            redTiles.Add((8298, 72884));
            redTiles.Add((7984, 72884));
            redTiles.Add((7984, 72088));
            redTiles.Add((6881, 72088));
            redTiles.Add((6881, 70684));
            redTiles.Add((6968, 70684));
            redTiles.Add((6968, 69636));
            redTiles.Add((6351, 69636));
            redTiles.Add((6351, 68560));
            redTiles.Add((5780, 68560));
            redTiles.Add((5780, 67344));
            redTiles.Add((5556, 67344));
            redTiles.Add((5556, 66431));
            redTiles.Add((4525, 66431));
            redTiles.Add((4525, 65176));
            redTiles.Add((4416, 65176));
            redTiles.Add((4416, 64086));
            redTiles.Add((3819, 64086));
            redTiles.Add((3819, 62866));
            redTiles.Add((3641, 62866));
            redTiles.Add((3641, 61564));
            redTiles.Add((3821, 61564));
            redTiles.Add((3821, 60371));
            redTiles.Add((3630, 60371));
            redTiles.Add((3630, 59211));
            redTiles.Add((3291, 59211));
            redTiles.Add((3291, 58099));
            redTiles.Add((2640, 58099));
            redTiles.Add((2640, 56886));
            redTiles.Add((2523, 56886));
            redTiles.Add((2523, 55695));
            redTiles.Add((2253, 55695));
            redTiles.Add((2253, 54449));
            redTiles.Add((2492, 54449));
            redTiles.Add((2492, 53280));
            redTiles.Add((1859, 53280));
            redTiles.Add((1859, 52045));
            redTiles.Add((2127, 52045));
            redTiles.Add((2127, 50825));
            redTiles.Add((2506, 50825));
            redTiles.Add((2506, 50355));
            redTiles.Add((94654, 50355));
            redTiles.Add((94654, 48393));
            redTiles.Add((1753, 48393));
            redTiles.Add((1753, 47161));
            redTiles.Add((1649, 47161));
            redTiles.Add((1649, 45990));
            redTiles.Add((2365, 45990));
            redTiles.Add((2365, 44812));
            redTiles.Add((2728, 44812));
            redTiles.Add((2728, 43562));
            redTiles.Add((2476, 43562));
            redTiles.Add((2476, 42288));
            redTiles.Add((2205, 42288));
            redTiles.Add((2205, 41205));
            redTiles.Add((3084, 41205));
            redTiles.Add((3084, 39987));
            redTiles.Add((3171, 39987));
            redTiles.Add((3171, 38884));
            redTiles.Add((3773, 38884));
            redTiles.Add((3773, 37470));
            redTiles.Add((3147, 37470));
            redTiles.Add((3147, 36318));
            redTiles.Add((3584, 36318));
            redTiles.Add((3584, 35157));
            redTiles.Add((3979, 35157));
            redTiles.Add((3979, 34015));
            redTiles.Add((4424, 34015));
            redTiles.Add((4424, 33111));
            redTiles.Add((5490, 33111));
            redTiles.Add((5490, 31733));
            redTiles.Add((5306, 31733));
            redTiles.Add((5306, 30643));
            redTiles.Add((5866, 30643));
            redTiles.Add((5866, 29505));
            redTiles.Add((6316, 29505));
            redTiles.Add((6316, 28497));
            redTiles.Add((7033, 28497));
            redTiles.Add((7033, 27460));
            redTiles.Add((7674, 27460));
            redTiles.Add((7674, 26167));
            redTiles.Add((7856, 26167));
            redTiles.Add((7856, 25202));
            redTiles.Add((8631, 25202));
            redTiles.Add((8631, 24336));
            redTiles.Add((9547, 24336));
            redTiles.Add((9547, 23414));
            redTiles.Add((10352, 23414));
            redTiles.Add((10352, 22432));
            redTiles.Add((11057, 22432));
            redTiles.Add((11057, 21152));
            redTiles.Add((11363, 21152));
            redTiles.Add((11363, 20469));
            redTiles.Add((12471, 20469));
            redTiles.Add((12471, 19669));
            redTiles.Add((13401, 19669));
            redTiles.Add((13401, 18692));
            redTiles.Add((14113, 18692));
            redTiles.Add((14113, 17344));
            redTiles.Add((14428, 17344));
            redTiles.Add((14428, 16855));
            redTiles.Add((15682, 16855));
            redTiles.Add((15682, 15949));
            redTiles.Add((16487, 15949));
            redTiles.Add((16487, 15210));
            redTiles.Add((17453, 15210));
            redTiles.Add((17453, 14246));
            redTiles.Add((18211, 14246));
            redTiles.Add((18211, 13558));
            redTiles.Add((19216, 13558));
            redTiles.Add((19216, 12858));
            redTiles.Add((20204, 12858));
            redTiles.Add((20204, 11629));
            redTiles.Add((20784, 11629));
            redTiles.Add((20784, 11086));
            redTiles.Add((21899, 11086));
            redTiles.Add((21899, 10069));
            redTiles.Add((22677, 10069));
            redTiles.Add((22677, 10058));
            redTiles.Add((24129, 10058));
            redTiles.Add((24129, 8966));
            redTiles.Add((24874, 8966));
            redTiles.Add((24874, 8693));
            redTiles.Add((26124, 8693));
            redTiles.Add((26124, 8154));
            redTiles.Add((27207, 8154));
            redTiles.Add((27207, 7628));
            redTiles.Add((28294, 7628));
            redTiles.Add((28294, 6848));
            redTiles.Add((29257, 6848));
            redTiles.Add((29257, 6198));
            redTiles.Add((30295, 6198));
            redTiles.Add((30295, 5433));
            redTiles.Add((31293, 5433));
            redTiles.Add((31293, 5518));
            redTiles.Add((32641, 5518));
            redTiles.Add((32641, 5084));
            redTiles.Add((33770, 5084));
            redTiles.Add((33770, 4790));
            redTiles.Add((34947, 4790));
            redTiles.Add((34947, 4454));
            redTiles.Add((36107, 4454));
            redTiles.Add((36107, 3372));
            redTiles.Add((37058, 3372));
            redTiles.Add((37058, 3549));
            redTiles.Add((38366, 3549));
            redTiles.Add((38366, 3623));
            redTiles.Add((39626, 3623));
            redTiles.Add((39626, 2649));
            redTiles.Add((40661, 2649));
            redTiles.Add((40661, 2813));
            redTiles.Add((41929, 2813));
            redTiles.Add((41929, 2021));
            redTiles.Add((43040, 2021));
            redTiles.Add((43040, 1969));
            redTiles.Add((44270, 1969));
            redTiles.Add((44270, 1933));
            redTiles.Add((45498, 1933));
            redTiles.Add((45498, 2175));
            redTiles.Add((46740, 2175));
            redTiles.Add((46740, 2296));
            redTiles.Add((47961, 2296));
            redTiles.Add((47961, 1776));
            redTiles.Add((49161, 1776));
            redTiles.Add((49161, 1876));
            redTiles.Add((50383, 1876));
            redTiles.Add((50383, 2074));
            redTiles.Add((51596, 2074));
            redTiles.Add((51596, 1674));
            redTiles.Add((52836, 1674));
            redTiles.Add((52836, 2320));
            redTiles.Add((54012, 2320));
            redTiles.Add((54012, 2159));
            redTiles.Add((55249, 2159));
            redTiles.Add((55249, 2145));
            redTiles.Add((56482, 2145));
            redTiles.Add((56482, 3018));
            redTiles.Add((57580, 3018));
            redTiles.Add((57580, 2797));
            redTiles.Add((58848, 2797));
            redTiles.Add((58848, 3391));
            redTiles.Add((59965, 3391));
            redTiles.Add((59965, 3412));
            redTiles.Add((61202, 3412));
            redTiles.Add((61202, 3683));
            redTiles.Add((62386, 3683));
            redTiles.Add((62386, 4199));
            redTiles.Add((63500, 4199));
            redTiles.Add((63500, 4587));
            redTiles.Add((64646, 4587));
            redTiles.Add((64646, 4303));
            redTiles.Add((66026, 4303));
            redTiles.Add((66026, 4755));
            redTiles.Add((67167, 4755));
            redTiles.Add((67167, 5712));
            redTiles.Add((68100, 5712));
            redTiles.Add((68100, 5682));
            redTiles.Add((69437, 5682));
            redTiles.Add((69437, 6242));
            redTiles.Add((70528, 6242));
            redTiles.Add((70528, 6642));
            redTiles.Add((71697, 6642));
            redTiles.Add((71697, 7617));
            redTiles.Add((72570, 7617));
            redTiles.Add((72570, 8366));
            redTiles.Add((73543, 8366));
            redTiles.Add((73543, 8965));
            redTiles.Add((74597, 8965));
            redTiles.Add((74597, 9337));
            redTiles.Add((75796, 9337));
            redTiles.Add((75796, 10371));
            redTiles.Add((76572, 10371));
            redTiles.Add((76572, 10424));
            redTiles.Add((78015, 10424));
            redTiles.Add((78015, 11590));
            redTiles.Add((78678, 11590));
            redTiles.Add((78678, 12238));
            redTiles.Add((79713, 12238));
            redTiles.Add((79713, 13323));
            redTiles.Add((80395, 13323));
            redTiles.Add((80395, 14143));
            redTiles.Add((81280, 14143));
            redTiles.Add((81280, 14799));
            redTiles.Add((82315, 14799));
            redTiles.Add((82315, 15799));
            redTiles.Add((83031, 15799));
            redTiles.Add((83031, 15981));
            redTiles.Add((84563, 15981));
            redTiles.Add((84563, 17236));
            redTiles.Add((85020, 17236));
            redTiles.Add((85020, 17966));
            redTiles.Add((86028, 17966));
            redTiles.Add((86028, 19196));
            redTiles.Add((86465, 19196));
            redTiles.Add((86465, 19837));
            redTiles.Add((87597, 19837));
            redTiles.Add((87597, 20876));
            redTiles.Add((88249, 20876));
            redTiles.Add((88249, 21704));
            redTiles.Add((89182, 21704));
            redTiles.Add((89182, 23164));
            redTiles.Add((89218, 23164));
            redTiles.Add((89218, 23700));
            redTiles.Add((90604, 23700));
            redTiles.Add((90604, 24983));
            redTiles.Add((90856, 24983));
            redTiles.Add((90856, 26196));
            redTiles.Add((91183, 26196));
            redTiles.Add((91183, 27052));
            redTiles.Add((92131, 27052));
            redTiles.Add((92131, 28071));
            redTiles.Add((92807, 28071));
            redTiles.Add((92807, 29406));
            redTiles.Add((92842, 29406));
            redTiles.Add((92842, 30501));
            redTiles.Add((93343, 30501));
            redTiles.Add((93343, 31471));
            redTiles.Add((94141, 31471));
            redTiles.Add((94141, 32454));
            redTiles.Add((94960, 32454));
            redTiles.Add((94960, 33646));
            redTiles.Add((95258, 33646));
            redTiles.Add((95258, 34752));
            redTiles.Add((95794, 34752));
            redTiles.Add((95794, 36019));
            redTiles.Add((95832, 36019));
            redTiles.Add((95832, 37122));
            redTiles.Add((96396, 37122));
            redTiles.Add((96396, 38226));
            redTiles.Add((97009, 38226));
            redTiles.Add((97009, 39503));
            redTiles.Add((96927, 39503));
            redTiles.Add((96927, 40706));
            redTiles.Add((97123, 40706));
            redTiles.Add((97123, 41878));
            redTiles.Add((97488, 41878));
            redTiles.Add((97488, 43044));
            redTiles.Add((97947, 43044));
            redTiles.Add((97947, 44345));
            redTiles.Add((97407, 44345));
            redTiles.Add((97407, 45566));
            redTiles.Add((97334, 45566));
            redTiles.Add((97334, 46732));
            redTiles.Add((97943, 46732));
            redTiles.Add((97943, 47937));
            redTiles.Add((98268, 47937));
            redTiles.Add((98268, 49160));
            redTiles.Add((98313, 49160));
            redTiles.Add((98313, 50383));
        }
    }
}
