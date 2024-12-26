//https://adventofcode.com/2024/day/19
namespace AdventOfCode.Year2024
{
    class Day19
    {
        List<string> patterns;
        List<string> designs;
        Dictionary<string, long> cache;

        public Day19()
        {
            patterns = [];
            designs = [];
            cache = [];
        }

        public void Run()
        {
            GetData();

            var start = DateTime.Now;

            cache = [];

            var success = 0;
            foreach (var design in designs)
            {
                if (CanBuildDesign(design))
                {
                    success++;
                }
            }

            Console.WriteLine($"Successful: {success}");
            // 285
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            cache = [];

            long totalNumberOfWays = 0;
            foreach (var design in designs)
            {
                totalNumberOfWays += FindAllWaysPossibleToBuildDesign(design);
            }

            Console.WriteLine($"Successful: {totalNumberOfWays}");
            // 636483903099279
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

        }

        bool CanBuildDesign(string design)
        {
            // If we already know this remaining portion of the design can be built
            // Just return true
            if (cache.ContainsKey(design))
                return true;

            foreach (var pattern in patterns)
            {
                // pattern matches remaining portion of design
                // which means a successful build of the entire design
                if (design == pattern)
                    return true;

                // pattern longer than remaining design
                if (pattern.Length > design.Length)
                    continue;

                // Pattern matches start of design
                // Recursively check if the rest of the design can be built
                if (design[..pattern.Length] == pattern)
                {
                    if (CanBuildDesign(design[pattern.Length..]))
                    {
                        // Add successfully built design portion to cache
                        cache.TryAdd(design, 1);

                        // remaining portion can be built, return true
                        return true;
                    }
                        
                }
            }

            // No combination of patterns found
            return false;
        }

        long FindAllWaysPossibleToBuildDesign(string design)
        {
            // If we already know how many ways this remaining portion of the design can be built
            // Just return that number
            if (cache.TryGetValue(design, out long value))
                return value;

            long numberOfWays = 0;

            foreach (var pattern in patterns)
            {
                // pattern matches remaining portion of design
                // which means a successful build of the entire design
                if (design == pattern)
                {
                    numberOfWays++;
                    continue;
                }

                // pattern longer than remaining design
                if (pattern.Length > design.Length)
                {
                    continue;
                }

                // Pattern matches start of design
                // Recursively find how many ways there are to build the rest
                if (design[..pattern.Length] == pattern)
                {
                    numberOfWays += FindAllWaysPossibleToBuildDesign(design[pattern.Length..]);
                }
            }

            // Save in cache
            cache.TryAdd(design, numberOfWays);

            // Return total number of ways
            return numberOfWays;
        }


        void AddDesign(string design) 
        {
            designs.Add(design);
        }

