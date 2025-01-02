//https://adventofcode.com/2023/day/14
namespace AdventOfCode.Year2023
{
    class Day14
    {
        const int mapSize = 100;
        char[,] map;
        
        Dictionary<long, long> loads;
        Dictionary<long, int> repeats;

        const int repeatSequenceThreshold = 5;

        public Day14()
        {
            map = new char[mapSize, mapSize];
            loads = [];
            repeats = [];
        }

        public void Run()
        {
            var start = DateTime.Now;

            GetData();

            TiltNorth();

            Console.WriteLine($"Total load of all stones: {CalculateLoad()}");
            // 113078
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            start = DateTime.Now;

            GetData();
            
            for (long cycle = 1; cycle <= 1000000000; cycle++)
            {
                TiltNorth();
                TiltWest();
                TiltSouth();
                TiltEast();

                var load = CalculateLoad();
                if (loads.ContainsValue(load))
                {
                    // Find repeating sequence at least repeatSequenceThreshold times
                    if (repeats.TryGetValue(load, out int repeatCount))
                    {
                        repeats[load] = repeatCount + 1;

                        if (repeatCount == repeatSequenceThreshold)
                        {
                            break;
                        }
                    }
                    else
                    {
                        repeats.Add(load, 1);
                    }
                }
                loads.Add(cycle, load);
            }

            // Using a repeating value matching threshold, find repeating sequence and last/prior cycle when it started
            var repeatValue = repeats.First(r => r.Value == repeatSequenceThreshold).Key;
            var lastRepeatCycle = loads.Last(l => l.Value == repeatValue).Key;
            var priorRepeatCycle = loads.Last(l => l.Value == repeatValue && l.Key < lastRepeatCycle).Key;
            var repeatSequence = loads.Where(l => l.Key >= priorRepeatCycle && l.Key < lastRepeatCycle).Select(l => l.Value).ToList();
            // Determine which index in sequence (load) occurs at 1,000,000,000 cycles
            var repeatIndex = (int)((1000000000 - lastRepeatCycle) % repeatSequence.Count);

            Console.WriteLine($"Total load of all stones after 1,000,000,000 tilt cycles: {repeatSequence[repeatIndex]}");
            // 94255
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        long CalculateLoad()
        {
            long load = 0;

            for (var y = 0; y < mapSize; y++)
            {
                for (var x = 0; x < mapSize; x++)
                {
                    if (map[x, y] == 'O')
                    {
                        load += mapSize - y;
                    }
                }
            }

            return load;
        }

        void TiltNorth()
        {
            var moved = false;

            do
            {
                moved = false;

                for (var y = 1; y < mapSize; y++)
                {
                    for (var x = 0; x < mapSize; x++)
                    {
                        if (map[x, y] == 'O' && map[x, y - 1] == '.')
                        {
                            map[x, y - 1] = 'O';
                            map[x, y] = '.';
                            moved = true;
                        }
                    }
                }
            } while (moved);
        }

        void TiltSouth()
        {
            var moved = false;

            do
            {
                moved = false;

                for (var y = mapSize - 1; y > 0; y--)
                {
                    for (var x = 0; x < mapSize; x++)
                    {
                        if (map[x, y] == '.' && map[x, y - 1] == 'O')
                        {
                            map[x, y - 1] = '.';
                            map[x, y] = 'O';
                            moved = true;
                        }
                    }
                }
            } while (moved);
        }

        void TiltWest()
        {
            var moved = false;

            do
            {
                moved = false;

                for (var x = 1; x < mapSize; x++)
                {
                    for (var y = 0; y < mapSize; y++)
                    {
                        if (map[x, y] == 'O' && map[x - 1, y] == '.')
                        {
                            map[x - 1, y] = 'O';
                            map[x, y] = '.';
                            moved = true;
                        }
                    }
                }
            } while (moved);
        }

        void TiltEast()
        {
            var moved = false;

            do
            {
                moved = false;

                for (var x = mapSize - 1; x > 0; x--)
                {
                    for (var y = 0; y < mapSize; y++)
                    {
                        if (map[x, y] == '.' && map[x - 1, y] == 'O')
                        {
                            map[x - 1, y] = '.';
                            map[x, y] = 'O';
                            moved = true;
                        }
                    }
                }
            } while (moved);
        }
        void AddRow(int row, string line)
        {
            for (var x = 0; x < mapSize; x++) 
            {
                map[x, row] = line[x];
            }
        }

        void GetData()
        {
            var row = 0;

            AddRow(row++, ".OO..##.###..#O.O..O#.OOO.....#..O##O#.OO...O..##.O.O.......##O...O...O#O.O.#..O..O.#O..OO...#......");
            AddRow(row++, "....OO..#.#...O#.O..#O..#OO...OO..O......O#........O...#..OO..#.#.O#..OO...O####.O...O.O.#.O..O.O..O");
            AddRow(row++, ".O#.......O.OO#OO##O#O..O.O....#.OO..O...O.OO........O..#...O.O......##....O.....##.O.#.#.O..O......");
            AddRow(row++, "..##.#.....#..#.O#O...O..O...OOO.##O.O##...O#.OO.#....OOO.OO.#O.O.O#...OO.OO.....O#..#...O...OO#...O");
            AddRow(row++, "OO..#..OO#..#..O..#O..O.O##O#OOO.#...OO.#....#..#....O.####O.##..O......#O..OOO#O.#...#.O...OO...OO#");
            AddRow(row++, "#.O.#O.#.#..O.#.............#.#.OO......#.O.O##O###........#.....#.....#.....O.O.OO.O....O.......O.#");
            AddRow(row++, "O#...#...........#..OO...O..#.....#...O.#.......O#.....##O#.OO##.......#..O......#..O..O.#O.#.#..O.#");
            AddRow(row++, "OO...O##..O.#.......O#.........OO#..O..#O..#OO.O........#.O.O.O....OO#...#.#.#O...##O.#OO.#.....#..O");
            AddRow(row++, "#OO...O...O...O....O...............O.#O..OO##O##O.....#.##...OO.O.#.##.##.O..#..#.O..#.#..O.........");
            AddRow(row++, ".O..O.#...##..#.#OO....#.O#...OO#.......O..OO.#...OO##..O..O...O.O...O.#.O#.....O..OO.O.#.OO..#.##..");
            AddRow(row++, "...OO.O.O.O#....#...O...#....OO.#O.#O..O.OO..OOO.O....#O.#...#.O#.O............#......O.O....#OO##.#");
            AddRow(row++, "...#O......#O#..O....#.##..O.O.O#..#O.#O...O...#..#O..##.O#..O#....O##.O.#OOO.#........O##.OO.O..OO#");
            AddRow(row++, "#OOO....#O...O.O....O.......O...O..#....##.#..#.O#O#.O#....#.#.#O#....#.#.....##.#O..OOO....#.O.....");
            AddRow(row++, "#OO.O...#O.O....O..#..O.#....#....O#..O...##..#..OO#.....#O.O#O..O....#OO....##OO....#...OOOO.OO###.");
            AddRow(row++, "O..#..O#.O.#..OO.#.OO...O..O......#........#O......O.OO.#....#..O#O.......OO##O..##OOOO.#.#OOO.....O");
            AddRow(row++, "...#OO.#OO.O.##.O#.OO#...#..#.O##O#...#.#O...#.....#..O..O....O.OO.O.#..O.#.O...#.O#..O....O.#...O#.");
            AddRow(row++, "#....O.....O.O...#..O....#..O..O.OO...#....O..O...#.....#...#....O.#.OO...OO#...O....#O.O..O.OO.O..O");
            AddRow(row++, "..O#.....#O#....#.....OOO.#.......OO.O..#.#....#O......O.#.O.#..O...#.O...O..O.##O.O#O..#.O......#.#");
            AddRow(row++, "....#O#.#..O#.OO.#.#.O.#......#OO...O#OO.........OOO..##...#..##O#..OOO...O..O..O##O...#..#.O.O..#..");
            AddRow(row++, "..O#.O.......OOO..##..#...#....#.#..#O.OO..O..##..OO#.O...O..#OO...##O.#O..OO.....#O..#.....#O.....#");
            AddRow(row++, "...OO.O.#.O.O..##O#...OO...##.O..OOO##.O.O.#..#.OO..O...O.#....O.O.....O##.#O.O.O#.#.......OO#......");
            AddRow(row++, "O...#.O...O...#..OO...O..#..#........O...#...#......O#.#.....O.#.........#.#......O#O.O.........#...");
            AddRow(row++, ".......O#...#...........O.#O#.O#.#.O.O..O..OO.#......##.OO..#......OO#....#O#OO.O.#..O#..O.OO..O..##");
            AddRow(row++, ".O.....O.#.........O...O...O...O...OOOO..O....OO....#.#....O#.#..#.OO.OO....#.......#...#OO.....O.OO");
            AddRow(row++, "O....OO....O..O.OO.###..O##.OO.#......O.O...##...#O...O..O...##.O.O...O....O.#OO.....O.....O.O.OO.OO");
            AddRow(row++, "#O......OO....##..O..###.O....#.#.#.OO.............O.O.OOOO#.O.O..#O...O#..O......O.O.O.#.#.......O.");
            AddRow(row++, "O#O.....#...#..O#.#....#..###..O..O...#....O....##.....#...O#.#.O.O......O..OOOOO....OO..#...#.OO#..");
            AddRow(row++, "..O........OO....##...#O.....#.O.....O#.O..#.O.#.O......O#...OOO....O...#..OO....O..#...O.O..##.....");
            AddRow(row++, "O...O..O.O....O.OO.O.OO..OO...#O...O.#.O..#O...#...#...O..##...#...OO#.....##..O#O.O....#.....OO#..O");
            AddRow(row++, "...O#O..O..##.OO.......#.#O...O..#.#...#.O.#O##OOO.OOO...#.....O....##.....OOO.....O..O.O#.O....O...");
            AddRow(row++, "O#..#O.O....#.........O#.OO#...OO......#......O.#...............O##OO.....O.....O.O..##..O#....#.OO.");
            AddRow(row++, "#O..O.OO....##O......O.O...O#O..#.O..........O....#........O#.#..O..O............#..#...OO#.###..#.#");
            AddRow(row++, "...#...#.#O....O.O.....OO.O.O...O.#.#.O...OO..O#....O#O......O.O..##.#.O.....#..O.O.#.#O...O.#......");
            AddRow(row++, ".O...O#.O.......O.#....O.#...#.#..#O.....#..#..#.#O#..#.......O..#.#..O.OO.#OO.OOO..O....#.....OO#.O");
            AddRow(row++, "O#.#..#.O..O..#O.......O.#..#....#.O..#...#......O...O##....O..#...O....#O..#.....OO.O.O#..O.#..#O..");
            AddRow(row++, "#.#.....O...#O.#..O...#O..##..#O..#O...O.#OO...#....OO#..O.#O#O........OOO...O##..#..OO.O.O.O.O.....");
            AddRow(row++, "..OO...#..OOO.O#..#...O..O.......O.#.#.O#.##OO....#..O....#..#.O.#O...O.O#O........O#OOOO#.#..O#.#..");
            AddRow(row++, ".O....#.O#OOO.....O.#O............O...O#...O...O..O.#..O.O.....O#.O....O......O...O.O..OOO..O.##.O..");
            AddRow(row++, "#...#........#..........###.O....OO.#.....#OOOO.OO#.OO...##...OO...O#.......O...#.#..#O..#.....#O#O.");
            AddRow(row++, "O.O.#...O.#.....OO...O.O#......#........#..O...O..O.#..OO#.....#...O.##O#....O#..O#...##.O.O.#.#....");
            AddRow(row++, "............OO.....O........##O.O...#..#.O.#...OO..#.O.#O.............O..#O.....#...O.#....O..O#..#.");
            AddRow(row++, ".#..#.O.#......O..O##O.O.#..O..#O.O..O.O........O.......#.#.#O.#..O...O...O....O.O#...#..OO..O....OO");
            AddRow(row++, "#..#..O....OO#.#.O.#......O...O.O#O...O#...O...#...#O.#....#..#O...O...........#.#.#..O##..#........");
            AddRow(row++, "..O......O.#.OO.#O...O..#O...OO.#.....##...#.#....O....OO.#O.O.#....OO..#....#.#...#.O.....OO#..O...");
            AddRow(row++, "#..........#...O.O....##....OO..O..O..#...#..###....##..O#..#.........#.....O#...#.#.......#.##.##O.");
            AddRow(row++, "###.O.#..#.O.O.O..O..O...O...#O...O##.#..#O..O...#....O.....#..##......OO.#O...O#..O.O.O......#...OO");
            AddRow(row++, "....O#........O.O#.O#O#...O.......#..O.#..#........O.....O#..O..O.OOO....OO.....#....O#.#..O.....##.");
            AddRow(row++, "#.#O...#.O#O..#.#OOOO#.......#....O...O#.##.O....#..O.......O.##.#.OO...O...#O..O..#O.#......O.O.##.");
            AddRow(row++, ".#O...OO#......O...#..##............O...##...O.#O....#..OO......O.O##OO..OO....##O.##..O............");
            AddRow(row++, "O.O..OO.O.#.#....#..O.O.O.OOO...OO..#....O....O.#.#....#.O#.#.....O#O...O.OO..O.#O.#..OO#.O#........");
            AddRow(row++, "#..#.O.##.##...#OO..O...OOOO#O##..#...#.##....#O.#O#.O.O#.#.O#...#.OO.......O........O..#.#.OOO#....");
            AddRow(row++, "##....O...O#....O...O.#OO.O..O.....O#O#.#...OO.........#.......O.#.#..O..#O...#..O.#...#.##...O..O.O");
            AddRow(row++, ".........O...O.O...#..O.O..OO.#...O.#.O........O.O.O....O...O..O..........O....O......O..#O.O..#O...");
            AddRow(row++, ".......#.#....O#OOO.O.O..O.##...O..###.......O#.##...O.O....#.OOO..O...#.#.......#O.OO...O...OO.O...");
            AddRow(row++, ".O...O...O....#.O.#...#..##O....#..O....O#..O.#.O..#...........#..O..##OO.......##..#.....O#...O#..#");
            AddRow(row++, "...O....O....#....O..####.#O#.....#O.O..#..#O.OO...#...O.##.#.#....#...O...O..#.....OO.O....#..O..O.");
            AddRow(row++, ".OO#.#...#....O.#O#.##....OO............O.....#.OO#O.#O.#..#...#.O....O.O..#..##.......O..O.#..##O.O");
            AddRow(row++, "O...#.........#.O....O..O.....#.......#.OO...OO.#O#.#...OOO.O.O#.O.#...#O......#....O.......#.O.#.O.");
            AddRow(row++, ".....O.............O.#..#.#..#.#...#..OOO....#.OO.O...O..#.##O.#O...O.O.OO......#....OO..O..........");
            AddRow(row++, "O....#....#.#....#.........#...O.O#....OO.#..#O..#....OO...#....O...O#...O.......OOO...#......#...#O");
            AddRow(row++, "..O..##O.#O.OOO..#..O.#....#.OOOO..O.O.....##O.....O.#.O.###..##....O..OOOO..#.#OO.O.OO.#O#..#...#.O");
            AddRow(row++, "O#...O......#..#.....#..##.#OO#O#.....O...##....OO...O##...O....O.#...#O..#.O#..#O.O..#...#...OOO..O");
            AddRow(row++, "O...##.O..O..O#....#..O........O....O#...O..O......O...OO...OO.#OO.O..#....O#O....O..#..#..OO.OO....");
            AddRow(row++, "O..#.#O....O..O.O..O#...O.O...O..#.#.#..##.#O......#...OOO.#O...#..O.....O###O.....#.......O#.O.O...");
            AddRow(row++, "OOO#..#.#...O...O..O.O..O#O.....#O.#..O....O...O.#..O.#...O#.......#.....#O.#.#..O...##..#.....O...O");
            AddRow(row++, "O#...O#....O.#.#......O.....O.##.......#.O.......#.#.#...#..#O....##O......O..O.#.#..O..#..O.O#O.O..");
            AddRow(row++, "O.O...O##.O#.....O......#...O.#...O.O..#........O..O..O#.O..OO#O.OO.........O...O..#.O......O...O#.O");
            AddRow(row++, "..#......OO.#....O...O..O..##...#O#O#O..O..O......O..O.#O#..O......O..........OO..OOO#..#.OO....O...");
            AddRow(row++, ".......##......#.O..#....#.O#O..O#...O#....O.OOOO..#.O.O..O..O..O.........O...O#...O..#O..#.#..O...O");
            AddRow(row++, ".O#..OOOO.O......#O......OOO#.#.O.#O...##...O.O...##.O#.O.#.......#.......##...#O.O...#.#..O..##O..O");
            AddRow(row++, "....OO......#.#.....O.#.OOO.O#.#...O.##.O..OO....#OO..O..O.O.O#..OO....O.O..#.O.##..O...OO..OO..OO#.");
            AddRow(row++, "........#O.....#....O#.#OOO.O...........OO....#.#.....OO#..#OO..#..#.....O#O..OO..OO..O......#..O.O.");
            AddRow(row++, "O#O....#.#......O#...#.##O..OO.O.....#O..#..O..O......O##..#OO...OOOO.....O#....O..O.....OOO...#O..O");
            AddRow(row++, "...O..#..O......OO.#.....#.O...O.O.#.O..OO..#....#...#....O.#O.O...O.O......O......O..#...#O###.OO..");
            AddRow(row++, "#O###.....#....#.#O..#...#O..OO....#.O...#.##OO.#.#.#.....#....O.###...#....#.#...O.......O...O..#.O");
            AddRow(row++, "..#..O.O..#.........O#..OOOO###..O##.#........O...#.#O#..#...O...O#....O..#.#...O.O..OO.....O.......");
            AddRow(row++, "#...O..#.......O........O....#.O......#...##.##......O.....O.##.O..O.....##...#OOO..O...O##.O.O.....");
            AddRow(row++, "......#.OO....###.......#.###..OO#.OOO#O#.OOO#..O....OO........#...O...O#.OO.#....O#.......#..#..#..");
            AddRow(row++, "#..#OOO....#.#.O.O..........#O....O#...O..O..#...O....O#..##.O..O.#O...#.#.#..O..O.O.#O..O..O...#.#.");
            AddRow(row++, ".#...O.O##.....#..O.....OOO.#.....#OOO#.#..O.....O.#.O.O.#.#.O.O#.........O....#...#O.O##.##..O.O#.O");
            AddRow(row++, "OOO...#O.OO....#.OOO..O....O.OO.O.#.O.#.#...#....OO........O..#..OO....#O....#.#O##.OO#O#.##OO.O.O.#");
            AddRow(row++, "...O#..OO.....O#...#O........O....O#O....OO...#....O###..##.O..#..#.#..O..#O.OO..O........##O#.#..OO");
            AddRow(row++, ".O...#..#....O.##...#.........##.#..O.O.....#.##..O.#O.#..O#...#....#.#.........##.O...O...OO#..O..#");
            AddRow(row++, "O....##.O...O........O#..#...OO#O..OO..#.........##......O.......OO..O...O.O..#..........#.O..#...O.");
            AddRow(row++, "###.#.........#OO....##...O#...O....O........#...#.O.#O.#.....##..O..#O.##......#.#.O.......O...O...");
            AddRow(row++, "..#.OOOO..O......O#O.O.#.O.#..#.O...O..O.O..##O.O.......#...O#..#..O.#.#.O#.##...O......#.#...#.....");
            AddRow(row++, "O.#.OOO.#......O....O..O...O#........#....#.......#O..O.OO..#.O#.OO..#.OOO..#O.....#OOOO..#.........");
            AddRow(row++, "##.O...####OO#...O#.OO...O...O.......O...OO..####...OOOO..O##O....OOO....#..O.O........O....#.#.#O..");
            AddRow(row++, "O.O....O...O#...O.#....O.....OO#O#OO..O##.O..O...O.#.....#O...O..#O...O#.....O##...O.OO...#.........");
            AddRow(row++, "....#OO..OO..#O#......O#.O..#O..#..OO..#....#.....#.....O.O..O#....O.O..O.#..O#O###O....O...OO...#.#");
            AddRow(row++, "#..OO.##O..OO.O..O...#....O.....##O..#............##.O.#...O.#O..#O..#...#...O.O....#O.....O.....O..");
            AddRow(row++, "..O#..#...##.#.#.O#.OOO.O#.#....OO.#....#...#...#.....#O.....O..O...O.....#.O.O.O#...#O.#...##.O..O#");
            AddRow(row++, "#....#.O...O#...##O.#....O........O...O#.#.#...#O#..O..O.#O.....O..O....#....O.O#O#....#O#..#......#");
            AddRow(row++, ".O.O#.........O.....##.O.#..............O...#.#OOO.O.OO..O..O..O##....#.......OO....O....#OO#..OO...");
            AddRow(row++, "##.O#...OO...O.O...........O..O.##O..#.#...#..O..#.##....#..###O..##........OOO.#.O.#......#......#.");
            AddRow(row++, "#......O#..O#.#.#..#O##..OO#O...##O.#...#.OO...#.O..O##O.O...........O.##...###O..#.#.O.O..OO.O..O..");
            AddRow(row++, "#.....#.##.O.O.#....O..#O......O.O#...#..O.#O#OO...#.OO...O.O##.O...O.......#...##....#O.....O.##O#.");
            AddRow(row++, "...O.OO.O...O#..OO.O....#.#.O....O...#..#O.O..OO.#....O.O#.O#..O..O...O..O........#..#..#..#..OO.O#.");
            AddRow(row++, ".#O.O....#....#.OO.#.O...O......O....O#.....##...#O...##...#.#OO..O#O..O##.#.#..O...O.....#..O##.O..");
            AddRow(row++, "OOO....OOOO...#.#...#.....###...O......O.#.O.....O...#.O##...##O.#.#...O....O.O...#.O...#O....O.....");
        }
    }
}