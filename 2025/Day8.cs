//https://adventofcode.com/2025/day/8
using System.Runtime.InteropServices;

namespace AdventOfCode.Year2025
{
    using MathNet.Numerics;
    using System.Linq;
    using Coordinate3D = (long x, long y, long z);

    class Day8 : IDay
    {
        private List<Coordinate3D> boxes = [];
        private List<List<Coordinate3D>> circuits = [];

        public void Run()
        {
            LoadData();

            var start = DateTime.Now;

            // 26400
            Console.WriteLine($"Part 1 answer: {Part1()}");
            // 8199963486
            Console.WriteLine($"Part 2 answer: {Part2()}");

            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        private long Part1()
        {
            // Limit combining to first 1000 pairs
            return CombineCircuits(1000);
        }

        private long Part2()
        {
            // Combine until a single global circuit achieved
            return CombineCircuits(-1);
        }

        private long CombineCircuits(int limit)
        {
            circuits = [];

            // Create all combinations of pairs of boxes
            var boxPairs = new List<(Coordinate3D pos1, Coordinate3D pos2, double distance)>();

            for (var a = 0; a < boxes.Count - 1; a++)
            {
                for (var b = a + 1; b < boxes.Count; b++)
                {
                    boxPairs.Add((boxes[a], boxes[b], Distance(boxes[a], boxes[b])));
                }
            }

            // Get box pairs ordered by distance
            var orderedBoxPairs = boxPairs.OrderBy(p => p.distance).ToList();

            // Enumerate box pairs by distance
            foreach (var (pos1, pos2, distance) in orderedBoxPairs)
            {
                // Find existing circuits containing either box in the pair
                var existing = circuits.Where(c => c.Contains(pos1) || c.Contains(pos2)).ToList();

                // If existing circuit(s) found...
                if (existing.Count != 0)
                {
                    // Get first circuit found
                    var first = existing.First();

                    // If another circuit found..
                    if (existing.Count > 1)
                    {
                        // Get the other circuit found
                        var last = existing.Last();

                        // Add any boxes in other circuit to first that aren't already present
                        foreach (var pos in last)
                        {
                            if (!first.Contains(pos))
                            {
                                first.Add(pos);
                            }
                        }

                        // Remove other circuit now merged into first
                        circuits.Remove(last);
                    }

                    // Add first box in pair to existing (now possibly-merged) circuit if it doesn't exist
                    if (!first.Contains(pos1))
                    {
                        first.Add(pos1);
                    }

                    // Add second box in pair to existing (now possibly-merged) circuit if it doesn't exist
                    if (!first.Contains(pos2))
                    {
                        first.Add(pos2);
                    }
                }
                else
                {
                    // Create new circuit with pair of boxes
                    circuits.Add([pos1, pos2]);
                }

                // If only combining a limited number of pairs
                if (limit > 0)
                {
                    // Decrement counter
                    limit--;

                    // If we've reached the limit...
                    if (limit == 0)
                    {
                        // Get the top 3 largest distinct circuit sizes
                        var sizes = circuits.Select(c => c.Count).OrderByDescending(c => c).Distinct().Take(3).ToList();

                        // Multiply their sizes together
                        long product = 1;

                        foreach (var size in sizes)
                        {
                            product *= size;
                        }

                        // Return product of top 3 largest distinct circuit sizes
                        return product;
                    }
                }
                else
                {
                    // If not limited to a number of pairs, check if we've made it to a single circuit containing all the boxes
                    if (circuits.Count == 1 && circuits.First().Count == boxes.Count)
                    {
                        // If so, return the product of the x coordinates of the last pair that achieved a global circuit
                        return pos1.x * pos2.x;
                    }
                }
            }

            // Should never get here
            return 0;
        }

        private double Distance(Coordinate3D pos1, Coordinate3D pos2)
        {
            // Distance in 3d space is sqrt( (x2-x1)^2 + (y2-y1)^2 + (z2-z1)^2 )
            return Math.Sqrt(Math.Pow(pos2.x - pos1.x, 2) + Math.Pow(pos2.y - pos1.y, 2) + Math.Pow(pos2.z - pos1.z, 2));
        }

        private void LoadData()
        {
            boxes.Add((21756, 55854, 50471));
            boxes.Add((60634, 55363, 26856));
            boxes.Add((46581, 80134, 49514));
            boxes.Add((51249, 76468, 86188));
            boxes.Add((89032, 41793, 41203));
            boxes.Add((35126, 2547, 78373));
            boxes.Add((65082, 82062, 70157));
            boxes.Add((83307, 3582, 73699));
            boxes.Add((37992, 91363, 1629));
            boxes.Add((31342, 15700, 12220));
            boxes.Add((27280, 86712, 46195));
            boxes.Add((63128, 56698, 61008));
            boxes.Add((63550, 77438, 13007));
            boxes.Add((46218, 49987, 34593));
            boxes.Add((7585, 43797, 79468));
            boxes.Add((74838, 11465, 90231));
            boxes.Add((89197, 49603, 5720));
            boxes.Add((91772, 55052, 60049));
            boxes.Add((52576, 55719, 98983));
            boxes.Add((72155, 68044, 36079));
            boxes.Add((20013, 88791, 7243));
            boxes.Add((64428, 92609, 56133));
            boxes.Add((35612, 58474, 1909));
            boxes.Add((83547, 13439, 51474));
            boxes.Add((1999, 39426, 73642));
            boxes.Add((64606, 66960, 28522));
            boxes.Add((99720, 14946, 12652));
            boxes.Add((64038, 83848, 66411));
            boxes.Add((66702, 76238, 56105));
            boxes.Add((56311, 53482, 81771));
            boxes.Add((62060, 71580, 96958));
            boxes.Add((8557, 34809, 76038));
            boxes.Add((66661, 91031, 61998));
            boxes.Add((61757, 47973, 32534));
            boxes.Add((11486, 86578, 87713));
            boxes.Add((64530, 19508, 77898));
            boxes.Add((87556, 7217, 1350));
            boxes.Add((5764, 11965, 69898));
            boxes.Add((50657, 21232, 72681));
            boxes.Add((91364, 3985, 97656));
            boxes.Add((9792, 55649, 61780));
            boxes.Add((21246, 29215, 74029));
            boxes.Add((45656, 35031, 80252));
            boxes.Add((73475, 99926, 18397));
            boxes.Add((74354, 32, 98583));
            boxes.Add((67900, 286, 34505));
            boxes.Add((28083, 58495, 81783));
            boxes.Add((30601, 82896, 95911));
            boxes.Add((35021, 51941, 31148));
            boxes.Add((98195, 5726, 85786));
            boxes.Add((98902, 58905, 23625));
            boxes.Add((5418, 45846, 76564));
            boxes.Add((86768, 99719, 94804));
            boxes.Add((79587, 44686, 73428));
            boxes.Add((60582, 16410, 59716));
            boxes.Add((82404, 48159, 14995));
            boxes.Add((95313, 19369, 49708));
            boxes.Add((80863, 71726, 22453));
            boxes.Add((28133, 99341, 88912));
            boxes.Add((83982, 90756, 25639));
            boxes.Add((97565, 83994, 9997));
            boxes.Add((89803, 88037, 32453));
            boxes.Add((62890, 55479, 8099));
            boxes.Add((48003, 33343, 58966));
            boxes.Add((40771, 27878, 40167));
            boxes.Add((14470, 7957, 23342));
            boxes.Add((31130, 12867, 81751));
            boxes.Add((54560, 74155, 14235));
            boxes.Add((8830, 6092, 7945));
            boxes.Add((51997, 92273, 94302));
            boxes.Add((7043, 31461, 45954));
            boxes.Add((97025, 12761, 34088));
            boxes.Add((87815, 66823, 68558));
            boxes.Add((86763, 77081, 96629));
            boxes.Add((33978, 42147, 15885));
            boxes.Add((81439, 87884, 50596));
            boxes.Add((25589, 84587, 42719));
            boxes.Add((8991, 2270, 11329));
            boxes.Add((95189, 8742, 42392));
            boxes.Add((56453, 24051, 342));
            boxes.Add((79571, 93945, 96465));
            boxes.Add((59828, 64908, 34015));
            boxes.Add((65280, 12637, 68311));
            boxes.Add((81621, 80115, 10417));
            boxes.Add((45583, 87871, 15420));
            boxes.Add((98742, 57985, 14854));
            boxes.Add((54007, 79374, 7333));
            boxes.Add((10807, 90611, 95101));
            boxes.Add((53070, 67510, 17717));
            boxes.Add((22572, 58766, 58810));
            boxes.Add((76545, 87150, 7342));
            boxes.Add((17911, 46235, 41390));
            boxes.Add((68139, 32883, 70802));
            boxes.Add((16401, 68737, 32690));
            boxes.Add((98632, 44072, 51761));
            boxes.Add((9309, 15584, 2175));
            boxes.Add((96580, 11366, 68009));
            boxes.Add((87670, 79898, 38604));
            boxes.Add((94429, 23754, 93152));
            boxes.Add((28701, 57506, 40129));
            boxes.Add((41260, 24131, 5271));
            boxes.Add((84570, 12388, 29229));
            boxes.Add((1241, 67012, 35700));
            boxes.Add((60172, 76617, 59086));
            boxes.Add((69906, 73736, 48744));
            boxes.Add((65422, 84070, 56266));
            boxes.Add((63634, 5118, 80649));
            boxes.Add((11006, 96790, 38881));
            boxes.Add((53984, 99671, 46536));
            boxes.Add((9640, 93040, 49815));
            boxes.Add((83486, 31223, 747));
            boxes.Add((11080, 55018, 24449));
            boxes.Add((71925, 35938, 56059));
            boxes.Add((15028, 13825, 30018));
            boxes.Add((59291, 39610, 6437));
            boxes.Add((96837, 85989, 60659));
            boxes.Add((40970, 25878, 842));
            boxes.Add((37205, 83513, 2751));
            boxes.Add((32969, 95685, 34469));
            boxes.Add((68084, 84174, 65675));
            boxes.Add((72951, 7037, 52629));
            boxes.Add((1262, 26969, 4658));
            boxes.Add((14560, 75919, 15683));
            boxes.Add((70939, 2040, 82000));
            boxes.Add((97980, 92261, 46204));
            boxes.Add((64322, 93396, 48340));
            boxes.Add((21728, 88930, 36337));
            boxes.Add((92799, 284, 75006));
            boxes.Add((84664, 4586, 63260));
            boxes.Add((71321, 38812, 1732));
            boxes.Add((22928, 23857, 80424));
            boxes.Add((70163, 87402, 82469));
            boxes.Add((10556, 1483, 79501));
            boxes.Add((20263, 39955, 84754));
            boxes.Add((79467, 67005, 49116));
            boxes.Add((39703, 23433, 78988));
            boxes.Add((17411, 70144, 15640));
            boxes.Add((12814, 573, 16563));
            boxes.Add((35361, 34262, 78544));
            boxes.Add((81448, 66924, 59369));
            boxes.Add((73657, 27894, 1512));
            boxes.Add((90877, 57404, 25337));
            boxes.Add((45808, 16115, 85734));
            boxes.Add((51714, 88969, 73027));
            boxes.Add((73609, 1167, 84807));
            boxes.Add((59426, 93115, 84501));
            boxes.Add((79952, 60542, 42220));
            boxes.Add((16383, 85794, 44798));
            boxes.Add((27430, 30564, 47149));
            boxes.Add((34392, 60280, 51066));
            boxes.Add((94302, 92059, 19638));
            boxes.Add((22958, 87888, 88508));
            boxes.Add((46771, 98167, 33990));
            boxes.Add((75755, 43752, 7615));
            boxes.Add((86125, 3416, 46044));
            boxes.Add((85749, 83020, 80263));
            boxes.Add((53453, 53702, 38172));
            boxes.Add((18601, 78687, 95489));
            boxes.Add((86305, 59793, 16523));
            boxes.Add((62699, 60574, 85264));
            boxes.Add((38366, 27688, 75421));
            boxes.Add((77091, 74380, 36268));
            boxes.Add((96719, 28754, 88331));
            boxes.Add((27246, 11433, 81499));
            boxes.Add((66994, 28751, 52505));
            boxes.Add((940, 7106, 77300));
            boxes.Add((74965, 26965, 67058));
            boxes.Add((53919, 20792, 56750));
            boxes.Add((69264, 87659, 2560));
            boxes.Add((23646, 61560, 37637));
            boxes.Add((99417, 46236, 24307));
            boxes.Add((53702, 8683, 62966));
            boxes.Add((3724, 8130, 68139));
            boxes.Add((73761, 54255, 38052));
            boxes.Add((38723, 55284, 33813));
            boxes.Add((93839, 66189, 39341));
            boxes.Add((48947, 32144, 69873));
            boxes.Add((71769, 33374, 55149));
            boxes.Add((3996, 43590, 36906));
            boxes.Add((87696, 14026, 87672));
            boxes.Add((34263, 10734, 50152));
            boxes.Add((7446, 49207, 69144));
            boxes.Add((79629, 82841, 51128));
            boxes.Add((31938, 57853, 30505));
            boxes.Add((73969, 67243, 17902));
            boxes.Add((67164, 44053, 69234));
            boxes.Add((33802, 87770, 79855));
            boxes.Add((31346, 12901, 63561));
            boxes.Add((46627, 43726, 6969));
            boxes.Add((43549, 48441, 23255));
            boxes.Add((74642, 99211, 13999));
            boxes.Add((83586, 24604, 43390));
            boxes.Add((57319, 69101, 59786));
            boxes.Add((26892, 55294, 10462));
            boxes.Add((88576, 30230, 98504));
            boxes.Add((87347, 75386, 22620));
            boxes.Add((15282, 84045, 46336));
            boxes.Add((19247, 17, 806));
            boxes.Add((17766, 83247, 61291));
            boxes.Add((94424, 67736, 57462));
            boxes.Add((42399, 98470, 60777));
            boxes.Add((78188, 13115, 51231));
            boxes.Add((71439, 88752, 26540));
            boxes.Add((98999, 35581, 93197));
            boxes.Add((25985, 56258, 12788));
            boxes.Add((42668, 54298, 81832));
            boxes.Add((37479, 40885, 99725));
            boxes.Add((61909, 8806, 66361));
            boxes.Add((29246, 94241, 54453));
            boxes.Add((2647, 40768, 11695));
            boxes.Add((55347, 22919, 71277));
            boxes.Add((12983, 36776, 9696));
            boxes.Add((47970, 30975, 86484));
            boxes.Add((86503, 65863, 37788));
            boxes.Add((17350, 67136, 28050));
            boxes.Add((86232, 36821, 75011));
            boxes.Add((36438, 20579, 48441));
            boxes.Add((74281, 71908, 59393));
            boxes.Add((93729, 74272, 36662));
            boxes.Add((89070, 42009, 96895));
            boxes.Add((39255, 90040, 68367));
            boxes.Add((24370, 8278, 6345));
            boxes.Add((88079, 2591, 5678));
            boxes.Add((22876, 71872, 58263));
            boxes.Add((39787, 19012, 78408));
            boxes.Add((79300, 23650, 60338));
            boxes.Add((56277, 5659, 91377));
            boxes.Add((85734, 81160, 18572));
            boxes.Add((36985, 61044, 68442));
            boxes.Add((942, 22322, 45174));
            boxes.Add((96402, 60367, 66829));
            boxes.Add((43542, 69713, 63006));
            boxes.Add((68487, 77828, 71924));
            boxes.Add((64108, 56545, 51859));
            boxes.Add((94016, 46935, 69666));
            boxes.Add((39772, 74210, 91924));
            boxes.Add((22692, 14415, 52770));
            boxes.Add((2722, 39089, 97961));
            boxes.Add((90384, 64338, 9796));
            boxes.Add((81074, 46229, 14652));
            boxes.Add((804, 76555, 83475));
            boxes.Add((76110, 87727, 54513));
            boxes.Add((7807, 36400, 94511));
            boxes.Add((31616, 35617, 40488));
            boxes.Add((62220, 41477, 90411));
            boxes.Add((81595, 62030, 7583));
            boxes.Add((91582, 63828, 91456));
            boxes.Add((37170, 1999, 77302));
            boxes.Add((84678, 93078, 58538));
            boxes.Add((39359, 14603, 21691));
            boxes.Add((11298, 38786, 36744));
            boxes.Add((85893, 26960, 88801));
            boxes.Add((1830, 91304, 74067));
            boxes.Add((77910, 56565, 97387));
            boxes.Add((52391, 73618, 29540));
            boxes.Add((69302, 75267, 14662));
            boxes.Add((13842, 75112, 78035));
            boxes.Add((43878, 69439, 41302));
            boxes.Add((43146, 21531, 69447));
            boxes.Add((99087, 84118, 92016));
            boxes.Add((55546, 38065, 67566));
            boxes.Add((93869, 82330, 11976));
            boxes.Add((21585, 31211, 59349));
            boxes.Add((45245, 46719, 5858));
            boxes.Add((17011, 76292, 24844));
            boxes.Add((23695, 60077, 53972));
            boxes.Add((93962, 81314, 44716));
            boxes.Add((12821, 1152, 42965));
            boxes.Add((44389, 78757, 59608));
            boxes.Add((81370, 11413, 63594));
            boxes.Add((62663, 65707, 7795));
            boxes.Add((59938, 61291, 86880));
            boxes.Add((66746, 40034, 1252));
            boxes.Add((54188, 58725, 36721));
            boxes.Add((70936, 32463, 9358));
            boxes.Add((62313, 5939, 7899));
            boxes.Add((45404, 23867, 76338));
            boxes.Add((31192, 6095, 69485));
            boxes.Add((88061, 34489, 35550));
            boxes.Add((50139, 21049, 14194));
            boxes.Add((85281, 81619, 10307));
            boxes.Add((14286, 79402, 19877));
            boxes.Add((27518, 97824, 87561));
            boxes.Add((24962, 12295, 10159));
            boxes.Add((77711, 40404, 25314));
            boxes.Add((82551, 81707, 89028));
            boxes.Add((87603, 5082, 58042));
            boxes.Add((17431, 38805, 82346));
            boxes.Add((17363, 70612, 94979));
            boxes.Add((33376, 35108, 58244));
            boxes.Add((36787, 81115, 91867));
            boxes.Add((73801, 79906, 8500));
            boxes.Add((93866, 38161, 10355));
            boxes.Add((13578, 54092, 77426));
            boxes.Add((41814, 40143, 87681));
            boxes.Add((79607, 24665, 72020));
            boxes.Add((54653, 51751, 6895));
            boxes.Add((76477, 88123, 65130));
            boxes.Add((85291, 47471, 78932));
            boxes.Add((4750, 56837, 3591));
            boxes.Add((57839, 55751, 70506));
            boxes.Add((24094, 66978, 64983));
            boxes.Add((19745, 83465, 43393));
            boxes.Add((14315, 7087, 58989));
            boxes.Add((32696, 62873, 51089));
            boxes.Add((14283, 1123, 39799));
            boxes.Add((9148, 96077, 43638));
            boxes.Add((55072, 19741, 52420));
            boxes.Add((75761, 3059, 57014));
            boxes.Add((82385, 12267, 47682));
            boxes.Add((66982, 44181, 48964));
            boxes.Add((37043, 66296, 77804));
            boxes.Add((79736, 13203, 24824));
            boxes.Add((77139, 62859, 75279));
            boxes.Add((86104, 75259, 44033));
            boxes.Add((63415, 64074, 75888));
            boxes.Add((2345, 51238, 69720));
            boxes.Add((77241, 95758, 35784));
            boxes.Add((92270, 35083, 89107));
            boxes.Add((68178, 81075, 6796));
            boxes.Add((76869, 74516, 36958));
            boxes.Add((3830, 199, 24173));
            boxes.Add((36023, 66269, 70371));
            boxes.Add((39057, 30718, 9818));
            boxes.Add((74480, 30228, 28673));
            boxes.Add((95569, 62273, 52619));
            boxes.Add((60963, 39000, 11381));
            boxes.Add((90765, 59924, 23108));
            boxes.Add((91026, 11203, 59712));
            boxes.Add((67231, 98130, 25202));
            boxes.Add((67892, 47307, 14213));
            boxes.Add((9664, 86368, 73924));
            boxes.Add((24020, 96967, 58025));
            boxes.Add((34259, 77450, 5476));
            boxes.Add((21, 54646, 53189));
            boxes.Add((82012, 44519, 67994));
            boxes.Add((66263, 33176, 26843));
            boxes.Add((89467, 31105, 52725));
            boxes.Add((43994, 43880, 58084));
            boxes.Add((2874, 2874, 16991));
            boxes.Add((94085, 75117, 7451));
            boxes.Add((52721, 68634, 64708));
            boxes.Add((12807, 10142, 36118));
            boxes.Add((17276, 37137, 63454));
            boxes.Add((4574, 97864, 51364));
            boxes.Add((49147, 62701, 45265));
            boxes.Add((11252, 75663, 92634));
            boxes.Add((87471, 5415, 39541));
            boxes.Add((27540, 39035, 31297));
            boxes.Add((79670, 54366, 85673));
            boxes.Add((92201, 28634, 4690));
            boxes.Add((95349, 14391, 31576));
            boxes.Add((35175, 14166, 91877));
            boxes.Add((32550, 96069, 97354));
            boxes.Add((76043, 23383, 76512));
            boxes.Add((54661, 81013, 97758));
            boxes.Add((12298, 21128, 87613));
            boxes.Add((4202, 40879, 91033));
            boxes.Add((7803, 85976, 93374));
            boxes.Add((18394, 47099, 87562));
            boxes.Add((60725, 12300, 49417));
            boxes.Add((89375, 55004, 61571));
            boxes.Add((71916, 61974, 84377));
            boxes.Add((59135, 57287, 54223));
            boxes.Add((56201, 62397, 7962));
            boxes.Add((7111, 34809, 98263));
            boxes.Add((27043, 15397, 84309));
            boxes.Add((78628, 50550, 6425));
            boxes.Add((80370, 38884, 26433));
            boxes.Add((21668, 44683, 4625));
            boxes.Add((89665, 38440, 40501));
            boxes.Add((63152, 76732, 62193));
            boxes.Add((24015, 1141, 24536));
            boxes.Add((23657, 14032, 28838));
            boxes.Add((11016, 88373, 68660));
            boxes.Add((57201, 40650, 1706));
            boxes.Add((22827, 16360, 26093));
            boxes.Add((62026, 82294, 92697));
            boxes.Add((6119, 14308, 9336));
            boxes.Add((34772, 3995, 50778));
            boxes.Add((850, 4167, 23676));
            boxes.Add((73581, 13045, 71604));
            boxes.Add((3639, 34654, 46919));
            boxes.Add((28192, 69633, 2361));
            boxes.Add((77878, 70051, 13568));
            boxes.Add((84000, 690, 46973));
            boxes.Add((61745, 43465, 10248));
            boxes.Add((43642, 12695, 18248));
            boxes.Add((49929, 88825, 24220));
            boxes.Add((48218, 15786, 93304));
            boxes.Add((7483, 57402, 97689));
            boxes.Add((82705, 59983, 71019));
            boxes.Add((91705, 74665, 1282));
            boxes.Add((33765, 13114, 10362));
            boxes.Add((97857, 16424, 41265));
            boxes.Add((74433, 4299, 40053));
            boxes.Add((8107, 62286, 55427));
            boxes.Add((7911, 7034, 93917));
            boxes.Add((11187, 51909, 10599));
            boxes.Add((44357, 47154, 73409));
            boxes.Add((90400, 4960, 57308));
            boxes.Add((77731, 83783, 939));
            boxes.Add((71687, 71671, 36253));
            boxes.Add((39032, 14410, 56834));
            boxes.Add((9890, 17158, 32353));
            boxes.Add((15676, 13753, 93730));
            boxes.Add((1820, 48108, 16979));
            boxes.Add((38162, 80415, 84544));
            boxes.Add((16832, 92604, 70201));
            boxes.Add((25716, 77690, 96427));
            boxes.Add((22015, 53786, 41355));
            boxes.Add((13998, 61498, 25745));
            boxes.Add((50005, 69490, 96550));
            boxes.Add((15226, 69771, 12534));
            boxes.Add((60896, 68347, 61823));
            boxes.Add((99960, 16810, 2573));
            boxes.Add((8675, 92884, 34484));
            boxes.Add((13565, 63514, 97304));
            boxes.Add((59288, 86375, 79838));
            boxes.Add((59621, 89727, 95877));
            boxes.Add((85588, 6549, 6902));
            boxes.Add((52932, 61115, 59258));
            boxes.Add((4967, 22411, 42257));
            boxes.Add((72957, 75724, 35802));
            boxes.Add((9992, 50968, 48850));
            boxes.Add((12864, 15876, 53680));
            boxes.Add((2344, 25617, 79655));
            boxes.Add((91025, 50948, 5357));
            boxes.Add((42140, 27474, 29133));
            boxes.Add((44208, 55112, 92667));
            boxes.Add((90756, 51749, 91376));
            boxes.Add((31819, 968, 32404));
            boxes.Add((95175, 27854, 43867));
            boxes.Add((78400, 7410, 13107));
            boxes.Add((78821, 67375, 65087));
            boxes.Add((70321, 61297, 93924));
            boxes.Add((22198, 10041, 83203));
            boxes.Add((95029, 84077, 40358));
            boxes.Add((60813, 84173, 11799));
            boxes.Add((2994, 3528, 35702));
            boxes.Add((92345, 890, 77507));
            boxes.Add((46506, 97239, 52787));
            boxes.Add((48288, 97290, 62752));
            boxes.Add((71744, 55019, 80327));
            boxes.Add((22445, 21202, 65303));
            boxes.Add((8961, 43916, 74386));
            boxes.Add((58442, 68977, 60629));
            boxes.Add((12509, 31312, 70692));
            boxes.Add((23496, 92936, 55340));
            boxes.Add((59208, 10437, 406));
            boxes.Add((95842, 99186, 43081));
            boxes.Add((38947, 42224, 68824));
            boxes.Add((43570, 7953, 65565));
            boxes.Add((39039, 79430, 37211));
            boxes.Add((90420, 30494, 56792));
            boxes.Add((89478, 32203, 99570));
            boxes.Add((30719, 7771, 47505));
            boxes.Add((10318, 62910, 22869));
            boxes.Add((79625, 72359, 11021));
            boxes.Add((5331, 58251, 23407));
            boxes.Add((27704, 58261, 53877));
            boxes.Add((82255, 61391, 91461));
            boxes.Add((62551, 5576, 22777));
            boxes.Add((84904, 43654, 25547));
            boxes.Add((31766, 57221, 86826));
            boxes.Add((90156, 17887, 71760));
            boxes.Add((98169, 52934, 74274));
            boxes.Add((84286, 70142, 82044));
            boxes.Add((80590, 9432, 4333));
            boxes.Add((62127, 15637, 31182));
            boxes.Add((56597, 17249, 85849));
            boxes.Add((31060, 55822, 75115));
            boxes.Add((44001, 57856, 41500));
            boxes.Add((45547, 63797, 18104));
            boxes.Add((72921, 88516, 53839));
            boxes.Add((56671, 80420, 76304));
            boxes.Add((29914, 3207, 73851));
            boxes.Add((1982, 41495, 40747));
            boxes.Add((57442, 83032, 48642));
            boxes.Add((66882, 55869, 49179));
            boxes.Add((33488, 51651, 61720));
            boxes.Add((71026, 78824, 87318));
            boxes.Add((17550, 78701, 59873));
            boxes.Add((87591, 39972, 74166));
            boxes.Add((37313, 43307, 42432));
            boxes.Add((58077, 55758, 20539));
            boxes.Add((76550, 61956, 95760));
            boxes.Add((89211, 68595, 3098));
            boxes.Add((85493, 66135, 2496));
            boxes.Add((5917, 94781, 17781));
            boxes.Add((88771, 66783, 36107));
            boxes.Add((93757, 50274, 75658));
            boxes.Add((19589, 1786, 61234));
            boxes.Add((57568, 19238, 15788));
            boxes.Add((51880, 9250, 31670));
            boxes.Add((58626, 62958, 94673));
            boxes.Add((54122, 74911, 24551));
            boxes.Add((29976, 92179, 13441));
            boxes.Add((83481, 50135, 78086));
            boxes.Add((56179, 553, 62102));
            boxes.Add((44481, 45687, 40356));
            boxes.Add((71568, 45603, 86605));
            boxes.Add((35342, 32001, 45620));
            boxes.Add((91802, 95295, 27533));
            boxes.Add((32084, 49238, 71837));
            boxes.Add((27760, 49498, 42233));
            boxes.Add((63825, 68222, 54429));
            boxes.Add((30911, 47640, 30392));
            boxes.Add((48144, 15002, 40683));
            boxes.Add((48201, 56760, 43308));
            boxes.Add((6748, 22776, 71702));
            boxes.Add((9939, 8217, 15059));
            boxes.Add((86803, 5507, 17618));
            boxes.Add((28687, 32621, 47795));
            boxes.Add((55678, 28095, 6094));
            boxes.Add((52813, 5423, 33585));
            boxes.Add((75034, 76702, 13578));
            boxes.Add((34525, 35449, 41507));
            boxes.Add((77900, 91248, 56411));
            boxes.Add((84767, 99158, 97198));
            boxes.Add((36088, 52001, 61413));
            boxes.Add((25943, 45313, 92064));
            boxes.Add((76566, 37475, 66340));
            boxes.Add((53987, 66674, 98478));
            boxes.Add((66439, 7912, 58450));
            boxes.Add((29615, 34043, 86257));
            boxes.Add((72033, 16331, 62157));
            boxes.Add((29999, 14480, 58957));
            boxes.Add((85536, 21226, 46325));
            boxes.Add((32957, 68291, 30825));
            boxes.Add((6665, 35149, 33600));
            boxes.Add((29532, 32961, 99478));
            boxes.Add((21224, 23839, 41395));
            boxes.Add((33804, 28703, 33688));
            boxes.Add((82306, 3127, 23183));
            boxes.Add((26801, 13739, 25258));
            boxes.Add((20639, 41372, 50307));
            boxes.Add((51128, 93110, 43100));
            boxes.Add((20838, 37418, 40904));
            boxes.Add((78743, 77956, 36177));
            boxes.Add((21613, 63082, 38106));
            boxes.Add((91262, 60103, 2094));
            boxes.Add((28349, 50193, 30218));
            boxes.Add((46976, 84460, 42130));
            boxes.Add((39230, 52160, 85950));
            boxes.Add((96761, 7540, 88530));
            boxes.Add((50985, 57711, 98828));
            boxes.Add((29594, 16128, 89453));
            boxes.Add((57942, 74260, 31160));
            boxes.Add((42096, 13199, 51899));
            boxes.Add((46999, 70204, 98790));
            boxes.Add((24462, 6327, 21124));
            boxes.Add((68911, 11485, 54469));
            boxes.Add((46107, 4233, 44067));
            boxes.Add((18541, 92186, 17783));
            boxes.Add((99303, 13645, 5538));
            boxes.Add((30661, 85543, 69960));
            boxes.Add((23204, 90971, 4640));
            boxes.Add((49923, 4131, 99195));
            boxes.Add((94937, 18435, 7591));
            boxes.Add((32855, 36740, 76472));
            boxes.Add((42502, 17630, 2824));
            boxes.Add((46225, 47042, 1569));
            boxes.Add((41864, 63565, 46908));
            boxes.Add((48455, 57066, 77128));
            boxes.Add((10315, 59276, 58102));
            boxes.Add((10362, 16562, 55029));
            boxes.Add((26910, 27470, 76231));
            boxes.Add((46389, 9972, 72262));
            boxes.Add((48426, 77197, 77117));
            boxes.Add((62542, 53201, 48937));
            boxes.Add((23376, 15849, 54694));
            boxes.Add((16703, 22302, 25445));
            boxes.Add((17267, 36329, 10782));
            boxes.Add((70380, 8148, 44828));
            boxes.Add((23649, 28848, 26011));
            boxes.Add((67945, 36324, 19920));
            boxes.Add((86286, 79990, 82907));
            boxes.Add((51826, 46630, 41325));
            boxes.Add((21031, 33372, 36354));
            boxes.Add((21357, 21825, 63739));
            boxes.Add((83047, 82136, 89473));
            boxes.Add((61093, 50283, 66097));
            boxes.Add((9113, 4993, 99148));
            boxes.Add((56705, 90901, 36163));
            boxes.Add((21113, 82473, 11613));
            boxes.Add((90599, 13064, 21549));
            boxes.Add((50256, 6051, 50576));
            boxes.Add((58743, 1480, 66580));
            boxes.Add((80861, 11194, 17480));
            boxes.Add((86094, 91312, 98739));
            boxes.Add((47008, 41563, 97129));
            boxes.Add((5492, 87115, 70081));
            boxes.Add((36563, 88975, 51161));
            boxes.Add((36314, 14576, 10710));
            boxes.Add((54468, 55186, 15233));
            boxes.Add((19256, 78899, 42507));
            boxes.Add((25180, 34866, 59417));
            boxes.Add((38606, 10833, 99048));
            boxes.Add((32098, 58657, 51321));
            boxes.Add((37110, 44501, 21489));
            boxes.Add((99092, 75215, 89123));
            boxes.Add((75975, 66931, 83359));
            boxes.Add((19985, 12049, 90940));
            boxes.Add((23245, 25086, 19765));
            boxes.Add((5912, 46008, 85360));
            boxes.Add((54291, 75034, 53720));
            boxes.Add((68310, 13052, 17883));
            boxes.Add((44172, 4098, 21318));
            boxes.Add((77217, 48539, 28784));
            boxes.Add((18280, 99809, 32806));
            boxes.Add((66453, 2943, 92996));
            boxes.Add((17266, 55079, 30201));
            boxes.Add((66099, 13900, 3761));
            boxes.Add((86167, 3570, 42997));
            boxes.Add((37750, 38478, 19503));
            boxes.Add((61098, 36847, 74065));
            boxes.Add((55927, 69273, 79177));
            boxes.Add((52096, 50475, 20228));
            boxes.Add((51104, 13698, 73215));
            boxes.Add((64788, 92575, 79276));
            boxes.Add((23588, 72561, 91221));
            boxes.Add((6918, 62790, 91061));
            boxes.Add((76130, 86439, 6198));
            boxes.Add((25069, 59707, 59270));
            boxes.Add((6327, 89715, 32194));
            boxes.Add((17621, 56772, 22499));
            boxes.Add((85040, 81665, 48821));
            boxes.Add((653, 65986, 41802));
            boxes.Add((18088, 59786, 97704));
            boxes.Add((97086, 51513, 20148));
            boxes.Add((3456, 9781, 53999));
            boxes.Add((10154, 15284, 20346));
            boxes.Add((37482, 8802, 13732));
            boxes.Add((44485, 27548, 74142));
            boxes.Add((91476, 72717, 38108));
            boxes.Add((63241, 8032, 59039));
            boxes.Add((42883, 97955, 28064));
            boxes.Add((65273, 43568, 72639));
            boxes.Add((13954, 95436, 63802));
            boxes.Add((5618, 83777, 34831));
            boxes.Add((71214, 91349, 91729));
            boxes.Add((3329, 77681, 38821));
            boxes.Add((39465, 44657, 91927));
            boxes.Add((72320, 67337, 33222));
            boxes.Add((95138, 738, 95105));
            boxes.Add((68395, 96453, 22507));
            boxes.Add((6307, 1296, 62986));
            boxes.Add((55664, 80909, 60201));
            boxes.Add((41232, 61059, 41558));
            boxes.Add((94070, 20857, 45046));
            boxes.Add((62162, 76613, 86379));
            boxes.Add((83875, 98849, 20336));
            boxes.Add((50350, 42245, 5410));
            boxes.Add((56450, 27268, 63961));
            boxes.Add((34163, 85020, 49621));
            boxes.Add((83366, 52220, 83864));
            boxes.Add((25506, 46046, 8801));
            boxes.Add((55097, 16045, 79507));
            boxes.Add((16007, 56636, 65189));
            boxes.Add((26026, 80019, 11164));
            boxes.Add((34925, 37001, 77219));
            boxes.Add((56913, 95897, 41091));
            boxes.Add((77708, 29976, 53159));
            boxes.Add((86539, 2745, 74811));
            boxes.Add((75192, 34713, 70890));
            boxes.Add((23021, 63373, 49491));
            boxes.Add((56444, 66645, 90290));
            boxes.Add((2250, 1791, 64116));
            boxes.Add((36234, 50305, 99493));
            boxes.Add((39017, 21060, 67471));
            boxes.Add((79740, 75048, 16475));
            boxes.Add((17593, 1582, 37614));
            boxes.Add((4678, 88143, 82368));
            boxes.Add((2438, 41645, 3270));
            boxes.Add((96957, 85260, 76030));
            boxes.Add((39613, 7332, 11306));
            boxes.Add((51657, 89105, 23007));
            boxes.Add((57640, 87327, 79406));
            boxes.Add((54267, 41195, 31924));
            boxes.Add((43088, 73192, 7063));
            boxes.Add((55453, 52926, 93692));
            boxes.Add((19291, 73206, 34191));
            boxes.Add((81121, 44750, 46178));
            boxes.Add((60629, 97254, 1588));
            boxes.Add((37243, 13470, 27965));
            boxes.Add((70349, 80666, 70417));
            boxes.Add((62045, 17964, 88834));
            boxes.Add((21827, 31559, 93705));
            boxes.Add((41570, 25497, 60596));
            boxes.Add((29948, 92236, 90378));
            boxes.Add((32278, 28436, 63562));
            boxes.Add((67574, 87757, 70962));
            boxes.Add((72568, 58031, 38113));
            boxes.Add((49394, 16259, 68022));
            boxes.Add((55799, 28006, 11389));
            boxes.Add((42694, 29393, 96893));
            boxes.Add((26294, 35535, 9905));
            boxes.Add((41048, 79679, 67504));
            boxes.Add((52185, 79353, 39673));
            boxes.Add((47757, 97639, 41754));
            boxes.Add((11179, 61492, 48668));
            boxes.Add((73044, 95568, 36567));
            boxes.Add((28025, 26777, 84346));
            boxes.Add((25326, 82602, 42681));
            boxes.Add((68360, 41873, 98533));
            boxes.Add((62431, 12188, 77068));
            boxes.Add((18560, 9852, 35988));
            boxes.Add((37227, 51623, 47419));
            boxes.Add((35757, 70342, 4627));
            boxes.Add((77604, 49585, 22009));
            boxes.Add((46333, 18134, 79365));
            boxes.Add((7853, 90095, 78991));
            boxes.Add((11885, 69611, 95040));
            boxes.Add((67143, 53344, 56899));
            boxes.Add((81975, 54979, 20018));
            boxes.Add((18768, 20643, 59579));
            boxes.Add((83674, 10363, 7158));
            boxes.Add((82836, 92110, 96075));
            boxes.Add((35224, 74482, 6883));
            boxes.Add((26542, 701, 69971));
            boxes.Add((68523, 93301, 24322));
            boxes.Add((84902, 159, 37223));
            boxes.Add((77815, 75009, 58090));
            boxes.Add((52121, 6337, 38732));
            boxes.Add((69199, 20692, 8058));
            boxes.Add((58965, 42364, 27641));
            boxes.Add((32089, 82642, 75273));
            boxes.Add((33539, 72711, 79473));
            boxes.Add((44815, 17667, 65453));
            boxes.Add((97037, 3362, 33435));
            boxes.Add((32260, 76548, 82524));
            boxes.Add((85432, 63487, 12804));
            boxes.Add((77946, 91091, 63830));
            boxes.Add((86642, 26467, 78241));
            boxes.Add((30942, 94351, 68449));
            boxes.Add((46506, 58046, 6593));
            boxes.Add((10322, 46197, 26732));
            boxes.Add((36815, 49257, 43684));
            boxes.Add((59123, 34251, 68369));
            boxes.Add((99496, 77819, 13067));
            boxes.Add((90412, 23013, 52730));
            boxes.Add((44330, 59682, 39575));
            boxes.Add((51504, 78095, 32788));
            boxes.Add((10779, 45118, 25424));
            boxes.Add((34046, 36587, 62915));
            boxes.Add((24778, 38060, 65526));
            boxes.Add((98006, 93184, 6149));
            boxes.Add((74689, 60613, 33378));
            boxes.Add((32921, 9455, 85370));
            boxes.Add((36117, 70634, 19));
            boxes.Add((60609, 98610, 40865));
            boxes.Add((80973, 50440, 30991));
            boxes.Add((22282, 15727, 81569));
            boxes.Add((62752, 9453, 80543));
            boxes.Add((19110, 98582, 67990));
            boxes.Add((56029, 32167, 31003));
            boxes.Add((96145, 46138, 69360));
            boxes.Add((57269, 30221, 36957));
            boxes.Add((15290, 96227, 71555));
            boxes.Add((86889, 65402, 71945));
            boxes.Add((45144, 25347, 43208));
            boxes.Add((54153, 35453, 23941));
            boxes.Add((39966, 88775, 53004));
            boxes.Add((89559, 2831, 47289));
            boxes.Add((17857, 36034, 48476));
            boxes.Add((92527, 63065, 95542));
            boxes.Add((70816, 36028, 55247));
            boxes.Add((84027, 24812, 22503));
            boxes.Add((50978, 90051, 54596));
            boxes.Add((58828, 90234, 93335));
            boxes.Add((39939, 35557, 12551));
            boxes.Add((69214, 27220, 9819));
            boxes.Add((41937, 6173, 1914));
            boxes.Add((10257, 54617, 65062));
            boxes.Add((31495, 31851, 33877));
            boxes.Add((76537, 38564, 44896));
            boxes.Add((51068, 95296, 52020));
            boxes.Add((95739, 46371, 70020));
            boxes.Add((72652, 13952, 30106));
            boxes.Add((12229, 92467, 12336));
            boxes.Add((81213, 47808, 33360));
            boxes.Add((12438, 87387, 55733));
            boxes.Add((49899, 3057, 87843));
            boxes.Add((59673, 47682, 35763));
            boxes.Add((38972, 61975, 47870));
            boxes.Add((36654, 2275, 44258));
            boxes.Add((20626, 13802, 37822));
            boxes.Add((4808, 75779, 25234));
            boxes.Add((92707, 36645, 44064));
            boxes.Add((42283, 78141, 49946));
            boxes.Add((83139, 60924, 89073));
            boxes.Add((12961, 70703, 31983));
            boxes.Add((2910, 83366, 94371));
            boxes.Add((66099, 97664, 59700));
            boxes.Add((51799, 26817, 11712));
            boxes.Add((34854, 42221, 24698));
            boxes.Add((66571, 41481, 65106));
            boxes.Add((25584, 38368, 55231));
            boxes.Add((66075, 59904, 72808));
            boxes.Add((55083, 52995, 78960));
            boxes.Add((30785, 21508, 85178));
            boxes.Add((24376, 47824, 99943));
            boxes.Add((49760, 23267, 19141));
            boxes.Add((44509, 65663, 1843));
            boxes.Add((55595, 2688, 73788));
            boxes.Add((59472, 96732, 75870));
            boxes.Add((35631, 75025, 51086));
            boxes.Add((20371, 25799, 29333));
            boxes.Add((1290, 23209, 32511));
            boxes.Add((98619, 72455, 95907));
            boxes.Add((27342, 49336, 37942));
            boxes.Add((19953, 86159, 18234));
            boxes.Add((24317, 64127, 25401));
            boxes.Add((76176, 97522, 66206));
            boxes.Add((87408, 15932, 60393));
            boxes.Add((92066, 36746, 92720));
            boxes.Add((8505, 18640, 2218));
            boxes.Add((9424, 60441, 5715));
            boxes.Add((54386, 24206, 97697));
            boxes.Add((30514, 59832, 60369));
            boxes.Add((55483, 21270, 16926));
            boxes.Add((80239, 99457, 87435));
            boxes.Add((95904, 5982, 94915));
            boxes.Add((58389, 72204, 9695));
            boxes.Add((28084, 95863, 26897));
            boxes.Add((77807, 17120, 82151));
            boxes.Add((63572, 75591, 60257));
            boxes.Add((33447, 55674, 86898));
            boxes.Add((6378, 80313, 42236));
            boxes.Add((92703, 9588, 62931));
            boxes.Add((59802, 35546, 93238));
            boxes.Add((32942, 62347, 21898));
            boxes.Add((13736, 44694, 93901));
            boxes.Add((46858, 82114, 96076));
            boxes.Add((56876, 45275, 39479));
            boxes.Add((76797, 64900, 21618));
            boxes.Add((23410, 8505, 46640));
            boxes.Add((18089, 19211, 86035));
            boxes.Add((62116, 2805, 2631));
            boxes.Add((9719, 91254, 85541));
            boxes.Add((69517, 22668, 94419));
            boxes.Add((88506, 17049, 84219));
            boxes.Add((43510, 41726, 48502));
            boxes.Add((94039, 68338, 49091));
            boxes.Add((80708, 61253, 72392));
            boxes.Add((5331, 41455, 19079));
            boxes.Add((35150, 33012, 73212));
            boxes.Add((50066, 86992, 75820));
            boxes.Add((24974, 31306, 77319));
            boxes.Add((4769, 51367, 68064));
            boxes.Add((49306, 19437, 28482));
            boxes.Add((32736, 36455, 9964));
            boxes.Add((90327, 1898, 39888));
            boxes.Add((67116, 35035, 15275));
            boxes.Add((72365, 18775, 12832));
            boxes.Add((7951, 39228, 39345));
            boxes.Add((554, 71783, 99542));
            boxes.Add((33584, 72575, 28368));
            boxes.Add((27291, 74009, 73416));
            boxes.Add((77676, 94780, 59151));
            boxes.Add((80967, 71494, 83831));
            boxes.Add((22443, 64816, 35723));
            boxes.Add((7583, 24419, 5262));
            boxes.Add((81494, 92878, 96183));
            boxes.Add((54391, 41682, 33677));
            boxes.Add((13877, 20443, 903));
            boxes.Add((16236, 92333, 15288));
            boxes.Add((46450, 31156, 13407));
            boxes.Add((67278, 12011, 2885));
            boxes.Add((98978, 8225, 61603));
            boxes.Add((68639, 608, 54596));
            boxes.Add((28223, 43844, 83761));
            boxes.Add((25004, 79533, 95348));
            boxes.Add((12841, 45100, 94916));
            boxes.Add((65346, 98504, 58564));
            boxes.Add((56795, 14083, 7562));
            boxes.Add((90673, 38829, 39895));
            boxes.Add((19833, 89002, 37795));
            boxes.Add((95604, 57181, 98524));
            boxes.Add((1471, 96388, 39134));
            boxes.Add((12840, 44284, 7018));
            boxes.Add((59422, 20922, 50135));
            boxes.Add((51014, 68247, 96229));
            boxes.Add((67465, 51968, 76698));
            boxes.Add((95110, 31795, 16504));
            boxes.Add((61852, 54426, 44358));
            boxes.Add((39747, 77766, 86130));
            boxes.Add((69551, 51802, 68220));
            boxes.Add((57173, 34611, 72865));
            boxes.Add((64099, 90099, 87603));
            boxes.Add((76580, 41630, 48154));
            boxes.Add((80015, 61179, 59967));
            boxes.Add((80244, 18991, 20298));
            boxes.Add((88355, 99025, 82835));
            boxes.Add((78338, 57561, 79359));
            boxes.Add((8356, 86460, 18595));
            boxes.Add((75881, 56831, 56144));
            boxes.Add((68311, 83507, 80477));
            boxes.Add((62369, 31981, 77474));
            boxes.Add((6882, 27533, 60883));
            boxes.Add((73843, 91163, 10147));
            boxes.Add((87282, 81904, 87702));
            boxes.Add((83699, 76158, 44678));
            boxes.Add((69157, 6855, 40840));
            boxes.Add((56412, 712, 45425));
            boxes.Add((83002, 9336, 14670));
            boxes.Add((20460, 89793, 37178));
            boxes.Add((94241, 38343, 61638));
            boxes.Add((14866, 54350, 62844));
            boxes.Add((23240, 57416, 31743));
            boxes.Add((52783, 83593, 71273));
            boxes.Add((15844, 166, 47980));
            boxes.Add((2377, 57186, 4725));
            boxes.Add((36472, 69928, 48164));
            boxes.Add((61381, 1027, 67583));
            boxes.Add((65798, 8529, 39449));
            boxes.Add((60146, 55696, 86599));
            boxes.Add((5861, 46744, 48927));
            boxes.Add((80674, 53816, 43191));
            boxes.Add((7043, 37954, 45129));
            boxes.Add((1569, 37330, 99907));
            boxes.Add((29479, 94703, 75885));
            boxes.Add((67051, 44733, 32892));
            boxes.Add((32987, 2598, 62124));
            boxes.Add((7744, 76985, 74106));
            boxes.Add((24343, 25487, 85624));
            boxes.Add((4646, 63894, 27524));
            boxes.Add((2692, 70688, 11970));
            boxes.Add((19186, 12216, 32310));
            boxes.Add((15541, 21391, 4677));
            boxes.Add((22049, 30447, 83829));
            boxes.Add((7380, 43490, 7365));
            boxes.Add((13520, 8396, 63309));
            boxes.Add((52117, 77513, 93386));
            boxes.Add((87657, 37846, 56044));
            boxes.Add((92832, 22258, 84937));
            boxes.Add((67947, 49, 3017));
            boxes.Add((84372, 97345, 90307));
            boxes.Add((38059, 19588, 67220));
            boxes.Add((3566, 86928, 84429));
            boxes.Add((18349, 60680, 84034));
            boxes.Add((83534, 50982, 22537));
            boxes.Add((65853, 5579, 88767));
            boxes.Add((71682, 23991, 19619));
            boxes.Add((46853, 83536, 89731));
            boxes.Add((31236, 75134, 46614));
            boxes.Add((4737, 93284, 8264));
            boxes.Add((25245, 58674, 40085));
            boxes.Add((46457, 8821, 35237));
            boxes.Add((21658, 38629, 33837));
            boxes.Add((17926, 71773, 60869));
            boxes.Add((19721, 12483, 83651));
            boxes.Add((90324, 78397, 10664));
            boxes.Add((46918, 63331, 55894));
            boxes.Add((71477, 51691, 14292));
            boxes.Add((47530, 88013, 25033));
            boxes.Add((25966, 66029, 79899));
            boxes.Add((92389, 85236, 92930));
            boxes.Add((50766, 64771, 54258));
            boxes.Add((3600, 7785, 86490));
            boxes.Add((66972, 50394, 97370));
            boxes.Add((64693, 10884, 34559));
            boxes.Add((57757, 19383, 74853));
            boxes.Add((4411, 67058, 5104));
            boxes.Add((70378, 69057, 25411));
            boxes.Add((34414, 77670, 78830));
            boxes.Add((16194, 86185, 16445));
            boxes.Add((82152, 25100, 72189));
            boxes.Add((14261, 63140, 47489));
            boxes.Add((84299, 53077, 19956));
            boxes.Add((94681, 16447, 1090));
            boxes.Add((696, 76982, 41260));
            boxes.Add((35681, 68007, 72834));
            boxes.Add((12222, 15818, 32555));
            boxes.Add((80960, 82141, 26340));
            boxes.Add((51809, 60266, 46057));
            boxes.Add((29710, 29932, 87762));
            boxes.Add((478, 24029, 21658));
            boxes.Add((2761, 38602, 21527));
            boxes.Add((31038, 19751, 27053));
            boxes.Add((96832, 28512, 11287));
            boxes.Add((35170, 6577, 18799));
            boxes.Add((63642, 40466, 8750));
            boxes.Add((42799, 10716, 85271));
            boxes.Add((9682, 82322, 77985));
            boxes.Add((19087, 80395, 18980));
            boxes.Add((55551, 9273, 4040));
            boxes.Add((82788, 97494, 91368));
            boxes.Add((43910, 61996, 24653));
            boxes.Add((69872, 24867, 60011));
            boxes.Add((45346, 19961, 25310));
            boxes.Add((6360, 29908, 93850));
            boxes.Add((90460, 11072, 1654));
            boxes.Add((4663, 16803, 2121));
            boxes.Add((81026, 62305, 70892));
            boxes.Add((86479, 28320, 25602));
            boxes.Add((243, 11476, 31128));
            boxes.Add((32218, 59128, 85389));
            boxes.Add((28309, 81727, 5251));
        }
    }
}
