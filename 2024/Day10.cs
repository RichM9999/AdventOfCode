//https://adventofcode.com/2024/day/10
namespace AdventOfCode.Year2024
{
    class Day10
    {
        const int mapSize = 53;

        public void Run()
        {
            var map = new char[mapSize, mapSize];

            SetupMap(ref map);

            var start = DateTime.Now;

            var sum = 0;

            for (var y = 0; y < mapSize; y++)
            {
                for (var x = 0; x < mapSize; x++)
                { 
                    if (map[y,x] == '0')
                    {
                        sum += ScoreTrailHead(y, x, ref map, false);
                    }
                }
            }

            Console.WriteLine($"Sum of scores: {sum}");
            //644
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            start = DateTime.Now;

            sum = 0;

            for (var y = 0; y < mapSize; y++)
            {
                for (var x = 0; x < mapSize; x++)
                {
                    if (map[y, x] == '0')
                    {
                        sum += ScoreTrailHead(y, x, ref map, true);
                    }
                }
            }

            Console.WriteLine($"Sum of ratings: {sum}");
            //1366
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        int ScoreTrailHead(int y, int x, ref char[,] map, bool forRating)
        {
            var step = 0;

            var nodes = new List<TrailNode> { new TrailNode { x = x, y = y, step = step } };

            while (step < 9)
            {
                step++;

                if (!FindNextSteps(ref nodes, step, ref map, forRating))
                {
                    return 0;
                }
            }

            return nodes.Count();
        }


        bool FindNextSteps(ref List<TrailNode> nodes, int step, ref char[,] map, bool forRating)
        {
            var foundNextStep = false;

            List<TrailNode> nextSteps = new List<TrailNode>();

            // Loop through prior step and find if next step is anywhere adjacent
            foreach (var node in nodes)
            {
                if (node.y > 0)
                {
                    // Check up
                    if (map[node.y-1, node.x] == step.ToString().ToCharArray()[0])
                    {
                        nextSteps.Add(new TrailNode { x = node.x, y = node.y - 1, step = step});
                        foundNextStep = true;
                    }                   
                }

                if (node.x > 0)
                {
                    // Check left
                    if (map[node.y, node.x - 1] == step.ToString().ToCharArray()[0])
                    {
                        nextSteps.Add(new TrailNode { x = node.x - 1, y = node.y, step = step });
                        foundNextStep = true;
                    }
                }

                if (node.y < mapSize-1)
                {
                    // Check down
                    if (map[node.y + 1, node.x] == step.ToString().ToCharArray()[0])
                    {
                        nextSteps.Add(new TrailNode { x = node.x, y = node.y + 1, step = step });
                        foundNextStep = true;
                    }

                }

                if (node.x < mapSize - 1)
                {
                    // Check right
                    if (map[node.y, node.x + 1] == step.ToString().ToCharArray()[0])
                    {
                        nextSteps.Add(new TrailNode { x = node.x + 1, y = node.y, step = step });
                        foundNextStep = true;
                    }
                }
            }

            if (foundNextStep)
            {
                // For ratings, want all possible paths
                // For scores, only want distinct paths
                if (!forRating)
                {
                    nextSteps = nextSteps
                                .Select(n => (n.x, n.y, n.step))
                                .Distinct()
                                .Select(n => new TrailNode { x = n.x, y = n.y, step = n.step })
                                .ToList();
                }

                // Replace nodes with next step nodes
                nodes = nextSteps;
            }

            return foundNextStep;
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
            AddMapRow(0, ref map, "78760345801298896321001230110976301870134341034545687");
            AddMapRow(1, ref map, "69671236989367765410678345221885432963233452127632796");
            AddMapRow(2, ref map, "10589987670456001323569896336797010154324569498101845");
            AddMapRow(3, ref map, "23478678561032132124428987445788923245013478545613932");
            AddMapRow(4, ref map, "12034569432983045034310176596657654356932103456754301");
            AddMapRow(5, ref map, "06123765301874126965223456780345543267843212769861212");
            AddMapRow(6, ref map, "67610854322965437876104320691275676189732301890770023");
            AddMapRow(7, ref map, "98545921013456946766782111234987089078721450321981167");
            AddMapRow(8, ref map, "23434530145697854325693003435672128569670965492843298");
            AddMapRow(9, ref map, "10127647654787765014234512345965432154589876782194587");
            AddMapRow(10, ref map, "01018758943439890143109671236878941003432106543087656");
            AddMapRow(11, ref map, "10129667872108743232198780987567650012343267890989943");
            AddMapRow(12, ref map, "78734531231065654651015697898232109871054154901077830");
            AddMapRow(13, ref map, "69632340345676544543234506701145696780567063212566321");
            AddMapRow(14, ref map, "12541256543989035672103215012098785991278343103455410");
            AddMapRow(15, ref map, "01230167612108120780043454123210034876369752101769511");
            AddMapRow(16, ref map, "43456898909817431891012763204987121765459861019878700");
            AddMapRow(17, ref map, "32197899768746532982367874215672100894312772923467011");
            AddMapRow(18, ref map, "44089432897655673675498985365523456783203689876554322");
            AddMapRow(19, ref map, "43476521784554984566780189474310327894104501298458943");
            AddMapRow(20, ref map, "32562340603443295432110276589210016765067898347457654");
            AddMapRow(21, ref map, "41001456512782106798023345341232108778754106756346655");
            AddMapRow(22, ref map, "58989867425691065897654301250343419689603245891258743");
            AddMapRow(23, ref map, "67670198304781478654761298765430589576512231230969512");
            AddMapRow(24, ref map, "50501078213210569763890181056521677433443100945872402");
            AddMapRow(25, ref map, "41432189362387659812083212347893478922109811876721321");
            AddMapRow(26, ref map, "32343276651096543202144307438982560013078320125630430");
            AddMapRow(27, ref map, "21054565789823430143435698729861011224565410432145561");
            AddMapRow(28, ref map, "78769834658712894354998705610154398348901565589006776");
            AddMapRow(29, ref map, "69876765541006765267888214567863287657652651671217889");
            AddMapRow(30, ref map, "56945454532214890122379823098978101098343780980368923");
            AddMapRow(31, ref map, "47834503654323012001456702112109780123210891276456914");
            AddMapRow(32, ref map, "32322112567410163100014511003498898194541201345567805");
            AddMapRow(33, ref map, "01012012398789654011723621014586767087687632436679876");
            AddMapRow(34, ref map, "01201034498218765327898734765675432101598543567987015");
            AddMapRow(35, ref map, "32789125567309014456783349898987013216967845678872126");
            AddMapRow(36, ref map, "45694876323418123242124456787016524567856934569543034");
            AddMapRow(37, ref map, "34543945410567032103023456689145434787643021078676545");
            AddMapRow(38, ref map, "67432876509878749854510965670236543694532122189089496");
            AddMapRow(39, ref map, "78761789034569858987627874321987231243231821672102387");
            AddMapRow(40, ref map, "89450321123988567546596765345698100340100910565201256");
            AddMapRow(41, ref map, "44321450210676875654587034276765657654327823434389855");
            AddMapRow(42, ref map, "32340565012363966703456123189894398789016434325670765");
            AddMapRow(43, ref map, "01051876765454567812890012007654289430145045410581678");
            AddMapRow(44, ref map, "98762998898323234906721063218723176521032156921892569");
            AddMapRow(45, ref map, "45101237012210105415430874349812067898149867865433454");
            AddMapRow(46, ref map, "36788946543101076010987965456701098778928766456521043");
            AddMapRow(47, ref map, "29897654218760187623875478309898105667810610347876542");
            AddMapRow(48, ref map, "10898743009451296874566329214321234354320521298985031");
            AddMapRow(49, ref map, "01734542112321345987017812105670985012131494567874120");
            AddMapRow(50, ref map, "45629610321030996506323901078987876543012383456923454");
            AddMapRow(51, ref map, "30018723478945887215489765212985432692105672321012765");
            AddMapRow(52, ref map, "21167634569876774345677894303876501783234561030109876");
        }
    }

    class TrailNode
    {
        public int x;
        public int y;
        public int step;
    }
}
