//https://adventofcode.com/2023/day/20
namespace AdventOfCode.Year2023
{
    class Day20
    {
        List<string> modules;

        public Day20()
        {
            modules = [];
        }

        public void Run()
        {
            GetInput();

            Button theButton = new(modules.ToArray());

            long part1Answer = theButton.PressPart1(1000);
            long part2Answer = theButton.PressPart2();

            Console.WriteLine($"Part 1: The product of the number of high and low signals after 1000 button pushes is {part1Answer}.");
            // 949764474
            Console.WriteLine($"Part 2: It will take {part2Answer} button pushes to start the machine.");
            // 243221023462303
        }

        void AddModule(string module)
        {
            modules.Add(module);
        }


        void GetInput()
        {
            AddModule("&pr -> pd, vx, vn, cl, hm");
            AddModule("%hm -> qb");
            AddModule("%nm -> dh, jv");
            AddModule("%lv -> jv, tg");
            AddModule("%dg -> tm, jm");
            AddModule("%mt -> jv, zp");
            AddModule("&ln -> kj");
            AddModule("&kj -> rx");
            AddModule("&dr -> kj");
            AddModule("%dx -> ts");
            AddModule("&qs -> kf, dr, sc, rg, gl, dx");
            AddModule("%dh -> jv, mc");
            AddModule("%rg -> qs, vq");
            AddModule("%kt -> jv, mt");
            AddModule("%lh -> qs, dl");
            AddModule("%tp -> pf, jm");
            AddModule("%bf -> vx, pr");
            AddModule("%mv -> qs, gl");
            AddModule("%ts -> ng, qs");
            AddModule("%kf -> dx");
            AddModule("%gv -> jm, km");
            AddModule("%dl -> qs");
            AddModule("%nd -> dg");
            AddModule("%km -> jm");
            AddModule("%ns -> pr, pn");
            AddModule("%gl -> kf");
            AddModule("%pd -> pr, jp");
            AddModule("%xv -> nd, jm");
            AddModule("%hf -> nm");
            AddModule("%vx -> ns");
            AddModule("%vq -> bs, qs");
            AddModule("%sc -> mv");
            AddModule("&jv -> hj, rc, kt, ln, zp, hf");
            AddModule("%rc -> hj");
            AddModule("%jp -> mx, pr");
            AddModule("%mf -> gv, jm");
            AddModule("&zx -> kj");
            AddModule("%tg -> jv");
            AddModule("%bs -> sc, qs");
            AddModule("%ng -> qs, lh");
            AddModule("%tk -> pr");
            AddModule("%qb -> bf, pr");
            AddModule("%pn -> pr, cb");
            AddModule("%cl -> hm");
            AddModule("%pb -> tp");
            AddModule("broadcaster -> kt, pd, xv, rg");
            AddModule("&jm -> pb, tm, zx, mk, xv, nd");
            AddModule("%vc -> jv, hf");
            AddModule("%mc -> jv, lv");
            AddModule("%mk -> pb");
            AddModule("%tm -> mh");
            AddModule("%cb -> pr, tk");
            AddModule("%hj -> vc");
            AddModule("%zp -> rc");
            AddModule("%mh -> mk, jm");
            AddModule("%pf -> mf, jm");
            AddModule("%mx -> cl, pr");
            AddModule("&vn -> kj");
        }

        // Below taken from https://github.com/Kezzryn/Advent-of-Code/blob/main/2023/Day%2020/Program.cs
        internal abstract class Node(List<string> targetNodes)
        {
            static public bool HIGH = true;
            static public bool LOW = false;
            public List<string> OutputModules { get => _targetNodes; }
            public long LowPulses { get; protected set; }
            public long HighPulses { get; protected set; }

            protected List<string> _targetNodes = targetNodes;
            protected bool _value = false;
            public void Reset()
            {
                _value = LOW;
                LowPulses = 0;
                HighPulses = 0;
            }

            protected List<(string, bool)> SendPulse()
            {
                if (_value == HIGH)
                    HighPulses += _targetNodes.Count;
                else
                    LowPulses += _targetNodes.Count;

                return _targetNodes.Select(s => (s, _value)).ToList();
            }

            public abstract List<(string, bool)> Pulse(bool inPulseValue, string source);
        }

        internal class Broadcaster(List<string> targetNodes) : Node(targetNodes)
        {
            public override List<(string, bool)> Pulse(bool inPulseValue, string source)
            {
                return SendPulse();
            }
        }

        internal class EndNode(List<string> targetNodes) : Node(targetNodes)
        {
            public override List<(string, bool)> Pulse(bool inPulseValue, string source)
            {
                return [];
            }
        }

