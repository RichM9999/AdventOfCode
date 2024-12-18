//https://adventofcode.com/2024/day/17
namespace AdventOfCode
{
    class Day17
    {
        long rA;
        long rB;
        long rC;

        string programList;

        int[] instructions;

        int pointer;

        public Day17()
        {
            instructions = [ ];
            programList = string.Empty;
        }

        public void Run()
        {
            var start = DateTime.Now;

            GetInput();
            RunProgram(rA, rB, rC, instructions, false, out List<long> output);

            Console.WriteLine($"Output: {string.Join(',', output.ToArray())}");
            //2,3,4,7,5,7,3,0,7
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
            Console.WriteLine();

            for (long i = 0; i < long.MaxValue; i++)
            {
                if (RunProgram(i, rB, rC, instructions, true, out output))
                {
                    Console.WriteLine($"Input: {programList}");
                    Console.WriteLine($"Register A value: {i}");
                    //190384609508367
                    Console.WriteLine($"Output: {string.Join(',', output.ToArray())}");
                    Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
                    break;
                }
                else
                {
                    // Logic to increase by matches copied from: https://github.com/larsdekwant/AdventOfCode2024/blob/main/Days/17/Day17.cs
                    // Description from creator on the logic:
                    //I found a pattern(explained below), which I programmed as follows:

                    //I keep increasing register A starting from 0, and every time the digits
                    //of the output(size X) match the last X digits of the given program,
                    //I multiply register A by 8.Until finally you reach N digits, where
                    //you return the value once all N digits match.

                    //How I found this pattern: I first noticed that the amount of digits in
                    //the answer strictly increases as the value for register A increases.
                    //Using this, I started printing the value for register A starting from 0,
                    //but only when the resulting output matched the last X digits of the
                    //given program.Here I noticed a pattern that the first match of X digits
                    //always led to the match for (X + 1) digits by multiplying by 8, giving a
                    //matching of X digits in an output of(X + 1).To now match all(X + 1)
                    //digits, simply keep increasing the value 1 by 1, until it matches
                    //(this is a small amount of work, at most 8 steps).Now continue doing
                    //this until all N digits match, and you have your result.

                    // Check whether the output of size N matches with the last N digits of program
                    bool matchLastDigits = instructions[(instructions.Length - output.Count)..]
                        .Select((x, i) => output[i] == x)
                        .All(b => b);

                    if (matchLastDigits)
                    {
                        // Found the value if all digits match, and output is same size as program.
                        if (output.Count == instructions.Length) break;
                        i = (i * 8) - 1; // pre-adjust -1 cuz loop counter will + 1 again.
                    }
                }
            }
        }

        bool RunProgram(long registerA, long registerB, long registerC, int[] program, bool seekOutputMatch, out List<long> output)
        {
            pointer = 0;
            output = new List<long>();

            while (true)
            {
                if (pointer == program.Length)
                {
                    break;
                }

                var instruction = program[pointer];
                var literalOperand = program[pointer + 1];
                long comboOperand = literalOperand;
                long outputValue = -1;

                switch (literalOperand)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 7:
                        break;
                    case 4:
                        comboOperand = registerA;
                        break;
                    case 5:
                        comboOperand = registerB;
                        break;
                    case 6:
                        comboOperand = registerC;
                        break;
                }


                switch (instruction)
                {
                    case 0:
                        registerA = registerA / (long)Math.Pow(2, comboOperand);
                        break;
                    case 1:
                        registerB = registerB ^ literalOperand;
                        break;
                    case 2:
                        registerB = comboOperand % 8;
                        break;
                    case 3:
                        if (registerA != 0)
                        {
                            pointer = literalOperand;
                        }
                        break;
                    case 4:
                        registerB = registerB ^ registerC;
                        break;
                    case 5:
                        outputValue = comboOperand % 8;
                        break;
                    case 6:
                        registerB = registerA / (long)Math.Pow(2, comboOperand);
                        break;
                    case 7:
                        registerC = registerA / (long)Math.Pow(2, comboOperand);
                        break;
                }

                if (outputValue >= 0)
                {
                    output.Add(outputValue);
                }

                if (instruction != 3 || registerA == 0)
                {
                    pointer += 2;
                }
            }

            if (!seekOutputMatch)
            {
                return true;
            }
            else 
            { 
                if (output.Count > 0)
                {
                    if (output.Count != program.Length)
                    {
                        return false;
                    }

                    for (var i = 0; i < output.Count; i++)
                    {
                        if (output[i] != program[i])
                        {
                            return false;
                        }
                    }

                    return true;
                }

                // no output
                return false;
            }
        }

        void GetInput()
        {
            //Register A: 61657405
            //Register B: 0
            //Register C: 0

            //Program: 2,4,1,2,7,5,4,3,0,3,1,7,5,5,3,0

            rA = 61657405;
            rB = 0;
            rC = 0;

            programList = "2,4,1,2,7,5,4,3,0,3,1,7,5,5,3,0";
            instructions = programList.Split(',').Select(p => int.Parse(p)).ToArray();
        }
    }
}
