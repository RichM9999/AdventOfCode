//https://adventofcode.com/2023/day/5
namespace AdventOfCode.Year2023
{
    using Map = (long Destination, long Source, long Range);

    class Day5
    {
        private List<long> seeds;

        private List<(long seedNumber, long range)> seedRanges;

        private List<Map> seedToSoilMap;
        private List<Map> soilToFertilizerMap;
        private List<Map> fertilizerToWaterMap;
        private List<Map> waterToLightMap;
        private List<Map> lightToTemperatureMap;
        private List<Map> temperatureToHumidityMap;
        private List<Map> humidityToLocationMap;

        public Day5()
        {
            seeds = [];
            
            seedRanges = [];

            seedToSoilMap = [];
            soilToFertilizerMap = [];
            fertilizerToWaterMap = [];
            waterToLightMap = [];
            lightToTemperatureMap = [];
            temperatureToHumidityMap = [];
            humidityToLocationMap = [];
        }

        public void Run()
        {
            var start = DateTime.Now;
            
            GetData();

            List<long> locations = [];

            foreach (var seed in seeds) 
            {
                var soil = MapDestination(seed, seedToSoilMap);
                var fertilizer = MapDestination(soil, soilToFertilizerMap);
                var water = MapDestination(fertilizer, fertilizerToWaterMap);
                var light = MapDestination(water, waterToLightMap);
                var temperature = MapDestination(light, lightToTemperatureMap);
                var humidity = MapDestination(temperature, temperatureToHumidityMap); 
                var location = MapDestination(humidity, humidityToLocationMap);

                locations.Add(location);
            }

            Console.WriteLine($"Min location: {locations.Min()}");
            //227653707
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            start = DateTime.Now;

            seeds = [];

            long minLocation = long.MaxValue;
            var range = 1;

            foreach (var seedRange in seedRanges)
            {
                for (var seed = seedRange.seedNumber; seed < seedRange.seedNumber + seedRange.range; seed++)
                {
                    if (seed % (seedRange.range / 10) == 0)
                    {
                        Console.Write(".");
                    }

                    var soil = MapDestination(seed, seedToSoilMap);
                    var fertilizer = MapDestination(soil, soilToFertilizerMap);
                    var water = MapDestination(fertilizer, fertilizerToWaterMap);
                    var light = MapDestination(water, waterToLightMap);
                    var temperature = MapDestination(light, lightToTemperatureMap);
                    var humidity = MapDestination(temperature, temperatureToHumidityMap);
                    var location = MapDestination(humidity, humidityToLocationMap);

                    minLocation = Math.Min(minLocation, location);
                }
                Console.WriteLine();
                Console.WriteLine($"Range {range++} done");
                Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
            }

            Console.WriteLine($"Min location: {minLocation}");
            //78775051
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
            //1805583.3535ms (30.09 minutes)
        }

        long MapDestination(long source, List<Map> map)
        {
            var destination = source;

            foreach (var entry in map)
            {
                if (source >= entry.Source && source <= entry.Source + entry.Range)
                {
                    destination += entry.Destination - entry.Source;
                    break;
                }
            }

            return destination;
        }

        void AddSeedRange(long start, long count)
        {
            seedRanges.Add((start, count));
        }

