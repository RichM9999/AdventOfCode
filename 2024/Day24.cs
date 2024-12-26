//https://adventofcode.com/2024/day/24
namespace AdventOfCode.Year2024
{
    class Day24
    {

        Dictionary<string, (string wire1, string operation, string wire2, byte? value)> gates;

        public Day24()
        {
            gates = [];
        }

        public void Run()
        {
            var start = DateTime.Now;

            GetData();

            // Part 1
            while (gates.Any(g => g.Value.value == null))
            {
                foreach (var gate in gates.Where(g => g.Value.value == null))
                {
                    if (gates[gate.Value.wire1].value != null && gates[gate.Value.wire2].value != null)
                    {
                        var wire1Value = gates[gate.Value.wire1].value ?? 0;
                        var wire2Value = gates[gate.Value.wire2].value ?? 0;
                        var operation = gate.Value.operation;
                        byte output = 0;

                        switch (operation)
                        {
                            case "AND":
                                output = (byte)(wire1Value & wire2Value);
                                break;
                            case "OR":
                                output = (byte)(wire1Value | wire2Value);
                                break;
                            case "XOR":
                                output = (byte)(wire1Value ^ wire2Value);
                                break;
                        }

                        gates[gate.Key] = (gate.Value.wire1, gate.Value.operation, gate.Value.wire2, output);
                    }
                }
            }

            var power = 0;
            long result = 0;

            foreach (var gate in gates.Where(g => g.Key.StartsWith('z')).OrderBy(g => g.Key))
            {
                result += (long)Math.Pow(2, power) * (gate.Value.value ?? 0);
                power++;
            }

            Console.WriteLine($"Z wire decimal output: {result}");
            //48063513640678 
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            // Part 2

            //From: https://www.reddit.com/r/adventofcode/comments/1hl698z/comment/m3lnhrw/
            //
            //Part 2: The key insight is that the circuit is trying to perform binary addition between x and y inputs.
            //The gates are swapped in pairs, causing incorrect outputs.
            //
            //The solution:
            // First, looks for z-wire gates that should be XOR gates(for addition) but aren't.  Last z-gate should be OR
            //
            // Then checks internal gates (non x,y,z) that incorrectly feed into XOR operations
            //
            // For gates with x/y inputs:
            //  * XOR gates should feed into other XOR gates(for addition)
            //  * AND gates should feed into OR gates(for carry propagation)
            //  * First bit(00) gates are handled separately as they start the carry chain
            //
            //The program identifies gates that don't follow these patterns, indicating their outputs were likely swapped.
            //The answer is the sorted list of the 8 wires (4 pairs) involved in these swaps.

            start = DateTime.Now;

            var faultyGates = new HashSet<string>();
            var lastZGate = gates
                                .Where(g => g.Key.StartsWith('z'))
                                .OrderByDescending(g => g.Key)
                                .First();

            // Check all gates except those with initial values
            foreach (var gate in gates.Where(g => g.Value.operation != "INIT"))
            {
                var isFaulty = false;

                if (gate.Key.StartsWith('z')) 
                {
                    if (gate.Key != lastZGate.Key)
                    {
                        // All but last Z gate should be populated by XOR
                        isFaulty = gate.Value.operation != "XOR";
                    }
                    else
                    {
                        // Last Z gate should be populated by OR
                        isFaulty = gate.Value.operation != "OR";
                    }
                }
                else if (!gate.Key.StartsWith('z') 
                            && !(gate.Value.wire1.StartsWith('x') || gate.Value.wire1.StartsWith('y')) 
                            && !(gate.Value.wire2.StartsWith('x') || gate.Value.wire2.StartsWith('y')))
                {
                    // non x,y,z gates should not use XOR operator
                    isFaulty = gate.Value.operation == "XOR";
                }
                else if ((gate.Value.wire1.StartsWith('x') || gate.Value.wire1.StartsWith('y')) 
                        && (gate.Value.wire2.StartsWith('x') || gate.Value.wire2.StartsWith('y')) 
                        && !(gate.Value.wire1.EndsWith("00") && gate.Value.wire2.EndsWith("00")))
                {
                    // gates populated by x,y gates, except for x00, y00,
                    // should feed into another gate with XOR, if populated with XOR, otherwise next operation should be OR
                    var output = gate.Key;
                    var expectedNextType = gate.Value.operation == "XOR" ? "XOR" : "OR";

                    var feedsIntoExpectedGate = gates.Any(other =>
                                                            other.Key != gate.Key 
                                                            && (other.Value.wire1 == output || other.Value.wire2 == output) 
                                                            && other.Value.operation == expectedNextType);

                    isFaulty = !feedsIntoExpectedGate;
                }

                if (isFaulty)
                {
                    faultyGates.Add(gate.Key);
                }
            }

            Console.WriteLine($"Swapped gates: {string.Join(",", faultyGates.OrderBy(g => g))}");
            // hqh,mmk,pvb,qdq,vkq,z11,z24,z38.
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        void AddInitialValue(string wire, byte value)
        {
            gates.Add(wire, (wire, "INIT", wire, value));
        }

        void AddGate(string wire1, string operation, string wire2, string outputWire)
        {
            gates.Add(outputWire, (wire1, operation, wire2, null));
        }

        void GetData()
        {
            AddInitialValue("x00", 1);
            AddInitialValue("x01", 0);
            AddInitialValue("x02", 1);
            AddInitialValue("x03", 1);
            AddInitialValue("x04", 0);
            AddInitialValue("x05", 0);
            AddInitialValue("x06", 1);
            AddInitialValue("x07", 1);
            AddInitialValue("x08", 0);
            AddInitialValue("x09", 1);
            AddInitialValue("x10", 1);
            AddInitialValue("x11", 1);
            AddInitialValue("x12", 1);
            AddInitialValue("x13", 1);
            AddInitialValue("x14", 1);
            AddInitialValue("x15", 0);
            AddInitialValue("x16", 0);
            AddInitialValue("x17", 1);
            AddInitialValue("x18", 0);
            AddInitialValue("x19", 1);
            AddInitialValue("x20", 1);
            AddInitialValue("x21", 0);
            AddInitialValue("x22", 1);
            AddInitialValue("x23", 0);
            AddInitialValue("x24", 0);
            AddInitialValue("x25", 1);
            AddInitialValue("x26", 0);
            AddInitialValue("x27", 0);
            AddInitialValue("x28", 1);
            AddInitialValue("x29", 0);
            AddInitialValue("x30", 0);
            AddInitialValue("x31", 1);
            AddInitialValue("x32", 1);
            AddInitialValue("x33", 0);
            AddInitialValue("x34", 1);
            AddInitialValue("x35", 1);
            AddInitialValue("x36", 0);
            AddInitialValue("x37", 1);
            AddInitialValue("x38", 0);
            AddInitialValue("x39", 1);
            AddInitialValue("x40", 0);
            AddInitialValue("x41", 0);
            AddInitialValue("x42", 0);
            AddInitialValue("x43", 1);
            AddInitialValue("x44", 1);
            AddInitialValue("y00", 1);
            AddInitialValue("y01", 0);
            AddInitialValue("y02", 0);
            AddInitialValue("y03", 1);
            AddInitialValue("y04", 1);
            AddInitialValue("y05", 0);
            AddInitialValue("y06", 0);
            AddInitialValue("y07", 0);
            AddInitialValue("y08", 0);
            AddInitialValue("y09", 0);
            AddInitialValue("y10", 0);
            AddInitialValue("y11", 1);
            AddInitialValue("y12", 0);
            AddInitialValue("y13", 0);
            AddInitialValue("y14", 1);
            AddInitialValue("y15", 1);
            AddInitialValue("y16", 1);
            AddInitialValue("y17", 0);
            AddInitialValue("y18", 1);
            AddInitialValue("y19", 1);
            AddInitialValue("y20", 0);
            AddInitialValue("y21", 0);
            AddInitialValue("y22", 1);
            AddInitialValue("y23", 0);
            AddInitialValue("y24", 1);
            AddInitialValue("y25", 0);
            AddInitialValue("y26", 1);
            AddInitialValue("y27", 0);
            AddInitialValue("y28", 0);
            AddInitialValue("y29", 0);
            AddInitialValue("y30", 0);
            AddInitialValue("y31", 0);
            AddInitialValue("y32", 1);
            AddInitialValue("y33", 0);
            AddInitialValue("y34", 0);
            AddInitialValue("y35", 1);
            AddInitialValue("y36", 0);
            AddInitialValue("y37", 0);
            AddInitialValue("y38", 1);
            AddInitialValue("y39", 1);
            AddInitialValue("y40", 0);
            AddInitialValue("y41", 1);
            AddInitialValue("y42", 0);
            AddInitialValue("y43", 0);
            AddInitialValue("y44", 1);

            AddGate("mrh", "XOR", "bnc", "z32");
            AddGate("y14", "XOR", "x14", "vvw");
            AddGate("bjt", "XOR", "mmm", "z42");
            AddGate("y41", "AND", "x41", "gwr");
            AddGate("sbs", "AND", "vbj", "kpf");
            AddGate("x01", "XOR", "y01", "rbr");
            AddGate("jkf", "XOR", "kmf", "z21");
            AddGate("x25", "XOR", "y25", "knp");
            AddGate("y05", "AND", "x05", "jcj");
            AddGate("qpn", "OR", "gmv", "krq");
            AddGate("x19", "AND", "y19", "mfq");
            AddGate("mrh", "AND", "bnc", "rvw");
            AddGate("wrg", "XOR", "mjr", "z44");
            AddGate("y10", "XOR", "x10", "gtn");
            AddGate("y42", "AND", "x42", "dmw");
            AddGate("wmj", "OR", "pft", "tkg");
            AddGate("x13", "AND", "y13", "qkc");
            AddGate("y05", "XOR", "x05", "tjs");
            AddGate("pmk", "XOR", "vqg", "z36");
            AddGate("tcg", "OR", "fbd", "z45");
            AddGate("y33", "AND", "x33", "wmj");
            AddGate("sqs", "OR", "thj", "sch");
            AddGate("y16", "AND", "x16", "hbc");
            AddGate("y09", "XOR", "x09", "svf");
            AddGate("mjb", "OR", "bbc", "bhq");
            AddGate("dvf", "XOR", "fdj", "z33");
            AddGate("x34", "AND", "y34", "pbp");
            AddGate("y44", "XOR", "x44", "mjr");
            AddGate("x28", "XOR", "y28", "qdq");
            AddGate("qkc", "OR", "crp", "mgc");
            AddGate("vgn", "AND", "wfn", "khj");
            AddGate("rwc", "OR", "svg", "dng");
            AddGate("y33", "XOR", "x33", "dvf");
            AddGate("y29", "XOR", "x29", "fqc");
            AddGate("vqg", "AND", "pmk", "stq");
            AddGate("rbr", "XOR", "rnv", "z01");
            AddGate("x37", "XOR", "y37", "kbh");
            AddGate("x35", "AND", "y35", "nsb");
            AddGate("hqn", "XOR", "kbh", "z37");
            AddGate("ftq", "XOR", "fqc", "z29");
            AddGate("y21", "XOR", "x21", "jkf");
            AddGate("y39", "XOR", "x39", "vvc");
            AddGate("x16", "XOR", "y16", "gnc");
            AddGate("x41", "XOR", "y41", "dgn");
            AddGate("y12", "XOR", "x12", "nvv");
            AddGate("dmw", "OR", "hgm", "cjb");
            AddGate("crb", "OR", "bgw", "mrh");
            AddGate("stq", "OR", "cfj", "hqn");
            AddGate("qdh", "XOR", "bhq", "z13");
            AddGate("svf", "XOR", "thf", "z09");
            AddGate("skb", "OR", "qdj", "nbm");
            AddGate("x24", "AND", "y24", "pwp");
            AddGate("x07", "XOR", "y07", "gpb");
            AddGate("qsj", "OR", "khj", "qvs");
            AddGate("y30", "XOR", "x30", "rdj");
            AddGate("x20", "XOR", "y20", "vtd");
            AddGate("qmm", "XOR", "svb", "z26");
            AddGate("y20", "AND", "x20", "tsn");
            AddGate("pbp", "OR", "cpb", "ngc");
            AddGate("cct", "AND", "pvb", "jnk");
            AddGate("y06", "AND", "x06", "nfr");
            AddGate("gnc", "AND", "nbm", "mfr");
            AddGate("tkg", "XOR", "bpm", "z34");
            AddGate("sbs", "XOR", "vbj", "vkq");
            AddGate("srj", "OR", "rvr", "wfn");
            AddGate("y04", "XOR", "x04", "brf");
            AddGate("x21", "AND", "y21", "drv");
            AddGate("mmk", "AND", "knp", "jss");
            AddGate("x22", "AND", "y22", "rvr");
            AddGate("y27", "AND", "x27", "srb");
            AddGate("vvc", "AND", "hth", "qvm");
            AddGate("mdm", "OR", "hqh", "hth");
            AddGate("bjr", "AND", "cjb", "wgc");
            AddGate("prm", "XOR", "jps", "z18");
            AddGate("hpg", "AND", "ssv", "jvt");
            AddGate("ngc", "XOR", "mqw", "z35");
            AddGate("gbw", "XOR", "vjb", "z02");
            AddGate("jbf", "OR", "ptp", "ghq");
            AddGate("pwp", "OR", "cwc", "z24");
            AddGate("hkh", "OR", "nst", "gnn");
            AddGate("fkb", "XOR", "vrp", "z03");
            AddGate("svb", "AND", "qmm", "jbf");
            AddGate("y09", "AND", "x09", "thj");
            AddGate("hdk", "AND", "gpb", "gmv");
            AddGate("gnn", "AND", "brf", "wfw");
            AddGate("knv", "OR", "nfr", "hdk");
            AddGate("mgc", "XOR", "vvw", "z14");
            AddGate("rbp", "OR", "qvm", "ssv");
            AddGate("y34", "XOR", "x34", "bpm");
            AddGate("qvs", "AND", "kmc", "cwc");
            AddGate("hqp", "OR", "nbf", "gbw");
            AddGate("svf", "AND", "thf", "sqs");
            AddGate("y30", "AND", "x30", "svg");
            AddGate("vgs", "OR", "gwr", "bjt");
            AddGate("y28", "AND", "x28", "pvb");
            AddGate("y17", "AND", "x17", "jcs");
            AddGate("bsv", "OR", "vrn", "vrp");
            AddGate("jpk", "OR", "rvw", "fdj");
            AddGate("x11", "AND", "y11", "z11");
            AddGate("vwh", "XOR", "dgn", "z41");
            AddGate("y08", "AND", "x08", "wwb");
            AddGate("wrg", "AND", "mjr", "tcg");
            AddGate("hbc", "OR", "mfr", "mjp");
            AddGate("knp", "XOR", "mmk", "z25");
            AddGate("y44", "AND", "x44", "fbd");
            AddGate("y15", "XOR", "x15", "mjc");
            AddGate("x32", "XOR", "y32", "bnc");
            AddGate("x04", "AND", "y04", "qvq");
            AddGate("ssg", "XOR", "shm", "z22");
            AddGate("ghq", "XOR", "fnc", "z27");
            AddGate("x10", "AND", "y10", "jmb");
            AddGate("y32", "AND", "x32", "jpk");
            AddGate("y07", "AND", "x07", "qpn");
            AddGate("ngc", "AND", "mqw", "hrj");
            AddGate("x43", "AND", "y43", "cts");
            AddGate("vmb", "AND", "mjc", "skb");
            AddGate("sch", "XOR", "gtn", "z10");
            AddGate("rnv", "AND", "rbr", "nbf");
            AddGate("frv", "AND", "rdj", "rwc");
            AddGate("tjs", "XOR", "ncd", "z05");
            AddGate("qbd", "OR", "jcs", "jps");
            AddGate("prm", "AND", "jps", "hrb");
            AddGate("qdq", "OR", "jnk", "ftq");
            AddGate("x29", "AND", "y29", "jft");
            AddGate("y40", "AND", "x40", "pqh");
            AddGate("gtn", "AND", "sch", "kbf");
            AddGate("wmp", "XOR", "mft", "z19");
            AddGate("tcj", "AND", "mjp", "qbd");
            AddGate("dvf", "AND", "fdj", "pft");
            AddGate("nkj", "OR", "drv", "shm");
            AddGate("jcj", "OR", "kfn", "cpw");
            AddGate("cpw", "XOR", "gqf", "z06");
            AddGate("jss", "OR", "qwb", "svb");
            AddGate("x02", "XOR", "y02", "vjb");
            AddGate("cts", "OR", "wgc", "wrg");
            AddGate("nmq", "OR", "jhd", "vmb");
            AddGate("mks", "XOR", "nvv", "z12");
            AddGate("jmb", "OR", "kbf", "sbs");
            AddGate("vkm", "OR", "wwb", "thf");
            AddGate("qhh", "OR", "jft", "frv");
            AddGate("jpf", "OR", "cdf", "dkp");
            AddGate("x36", "XOR", "y36", "vqg");
            AddGate("nbm", "XOR", "gnc", "z16");
            AddGate("gqf", "AND", "cpw", "knv");
            AddGate("ghg", "OR", "mfq", "gnk");
            AddGate("y38", "AND", "x38", "mdm");
            AddGate("y23", "XOR", "x23", "vgn");
            AddGate("pqh", "OR", "jvt", "vwh");
            AddGate("vtd", "AND", "gnk", "hbb");
            AddGate("x18", "AND", "y18", "sds");
            AddGate("x02", "AND", "y02", "bsv");
            AddGate("ssv", "XOR", "hpg", "z40");
            AddGate("qvs", "XOR", "kmc", "mmk");
            AddGate("y26", "AND", "x26", "ptp");
            AddGate("qdh", "AND", "bhq", "crp");
            AddGate("y12", "AND", "x12", "bbc");
            AddGate("y23", "AND", "x23", "qsj");
            AddGate("y37", "AND", "x37", "cdf");
            AddGate("hth", "XOR", "vvc", "z39");
            AddGate("pkp", "OR", "srb", "cct");
            AddGate("fnc", "AND", "ghq", "pkp");
            AddGate("x18", "XOR", "y18", "prm");
            AddGate("y43", "XOR", "x43", "bjr");
            AddGate("x26", "XOR", "y26", "qmm");
            AddGate("kbh", "AND", "hqn", "jpf");
            AddGate("x08", "XOR", "y08", "gjd");
            AddGate("vwh", "AND", "dgn", "vgs");
            AddGate("x27", "XOR", "y27", "fnc");
            AddGate("gnn", "XOR", "brf", "z04");
            AddGate("kpf", "OR", "vkq", "mks");
            AddGate("frv", "XOR", "rdj", "z30");
            AddGate("y25", "AND", "x25", "qwb");
            AddGate("ftq", "AND", "fqc", "qhh");
            AddGate("gjd", "AND", "krq", "vkm");
            AddGate("x35", "XOR", "y35", "mqw");
            AddGate("y06", "XOR", "x06", "gqf");
            AddGate("x15", "AND", "y15", "qdj");
            AddGate("tjs", "AND", "ncd", "kfn");
            AddGate("nvv", "AND", "mks", "mjb");
            AddGate("dng", "XOR", "pwd", "z31");
            AddGate("mjc", "XOR", "vmb", "z15");
            AddGate("hdk", "XOR", "gpb", "z07");
            AddGate("y36", "AND", "x36", "cfj");
            AddGate("y39", "AND", "x39", "rbp");
            AddGate("y03", "AND", "x03", "nst");
            AddGate("dng", "AND", "pwd", "crb");
            AddGate("x03", "XOR", "y03", "fkb");
            AddGate("mgc", "AND", "vvw", "nmq");
            AddGate("x31", "AND", "y31", "bgw");
            AddGate("y13", "XOR", "x13", "qdh");
            AddGate("wfw", "OR", "qvq", "ncd");
            AddGate("x22", "XOR", "y22", "ssg");
            AddGate("x11", "XOR", "y11", "vbj");
            AddGate("bjr", "XOR", "cjb", "z43");
            AddGate("x17", "XOR", "y17", "tcj");
            AddGate("pvb", "XOR", "cct", "z28");
            AddGate("tsn", "OR", "hbb", "kmf");
            AddGate("y38", "XOR", "x38", "vsb");
            AddGate("x42", "XOR", "y42", "mmm");
            AddGate("bjt", "AND", "mmm", "hgm");
            AddGate("hrb", "OR", "sds", "mft");
            AddGate("x24", "XOR", "y24", "kmc");
            AddGate("vtd", "XOR", "gnk", "z20");
            AddGate("y00", "AND", "x00", "rnv");
            AddGate("wfn", "XOR", "vgn", "z23");
            AddGate("vsb", "AND", "dkp", "z38");
            AddGate("y00", "XOR", "x00", "z00");
            AddGate("mjp", "XOR", "tcj", "z17");
            AddGate("shm", "AND", "ssg", "srj");
            AddGate("dkp", "XOR", "vsb", "hqh");
            AddGate("vrp", "AND", "fkb", "hkh");
            AddGate("gjd", "XOR", "krq", "z08");
            AddGate("vjb", "AND", "gbw", "vrn");
            AddGate("kmf", "AND", "jkf", "nkj");
            AddGate("x14", "AND", "y14", "jhd");
            AddGate("y31", "XOR", "x31", "pwd");
            AddGate("x01", "AND", "y01", "hqp");
            AddGate("y40", "XOR", "x40", "hpg");
            AddGate("nsb", "OR", "hrj", "pmk");
            AddGate("bpm", "AND", "tkg", "cpb");
            AddGate("y19", "XOR", "x19", "wmp");
            AddGate("wmp", "AND", "mft", "ghg");
        }
    }
}