        internal class FlipFlop(List<string> targetNodes) : Node(targetNodes)
        {
            public override List<(string, bool)> Pulse(bool inPulseValue, string source)
            {
                if (inPulseValue == HIGH) return [];

                _value = !_value;
                return SendPulse();
            }
        }
        internal class Conjunction : Node
        {
            public List<string> InputModules => _linkedModules.Keys.ToList();

            readonly Dictionary<string, bool> _linkedModules = [];
            public Conjunction(List<string> targetNodes, List<string> linkedModules)
               : base(targetNodes)
            {
                foreach (string module in linkedModules)
                {
                    _linkedModules.Add(module, LOW);
                }
            }

            public new void Reset()
            {
                base.Reset();
                foreach (string key in _linkedModules.Keys)
                {
                    _linkedModules[key] = false;
                }
            }

            public override List<(string, bool)> Pulse(bool inPulseValue, string source = "")
            {
                _linkedModules[source] = inPulseValue;

                _value = HIGH;
                if (_linkedModules.All(x => x.Value == HIGH)) _value = LOW;

                return SendPulse();
            }
        }

        internal class Button
        {
            private const char CONJ = '&';
            private const char FLFP = '%';
            private const string BROADCASTER = "broadcaster";
            private Dictionary<string, Node> _modules = [];
            private Queue<(string source, string dest, bool pulse)> _pulseQueue = [];
            private Dictionary<string, long> _intercept = [];
            public Button(string[] puzzleInput)
            {
                // build the list of inputs to each conjunction.
                Dictionary<string, List<string>> conjSourceModules =
                    puzzleInput.Where(wa => wa[0] == CONJ)
                    .ToDictionary(k => k[1..3],
                                  v => puzzleInput.Where(wb => wb[7..].Contains(v[1..3])).Select(s => s[1..3]).ToList());

                foreach (string line in puzzleInput)
                {
                    //&tb -> sx, qn, vj, qq, sk, pv
                    char type = line[0];
                    string label = line[1..3];
                    List<string> targetModules = [.. line[(line.IndexOf('>') + 1)..].Split(',', StringSplitOptions.TrimEntries)];

                    if (line.StartsWith(BROADCASTER)) _modules.Add(BROADCASTER, new Broadcaster(targetModules));
                    if (type == CONJ) _modules.Add(label, new Conjunction(targetModules, conjSourceModules[label]));
                    if (type == FLFP) _modules.Add(label, new FlipFlop(targetModules));
                }

                string endNode = _modules.SelectMany(x => x.Value.OutputModules).Except(_modules.Keys).FirstOrDefault("NA");
                _modules.Add(endNode, new EndNode([]));
            }

            private string BroadCast()
            {
                // only used for part two.
                string returnValue = String.Empty;
                _pulseQueue.Enqueue(("", BROADCASTER, Node.LOW));

                while (_pulseQueue.TryDequeue(out (string source, string curModule, bool value) pulse))
                {
                    foreach ((string target, bool pulseValue) in _modules[pulse.curModule].Pulse(pulse.value, pulse.source))
                    {
                        if (_intercept.ContainsKey(target) && pulseValue == Node.LOW) returnValue = target;
                        _pulseQueue.Enqueue((pulse.curModule, target, pulseValue));
                    }
                }
                return returnValue;
            }

            public long PressPart1(int numPresses)
            {
                _intercept.Clear();
                foreach (string key in _modules.Keys)
                {
                    _modules[key].Reset();
                }

                for (int i = 1; i <= numPresses; i++)
                {
                    BroadCast();
                }

                //Remember to add back number of low signals signals sent by the button.
                return (_modules.Values.Sum(x => x.LowPulses) + numPresses) * _modules.Values.Sum(x => x.HighPulses);
            }

            public long PressPart2()
            {
                long numPresses = 0;
                foreach (string key in _modules.Keys)
                {
                    _modules[key].Reset();
                }

                string endNode = _modules.Where(x => x.Value is EndNode).First().Key;

                _intercept = _modules.Values.Where(x => x is Conjunction)
                    .Select(c => (Conjunction)c)
                    .Where(x => x.OutputModules.Contains(endNode))
                    .SelectMany(x => x.InputModules)
                    .ToDictionary(k => k, v => -1L);

                do
                {
                    numPresses++;
                    string temp = BroadCast();

                    if (temp != String.Empty) _intercept[temp] = numPresses;
                } while (_intercept.Any(x => x.Value == -1L) && numPresses < 5000);

                //At this point I'd take the LCM of the numbers.
                //However, all the answers for my input are prime.
                //So... 
                return _intercept.Values.Aggregate((x, y) => x * y);
            }
        }
    }
}