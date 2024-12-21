//https://adventofcode.com/2024/day/16

// Based on https://github.com/boombuler/adventofcode/blob/master/2024/Day21.cs
namespace AdventOfCode
{
    using System.Text;

    using Coordinate = (int x, int y);

    class Day21
    {
        private Dictionary<(char lastPos, char newPos, int level), long> cache = [];

        private List<string> input;
        const char startButton = 'A';

        public Day21() 
        {
            input = [];
        }  

        public void Run()
        {
            input = [];
            input.Add("279A");
            input.Add("341A");
            input.Add("459A");
            input.Add("540A");
            input.Add("085A");

            var start = DateTime.Now;

            long sum = 0;
            int robotKeypads = 2;
            foreach (var line in input)
            {
                var lineFactor = long.Parse(line[0..^1]);
                var numericPadMoves = NumPad.MovesFromString(line).Prepend(startButton).ToArray();

                long moveSum = 0;
                for (var i = 0; i < numericPadMoves.Length - 1; i++)
                {
                    moveSum += CountDirPadMoves(numericPadMoves[i], numericPadMoves[i + 1], robotKeypads);
                }

                sum += lineFactor * moveSum;
            }

            Console.WriteLine($"Sum of complexities with {robotKeypads} robot keypads: {sum}");
            //123096
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            sum = 0;
            robotKeypads = 25;
            foreach (var line in input)
            {
                var lineFactor = long.Parse(line[0..^1]);
                var numericPadMoves = NumPad.MovesFromString(line).Prepend(startButton).ToArray();

                long moveSum = 0;
                for (var i = 0; i < numericPadMoves.Length - 1; i++)
                {
                    moveSum += CountDirPadMoves(numericPadMoves[i], numericPadMoves[i + 1], robotKeypads);
                }

                sum += lineFactor * moveSum;
            }

            Console.WriteLine($"Sum of complexities with {robotKeypads} robot keypads: {sum}");
            //154517692795352
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        private long CountDirPadMoves(char lastPos, char newPos, int level)
        {
            if (cache.TryGetValue((lastPos, newPos, level), out var moveSum))
            {
                return moveSum;
            }
            else
            {
                moveSum = 0;
                var todo = DirPad.Transition(lastPos, newPos);
                if (level == 1)
                    return todo.Length;

                var moves = todo.Prepend(startButton).ToArray();
                
                for (var i = 0; i < moves.Length - 1; i++)
                {
                    moveSum += CountDirPadMoves(moves[i], moves[i + 1], level - 1);
                }

                cache[(lastPos, newPos, level)] = moveSum;
                return moveSum;
            }
        }

        class KeyPad
        {
            private readonly Coordinate keypadMissingButtonLocation;
            private readonly Dictionary<char, Coordinate> keyMap;

            //    +---+---+
            //    | ^ | A |
            //+---+---+---+
            //| < | v | > |
            //+---+---+---+
            // Assuming top left = 1,1, bottom right = 3,2
            // <v^> = Direction Keys ordered by manhattan distance from the A key on the direction keypad.
            //
            // Distances:
            // < = 3  |1 - 3| + |2 - 1| = 3
            // v = 2  |2 - 3| + |2 - 1| = 2
            // ^ = 1  |2 - 3| + |1 - 1| = 1
            // > = 1  |3 - 3| + |2 - 1| = 1
            //
            // NOTE that <v>^ also works
            private static readonly (char Char, Coordinate Direction)[] Directions = "<v^>".Select(c => (c, DirectionDiff(c))).ToArray();

            public KeyPad(Dictionary<char, Coordinate> map)
            {
                keyMap = map;
                // missing button is always on left edge (x = 0) of row with A button
                keypadMissingButtonLocation = (0, keyMap['A'].y);
            }

            public string MovesFromString(string input)
            {
                var moveString = input.Prepend(startButton).ToArray();

                var moves = string.Empty;
                for (var i = 0; i < moveString.Length - 1; i++)
                {
                    moves += Transition(moveString[i], moveString[i + 1]);
                }
                return moves;
            }

            public string Transition(char from, char to)
            {
                var sb = new StringBuilder();
                var target = keyMap[to];
                var curPos = keyMap[from];
                var delta = new Coordinate(target.x - curPos.x, target.y - curPos.y);
                var d = 0;

                while (delta != (0, 0))
                {
                    var (dirChar, dir) = Directions[(d++ % Directions.Length)];
                    var amount = dir.x == 0 ? delta.y / dir.y : delta.x / dir.x;
                    if (amount <= 0)
                        continue;
                    var dirTimesAmount = new Coordinate(dir.x * amount, dir.y * amount);
                    var dest = new Coordinate(curPos.x + dirTimesAmount.x, curPos.y + dirTimesAmount.y);
                    if (dest == keypadMissingButtonLocation)
                        continue;
                    curPos = dest;
                    delta = new Coordinate(delta.x - dirTimesAmount.x, delta.y - dirTimesAmount.y);

                    sb.Append(new string(dirChar, amount));
                }
                sb.Append(startButton);
                return sb.ToString();
            }

            static Coordinate DirectionDiff(char direction)
            {
                switch (direction)
                {
                    case '<':
                        return (-1, 0);
                    case '>':
                        return (1, 0);
                    case '^':
                        return (0, -1);
                    case 'v':
                        return (0, 1);
                }
                return (0, 0);
            }
        }

        private static readonly KeyPad NumPad = new KeyPad(new Dictionary<char, Coordinate>()
        {
            ['7'] = (0, 0),
            ['8'] = (1, 0),
            ['9'] = (2, 0),
            ['4'] = (0, 1),
            ['5'] = (1, 1),
            ['6'] = (2, 1),
            ['1'] = (0, 2),
            ['2'] = (1, 2),
            ['3'] = (2, 2),
            ['0'] = (1, 3),
            ['A'] = (2, 3),
        }.ToDictionary());

        private static readonly KeyPad DirPad = new KeyPad(new Dictionary<char, Coordinate>()
        {
            ['^'] = (1, 0),
            ['A'] = (2, 0),
            ['<'] = (0, 1),
            ['v'] = (1, 1),
            ['>'] = (2, 1),
        }.ToDictionary());
    }
}
