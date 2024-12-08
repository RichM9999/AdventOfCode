//https://adventofcode.com/2024/day/8
namespace AdventOfCode
{
    class Day8
    {
        const int mapSize = 50;

        public void Run()
        {
            var map = new char[mapSize, mapSize];

            SetupMap(ref map);

            var start = DateTime.Now;

            var types = new Dictionary<char, List<(int x, int y)>>();

            // Find antenna types and locations
            for (var y = 0; y < mapSize; y++)
            {
                for (var x = 0; x < mapSize; x++)
                {
                    if (map[y, x] != '.')
                    {
                        if (!types.ContainsKey(map[y,x]))
                        {
                            types.Add(map[y, x], new List<(int x, int y)> { (x, y) });
                        }
                        else
                        {
                            types[map[y, x]].Add((x, y));
                        }
                    }
                }
            }

            var antiNodes = new List<(int x, int y)>();

            // Enumerate each type
            foreach (var type in types.Keys)
            {
                var locations = types[type];
                
                // Enumerate each pair of locations
                for (var i = 0; i < locations.Count - 1; i++)
                {
                    for (var j = i + 1; j < locations.Count; j++)
                    {
                        var ix = locations[i].x;
                        var iy = locations[i].y;
                        var jx = locations[j].x;
                        var jy = locations[j].y;

                        var anti1 = (x: 2 * ix - jx, y: 2 * iy - jy);
                        var anti2 = (x: 2 * jx - ix, y: 2 * jy - iy);

                        if (anti1.x >=0 && anti1.x < mapSize
                            && anti1.y >= 0 && anti1.y < mapSize)
                        {
                            if (!antiNodes.Contains(anti1))
                            {
                                antiNodes.Add(anti1);   
                            }
                        }

                        if (anti2.x >= 0 && anti2.x < mapSize
                            && anti2.y >= 0 && anti2.y < mapSize)
                        {
                            if (!antiNodes.Contains(anti2))
                            {
                                antiNodes.Add(anti2);
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Anti-nodes: {antiNodes.Count}");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            start = DateTime.Now;

            antiNodes = new List<(int x, int y)>();

            // Enumerate each type
            foreach (var type in types.Keys)
            {
                var locations = types[type];

                // Enumerate each pair of locations
                for (var i = 0; i < locations.Count - 1; i++)
                {
                    for (var j = i + 1; j < locations.Count; j++)
                    {
                        // Add possible anti-nodes at each node
                        if (!antiNodes.Contains((locations[i].x, locations[i].y)))
                        {
                            antiNodes.Add((locations[i].x, locations[i].y));
                        }

                        if (!antiNodes.Contains((locations[j].x, locations[j].y)))
                        {
                            antiNodes.Add((locations[j].x, locations[j].y));
                        }

                        var ix = locations[i].x;
                        var iy = locations[i].y;
                        var jx = locations[j].x;
                        var jy = locations[j].y;

                        var dx = ix - jx;
                        var dy = iy - jy;

                        var anti1 = (x: 2 * ix - jx, y: 2 * iy - jy);
                        var anti2 = (x : 2 * jx - ix, y : 2 * jy - iy);

                        var foundValid = false;

                        do
                        {
                            foundValid = false;

                            if (anti1.x >= 0 && anti1.x < mapSize
                                && anti1.y >= 0 && anti1.y < mapSize)
                            {
                                foundValid = true;
                                if (!antiNodes.Contains(anti1))
                                {
                                    antiNodes.Add(anti1);
                                }
                            }

                            if (anti2.x >= 0 && anti2.x < mapSize
                                && anti2.y >= 0 && anti2.y < mapSize)
                            {
                                foundValid = true;
                                if (!antiNodes.Contains(anti2))
                                {
                                    antiNodes.Add(anti2);
                                }
                            }

                            anti1.x += dx;
                            anti1.y += dy;

                            anti2.x -= dx;
                            anti2.y -= dy;
                        } while (foundValid);
                    }
                }
            }

            Console.WriteLine($"Anti-nodes repeating: {antiNodes.Count}");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        void AddMapRow(int row, ref char[,] map, string data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                var space = data[i];
                map[row, i] = space;
            }
        }

        void SetupMap(ref char[,] map)
        {
            AddMapRow(0, ref map, "..F..........L............5.......................");
            AddMapRow(1, ref map, "............................L.U...................");
            AddMapRow(2, ref map, "..................................................");
            AddMapRow(3, ref map, ".............z.L.........5.....4........8....1.P..");
            AddMapRow(4, ref map, "...F................D..4.8.............P......J...");
            AddMapRow(5, ref map, "......f................8....z........U..J.........");
            AddMapRow(6, ref map, ".......D..f........B..o.........m..........JX.....");
            AddMapRow(7, ref map, "......o...5........F..........m.......6....X......");
            AddMapRow(8, ref map, "....s........f...n.....54.U....E................3.");
            AddMapRow(9, ref map, "....F.......l.......k..............6.3n...........");
            AddMapRow(10, ref map, "L..........z....7..U............E...k.P..3b.......");
            AddMapRow(11, ref map, "..s.......D..........h...k.....G........y..m......");
            AddMapRow(12, ref map, "d..............o.........X............8...n.......");
            AddMapRow(13, ref map, "...........o.......D.......J......................");
            AddMapRow(14, ref map, "....................z.....1.9....G6..Y.b....y.....");
            AddMapRow(15, ref map, ".d................4.........EN..G.9.b.............");
            AddMapRow(16, ref map, ".......................7..........................");
            AddMapRow(17, ref map, "..d................l.........pc..n................");
            AddMapRow(18, ref map, "..............l........1Nm..........G..9..........");
            AddMapRow(19, ref map, ".f.........s...7........1........E........X....y..");
            AddMapRow(20, ref map, ".............d...................6......v.........");
            AddMapRow(21, ref map, "........................h.............B...........");
            AddMapRow(22, ref map, ".......l.......................h........B.....p..y");
            AddMapRow(23, ref map, "........w......A................................M.");
            AddMapRow(24, ref map, ".....s.................O...........p.......2......");
            AddMapRow(25, ref map, "...............9.........................B.b......");
            AddMapRow(26, ref map, "......................w..0..............H.........");
            AddMapRow(27, ref map, ".....................w7.j..O....................e.");
            AddMapRow(28, ref map, ".A......Z...................K...h...M.............");
            AddMapRow(29, ref map, ".................S....KZ..........................");
            AddMapRow(30, ref map, ".................V..............x.................");
            AddMapRow(31, ref map, "......Z...............................N...........");
            AddMapRow(32, ref map, ".......................a..........................");
            AddMapRow(33, ref map, "....A..........................K.................M");
            AddMapRow(34, ref map, ".......Z..................ON.KT.........c.........");
            AddMapRow(35, ref map, "...........................YO....t.......x........");
            AddMapRow(36, ref map, "..............g........w.T.............k...c......");
            AddMapRow(37, ref map, "..........................v.......................");
            AddMapRow(38, ref map, "....S..................................u..........");
            AddMapRow(39, ref map, "..........0............v...............c...e..C.p.");
            AddMapRow(40, ref map, ".......S............V.j........v.......x..........");
            AddMapRow(41, ref map, "......S..0W.......HT....a.........................");
            AddMapRow(42, ref map, "A..............H...W..a......C....................");
            AddMapRow(43, ref map, "................T.2.....V......H..t...u........C..");
            AddMapRow(44, ref map, ".................g.j....2.........u..t...e......C.");
            AddMapRow(45, ref map, ".........W...........g.......................u....");
            AddMapRow(46, ref map, "........W...0.................Y.........e.tM......");
            AddMapRow(47, ref map, "................g..a.j............................");
            AddMapRow(48, ref map, "..................................................");
            AddMapRow(49, ref map, ".................2........Y...........x...........");
        }
    }
}