        void GetData()
        {
            var patternList = "ububwuu, ub, gbwbgr, rrur, ggbuwg, uuubrwb, bww, brug, bgb, ugrbb, ggwuu, uur, wbwr, gug, gbgub, rru, urwbw, bgwgu, gguguu, wbgguuw, buubuuu, bgugrbr, wrwbr, ug, rrww, ugug, urr, ugbgbw, rugg, wrguuw, rwwbwwww, wgwbbr, bbur, wwgwr, rrug, rugugr, bbrw, wbgruw, urwbbw, bgrwgb, rgwgrr, wuruur, bruub, wg, wwguwu, bggbubbg, bu, urb, rugwuuu, gwgww, wubbg, gbb, urw, bgwrb, rrwwu, gurbrg, ggubgu, rub, rgub, buwug, rbguwwb, bgrur, bguuguu, uggbu, rug, rgrw, wr, wbbrw, ugwrwb, bugbubg, uwrb, gwwwuu, rgw, uw, grbrr, ugbwbgg, ubrr, wug, rrbgw, buru, bwb, rgwrg, gwrwubb, buruurw, bwgwg, gwbruuw, bwbr, gw, br, wgugg, gurwb, www, rgggg, rgu, grwgrwr, uru, uwu, wwuugbr, gbr, gbbwgw, wwbbrb, uubg, brgwbrr, gww, ubbg, g, bwwg, rrbb, bug, uburg, guu, rubruw, rrbr, rgwrrug, urbwrub, rubggrgb, uggw, wgu, ugw, bubbw, uwbu, wrgg, ubgub, wbu, bgrrb, gwbb, rgbu, grgu, rgb, wbw, bbbb, gbwbwbw, bur, wu, gub, wub, brg, gbrw, gwu, ru, uugw, ggwbrb, ruu, rgru, rb, ubuu, gurgbu, urww, gru, uwbb, wburw, ugbbrwgr, gg, wur, bwgbb, bgggr, wrur, buwub, uwwguuu, uww, bugb, bbg, bugbu, rubg, wuu, grgbug, wrbg, ggw, uugu, rburug, bgru, bbwuwg, gbubg, urwwgw, buu, wwb, rguu, brb, wwrwg, grb, gwbrgw, ugu, wbruu, ubb, rubgw, grrbr, r, ubbwub, urrg, rgbgu, gbwrggwb, wrg, rrr, rw, guw, wrbruurg, rgr, grrub, rrgguug, rrw, uwuu, grwu, uug, rr, ubu, gggrr, guburg, guwrb, wgwrugb, rbbgu, ugbru, buwwbur, rwwr, wwgr, bwg, rgwgg, gbgrur, wguwg, ggbrb, wgguwgg, bwr, grgur, gbwuggr, gu, urrgrw, uburu, wrbrwbwu, gruwgr, wgbgubrw, gbbg, ubw, grwwrbu, wbgw, rrbgr, rwr, ggu, bgbbwu, ww, ur, ubuuwr, wwugb, urrb, grr, grrw, rwg, wwgurwr, grbr, uub, rguurrg, uurg, urrggbu, rbrw, uwg, rgrrubgb, grbur, grgb, rurg, ugrbr, rww, bwrugu, bgwg, uguw, uwwu, ggrgbg, grg, wwu, rbgr, rggrbwuw, bgr, bwrw, wbuu, wrwbwu, wgr, ugwwwg, gggu, rwwrbrb, rrg, wrrr, rurb, wubruruw, brr, ugubw, gbuuwr, uu, gwb, bwru, wbwggug, bgu, gggrbrrg, wubu, grw, gbu, ruuu, bbwb, bw, wuw, ugrw, brurb, wuwrbr, wrb, brw, rbg, bgurb, uuw, uwur, rwwruwgu, gbuuug, urwrwugr, bgrr, gwr, wgwr, ggugu, wburwrb, rggur, ggg, rbb, gguu, wb, rwwgu, rbw, grrbuuu, rrgg, wrw, wwbg, wwg, wrww, gbug, bwubwuu, wru, ubrguu, wggg, rg, wwwb, ubr, bbw, rgwwbwwr, urbwub, rwb, urub, gguug, rburru, u, rgwr, ruwurru, rggwg, wbg, ugwbbwb, ruub, wbuw, ggr, gwwgb, bbbrru, rbub, rur, rwubbrru, bruuwg, rrrw, gwggww, rwu, rggw, gubuuw, gugbbggw, ubg, wgw, bwug, wwuruu, wbb, ubguub, rgg, brrwr, wwrgb, brrbg, uwb, w, gubuu, uggbbr, uwr, ugwg, ggwbww, bwu, ubgbu, rbbbu, brgr, bwub, uuwgu, gb, bbu, urg, rurwrbrr, rrb, ugbbru, grwrw, wwr, wbbbg, ggurbgu, rrbgrbur, rbu, gbg, rwwwr, ugr, ugrrb, uwwbugu, gbuuwg, rguubu, urruu, brgub, gwg, uuu, bgw, rgrgb, rrurb, uggr, rgbbrg, burr, wbru, wuug, rwgubuww, ugb, gggr, ugg, ruuug, brrg, wuur, wuwb, grgrrb, wgb, bb, bbr, bbrgbw, grgbr, urbb, gbw, bub, ruw, ggb, bru, wbr, wgg, grrwug, uwwgr, brru, gggbgrw, rrurbgu, wruw, wrr, ubbgb, bgguwb";
            patterns = patternList.Split(", ").OrderBy(p => p.Length).ToList();

            designs = [];
            AddDesign("bggwbgrwuguugubbuuubgrwgubgwgugrbuwguuwgugggburgurwbwgbwrg");
            AddDesign("uwrrurwgubgrgwbgrwgwgbgbgwbrgbwwguwguuggrgbwuwbbbwgwrbgg");
            AddDesign("rgwwbrurrrbbwrwuubwubuwrgbbrruuwwgwuggwwgubgbbubgr");
            AddDesign("gbubwggrgrbrggrbgrgbuwuguwrrrrrwbgrrgguugurbuwggwwbrguugbu");
            AddDesign("gwrgubuggbuwbgwbrrgbuwrrrgbrbwburgwrrwuguwuwuwgrurrwwggg");
            AddDesign("rbwurwubrwwuggrrgrbgurbgbgruwbwgwuururwuggggbwrgu");
            AddDesign("brugwwwubbgwgrgbwggrggwugugrwwgrwgwgruwrgwgguru");
            AddDesign("bwuuwurubugbwgwubruruwrurbbuwggbrwgurwbuwwurggwuugbb");
            AddDesign("wwwbrrwugbbrrbgwrwurwbugwggbubgbggwbrrbgwub");
            AddDesign("bubrubwbgbrgubuuuwugrwgwwrbbubwrruwrrrrrrruubruwgburbr");
            AddDesign("rguwwrrggubrbrbuggubwubgwugwrrbgrrrwugwbwurugurbwwugbg");
            AddDesign("ubgbwuggwrrrrururgbwwrwgurwwbrwgwggwgwubrrwwrrwwrrbbu");
            AddDesign("grururbubgwrubbrwwbrwubrrbbwwggwrbbrruubbubrug");
            AddDesign("wbuuuwgwgwgwggwguwrwwrwbwwbwruwugwgbbgruurbrwg");
            AddDesign("ubububbwugrgurrgbrbwruggrwuubbwuuubbrrbuggwgrbuw");
            AddDesign("guwwuwuwbrgugwwbbuwbgwguruurrgrbggbruwrruwb");
            AddDesign("bggbuuwggwrbbwwgguggbwubggwugbgrbwbuwrgguruwubbgrg");
            AddDesign("bggwubbugubugurbbrgbbbrgurggrruubwbbrgrwgbrgbwguwbrbwwurwrww");
            AddDesign("rurrwbggrwwggbrbwugrgrgwurugbbugbguruuwrwrrrgbruguggbug");
            AddDesign("ggwwugwrbggwgrbbuugwgggubrbwgwgbgurubwuuwbggrwugbgwbrbwbb");
            AddDesign("bggwggbuwrrwbguurwrruwbguurgubgbruurbrgbbgwggrbggurrrb");
            AddDesign("gbrwbrwwugwbwguwgurggbwwrbuwbuuuruugbuuggwgw");
            AddDesign("bbgrubbbrgbwurwbwgrgbwgugurbgbgbuggwrbbrgbbu");
            AddDesign("rwwbbgbugbwrrgrubgbwggrwgwruwurrubwruwwurggrrbwwwbgurw");
            AddDesign("wwgbgurrrbwwgruwgggugbwwurwbbgrwruwbbbbgwbw");
            AddDesign("rrwbbwwrwgbbgrgwrubbwwrbuwrgbgrwwugrbgwguburrrguuubrrggbb");
            AddDesign("buwururbrwurbguuguggggbuguugguuwbubbrrgubwbuuwwgwwrgruw");
            AddDesign("bggwguwrwruwruwrrgbrbgwugwbbwugbggugwwbrrurubu");
            AddDesign("bggrbwwuwurgwrrgrgwgguwwgburuwbbbgrruwgbbwruugu");
            AddDesign("gruruurbgrbbwwubwbrubruurrruwrbrwubburruub");
            AddDesign("wwwruuwbwwwwubgrruggbbggwbubwrrrbgrbwrbguwgwububuubg");
            AddDesign("bwwwrubwwuwwwuwuggugrgbrrbggubrwgrgwbrrrgwgbggubuw");
            AddDesign("bggbbggwbwwuwwbgrwrrbrbwbgrgggrggbwrbuwwrwwwwubuuubwg");
            AddDesign("bggrwbguugwubwrgbrwuurgruuuwbrbwgwbwrwuuwwgrrrgrgwwgguwrwb");
            AddDesign("grwbbbgggwubwbuwubbwwbuwubrrrrguuguwugbrrubwgrwrbbr");
            AddDesign("bbgugggwuruwurbrbruwbwgrggugbbrwgrgbwbggrr");
            AddDesign("wrrwwubrbbbwrbugwwwugwwrggubwugubrrrwgbubbguwwbrrgbu");
            AddDesign("rwugwuwubuwwrggwrurgwbwubrbgurgbbubwurwrbrbgrwbrw");
            AddDesign("gbrwuruurguwbbwgburbgugubgguwwruwruruugwgbwbgg");
            AddDesign("uwbrbbrgrubwggggwbrrrurururbwbrburgbbrgwuuruurrbrgbbwg");
            AddDesign("bggrbwguggurrgubrbgrubwwggrgrwrrbrubwrbrgwbgwruuwuwwggrubbuw");
            AddDesign("uuuubgbubbrbrbwgbgrbggwruubwuwbbrurbwwbugbrrwwbuugr");
            AddDesign("bggbgurwbgwwrwbbwuurgwbrwubbuuurwgbruwbubrrwuburgbwbbgugub");
            AddDesign("bggbuubwrurwwgrurwrrbbbrrwwrwbgggwurbbwurrgbwrrrgbbbwr");
            AddDesign("urwgrgubguuggwgwgbwuuwguwgrgubrgwwwwwrgugrrbu");
            AddDesign("bgwggugubugwgrrurugugurbguubbruubwbgrwguuuubgggruu");
            AddDesign("wbububwrubbrruuurguwugbggugrrgbuguubbbggguwguuwwbwbgwwgwww");
            AddDesign("ubbrbbuuubrrwurubwwbgrurbrrrwgwbburrguguwbuwrbw");
            AddDesign("gruguubrbbbrbbwbwwbrwwgwggwwrubgrwggugbrgbgub");
            AddDesign("bggugbuurbwubgbbgrwruwgwbwrgwwubburbuwwubwuwuggubrg");
            AddDesign("ggwrwwuugugwgubggggbrguwbwwbugrugwubwugbrbwbbbgb");
            AddDesign("bggbwurrbgrwbwuguggwbbwwuuuuwugwbgwubggbwuurwuugwrr");
            AddDesign("urwuuubwuwrbuurbbwrrbbwgbwgrrurwwwwbwbgbwu");
            AddDesign("bggbgbubbggugwburwrrgwwrrubrgrggubwrbbggbbubww");
            AddDesign("rurbrbubburrwbuuwwwgwrwwwwuguwbwgggwubgwbg");
            AddDesign("uuugubwggwwrwubrguwurbrurubrbrrwwrbrrwugbwbggwrgbbrrbgbru");
            AddDesign("bbggbgbgwwgbwwwbwrwubbbgwgrbrbrubgrguwgwgrwwgu");
            AddDesign("ubbwwwrbuuuwguwrwwbrbruguwurggurwuwwwbbburgbgw");
            AddDesign("wurgrwwuururrbugrwburugrbbwuurruwuwwguwrugbruguwrrrgrurw");
            AddDesign("bggwbrwggwrrgrbwbgwwggwuurgburuuwwubwrburggrrggru");
            AddDesign("wrgbwgwruwrwurruwgggrgwgwgbwrrruubggbgbggubg");
            AddDesign("rbwwubgbrgbuugubbwbgggwwwugwwrugurgruwuuuuuwwbwugguuguwbwr");
            AddDesign("bggbuggbrwbgwubwgubbrgrugbrrrugbbrbbuwuuubrwrbgbuguwwgbu");
            AddDesign("bggrwburrwbwrbrwbwurrrruugurgwwbbrwugwururbwwbburrbgrrrgbr");
            AddDesign("bgbwubwwggrrrrgugbbggwbrgugubbbggggbruubggrbbrr");
            AddDesign("gugrgbggbrwggrwuuuuwrbrbrwrrggbbrrwwbugbubwburbwuuguwugr");
            AddDesign("wbuggwbbruwwurgwbrgbwubbbbgrwwwrbwbrbgggugrrwburrb");
            AddDesign("bgguwgwbbwwubugwurrrrwrugwbbggguugrgwubbwrguggbbwu");
            AddDesign("rgwwubbuwwubrruwbggwwgbruuuwwbuubrrgwurruww");
            AddDesign("bbbggwgwuuggurwwwggurrbrwwwbwrbububbbwguwuwgruubgrwrubbbbwgg");
            AddDesign("bggbguggbwrugwwbgubrwbbrbggrrrwwggbwrggbgubbwrwbwgwrwubgrb");
            AddDesign("bgggbgrwgwrguuwwrbrgrrwgrwgbwbrwbwguwgrrwbuwwuwwgwbrrwg");
            AddDesign("bggrbwurrugurwbgwbubrubrugubbubgwwrbrwwrruur");
            AddDesign("bgggwrbugbbrruwbbbuuugwruruwbugwwuguwubuugubgwuwgbrrrubr");
            AddDesign("gubugbgbuuuubgruwrrgurgrwrbrbugwwwwgrbgwrbuugrrbwrwwrw");
            AddDesign("bbbggburrwggrrwrubgugwuururwgwwrrgbwwgbwwwrwrwbwwwbugrubgbwb");
            AddDesign("bwbrwrbbgurgugrbugwbgruuwbuuuuuwbwwgbubgbwuwbg");
            AddDesign("bggwrggwurwugubbrubgwubgrrugrbguburgwugbbrwgrbwwugbgbuwr");
            AddDesign("rwwurugrugrugbwbwrbrwwgwuggbrurruwggrgurbrwb");
            AddDesign("bggrbbgruggbwrguubgrbbgugrgbrbrrbrugwwbuggbgbuwbwuw");
            AddDesign("bggubgugwwgguwbgwurrbgwwbgbbuwgwurwbbwgbugubgbrr");
            AddDesign("rwbrrbwbguuwuwrbbwgugrbgwgrwrbguuuwbbwwwubgrbwbwwwuwrub");
            AddDesign("bggrrbuguuuwrrwbrrbgrguugbruburwrgwwbwbwwurgrbrbrggrbwbubwb");
            AddDesign("bgguuuugwurgurbwrrggbbugwurbgubrurwrbrrb");
            AddDesign("bwggwbbrwbbrbgrrrbwuwrgwbbgbguwbrgugbuwggurubbbgb");
            AddDesign("bbgrububguwwruuwgbuwbbwrgrrubgrbgrbuurbguwwwbbbrggbbw");
            AddDesign("gbgbgbrbbbbgrgwrrwgwbwgwurbbbubuurrgubbwwwgwrgbbwuwb");
            AddDesign("bggwruwrrgwggbgbbggbuwbgugbuwgbrurwwrbwwrgu");
            AddDesign("bggwrbggggbwrwrrguwbguburbwguwuggrwrgwwwbguuwrbwbgw");
            AddDesign("bggwwbgggwrrbwuuwguugwbwubrbgbbwubruruwrruwrguuubbubg");
            AddDesign("bggbrbbugbuwrubwggubuwwgwgwggggbubrrrguuuwu");
            AddDesign("rbrrwguwbubgwuurwwbwwrwwggbbbugwgwbuggurugwwbuwgr");
            AddDesign("bggwwwgugbbggwggwuuwrrrggrgrwrbrrgbrugurwugwgubbbgwwbbwr");
            AddDesign("ubbrwwruwguurrggbbwwgugwbrwbrwwuwwwrgbbbwrgubr");
            AddDesign("bgbwgrwbrgguubbbbbgbwburrrwrururwuuwuburrugugbwg");
            AddDesign("wuurwwgwrurwrurrrwrruubruuwrgbbwuurwbubrwuruuuubuub");
            AddDesign("bggurwwuugbrbugrbbrgrbwbrbbrurrburrwubbrruurrwgwgurggw");
            AddDesign("uwrgugrbrrrbgwuggbwuuuwurbrwuwrugbgruwurubwwubgwwrw");
            AddDesign("gurrwrbbrurgurbgburrugggruwrrwwwruwuurrrgubuuwurrwbbw");
            AddDesign("rurubgggrbuuwwwubgbruuubrubrgurrgburbwurrrubwrubgu");
            AddDesign("bbbggbuuwggrrwrbbwbbugwwugwbgbgwwwuuwgbuwguruwwbwgrrbbgw");
            AddDesign("rurbwgugurbuuuwrrwgrurgrgbbuwwrwguuwurwbggrbgwgggrrrbrbuuu");
            AddDesign("gwburggwurbrwwgbrgbgbbwwbwrrrwgugwwwggrugubruu");
            AddDesign("uwwurbuuubrubrrubuwwuurrbrgbgrubwbubbgwbuugbu");
            AddDesign("bgggwwrrguggrggbrbgubgbugugugrgguurbgrgguuguwgbrbr");
            AddDesign("uwubrbbgrurwgbbwgbugwrgwbwbrrgbwbgrurwuwruwbrrwbggbw");
            AddDesign("bggbbbrgwbwrrgbwwwggwbgubrrbrrrguwwwbbbbbgburb");
            AddDesign("bggwuwuwgrbubgwguubgwgugbbwbwbgrgrwuwuwwrwww");
            AddDesign("wggggbwbrrwwwgubbwrgbuuurruwggrrwggwggruugrbg");
            AddDesign("uwrbwgrrrguwgbugwurruurbubuurugurwbggbrruurwwg");
            AddDesign("bggwggbwbubbbrurrugrrbggwwuubburwgruwruguubbuwbug");
            AddDesign("bbrrwguwwuurwwrggugggrwuuugwwbbgwrubrrubwbggwrbwbwgugb");
            AddDesign("grrgurbbubbggwwrwuugwwbwrrrububrbgwbwruubuwwwurwb");
            AddDesign("gbrwwbbrruwbggbwgrwrugwuuwuuururbbgrrgrrurrrg");
            AddDesign("gwbggbgurrwgbubwrbgbbbgbrgwrubbrrrugbggruugbwuu");
            AddDesign("bgbwbwgububwwgggbrurwuwgbbwurbggrggurugwgrrrwbruggbrrbw");
            AddDesign("bwwrwwrbbrwuuuwwrugrwgbbbrwbbrbwwubuurbgwwrgugbbbrg");
            AddDesign("bggwgrbruwugburbwggwurbwrgwggbbgrrgruwbrwuuurwgburbrgbugwr");
            AddDesign("buuuubbbugrugburubrggwugwurgurwwuwurbrwbbbgrwbwb");
            AddDesign("ubrgrrubgbbwubwwbrruuuggrgwbbwuwrbrburbubbgugugbgwuw");
            AddDesign("wrwuwrwwugwwggrwgwbuwwrgwguwrrbuuggurgwrggg");
            AddDesign("ubuwwbrbbbbrgwwggruugguggwgwwwuruggurgwrbrwgbbrbbrr");
            AddDesign("bggggrgbubwbgrwbbrbuwuwrwggbuwrurggbruubrurruwug");
            AddDesign("bbgwwgwbgrbrbwwuwwbbgrwrrgrurwgugbbrubrwgrugrbbrrb");
            AddDesign("wguwgbruwuuggrrubbgbuurgbuuburwrwbruwbwbgbrgbwwurbr");
            AddDesign("wbbrgwruwruwbggbbwwurwbubrwggrrrugwrggrbwuwubuwb");
            AddDesign("ugguubrrgrbuwbrrruggwruwrgrurbbbwrggrurgurwwurgrwwu");
            AddDesign("burruwrbwuwbrbgwgwgburbuurwrwbbuwururguwgurgwgbwbwrugru");
            AddDesign("wggbwbguwwbwwwbrurgggrwgubuwwrbrurgwbbwwubbw");
            AddDesign("bggrbbgrgwwrurugwbgrgrururwwrwbwbwuggrbguwbbrrbwwuubrwrg");
            AddDesign("gwubgwbbgwubbwrbwwwbbwuugwbwbbggrubgwbrgrbwwgrbwwwbr");
            AddDesign("wggbrrrbbbgbrgbgwbuwgwurgurbbrgrgbrgbgrbgrrbuwbwu");
            AddDesign("grbwubuuuguguwrurwwrgwuwuggbwbwgrruwrgwrbrruwrrb");
            AddDesign("bwbbbbbgubrbbugwrgrbbbrbrbugbuuubwwgbbgwwuruwwrbruurggg");
            AddDesign("wwgbugrrwguuuuwrgwwwwwwbgruwggruuuuuburbgbugurgwurruguubu");
            AddDesign("rgbbbuwuubuuuugbrrbrwwubuwubrurwwrrwgruwgubgw");
            AddDesign("bwgguwuwgbrgbrgwbuwrgbrgugrrrgwrwrrurgbrwwbbguuwuu");
            AddDesign("brubbgbugwruubgruwwbwurrbuubuwwuwwuwgbugruwrgrgbruug");
            AddDesign("bbrwguwwrwuurgrwwugwrguwubuubrbrrgwgrwguggrgwu");
            AddDesign("bwurrbbwubgwbgwwubgubrwrrbgrbrwguguwgbgrbwuwbggwuuwbbw");
            AddDesign("ruwruuwgbbwrwruwbuwgrwbuwgwrubbbgrbwrggggrg");
            AddDesign("gwrrruwwuwgwwgwbgwuubwuuruggwuurrrbgrguuubuu");
            AddDesign("uurbgwrrguwggurbwbwuggrbrgwuuwubgruubbgubb");
            AddDesign("bgguugrrurrugrgwrubwbrwruburrrrggbrgbbuwrwbrbguuwgbbrgrrbrrr");
            AddDesign("brwguwugrburubbrurrguuggrbwbwbwgbbuwbgwgbbrb");
            AddDesign("bugwbuwruuuurbrrbrrrrrubbbuwwugurwbgbubbburwrgbwrugwrgb");
            AddDesign("bggwuubbubwurgurbbguuggggwrgbbguuwrrrggrgrbgwb");
            AddDesign("bububugrrbrwbrurguuwrrrwwbwuubuwgururgrguubgww");
            AddDesign("wwurrgwrwwurgruuwrbgurwgrwwwugbgwuuuwrrbwuggbrrwbgbruuu");
            AddDesign("bggguwwrwbwurubgubrgrbbuwwugrgurgwrbrgbgrwwwbrgbwrgwg");
            AddDesign("wuguguugwruwuuggwgbggugrrburbugrbwrugguwwwggbuugu");
            AddDesign("bggwbbubrrwrgbrwrgubgwrbwwwurrbwrrbgwwubbbuwburugbru");
            AddDesign("uggbrrurwrwugrgurrwugugrrbwggwrgbbrbwuruggwwugrguwg");
            AddDesign("rbrbuburuwuruugrgbrbwburguwuuwubbbwwwgwuubwww");
            AddDesign("bggubrbwgugggbggbubbgburgwwwubbrrguuguuuururrrwrbbrgr");
            AddDesign("bggrwrrwwurrruuwwgwuwrrbrurbuubrgbuugrbrwwurubwuw");
            AddDesign("wgburubrbrrgrguruggwwgugwuuwwwggwgwwrguwrbwwuurrbgrburwrg");
            AddDesign("bwgburbuuuurbuuggruuwrrruwwuubbwguwgggwbubgwggrwug");
            AddDesign("rgwgbuwgugwguwgbrbggbubwbubrurwwwguwuuwgrugr");
            AddDesign("rbugugwggrgruwuguuurrggruuwggbburwgugrgburbrgwugwugubgb");
            AddDesign("burruwggruuubbrbggbwgwrgurwwggubwbuuugrbrg");
            AddDesign("bgbrrwwbuwwuubuubuubbbrbubuuurggwuwwwgbwbwurw");
            AddDesign("gbrgurgbwbgububbgbbgburbubrrugurwgugwgubugugrwgu");
            AddDesign("bggwrrwbbgggrbrrguwgwggugbbgbuwbburrrbrgggruugruwgwbbuuwwu");
            AddDesign("rrururubrbbrrwgbwrwbggbbrwgurbwrubwwwgbrrbwrwgbugbwrgwgbb");
            AddDesign("bggwugrbgwwubguugrgrbbrurrgrrrgbrugugwur");
            AddDesign("wgruwwggrggrgrubbrrgurwwbgguggrruuwuuwrrwrurwrbrrgwbbwuuu");
            AddDesign("bggbguwguwrgbrwbbgwrrubbgwwwbrggruurwuwwgbbbggrwrgu");
            AddDesign("rwuubwugguugbrggwuwrwbgwuwgugwbrugwrrrbwuubrgwuugbg");
            AddDesign("uubwwwggbgubbwurwugwururggwrrbwgwgburrruuur");
            AddDesign("bwbwubggbubbgwguwrbguwuwgubbbrgwbuwgggbubgbuugrwbbrbbgu");
            AddDesign("urrgwbugwbwrbbwgrggrbrguuwwwwruruwruwrrwurwgbuuwbguurgbg");
            AddDesign("rugwuuwbuuwgwubrgurwwrbrwbwurbwwbgwburuuuggbwwubbguwrbb");
            AddDesign("ggbgubrruuuurwugubgruubrgwrubrgrugbruggwgurrrwwruwrgwu");
            AddDesign("wrrrbrubggrgbugwuuubwbwrbrrgrgrgbwgbbgbwrugbwwwgbggrubuur");
            AddDesign("bggwuurruggwwbwuwwuurwbrguuuggruuwwbrurgbwuurgurrwrguruw");
            AddDesign("bbuwwwbwuwrrrwguuurbugugwwgwgrwuruwrubrgrugurrbubg");
            AddDesign("wugwgwuwwwuwubwrrrrruubuuguruguggbburuubgurwuuwubgwugbubw");
            AddDesign("uurbugubbwgggrgbgwbbbbbwubrwrbrgwgrwrwrgugrgbuug");
            AddDesign("wbgwuwbbwrgbwgwrgwuurbbugbwbuubwbubgwgbbbgbbbbbugrrrwbgb");
            AddDesign("wrbbgubbbuwrgrguurrrwrugugurwbbwwwrbbgggrbrrrgbu");
            AddDesign("bggbgugrbbgbbgwgbbrgrrbuwggwuurguwgugrwugugbugwwuuw");
            AddDesign("bggbbwgurrgrurbwuugrwugubrrwwrbbbrburbuubuugbgrw");
            AddDesign("bbgrbwgrrbuwwugrrwrrwguwwgbgggguurbwrggwwwgugggg");
            AddDesign("bggruuuwwbrbrbrbbbuwgwgwrwrbruurgwwgbgburubwrubgrgwuwrb");
            AddDesign("urbbwrrgbwuubbrgrbugrbguubbubuugggrrrbwgwwguurrbwgwrur");
            AddDesign("uugrbbwbbgrrbbbuwrwrgbgbggwwuwbwwwubwgbrgug");
            AddDesign("guubrubrururrrrubbrbbgggrgubuggbrbuubuguuubwg");
            AddDesign("bggwurgggburgwrrwurbwrbbggguwwugggubwwwgbrwuuwuubggbg");
            AddDesign("wrbgbwbwwwurwwrrguuurgwrrugwububbuwrrbgurgbwwww");
            AddDesign("bbbggugugwuuwugbgrbbwurbrgwgrgbbgrgrwwugwgrguwbr");
            AddDesign("rwwwrgwwbwwrrrwbrgwrwwgubgbggbgbubggbwbgbugrubbbwgwbuwwrbb");
            AddDesign("wguubrrrgwurwwuwwbrrurbguwrgugrubwwuwgurbbwrr");
            AddDesign("uwgbrwgrgbwbwuuggbrurgguwwwwrgbrruubrrbwgrbugwrw");
            AddDesign("uuwrugrrbgrrgwrbbwggbbbuwrbgbubgbruwuuuuugggw");
            AddDesign("gwwubbwrwgwgugwbubrguwbbubuuggbrgugwugbbwbbgggwwrggrg");
            AddDesign("buuruuurbwwrubgrbwggwugugugwuwrugbubuguwubruwrrwg");
            AddDesign("rrwwburggrrgwgrwwbugbrrubwwrrwwbwrwwggwubgwwgrb");
            AddDesign("uurrrwgrgrubrbwrwbgbwgbbrbruubwwuggbuubrbburb");
            AddDesign("bgggburbwbrguububbwubbbrwrubburwwbrbbubwubrbwgrrbbrbgrruw");
            AddDesign("gwbwrbrubbgrbbwwrbwwguwgwgrbuubgbrbruurrrrugbu");
            AddDesign("bggbgurwbuwrwgbrwbgbwggbwugwwubwuwwgburuuwuugwwwgu");
            AddDesign("bugurruwuuuwuwburwbrrggbguruubrwggrgubwugubbugrbgwwbgw");
            AddDesign("bggguububggwwuubwgrwwbwbrbwuuggrguurrurgwugbrrbwbbgrb");
            AddDesign("uwbwurrwgubbrruurururbgbbgrbguwgubrrgguwuggrrgbbr");
            AddDesign("wuuuggrwgwrwurgruuwbwuubwwgugwgwugrwruuurw");
            AddDesign("uguggggrgwbwgbwrbbrruwgwuguubwbuuwwrwugrubrgbruwb");
            AddDesign("rbgubbrubbrgbbrggrrggrubugbwbugbgrrgbgrggwrguggbbrrwr");
            AddDesign("ugbrwuwrgrbbugwrbugggwbwbbbgbgbgwugwguuubr");
            AddDesign("gwgrrbbuwrggubrugggbgwuwbrrwrwguubrrbuwrbuuuuwww");
            AddDesign("rbwbburwgurruruggrrwgwgwbrgbrwwbgbbbbwwuurggwrggruggu");
            AddDesign("uuwwrrggwburrbrubwgwrwgguugwbwwuggrwwbwgbwb");
            AddDesign("gurgggubbbwguuubwrbggwbguggugwguwgbguwrrgbgwbgggr");
            AddDesign("bggbrruuurubwwggbburrbbgbwwwurwguubuuwgbuwurwwrwwgguw");
            AddDesign("wwwurwgubwbuggurubwuwurwurrbrwburwbbgwruwwrgruuwuuwrgrr");
            AddDesign("bbbggrwwururubuuurguubrbuwguruwgggwgggurgrgruurbggwuruubbbrr");
            AddDesign("bwwuwugrwuuuugbrgurrruwbwwrubruugurguwbububgwu");
            AddDesign("gwuuwuurrbrurrwgbggrurbugwggwwrwbburgubguugrgrbwgbuwwr");
            AddDesign("bggwgbbbuwwwbgurwbbruurwubugburwubuugwburrbgbwwrbuwbbwuwur");
            AddDesign("bugbgrbwububruwuugwbgbuuubuwwrugugrguburrbbgbgrrwwgrwg");
            AddDesign("bbbggbugggwgwbbruugrggwbrwrwuwuuwrrwwgggburw");
            AddDesign("wrrbwgbuugugbwrrbgbuuwrubrggbbbbruuwuwbugwgrugwrbub");
            AddDesign("bggurbgubwbrurgwbggrgruwwurgbgugwbbgugbuggwgr");
            AddDesign("brrwuugwwrurwwgwbbrbururgwbgrwguurggbrruubwwubwubbggwgrwr");
            AddDesign("rgbwrburbrrbubwugrwuurbgrugwwgrgurwwugwgbubgggrrwbwuburg");
            AddDesign("wgrbgwubbwuubrbrgrrugrwwwbbrbuwrwgbwwwbrrrgwwbggu");
            AddDesign("gbgrrbbwrbrbbbubbuwbgugwuurgwburwwuwgwuuuwbwubr");
            AddDesign("bggwgwbbrrrrubrwurururrrwbugubrrgggbrggwrwwggwrbugw");
            AddDesign("gbuguugwgwwggbuuggrurbruuuguwrgubwgrugubrgubwu");
            AddDesign("gugrgbgrwwwbubwrgbwrwbwurubwggbwgwwbrbgrubuu");
            AddDesign("urwrgurwgbwbrwuuggrbbrgwubwruwbuwurggbbwrbwu");
            AddDesign("wbuwguuwbrgrurbgggbbuurbbwwuuuwbwbgruubwuru");
            AddDesign("wggruggwwuwgugrbgbbwggguuuugwwgbbuwwgrrwgg");
            AddDesign("gwrgrbgwrrgwbbgugubbwwwrugbbbwrwrbbwrwruwgu");
            AddDesign("bggrbgwwrwrwuurwuggbrwbbrbbbwwbubbwggrgrrwbw");
            AddDesign("gubbgwuwuggbwwwguggwubwwrwrbwubggbgrrgwbbubbbugwwwbrwburg");
            AddDesign("gbbwwgrrbrgwguwgwbrbgrggrgwruuuwgbwurubrbgrrb");
            AddDesign("bggrrrurrwwrurrrwrgrubuwrwugrgbgwggwuuwuuwwuwrwwbwwwwbbggb");
            AddDesign("rrugbgbbgrwrrubbuuwuurrubuurgurugbbbrwgbugwr");
            AddDesign("uwrbuwbrgwubrwbgggrbrwbgugwwwrwruguwrggbbggrug");
            AddDesign("bgguwuwrurwbbbwbwwgrrgrbrbwrburbgwurugguruwbruwwbbw");
            AddDesign("bggbggwubbwburwrwgubwurububrugbbugbgrbgurugbgrururg");
            AddDesign("rrrggwwubwwrrwwrrwwbuwgrwwwwwugrggbgurgggrrrbrurgbbuuuw");
            AddDesign("urwggwbggburgbwuwgrguwbbbrgbuwbuuwuuguuuguwrgww");
            AddDesign("bggururbbubrwugrurrrubgubbbuwwgbwwwwbwrggbbrrwwgbuurrwr");
            AddDesign("grgwbrrurgwbrwwbguuwgwwbwbubbwwrbwubwwbrgurwrruwgbgrbwwwbw");
            AddDesign("bggbrgugbbgbwrwguuggwuwrgbugrgrgbuubrgwgwgwrgwbrggbwbbggggu");
            AddDesign("rrruubwrurrbbuwurgrwggwugrwruuwgbgwrwgwburbururbwrbrwgb");
            AddDesign("grwggwbgburrwgrgwwrbuwrwbrbrwuuuwurrwurrwbbg");
            AddDesign("bggguruwwugrggrwbgwbbgrwrrguwrgubrguugwbwrrb");
            AddDesign("ggurwbrurguubbggbgbuurgbwrbbgubgurubugbrbuwrgbwrrb");
            AddDesign("wbbbrubbgggguwrbwrbrruurgguwguwbruwggrgrwruuubbuw");
            AddDesign("wbrwuuwwbbrwbrbrwgbruwubuwrbuggguwgwgwbgurwguurgwrurrggbg");
            AddDesign("bggbuwbbgrgwgbubgwgbrrgurrbwwbgugbugbwrgrguugbruwbu");
            AddDesign("wwbguuuggwrrwurbbgwbbbugbwuwbbwguurwrugrugggbuuubgurwwg");
            AddDesign("grgurwuubgbrrrurwgwrrruuwurwrrgbgwwwwurubuguwgubbguu");
            AddDesign("rrbbgbwuwgrgurbubruugrggugwrugggubgwwrwggrrbr");
            AddDesign("rgbgbuuwugbuwbbbrrrwgggrrbbrgwurgurugrgurwbbrr");
            AddDesign("bggwgrrwwrruwrrbbubgwugbbrggwbbugbuuurbwgrrwrwbuurugr");
            AddDesign("uuuwubrbwgwguuwwuubwgwbbwbgrbrggbubwggubrgurrbgwb");
            AddDesign("gbbbgrbbrwbwwwurrbbrugbbwwwuwruggrwwwbgrrwwuuug");
            AddDesign("bgwbgguggwgugrrgguubbbwrggruuuwgrurugwggrbrbbrggururuwb");
            AddDesign("wbwugubwwgbbrwwrurrwgwuwrgwuwurruwrwbrgrwwrurgwrrgbrbwbub");
            AddDesign("rwgrubrwgbuubwwbrruugbuwggubugwurgruwwwuruguwggwbug");
            AddDesign("wgwrugguuubguuuwwuwbbgwrurwbrbguwrgrgrbrrrggbwwbuwwbb");
            AddDesign("rbwwrwbuwwbgurrgwbrgrrbuggugbbugbgwgwbwbwu");
            AddDesign("bggrrubugrrwubwrrbrrugrguubwwuurubuwgrurbbuubwuwrurgrgbu");
            AddDesign("rbwugruurwbgurwwbgwurrurgrrubgbgguurrbuuuuwwwb");
            AddDesign("bggrguwruurwgrbgrgrwubrbwgbbgbwbrwbrwgguwwwburgbb");
            AddDesign("guwggbwgubuubwwwugrgguggbbuugrwwugggrgrwwruwuggwwrbuurbw");
            AddDesign("wgwbburwrrwrugubwrbrwbrwburuwubrbwwgurrbuwub");
            AddDesign("bguwgwbgggrbrrgrgbrrrurwwwwbwgwgrwwuurgwugbu");
            AddDesign("rwugugrbrrwrguubrbbuurgbugrwuburbrrubrgwrbbgur");
            AddDesign("ggwrwrwuuwbrbuwruwwrbbwrwugwruwbgurrgubbbb");
            AddDesign("bggbbrgguruwgwbuwbwwrubbgrgwubrbrrwbrrbburwwwrurrggrb");
            AddDesign("wurwrwbuwgurwwgwgwbwbbrwbwbububwwwrrgwwrbgwubgubrrgru");
            AddDesign("rugbwwbbuwgruwrgwgubwugwbbgbbgbgrbwuubwugbgwggurwurwwuu");
            AddDesign("brrrbrrrggbbrwuburgwurrwwbgwrggrwbubggrbubwuurrrbwugrrrgg");
            AddDesign("rgurrbgwwwbwurwbgggugwguurbbwbgbruwrgwruuwggwggruwrugrburu");
            AddDesign("bggugrgurwuubbuuugbguuwgrgrrwrgbwrrgbguwwgbruggwgugu");
            AddDesign("bbruuuwuguubwrbwuurwwbbrugbrwbbuubbbuwwgurgrwruubwwguu");
            AddDesign("rwbgrbwgubugurwrugwuwrwrgrggugwuuuwubwwwbbbwuggrrrrwgbg");
            AddDesign("rbgguggrburrburuwwgwubrgrgbbwrrguurwwrwbgb");
            AddDesign("wwrgrurbrgbgwugbgbguguwwwubwbubrurbrbgwugwgurb");
            AddDesign("bggbbbrrwuguuuwuwwuugwbgwwbuggwrrrguwrrgrwwbbgbrubbggu");
            AddDesign("bgguggwgurwgrubwuuuuurbbugbrbbbubuuwubbbwbgwbuugrg");
            AddDesign("burrgugrugbwburwgbgguwrwbwgwgwbuuuwrwbwrgubrbwrwwr");
            AddDesign("rrbbuwwgrwrgurgbbguggrwrbrrgugbwgbuwubgwuuuwguwwguwurrgw");
            AddDesign("rbwgbgbbgbrruurrbrwwrrrbwgbbgwubbwgbbwrrgubr");
            AddDesign("bggrwrwbbrbrwgbuwrgggruwugurgurgrrubgbuwgrgburbuuugrgbrw");
            AddDesign("gubrbgrwggrggbubbbbuwrwurwrwgubbgbrwbgwbwwbubgwwwggbugw");
            AddDesign("rwuggwgburburwugubrgwbuwgbubrgbugrbwubrbwwu");
            AddDesign("uurggrgwbrwrgbuuggbbggbgrwbwwwgurrbbuwgwbbugu");
            AddDesign("rwuwwgwubwbubguugrguugguubgrwbrrwgbwbgwuwwbuuwurbwb");
            AddDesign("bggrrwrrwrgubwgwubwguwugububbbgbguwbwurrubgwubbrwb");
            AddDesign("bggbrguuururrwrburrwwugruwbruwwugwguugwrwgburuugwgwrbwrbrwrw");
            AddDesign("ggbwrguggguurruuwgrrubgubbuurubwuugrbuwwbwbbwbwrbuwgbrwrrg");
            AddDesign("rbbgguburrwurwwuuuubggwuggubbwuurwrgbbrwwbguuww");
            AddDesign("ggbruubbwburguurbbuugubrwuuwubwwwbgugguwuubrbwrbgwguurg");
            AddDesign("bwuwbbbbbrbgbwubggrwgwrgggugwburwuubwuurbubwrrwgbr");
            AddDesign("grugurrwwugbuwggbbrurrwbbggrbgggrbbbwurgggbbbbbugrggrubugw");
            AddDesign("wbguugbuwuwgrrbgrrwgrrugubgrbwugubuwbbwubwwwruuwwgbgbrgg");
            AddDesign("bbbgwbggurbbuububuuruwguurrgrgrrrrbrrgguubguubb");
            AddDesign("wuwrwrggrrbubruruubububwrrubrgbbburbwgrwrwwubbwgwgwwgru");
            AddDesign("rrwwrrrubrguubwgrrbbwwwgbbrbgrbgwugwguwrbwbgbrwuwubw");
            AddDesign("uurggbrwgrubuwubgwruguubguwgburrrrgubbgwwgbwubggbb");
            AddDesign("urruwwurgbwgwrrgggwgbgbgurwbrwbwrbbgbubguwgrbrugr");
            AddDesign("bggrgrbwgwbwugwgbwrwbrrwwwrwbbuggburwbwwrrbwuuwrurubwbr");
            AddDesign("rwbwrbwgrgrurrwrwwbbbbrwrbgbbuwgbwrbururrurrrwurgbbrbuwrb");
            AddDesign("wwbwbwrwrubrwwwrwwgrwrwbuugrbuurruuuwgbugwbwggbbguugrwbr");
            AddDesign("ggugubwbrguwbbggrwguwggwggwwbbbugbbrwbrwbgu");
            AddDesign("wbrguwwwrbbbwwurwgwubwggugwbrgurubwurwbrbgugwgub");
            AddDesign("bggwuwbbrugrwgruwbwrrwubbbgwrgbrgrrggrggrbrwbuwgrwgrrgg");
            AddDesign("bgggwwubwgurguwrgwgwbgbrwuwgbbggbuwgrwwbbuwgrurwrru");
            AddDesign("ugurrrbrggrwruuggrgurrurruggwgwgrbrwugugwurbgu");
            AddDesign("bgguuwuruwrwuugwrbuurrbubgwwubbuwbwgrrbrwrwg");
            AddDesign("rwuwuruuuwwubwrwuwgwrbbubuuuugbubwwbbwubuwgbub");
            AddDesign("bbrgrrubbubuwrrbwurbuggbguguuwwgwbbwburuugugwrbugru");
            AddDesign("gbrrbgubgrwbuwrrrwubbrrubgbggbwurugbgurwbb");
            AddDesign("bbwrbbwwwwbwurbgrwrbwggrrrwbbwubuwgrurgrubbugbug");
            AddDesign("wbruggrwwwgurwbwgbbgubgggrrbuwgururbbbwrbuwgbwrg");
            AddDesign("bggbbwgwbugrwgurbbrwwwuwugrggbugruuggbubbbuuburguwwg");
            AddDesign("wgbbbrgrbbgbrrbuggrrgwwrgrgwuwgrwugrrbbugbbguwuwgubruug");
            AddDesign("bggwgrrbbrwuguubwuruwgrbrguurrwwbrruurburbrwurbwu");
            AddDesign("wrbgrbwurruwbgubbubgggurbwwuggrgrbgubwrrbrrbbwwgubgbrw");
            AddDesign("wrrgbwwrubuwubrbrrgbwggwrbggrbwbwbrbrwgrrgrrwgubwugb");
            AddDesign("bggurwubwwuugbuwugubwrgwrbrguuwrrrwubwrwurrrbugugg");
            AddDesign("brurgwwurwbuwrrbuuugwgugwwgrwggrrrbbgrggbwuuwr");
            AddDesign("gwbrrbwbrwwuwurbgruurbwubrrwbbbwwwgrguwrrwubg");
            AddDesign("ruwwgguurgrrrwbrbuwrrgrrugbbrbgugruwgrbuugrwuwwbgbbru");
            AddDesign("wgrwwwrgbrrrrbbwgubuggrwbgbubbbwuugggurrrbbwwururuwugbb");
            AddDesign("bggbgurguwgurrbgrburgrrgwrurwwrubuurrruw");
            AddDesign("rbwggbugwbbgubububwrwbbrurwurwwuggrbrbugrwwrburgubugbubgr");
            AddDesign("ggbrurubgwwwubburugwwbugbwrggwbggwbgwwrrbwbrwbr");
            AddDesign("bggwrwrgbubbubuwwgruurwugwwuubrbwgbbbbugrbbbbg");
            AddDesign("wuwrwggwubuwgbbrwurggbbwbwgguuwurgwgbgbrwwb");
            AddDesign("bbrwggugwbgggwrgugrwgwburgrbgrubruggwwubgwww");
            AddDesign("bggwwugrbgrrwgubuwwuurwwgbuggurgrurwwuubgr");
            AddDesign("rgwwuurubugrwbwwggruwwgbbugrgggwrbgrgwrwgburgbbuugwwwg");
            AddDesign("bubgubwgwgubuwwwbggwgwburrwrubguwwruugbbrbgrurw");
            AddDesign("rrbwbwrbrbgrbgrburubrwwwuwgbgggbrwwubwrubwguwbbbuubwuwbuuu");
            AddDesign("ugugubwuuuwrgbwuggburwugrwbuururbgbrgbgwrrbgbuuwbwbbbrruu");
            AddDesign("bggwburgurugwubbruburwbrubggrgbubrbbugwubgwgbubwggrrwbrwbbg");
            AddDesign("gbbbbrbrurbwuwbrgbguuurgruurruburgbugbgbrbggw");
            AddDesign("wugrrrgggbrgbbbrrrrwwbwwwwgrrgwwbgurwuwgugrrru");
            AddDesign("bggggrrgbbwugbrbrbrgugbbgrwwurbbwuwggggbbuuuuwurwrggwwwwruww");
            AddDesign("bggrgugbbwgbgubrwgugwurugububbgwbubuuguugrrugbuwbggbrwgw");
            AddDesign("wggbrrwrrgwrgruuwbrubggwrbrwbrbbbguuwgrwuwr");
            AddDesign("bggrwwruwgugrgwugrrwbgrrrwwbbuubuwbgurggbbguwwrggbwgwwwgu");
            AddDesign("rgbbrrgwuuwrbrbrwwgwuurbuuwrurgbwgwrurwbbwrbgwrgbbwrb");
            AddDesign("bggrwrubgbubbgwgrwbrgwwbwwrrgwuwugggrrgrgbwbgrgubgbugwbugb");
            AddDesign("rgrbrgurrwurubrwbbbbbgubrburugrugwgggbbwbrrbrbugwwu");
            AddDesign("rgwrugrwrrgwrugubwgrbbrrwbgbbgrrbrrgwrburwbbbbwugrruurbg");
            AddDesign("bggrruubgrugbubrrubwrwbwgbrrgurgbwrbbbrwrbuwrgrwgbwgwww");
            AddDesign("wrwwggrbrbbwwrugwuubbgwgwbgururbuuugugbuubrgruggwwr");
            AddDesign("guguuwrwguwwbwgggruwgbgwrrrggwbrbwrbwwuwrubwuuwbuwurrrwrbb");
            AddDesign("urwuurrbugguubwuggguwgwwuwbuwuurgrgburgwgbrwggrwrgwrw");
            AddDesign("bggwwwggbuwggwuuwggwrgrgwggbrrgbrwwguuuwrrwuwugwbuurgrbgggb");
            AddDesign("bggruwrbbbwruwrrbwwgbbwrbuubgubbrbbugwuw");
            AddDesign("gwbrbguruugbwubwuurubwrwguwugwwrgwwbburbgubrbgwbbgwwuwu");
            AddDesign("ggrguuggrgwubgbbububrrbguwubbrbugurwrwubww");
            AddDesign("wrgbwgrbgubggrgruruwburrwgrgubrubwwwgugbuwwbwwwgwg");
            AddDesign("bggwbgurbbwugbugwbwruwrbrguwrguubwurwrrrrwrwbw");
            AddDesign("wbrbrgrwgwgwgbgrwgrrwwuugbbugrgbgwrugrbwgrbrwbgbuwwwrbubwu");
            AddDesign("bgguuburbgurgrrrwwbbubuwrugbrwwwbruubwrrgugrgrggubbub");
            AddDesign("gwwrbwwburgwggrgwruwwbuugbbbbbgbbubgubgrwuwuubgbwbr");
            AddDesign("bggguwbgrgggwggburwrwugrrguuwuurrgbwggwgrrwgrbbrggurbwrrug");
            AddDesign("ruwubwbrwrwuwwwrbbwuurgwuuwwrgbbuuwggrurbggwgubrwugr");
            AddDesign("rbbggwuuuwwrrgbwwurbrwubwguurwwrgwrgguugruruuwrugrww");
            AddDesign("urwubbuugwuwwrubbububwurwbrgwbbugrrwrwgwwwrbgrbbrrwbgbuwg");
            AddDesign("bggrrgrrubwrgubgwbuurrbuugrubgbuggwwuwbgbgguwwwbububrrrr");
            AddDesign("grgubuwrwrgrwgwwwrwugrbwruwrguwbgwrrruuurwrgurbuur");
            AddDesign("wwrubrgbbgubburrgbuburggbrugubuwbrbrwwrbbwgwrwwbgrr");
            AddDesign("buwgbgubrwbwrwbwgugbgbgwgubbrwurugwurbwwwrgbruuwburru");
            AddDesign("wwbgrrwrrwrrgggggwrgbwbrurbgubruwruwuwbbrgubuubggw");
            AddDesign("grgurrbrugbwwbbbrwwrwbugrgbuwbuugbuwuubbubugrbggubgwwub");
            AddDesign("uwbgrwgwrrbrrwgrggubbubrbrrbbbuwbbbrgggguwwwuwwwurbrur");
            AddDesign("bbbrwuwbggrwrggrwruugwbggrwbbwbgrgwggrwrwbggwrgwu");
            AddDesign("rwggbbuuuugwwurrbbbguuuugwwrwgbbburrbrgrrbuggbwwwwwb");
            AddDesign("uurruuburugbuuggbuuwwurrgrrruugwguuwwgguuggr");
            AddDesign("ubwbbrrwbubuurbrgubgwuwuwrrgbrbwgrwbwrwurwrg");
            AddDesign("bgggwrgubguwrwrubgubgrbrwwrrwggguubwuwurrrgwuwubuu");
            AddDesign("wrbgwgruuburrwbwrubuwgbuwgrrruubggruuuggwgbb");
            AddDesign("gwuubuwrwubrbrgrugrgubburwwuwwggggrggugbwbwwgwwbubrbu");
            AddDesign("ggwbwwugrwrgrrrrbwrggbrgwugrbwrbubbuubgbuuwgrwuwgbrrugbw");
            AddDesign("bwuuuwubugrubggbubggugbwrwruuguwgwggbbuurwwubrugbur");
            AddDesign("bgbbggggubrrrbgbbuuburgggububggrggrgwbwgrurbbrgbwugwwbrburg");
            AddDesign("bbbggbwrggwbugbuwrrwwgbubuurrwuubwgwgrbg");
            AddDesign("bggbubwuguwbrbgrggrbwuwgbbuwgwrwubbrurrggw");
            AddDesign("wuuguwgwwgggrwugrwubrguuwbrrrurbubuwguurrgrggwb");
            AddDesign("uwgwwwgggwbwwbgrwbwgggrbgwggrugbbrwgwrwgrugurrr");
            AddDesign("guuugrrwruuwbuwgubburwbgbwwwrwrwwwugrwwuwuggbrbgruguu");
            AddDesign("bruurrrwrrgrrggrrwbuuurbbbggbgwugwgburwuuurrruwbwgwuurb");
            AddDesign("wrugbbbbugwruguuurbugwbrwbrubwrwgwbgrwwwrbubwwr");
            AddDesign("bggwbwwbrbubbuubbwrbbugrgwuuwwbwbwuwgwgbubrrrrrrw");
            AddDesign("wgrugbwuurrwgbrrwrburwwwbuurbrbwgwbrgugwrr");
            AddDesign("gwrrrwwggbrrurgwbubugwwrrgggubgugguwrbrgwuwbgwrubgbgbb");
            AddDesign("wguurrrrrgbugrubwgbuwwbuuwubrbwrbgrwbbrugur");
            AddDesign("gubwurbbguwugrubbwwubrgbwwubwwbgrggbwrubggrggburwbrgruurgg");
            AddDesign("bgggurugwbbgwuurbrgwrwurgrggubbwwwbgbuwrbbwwwuug");
        }
    }
}