        void GetData()
        {
            seeds.Add(2880930400);
            seeds.Add(17599561);
            seeds.Add(549922357);
            seeds.Add(200746426);
            seeds.Add(1378552684);
            seeds.Add(43534336);
            seeds.Add(155057073);
            seeds.Add(56546377);
            seeds.Add(824205101);
            seeds.Add(378503603);
            seeds.Add(1678376802);
            seeds.Add(130912435);
            seeds.Add(2685513694);
            seeds.Add(137778160);
            seeds.Add(2492361384);
            seeds.Add(188575752);
            seeds.Add(3139914842);
            seeds.Add(1092214826);
            seeds.Add(2989476473);
            seeds.Add(58874625);

            AddSeedRange(2880930400, 17599561);
            AddSeedRange(549922357, 200746426);
            AddSeedRange(1378552684, 43534336);
            AddSeedRange(155057073, 56546377);
            AddSeedRange(824205101, 378503603);
            AddSeedRange(1678376802, 130912435);
            AddSeedRange(2685513694, 137778160);
            AddSeedRange(2492361384, 188575752);
            AddSeedRange(3139914842, 1092214826);
            AddSeedRange(2989476473, 58874625);

            seedToSoilMap.Add((341680072, 47360832, 98093750));
            seedToSoilMap.Add((1677587229, 1836834678, 160297919));
            seedToSoilMap.Add((1122651749, 4014790961, 280176335));
            seedToSoilMap.Add((2279929873, 2689269992, 53644948));
            seedToSoilMap.Add((3916120104, 1199400457, 172302726));
            seedToSoilMap.Add((0, 381576527, 58197295));
            seedToSoilMap.Add((1402828084, 3450816018, 274759145));
            seedToSoilMap.Add((3909949227, 2540063154, 6170877));
            seedToSoilMap.Add((802918801, 2384227172, 155835982));
            seedToSoilMap.Add((4088422830, 3244271552, 206544466));
            seedToSoilMap.Add((958754783, 1997132597, 28874650));
            seedToSoilMap.Add((58197295, 306349987, 75226540));
            seedToSoilMap.Add((180784667, 145454582, 160895405));
            seedToSoilMap.Add((2334903647, 1543332738, 293501940));
            seedToSoilMap.Add((3699983017, 2997982209, 25342830));
            seedToSoilMap.Add((2333574821, 2687941166, 1328826));
            seedToSoilMap.Add((3111317969, 1371703183, 171629555));
            seedToSoilMap.Add((2806959198, 2135774873, 248452299));
            seedToSoilMap.Add((2766721604, 717118138, 40237594));
            seedToSoilMap.Add((3055411497, 2632034694, 55906472));
            seedToSoilMap.Add((2628405587, 3023325039, 138316017));
            seedToSoilMap.Add((1837885148, 757355732, 442044725));
            seedToSoilMap.Add((3725325847, 2813358829, 184623380));
            seedToSoilMap.Add((3353391413, 2026007247, 109767626));
            seedToSoilMap.Add((987629433, 3962399141, 10015813));
            seedToSoilMap.Add((717118138, 2546234031, 85800663));
            seedToSoilMap.Add((3282947524, 2742914940, 70443889));
            seedToSoilMap.Add((1080275742, 3972414954, 42376007));
            seedToSoilMap.Add((133423835, 0, 47360832));
            seedToSoilMap.Add((3463159039, 3725575163, 236823978));
            seedToSoilMap.Add((997645246, 3161641056, 82630496));

            soilToFertilizerMap.Add((303672059, 1087423328, 103502353));
            soilToFertilizerMap.Add((171922589, 2907629744, 91556518));
            soilToFertilizerMap.Add((2064217168, 468859004, 91214014));
            soilToFertilizerMap.Add((1129888530, 1046445685, 40977643));
            soilToFertilizerMap.Add((3698610046, 4215442249, 79525047));
            soilToFertilizerMap.Add((1045870106, 1586657152, 41455160));
            soilToFertilizerMap.Add((1170866173, 1322928302, 17679660));
            soilToFertilizerMap.Add((4160148003, 3332238470, 107558461));
            soilToFertilizerMap.Add((4267706464, 3853049576, 27260832));
            soilToFertilizerMap.Add((0, 3007612896, 90771201));
            soilToFertilizerMap.Add((3447204990, 3880310408, 249339913));
            soilToFertilizerMap.Add((1189561657, 1438888401, 43309463));
            soilToFertilizerMap.Add((4019710828, 3219712242, 104981462));
            soilToFertilizerMap.Add((2226263856, 2187322171, 114088350));
            soilToFertilizerMap.Add((553216166, 1847338068, 182148047));
            soilToFertilizerMap.Add((3217647099, 3439796931, 229557891));
            soilToFertilizerMap.Add((2832115692, 1482197864, 23307900));
            soilToFertilizerMap.Add((867366834, 94995931, 178503272));
            soilToFertilizerMap.Add((1969221237, 0, 94995931));
            soilToFertilizerMap.Add((3785679859, 3704810535, 148239041));
            soilToFertilizerMap.Add((1095751900, 2693816297, 34136630));
            soilToFertilizerMap.Add((4124692290, 3669354822, 35455713));
            soilToFertilizerMap.Add((454935727, 1340607962, 98280439));
            soilToFertilizerMap.Add((2204466075, 1628112312, 21797781));
            soilToFertilizerMap.Add((1947833351, 2164918461, 21387886));
            soilToFertilizerMap.Add((2634687717, 1649910093, 197427975));
            soilToFertilizerMap.Add((263479107, 2850768768, 40192952));
            soilToFertilizerMap.Add((3696544903, 3217647099, 2065143));
            soilToFertilizerMap.Add((735364213, 1190925681, 132002621));
            soilToFertilizerMap.Add((3050783393, 2337205982, 47600704));
            soilToFertilizerMap.Add((1087325266, 2999186262, 8426634));
            soilToFertilizerMap.Add((3778135093, 3324693704, 7544766));
            soilToFertilizerMap.Add((2855423592, 273499203, 195359801));
            soilToFertilizerMap.Add((90771201, 1505505764, 81151388));
            soilToFertilizerMap.Add((2340352206, 560073018, 290906919));
            soilToFertilizerMap.Add((1531641800, 939263745, 107181940));
            soilToFertilizerMap.Add((1188545833, 2186306347, 1015824));
            soilToFertilizerMap.Add((1355686961, 850979937, 88283808));
            soilToFertilizerMap.Add((1492181516, 2029486115, 39460284));
            soilToFertilizerMap.Add((1638823740, 2384806686, 309009611));
            soilToFertilizerMap.Add((2155431182, 2894390312, 13239432));
            soilToFertilizerMap.Add((1443970769, 2068946399, 48210747));
            soilToFertilizerMap.Add((1232871120, 2727952927, 122815841));
            soilToFertilizerMap.Add((3933918900, 4129650321, 85791928));
            soilToFertilizerMap.Add((2631259125, 2890961720, 3428592));
            soilToFertilizerMap.Add((407174412, 2117157146, 47761315));
            soilToFertilizerMap.Add((2168670614, 2301410521, 35795461));

            fertilizerToWaterMap.Add((4253126168, 3603943470, 41841128));
            fertilizerToWaterMap.Add((3150812716, 3873122781, 161556325));
            fertilizerToWaterMap.Add((4132148538, 3445929121, 16652907));
            fertilizerToWaterMap.Add((4071215062, 2557593856, 10373731));
            fertilizerToWaterMap.Add((3585414898, 2401284809, 61555959));
            fertilizerToWaterMap.Add((124617758, 989226185, 56063423));
            fertilizerToWaterMap.Add((1311995731, 916233018, 72993167));
            fertilizerToWaterMap.Add((180681181, 891200267, 25032751));
            fertilizerToWaterMap.Add((1577315548, 1448436684, 231921083));
            fertilizerToWaterMap.Add((69948934, 1391916079, 39397864));
            fertilizerToWaterMap.Add((2730663795, 3577458422, 26485048));
            fertilizerToWaterMap.Add((2453473122, 3255362867, 102306532));
            fertilizerToWaterMap.Add((4148801445, 3801350502, 12292818));
            fertilizerToWaterMap.Add((3002725397, 3107275548, 148087319));
            fertilizerToWaterMap.Add((3525935437, 3813643320, 59479461));
            fertilizerToWaterMap.Add((3982955340, 3357669399, 88259722));
            fertilizerToWaterMap.Add((2631712351, 2567967587, 98951444));
            fertilizerToWaterMap.Add((628324302, 2038793089, 109830184));
            fertilizerToWaterMap.Add((3427245435, 3721480891, 3936914));
            fertilizerToWaterMap.Add((796140554, 1045289608, 36965524));
            fertilizerToWaterMap.Add((939693576, 1140241200, 24301167));
            fertilizerToWaterMap.Add((205713932, 1680357767, 358435322));
            fertilizerToWaterMap.Add((4161094263, 2666919031, 92031905));
            fertilizerToWaterMap.Add((1103981621, 0, 206162329));
            fertilizerToWaterMap.Add((1809236631, 761213122, 129987145));
            fertilizerToWaterMap.Add((1310143950, 1431313943, 1851781));
            fertilizerToWaterMap.Add((4081588793, 3056715803, 50559745));
            fertilizerToWaterMap.Add((738154486, 1082255132, 57986068));
            fertilizerToWaterMap.Add((564149254, 206162329, 64175048));
            fertilizerToWaterMap.Add((3722667150, 4034679106, 260288190));
            fertilizerToWaterMap.Add((3431182349, 2462840768, 94753088));
            fertilizerToWaterMap.Add((109346798, 1433165724, 15270960));
            fertilizerToWaterMap.Add((2757148843, 2811139249, 245576554));
            fertilizerToWaterMap.Add((1044349135, 2294919620, 59632486));
            fertilizerToWaterMap.Add((833106078, 270337377, 106587498));
            fertilizerToWaterMap.Add((3646970857, 3645784598, 75696293));
            fertilizerToWaterMap.Add((0, 2354552106, 4006979));
            fertilizerToWaterMap.Add((1974270838, 376924875, 384288247));
            fertilizerToWaterMap.Add((3312369041, 3462582028, 114876394));
            fertilizerToWaterMap.Add((2401284809, 2758950936, 52188313));
            fertilizerToWaterMap.Add((1384988898, 1199589429, 192326650));
            fertilizerToWaterMap.Add((963994743, 2214565228, 80354392));
            fertilizerToWaterMap.Add((1939223776, 1164542367, 35047062));
            fertilizerToWaterMap.Add((4006979, 2148623273, 65941955));
            fertilizerToWaterMap.Add((2555779654, 3725417805, 75932697));

            waterToLightMap.Add((1553071310, 2767299260, 81555093));
            waterToLightMap.Add((1638385137, 3758734, 7165416));
            waterToLightMap.Add((3923895602, 3742459208, 355646104));
            waterToLightMap.Add((2563492152, 40550035, 317968));
            waterToLightMap.Add((357175543, 151852464, 53516575));
            waterToLightMap.Add((756535305, 2730597762, 36701498));
            waterToLightMap.Add((1142337672, 1915537677, 164067723));
            waterToLightMap.Add((436470886, 2848854353, 61956232));
            waterToLightMap.Add((1316538987, 1679005354, 102639946));
            waterToLightMap.Add((609765571, 2079605400, 146769734));
            waterToLightMap.Add((1306405395, 2660382036, 10133592));
            waterToLightMap.Add((3817572860, 3406157555, 106322742));
            waterToLightMap.Add((2023184953, 353497588, 62195869));
            waterToLightMap.Add((3531543848, 4223491605, 71475691));
            waterToLightMap.Add((410692118, 126073696, 25778768));
            waterToLightMap.Add((4279541706, 3727562743, 14896465));
            waterToLightMap.Add((2903607795, 1495097536, 74179449));
            waterToLightMap.Add((1794747312, 2279656479, 95385933));
            waterToLightMap.Add((2783150325, 2269091374, 10565105));
            waterToLightMap.Add((3406157555, 4098105312, 125386293));
            waterToLightMap.Add((2145462956, 205369039, 148128549));
            waterToLightMap.Add((2833741244, 2401175172, 69866551));
            waterToLightMap.Add((793236803, 685594104, 306384629));
            waterToLightMap.Add((1645550553, 2511185277, 149196759));
            waterToLightMap.Add((1419178933, 1781645300, 133892377));
            waterToLightMap.Add((1634626403, 0, 3758734));
            waterToLightMap.Add((3274934245, 2471041723, 36449164));
            waterToLightMap.Add((0, 1097896179, 357175543));
            waterToLightMap.Add((2563810120, 991978733, 105917446));
            waterToLightMap.Add((4294438171, 3727033618, 529125));
            waterToLightMap.Add((2688189328, 1569276985, 94960997));
            waterToLightMap.Add((2669727566, 2507490887, 3694390));
            waterToLightMap.Add((3311383409, 10924150, 29625885));
            waterToLightMap.Add((1099621432, 2226375134, 42716240));
            waterToLightMap.Add((1890133245, 2910810585, 133051708));
            waterToLightMap.Add((583632811, 2375042412, 26132760));
            waterToLightMap.Add((498427118, 40868003, 85205693));
            waterToLightMap.Add((2293591505, 415693457, 269900647));
            waterToLightMap.Add((2673421956, 1664237982, 14767372));
            waterToLightMap.Add((2085380822, 2670515628, 60082134));
            waterToLightMap.Add((3603019539, 3512480297, 214553321));
            waterToLightMap.Add((2977787244, 3043862293, 297147001));
            waterToLightMap.Add((2793715430, 1455071722, 40025814));

            lightToTemperatureMap.Add((3752181853, 3850028427, 61847460));
            lightToTemperatureMap.Add((3392702182, 4061054452, 68370555));
            lightToTemperatureMap.Add((3610251302, 4129425007, 141930551));
            lightToTemperatureMap.Add((2019529001, 2633762146, 55812503));
            lightToTemperatureMap.Add((1423059901, 2612524947, 21237199));
            lightToTemperatureMap.Add((1637625157, 3160312690, 128493598));
            lightToTemperatureMap.Add((2109055159, 2018596226, 368399035));
            lightToTemperatureMap.Add((343891384, 811352094, 920120231));
            lightToTemperatureMap.Add((154347384, 2422980947, 189544000));
            lightToTemperatureMap.Add((2075341504, 1978947609, 33713655));
            lightToTemperatureMap.Add((1444297100, 2966984633, 193328057));
            lightToTemperatureMap.Add((35985686, 2689574649, 118361698));
            lightToTemperatureMap.Add((2477454194, 0, 811352094));
            lightToTemperatureMap.Add((1772053717, 1854798742, 124148867));
            lightToTemperatureMap.Add((1264011615, 2807936347, 159048286));
            lightToTemperatureMap.Add((0, 2386995261, 35985686));
            lightToTemperatureMap.Add((1766118755, 2012661264, 5934962));
            lightToTemperatureMap.Add((3814029313, 3392702182, 457326245));
            lightToTemperatureMap.Add((3461072737, 3911875887, 149178565));
            lightToTemperatureMap.Add((1896202584, 1731472325, 123326417));

            temperatureToHumidityMap.Add((3344602117, 2991074372, 262457649));
            temperatureToHumidityMap.Add((1707309180, 3911386116, 383581180));
            temperatureToHumidityMap.Add((3778482785, 2130995124, 374719434));
            temperatureToHumidityMap.Add((3607059766, 3253532021, 171423019));
            temperatureToHumidityMap.Add((584508486, 478912361, 161371970));
            temperatureToHumidityMap.Add((1578590582, 2505714558, 128718598));
            temperatureToHumidityMap.Add((3294145751, 1806488186, 50456366));
            temperatureToHumidityMap.Add((1143023241, 2829557603, 161516769));
            temperatureToHumidityMap.Add((1304540010, 1856944552, 274050572));
            temperatureToHumidityMap.Add((2090890360, 3424955040, 344665999));
            temperatureToHumidityMap.Add((2435556359, 1143023241, 663464945));
            temperatureToHumidityMap.Add((496214964, 1000471163, 88293522));
            temperatureToHumidityMap.Add((0, 640284331, 360186832));
            temperatureToHumidityMap.Add((3099021304, 2634433156, 195124447));
            temperatureToHumidityMap.Add((360186832, 342884229, 136028132));
            temperatureToHumidityMap.Add((745880456, 0, 342884229));
            temperatureToHumidityMap.Add((4153202219, 3769621039, 141765077));

            humidityToLocationMap.Add((3114211644, 984440400, 35385940));
            humidityToLocationMap.Add((3530465412, 479339778, 128291026));
            humidityToLocationMap.Add((0, 3699707734, 285474938));
            humidityToLocationMap.Add((2898087648, 3606829306, 92878428));
            humidityToLocationMap.Add((2762235329, 607630804, 135852319));
            humidityToLocationMap.Add((4210792153, 4197161772, 84175143));
            humidityToLocationMap.Add((3149597584, 31394121, 380867828));
            humidityToLocationMap.Add((1848709689, 0, 31394121));
            humidityToLocationMap.Add((1880103810, 412261949, 67077829));
            humidityToLocationMap.Add((285474938, 1579019790, 1563234751));
            humidityToLocationMap.Add((2990966076, 3566305423, 40523883));
            humidityToLocationMap.Add((2434079297, 1019826340, 328156032));
            humidityToLocationMap.Add((2371232521, 3985182672, 62846776));
            humidityToLocationMap.Add((1947181639, 3142254541, 424050882));
            humidityToLocationMap.Add((3899713715, 1347982372, 148315733));
            humidityToLocationMap.Add((3031489959, 1496298105, 82721685));
            humidityToLocationMap.Add((4197161772, 4281336915, 13630381));
            humidityToLocationMap.Add((3658756438, 743483123, 240957277));
        }
    }
}