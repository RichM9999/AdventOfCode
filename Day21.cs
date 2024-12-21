//https://adventofcode.com/2024/day/16

// Based on https://github.com/boombuler/adventofcode/blob/master/2024/Day21.cs
namespace AdventOfCode
{
    using System.Text;

    using Coordinate = (int x, int y);

    class Day21
    {
        private Dictionary<(char lastPos, char newPos, int level), long> cache;
        private List<string> input;

        const char startButton = 'A';

        public Day21()
        {
            input = [];
            cache = [];
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

            // Part 1: 2 intermediate robots
            GetSumOfMoveComplexities(input, 2);
            //123096
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            start = DateTime.Now;

            // Part 2: 25 intermediate robots
            GetSumOfMoveComplexities(input, 25);
            //154517692795352
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        private void GetSumOfMoveComplexities(List<string> input, int robotKeypads)
        {
            long sum = 0;

            foreach (var line in input)
            {
                // The numeric part of the code ignoring final "A"
                var lineFactor = long.Parse(line[0..^1]);

                // Moves required on numeric keypad to directly enter the input sequence
                var numericPadMoves = NumericKeypad.MovesFromString(line).Prepend(startButton).ToArray();

                long moveSum = 0;
                // For each move on the numeric keypad
                for (var i = 0; i < numericPadMoves.Length - 1; i++)
                {
                    // Get the translated number of movements required for each layer of robot directional keypads
                    moveSum += CountDirectionalPadMoves(numericPadMoves[i], numericPadMoves[i + 1], robotKeypads);
                }

                // Complexity is numeric part of sequence * length of movements required on translated keypads
                sum += lineFactor * moveSum;
            }

            Console.WriteLine($"Sum of complexities with {robotKeypads} robot keypads: {sum}");
        }

        private long CountDirectionalPadMoves(char lastPosition, char newPosition, int robotLevel)
        {
            long moveSum = 0;

            // If we've already moved from lastPos to newPos at this level, just return sum already calculated
            if (cache.TryGetValue((lastPosition, newPosition, robotLevel), out moveSum))
            {
                return moveSum;
            }

            // Get move sequence from last position to new position
            var todo = DirectionalKeypad.GetMovesFromTo(lastPosition, newPosition);

            // If lowest level robot, return length of moves as result
            if (robotLevel == 1)
                return todo.Length;

            // Add starting position to move list
            var moves = todo.Prepend(startButton).ToArray();
                
            // Process all moves required for todo list
            for (var i = 0; i < moves.Length - 1; i++)
            {
                // Get number of moves required for to do list, recursively, in descending robot level order
                moveSum += CountDirectionalPadMoves(moves[i], moves[i + 1], robotLevel - 1);
            }

            // cache sum to go from lastPos to newPos at this level
            cache[(lastPosition, newPosition, robotLevel)] = moveSum;

            // return sum of moves to complete sequence at this level
            return moveSum;
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
                    moves += GetMovesFromTo(moveString[i], moveString[i + 1]);
                }
                return moves;
            }

            public string GetMovesFromTo(char from, char to)
            {
                var moveSequence = new StringBuilder();
                var target = keyMap[to];
                var currentPosition = keyMap[from];
                var distance = new Coordinate(target.x - currentPosition.x, target.y - currentPosition.y);
                var directionIndex = 0;

                while (distance != (0, 0))
                {
                    // Enumerate directions in preferred Manhattan distance order
                    var (direction, directionDiff) = Directions[(directionIndex++ % Directions.Length)];

                    // Determine number of repeated keypresses in direction to go distance
                    var amount = directionDiff.x == 0 ? distance.y / directionDiff.y : distance.x / directionDiff.x;
                    if (amount <= 0)
                        continue;

                    // Calculate coordinate amounts to add to current position to get to destination
                    var directionTimesAmount = new Coordinate(directionDiff.x * amount, directionDiff.y * amount);

                    // Determine new destination
                    var destination = new Coordinate(currentPosition.x + directionTimesAmount.x, currentPosition.y + directionTimesAmount.y);

                    // Don't move robot arm over the gap in the keypad
                    if (destination == keypadMissingButtonLocation)
                        continue;

                    // Current position is now the destination
                    currentPosition = destination;

                    // Distance from target is reduced by distance just moved (direction * amount)
                    distance = new Coordinate(distance.x - directionTimesAmount.x, distance.y - directionTimesAmount.y);

                    // Add move sequence to list.  Use amount to create repetitive move sequence
                    moveSequence.Append(new string(direction, amount));
                }
                moveSequence.Append(startButton);
                return moveSequence.ToString();
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

        private static readonly KeyPad NumericKeypad = new KeyPad(new Dictionary<char, Coordinate>()
        {
            //     0   1   2
            //   +---+---+---+
            // 0 | 7 | 8 | 9 |
            //   +---+---+---+
            // 1 | 4 | 5 | 6 |
            //   +---+---+---+
            // 2 | 1 | 2 | 3 |
            //   +---+---+---+
            // 3     | 0 | A |
            //       +---+---+

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

        private static readonly KeyPad DirectionalKeypad = new KeyPad(new Dictionary<char, Coordinate>()
        {
            //     0   1   2
            //       +---+---+
            // 0     | ^ | A |
            //   +---+---+---+
            // 1 | < | v | > |
            //   +---+---+---+

            ['^'] = (1, 0),
            ['A'] = (2, 0),
            ['<'] = (0, 1),
            ['v'] = (1, 1),
            ['>'] = (2, 1),
        }.ToDictionary());
    }
}
