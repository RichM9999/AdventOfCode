//https://adventofcode.com/2023/day/7
using static AdventOfCode.Year2023.Day7;

namespace AdventOfCode.Year2023
{
    class Day7
    {
        public enum HandTypeRank
        {
            Five = 7,
            Four = 6,
            Full = 5,
            Three = 4,
            Two = 3,
            One = 2,
            High = 1,
            None = 0
        }

        List<Hand> hands;
        Dictionary<char, byte> CardRanks;
        Dictionary<char, byte> CardRanksPart2;

        public Day7()
        {
            hands = [];

            CardRanks = new Dictionary<char, byte>()
            {
                {'A', 14},
                {'K', 13},
                {'Q', 12},
                {'J', 11},
                {'T', 10},
                {'9', 9},
                {'8', 8},
                {'7', 7},
                {'6', 6},
                {'5', 5},
                {'4', 4},
                {'3', 3},
                {'2', 2}
            };

            CardRanksPart2 = new Dictionary<char, byte>()
            {
                {'A', 14},
                {'K', 13},
                {'Q', 12},
                {'T', 11},
                {'9', 10},
                {'8', 9},
                {'7', 8},
                {'6', 7},
                {'5', 6},
                {'4', 5},
                {'3', 4},
                {'2', 3},
                {'J', 2}
            };
        }

        public void Run()
        {
            var start = DateTime.Now;

            GetData();

            for (var h = 0; h < hands.Count; h++)
            {
                hands[h].TypeRank = (byte)GetHandType(hands[h].Cards);
            }

            RankHands();

            long winnings = 0;

            foreach (var hand in hands)
            {
                winnings += (long)hand.Rank * (long)hand.Bid;
            }

            Console.WriteLine($"Total winnings: {winnings}");
            // 250474325
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            start = DateTime.Now;

            hands = [];

            GetData();

            for (var h = 0; h < hands.Count; h++)
            {
                hands[h].TypeRank = (byte)GetHandTypeWithJokers(hands[h].Cards);
            }

            RankHandsPart2();

            winnings = 0;

            foreach (var hand in hands)
            {
                winnings += (long)hand.Rank * (long)hand.Bid;
            }

            Console.WriteLine($"Total winnings: {winnings}");
            // 248909434
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        HandTypeRank GetHandType(string hand)
        {
            var cards = hand.ToCharArray().Select(c => c.ToString()).ToList();

            return GetHandType(cards);
        }

        HandTypeRank GetHandTypeWithJokers(string hand)
        {
            if (!hand.Contains('J'))
                return GetHandType(hand);

            var highest = GetHandType(hand);

            var cardValues = "23456789TQKA".ToCharArray();

            for (var c = 0; c < 12; c++)
            {
                var replacement = cardValues[c];
                var cards = hand.Replace('J', replacement).ToCharArray().Select(c => c.ToString()).ToList();

                var type = GetHandType(cards);

                if (type == HandTypeRank.Five)
                    return type;

                if ((byte)type > (byte)highest)
                {
                    highest = type;
                }
            }
            
            return highest;
        }

        HandTypeRank GetHandType(List<string> cards)
        {
            cards.Sort();

            if (cards[0] == cards[1] && cards[1] == cards[2] && cards[2] == cards[3] && cards[3] == cards[4])
                return HandTypeRank.Five;

            if (cards[0] == cards[1] && cards[1] == cards[2] && cards[2] == cards[3])
                return HandTypeRank.Four;

            if (cards[1] == cards[2] && cards[2] == cards[3] && cards[3] == cards[4])
                return HandTypeRank.Four;

            if (cards[0] == cards[1] && cards[1] == cards[2] && cards[3] == cards[4])
                return HandTypeRank.Full;

            if (cards[0] == cards[1] && cards[2] == cards[3] && cards[3] == cards[4])
                return HandTypeRank.Full;

            if (cards[0] == cards[1] && cards[1] == cards[2])
                return HandTypeRank.Three;

            if (cards[2] == cards[3] && cards[3] == cards[4])
                return HandTypeRank.Three;

            if (cards[1] == cards[2] && cards[2] == cards[3])
                return HandTypeRank.Three;

            if (cards[0] == cards[1] && cards[2] == cards[3])
                return HandTypeRank.Two;

            if (cards[1] == cards[2] && cards[3] == cards[4])
                return HandTypeRank.Two;

            if (cards[0] == cards[1] && cards[3] == cards[4])
                return HandTypeRank.Two;

            if (cards[0] == cards[1])
                return HandTypeRank.One;

            if (cards[1] == cards[2])
                return HandTypeRank.One;

            if (cards[2] == cards[3])
                return HandTypeRank.One;

            if (cards[3] == cards[4])
                return HandTypeRank.One;

            return HandTypeRank.High;
        }

        void RankHands()
        {
            var rank = 1;

            foreach (var hand in hands
                                    .OrderBy(h => h.TypeRank)
                                    .ThenBy(h => CardRanks[h.Cards[0]])
                                    .ThenBy(h => CardRanks[h.Cards[1]])
                                    .ThenBy(h => CardRanks[h.Cards[2]])
                                    .ThenBy(h => CardRanks[h.Cards[3]])
                                    .ThenBy(h => CardRanks[h.Cards[4]])
                                    )
            {
                hand.Rank = rank++;
            }
        }

        void RankHandsPart2()
        {
            var rank = 1;

            foreach (var hand in hands
                                    .OrderBy(h => h.TypeRank)
                                    .ThenBy(h => CardRanksPart2[h.Cards[0]])
                                    .ThenBy(h => CardRanksPart2[h.Cards[1]])
                                    .ThenBy(h => CardRanksPart2[h.Cards[2]])
                                    .ThenBy(h => CardRanksPart2[h.Cards[3]])
                                    .ThenBy(h => CardRanksPart2[h.Cards[4]])
                                    )
            {
                hand.Rank = rank++;
            }
        }

        void AddHand(string cards, int bid)
        {
            hands.Add(new Hand { Cards = cards, Bid = bid, TypeRank = 0, Rank = 0 });
        }

        void GetData()
        {
            AddHand("8T64Q", 595);
            AddHand("79J27", 258);
            AddHand("88885", 88);
            AddHand("8933J", 444);
            AddHand("72527", 676);
            AddHand("5555T", 788);
            AddHand("69946", 463);
            AddHand("572QQ", 827);
            AddHand("553JQ", 932);
            AddHand("99T99", 567);
            AddHand("47Q7Q", 112);
            AddHand("8J8QQ", 186);
            AddHand("5K499", 862);
            AddHand("2837Q", 321);
            AddHand("55557", 310);
            AddHand("KAAAA", 263);
            AddHand("J4999", 783);
            AddHand("4QQQ4", 961);
            AddHand("64464", 329);
            AddHand("8AQ9K", 153);
            AddHand("763AK", 341);
            AddHand("Q3K3Q", 353);
            AddHand("4TJT6", 593);
            AddHand("KJ46J", 666);
            AddHand("AA92Q", 176);
            AddHand("88555", 738);
            AddHand("8KJJJ", 431);
            AddHand("46T35", 295);
            AddHand("86868", 400);
            AddHand("884A4", 19);
            AddHand("QQK44", 860);
            AddHand("99996", 794);
            AddHand("6J778", 159);
            AddHand("45Q9T", 763);
            AddHand("8AQAQ", 39);
            AddHand("4JJ2K", 764);
            AddHand("Q42AT", 3);
            AddHand("77Q7Q", 905);
            AddHand("57ATJ", 185);
            AddHand("QQQJ6", 707);
            AddHand("TKT2T", 115);
            AddHand("JK646", 951);
            AddHand("3KT2K", 324);
            AddHand("94J64", 569);
            AddHand("Q278J", 998);
            AddHand("36QKA", 979);
            AddHand("89T98", 772);
            AddHand("55T66", 669);
            AddHand("62747", 161);
            AddHand("742TK", 264);
            AddHand("J5A2Q", 252);
            AddHand("JKAJK", 455);
            AddHand("72777", 659);
            AddHand("4454T", 940);
            AddHand("Q2278", 479);
            AddHand("63K36", 53);
            AddHand("3K839", 512);
            AddHand("A5AA5", 223);
            AddHand("T3332", 27);
            AddHand("KQ55Q", 217);
            AddHand("75TT7", 706);
            AddHand("53555", 775);
            AddHand("9KK96", 348);
            AddHand("9A999", 984);
            AddHand("9TT99", 943);
            AddHand("TTATJ", 906);
            AddHand("6964K", 711);
            AddHand("42452", 983);
            AddHand("22AA2", 241);
            AddHand("22282", 521);
            AddHand("77733", 627);
            AddHand("KQA47", 987);
            AddHand("738J7", 963);
            AddHand("KK2KK", 231);
            AddHand("22322", 709);
            AddHand("4T7J6", 665);
            AddHand("Q6K6A", 417);
            AddHand("2479K", 955);
            AddHand("7J22T", 861);
            AddHand("77A4A", 446);
            AddHand("8642K", 934);
            AddHand("25J32", 435);
            AddHand("A3A3A", 883);
            AddHand("QQ3JQ", 280);
            AddHand("K7666", 489);
            AddHand("2QAQ9", 732);
            AddHand("85422", 976);
            AddHand("T3393", 322);
            AddHand("28888", 298);
            AddHand("J9759", 608);
            AddHand("33438", 564);
            AddHand("A3KQ4", 377);
            AddHand("2K6TJ", 209);
            AddHand("QQ64J", 482);
            AddHand("8T5T3", 815);
            AddHand("3K6K6", 123);
            AddHand("37757", 741);
            AddHand("555A6", 858);
            AddHand("5TAAA", 12);
            AddHand("2225K", 965);
            AddHand("Q29QQ", 355);
            AddHand("2277K", 62);
            AddHand("99932", 169);
            AddHand("77722", 458);
            AddHand("Q9444", 550);
            AddHand("KJ8KK", 338);
            AddHand("9K99A", 238);
            AddHand("5Q5Q2", 427);
            AddHand("7A6A6", 675);
            AddHand("798AQ", 766);
            AddHand("386K8", 420);
            AddHand("88788", 681);
            AddHand("2K4JK", 974);
            AddHand("53426", 776);
            AddHand("46892", 884);
            AddHand("7Q47T", 754);
            AddHand("923A4", 719);
            AddHand("449JQ", 506);
            AddHand("2Q2Q2", 596);
            AddHand("K888T", 235);
            AddHand("6AA6K", 232);
            AddHand("57T5T", 314);
            AddHand("T3TA4", 171);
            AddHand("3QQ4A", 581);
            AddHand("TAA98", 946);
            AddHand("75Q8A", 170);
            AddHand("46A4A", 807);
            AddHand("36836", 539);
            AddHand("888T3", 515);
            AddHand("5KA55", 520);
            AddHand("92J56", 164);
            AddHand("Q3J56", 1);
            AddHand("KAAJ7", 499);
            AddHand("7QJQ4", 196);
            AddHand("TTKTK", 552);
            AddHand("AQ4QA", 257);
            AddHand("7JTTT", 249);
            AddHand("7QQ89", 268);
            AddHand("96998", 733);
            AddHand("39933", 713);
            AddHand("44JJ4", 603);
            AddHand("6877K", 206);
            AddHand("K224J", 450);
            AddHand("TTTKJ", 38);
            AddHand("T7999", 22);
            AddHand("7T55J", 660);
            AddHand("66363", 61);
            AddHand("33363", 610);
            AddHand("TTAAT", 795);
            AddHand("36336", 739);
            AddHand("55255", 604);
            AddHand("982JJ", 146);
            AddHand("9ATQ6", 313);
            AddHand("TT688", 293);
            AddHand("TT7KT", 648);
            AddHand("93298", 817);
            AddHand("7TJ77", 288);
            AddHand("23J3J", 184);
            AddHand("JT7T3", 132);
            AddHand("Q9Q95", 430);
            AddHand("5554A", 14);
            AddHand("QAQQA", 212);
            AddHand("TT33T", 538);
            AddHand("7A7J7", 97);
            AddHand("JQ738", 968);
            AddHand("AKA8K", 923);
            AddHand("K9988", 498);
            AddHand("J424A", 481);
            AddHand("5AT66", 792);
            AddHand("QQ444", 835);
            AddHand("22262", 916);
            AddHand("TJTTJ", 334);
            AddHand("77Q47", 462);
            AddHand("84884", 871);
            AddHand("959AQ", 950);
            AddHand("9637J", 492);
            AddHand("QKQQ3", 214);
            AddHand("68K6A", 13);
            AddHand("26624", 843);
            AddHand("TTTT4", 470);
            AddHand("48TT5", 81);
            AddHand("A6AQA", 540);
            AddHand("TTQ9Q", 285);
            AddHand("A8AAA", 473);
            AddHand("88848", 900);
            AddHand("K779K", 688);
            AddHand("3QJJ5", 852);
            AddHand("66T6K", 224);
            AddHand("55595", 720);
            AddHand("2222T", 784);
            AddHand("83QT5", 870);
            AddHand("6QQQ2", 426);
            AddHand("44K55", 625);
            AddHand("4AJ94", 472);
            AddHand("6A55A", 284);
            AddHand("85584", 43);
            AddHand("Q96J2", 986);
            AddHand("KA5AA", 939);
            AddHand("62662", 74);
            AddHand("89277", 390);
            AddHand("4JKKJ", 349);
            AddHand("336AA", 82);
            AddHand("A99A9", 477);
            AddHand("Q95J9", 116);
            AddHand("K99K8", 108);
            AddHand("AKA6A", 505);
            AddHand("37J92", 800);
            AddHand("TT98T", 15);
            AddHand("JA75Q", 276);
            AddHand("44433", 91);
            AddHand("93494", 684);
            AddHand("7A772", 220);
            AddHand("K6JKA", 9);
            AddHand("8TT85", 685);
            AddHand("44448", 600);
            AddHand("64T6T", 484);
            AddHand("A3K29", 547);
            AddHand("4455T", 698);
            AddHand("932J6", 742);
            AddHand("57595", 198);
            AddHand("937QJ", 114);
            AddHand("69696", 423);
            AddHand("A3A55", 143);
            AddHand("K8J4J", 25);
            AddHand("7AQ64", 646);
            AddHand("8Q44A", 166);
            AddHand("J2AQA", 809);
            AddHand("9993Q", 272);
            AddHand("A6JT4", 812);
            AddHand("K44T4", 222);
            AddHand("J5567", 577);
            AddHand("22244", 109);
            AddHand("TKTTT", 631);
            AddHand("Q678K", 919);
            AddHand("Q9KT3", 157);
            AddHand("4AJ64", 602);
            AddHand("7667T", 912);
            AddHand("A9K35", 282);
            AddHand("272A2", 78);
            AddHand("J887J", 113);
            AddHand("K9A57", 246);
            AddHand("JA7A2", 915);
            AddHand("A3T9A", 73);
            AddHand("264T4", 129);
            AddHand("KK777", 386);
            AddHand("T6665", 507);
            AddHand("JT2AT", 642);
            AddHand("66K66", 382);
            AddHand("J4666", 94);
            AddHand("235KQ", 966);
            AddHand("T3T53", 868);
            AddHand("3QA4A", 584);
            AddHand("QQ7QQ", 432);
            AddHand("JKK6K", 37);
            AddHand("64594", 575);
            AddHand("95669", 931);
            AddHand("37383", 366);
            AddHand("K9KKJ", 563);
            AddHand("4Q9T3", 970);
            AddHand("8688T", 180);
            AddHand("Q999Q", 242);
            AddHand("74724", 243);
            AddHand("6666A", 623);
            AddHand("66AT6", 250);
            AddHand("4KA58", 451);
            AddHand("J622J", 493);
            AddHand("2J32T", 47);
            AddHand("Q4586", 859);
            AddHand("3393Q", 221);
            AddHand("9Q55Q", 908);
            AddHand("66277", 121);
            AddHand("T7A94", 461);
            AddHand("8KT58", 160);
            AddHand("K9K99", 989);
            AddHand("29292", 23);
            AddHand("48462", 574);
            AddHand("6T443", 1000);
            AddHand("33344", 414);
            AddHand("22728", 964);
            AddHand("Q58JQ", 680);
            AddHand("35K7T", 542);
            AddHand("5A3QK", 673);
            AddHand("A5553", 453);
            AddHand("84Q7T", 743);
            AddHand("9489J", 903);
            AddHand("TJT94", 5);
            AddHand("2925A", 195);
            AddHand("45954", 17);
            AddHand("J6T79", 460);
            AddHand("6638J", 848);
            AddHand("977J9", 192);
            AddHand("AAAJ5", 131);
            AddHand("3T39T", 401);
            AddHand("87828", 447);
            AddHand("9JJK7", 651);
            AddHand("42552", 632);
            AddHand("T7TJJ", 389);
            AddHand("AA75A", 274);
            AddHand("KA46K", 11);
            AddHand("Q2Q2Q", 55);
            AddHand("KJKK3", 697);
            AddHand("5K3K3", 495);
            AddHand("84786", 21);
            AddHand("5J5QQ", 413);
            AddHand("25755", 849);
            AddHand("29222", 780);
            AddHand("J5A9K", 630);
            AddHand("K6TA6", 863);
            AddHand("AKKKJ", 395);
            AddHand("Q4456", 777);
            AddHand("547K7", 328);
            AddHand("QT35K", 559);
            AddHand("AJAA4", 872);
            AddHand("KTJ6K", 48);
            AddHand("TQQTQ", 215);
            AddHand("22662", 768);
            AddHand("28547", 526);
            AddHand("7J77Q", 958);
            AddHand("2J2JK", 42);
            AddHand("AK648", 691);
            AddHand("K6833", 716);
            AddHand("55J55", 888);
            AddHand("33322", 904);
            AddHand("2A6J7", 605);
            AddHand("58887", 586);
            AddHand("4J8Q7", 621);
            AddHand("9A8Q8", 824);
            AddHand("JQ2T9", 356);
            AddHand("3TTTT", 383);
            AddHand("JQQQJ", 699);
            AddHand("56T3Q", 351);
            AddHand("22422", 879);
            AddHand("K25AJ", 585);
            AddHand("82J66", 305);
            AddHand("QJQ68", 703);
            AddHand("96974", 189);
            AddHand("4JT36", 419);
            AddHand("84TJ2", 549);
            AddHand("A95A2", 84);
            AddHand("KK3KK", 724);
            AddHand("A5KKA", 421);
            AddHand("TTTAT", 636);
            AddHand("2T22K", 822);
            AddHand("337J7", 634);
            AddHand("98TT9", 770);
            AddHand("J99JJ", 167);
            AddHand("88454", 236);
            AddHand("J2985", 405);
            AddHand("KQKKK", 798);
            AddHand("6KQ82", 168);
            AddHand("AJJ9A", 92);
            AddHand("95595", 150);
            AddHand("82943", 672);
            AddHand("9Q7JA", 69);
            AddHand("43QJQ", 829);
            AddHand("A8382", 773);
            AddHand("332J4", 8);
            AddHand("7777K", 657);
            AddHand("9J999", 851);
            AddHand("35J5J", 99);
            AddHand("AKQ82", 24);
            AddHand("59355", 658);
            AddHand("AAAQA", 452);
            AddHand("98888", 44);
            AddHand("77947", 476);
            AddHand("48222", 533);
            AddHand("T6666", 30);
            AddHand("TT4T7", 144);
            AddHand("3Q8Q8", 297);
            AddHand("J3888", 309);
            AddHand("QJ22A", 933);
            AddHand("6TQ88", 977);
            AddHand("23A4J", 18);
            AddHand("66A5A", 262);
            AddHand("6T5A5", 914);
            AddHand("5K9J6", 922);
            AddHand("Q4Q99", 751);
            AddHand("3874Q", 60);
            AddHand("TTKT8", 653);
            AddHand("7TT7T", 126);
            AddHand("9J668", 364);
            AddHand("9ATK2", 155);
            AddHand("44462", 187);
            AddHand("85AJA", 692);
            AddHand("K54J9", 594);
            AddHand("2TK95", 318);
            AddHand("QT289", 744);
            AddHand("AQ45Q", 846);
            AddHand("K2583", 841);
            AddHand("55556", 837);
            AddHand("76662", 286);
            AddHand("79577", 893);
            AddHand("K3J47", 140);
            AddHand("QAT37", 644);
            AddHand("89QT7", 921);
            AddHand("3AAAA", 379);
            AddHand("K49K8", 647);
            AddHand("7373Q", 396);
            AddHand("9J5Q8", 254);
            AddHand("799Q7", 85);
            AddHand("5J55K", 558);
            AddHand("TJ254", 528);
            AddHand("Q5QJQ", 207);
            AddHand("23223", 527);
            AddHand("AQ83K", 694);
            AddHand("9TJ2J", 101);
            AddHand("9476K", 590);
            AddHand("TTQ5J", 359);
            AddHand("A9TQ2", 641);
            AddHand("2J35Q", 519);
            AddHand("55522", 398);
            AddHand("99949", 708);
            AddHand("94K3K", 234);
            AddHand("4J884", 336);
            AddHand("3T32T", 633);
            AddHand("33J3K", 137);
            AddHand("8J778", 456);
            AddHand("8Q57T", 726);
            AddHand("79975", 177);
            AddHand("J37Q2", 467);
            AddHand("TKKKT", 553);
            AddHand("47442", 992);
            AddHand("AT7K3", 875);
            AddHand("76666", 573);
            AddHand("424QJ", 497);
            AddHand("333K8", 597);
            AddHand("AJTAA", 29);
            AddHand("89Q26", 844);
            AddHand("3KK33", 723);
            AddHand("A9AKJ", 138);
            AddHand("77555", 592);
            AddHand("33888", 762);
            AddHand("T5342", 561);
            AddHand("75547", 190);
            AddHand("TT4JT", 650);
            AddHand("45Q55", 818);
            AddHand("7J777", 935);
            AddHand("333TQ", 478);
            AddHand("566J4", 71);
            AddHand("T7QTT", 854);
            AddHand("28558", 643);
            AddHand("26895", 273);
            AddHand("8762A", 58);
            AddHand("J333J", 79);
            AddHand("387J4", 437);
            AddHand("T9926", 980);
            AddHand("4K425", 325);
            AddHand("97Q5K", 546);
            AddHand("Q57K3", 898);
            AddHand("AA988", 579);
            AddHand("669JA", 678);
            AddHand("859J5", 701);
            AddHand("29992", 700);
            AddHand("JA66A", 141);
            AddHand("4J777", 440);
            AddHand("J7A7A", 816);
            AddHand("JQQ96", 941);
            AddHand("43354", 924);
            AddHand("66T22", 408);
            AddHand("49444", 865);
            AddHand("6KA5A", 649);
            AddHand("Q5T28", 106);
            AddHand("77977", 352);
            AddHand("J56Q5", 31);
            AddHand("22656", 814);
            AddHand("AT333", 518);
            AddHand("6JTT6", 2);
            AddHand("99889", 838);
            AddHand("JAKAK", 790);
            AddHand("35355", 247);
            AddHand("T2A82", 32);
            AddHand("68888", 429);
            AddHand("Q69T3", 944);
            AddHand("8688K", 72);
            AddHand("684K8", 337);
            AddHand("T9576", 385);
            AddHand("333A4", 674);
            AddHand("3T633", 994);
            AddHand("92JQK", 894);
            AddHand("A6293", 517);
            AddHand("KK288", 327);
            AddHand("373Q4", 66);
            AddHand("KKJJ8", 737);
            AddHand("38T67", 995);
            AddHand("KKJ4K", 609);
            AddHand("95898", 615);
            AddHand("AQQA9", 802);
            AddHand("TT8J7", 67);
            AddHand("3A438", 397);
            AddHand("AJAKJ", 895);
            AddHand("254A2", 808);
            AddHand("J9T26", 117);
            AddHand("AJAAJ", 954);
            AddHand("94499", 847);
            AddHand("T8999", 836);
            AddHand("K4T7J", 529);
            AddHand("24444", 535);
            AddHand("J2222", 956);
            AddHand("44KA4", 45);
            AddHand("6A66A", 95);
            AddHand("52778", 856);
            AddHand("JKJKK", 346);
            AddHand("AAJAA", 211);
            AddHand("J52J5", 103);
            AddHand("786JJ", 693);
            AddHand("Q66J6", 876);
            AddHand("5353J", 511);
            AddHand("KK6K6", 438);
            AddHand("K9Q99", 424);
            AddHand("948T9", 655);
            AddHand("K47KK", 380);
            AddHand("97937", 271);
            AddHand("KKK4K", 617);
            AddHand("J4978", 142);
            AddHand("7Q848", 149);
            AddHand("8733J", 312);
            AddHand("88778", 172);
            AddHand("6A363", 926);
            AddHand("KQQ4K", 468);
            AddHand("AK9Q7", 962);
            AddHand("9TQ58", 436);
            AddHand("KJK57", 165);
            AddHand("KA464", 967);
            AddHand("8TTQQ", 10);
            AddHand("7464K", 306);
            AddHand("558Q7", 583);
            AddHand("29999", 267);
            AddHand("QAA82", 656);
            AddHand("K6A75", 760);
            AddHand("7775J", 442);
            AddHand("KJ3K3", 332);
            AddHand("JQ2T4", 128);
            AddHand("T9728", 350);
            AddHand("J4494", 89);
            AddHand("498TT", 365);
            AddHand("AJA4Q", 193);
            AddHand("666Q4", 806);
            AddHand("37476", 410);
            AddHand("896QK", 302);
            AddHand("KKJ6Q", 614);
            AddHand("29522", 265);
            AddHand("5TA48", 287);
            AddHand("AJ24T", 705);
            AddHand("76TK2", 173);
            AddHand("A2749", 261);
            AddHand("J9575", 433);
            AddHand("KQ948", 828);
            AddHand("59545", 952);
            AddHand("3AT56", 275);
            AddHand("664Q8", 182);
            AddHand("6429T", 554);
            AddHand("QQ8QQ", 406);
            AddHand("24AA4", 259);
            AddHand("5J66J", 315);
            AddHand("9338Q", 981);
            AddHand("66637", 503);
            AddHand("TT888", 354);
            AddHand("23243", 179);
            AddHand("6K54J", 804);
            AddHand("277J3", 855);
            AddHand("22727", 682);
            AddHand("3693Q", 64);
            AddHand("97489", 375);
            AddHand("57T88", 174);
            AddHand("TQA36", 428);
            AddHand("KK5KK", 839);
            AddHand("Q343Q", 589);
            AddHand("A2262", 628);
            AddHand("A9AJA", 373);
            AddHand("56757", 565);
            AddHand("3K333", 454);
            AddHand("96622", 233);
            AddHand("JJJ8J", 370);
            AddHand("9KTTT", 466);
            AddHand("K22JK", 702);
            AddHand("KKK33", 508);
            AddHand("T9T9T", 715);
            AddHand("99595", 368);
            AddHand("TJT55", 485);
            AddHand("J28T8", 591);
            AddHand("T8768", 291);
            AddHand("T7K68", 845);
            AddHand("J666J", 663);
            AddHand("Q966J", 105);
            AddHand("J3333", 360);
            AddHand("8AQA3", 496);
            AddHand("KJTQK", 316);
            AddHand("KTTT3", 372);
            AddHand("A259T", 181);
            AddHand("7777Q", 191);
            AddHand("78777", 911);
            AddHand("7A925", 208);
            AddHand("QAQKQ", 930);
            AddHand("Q736K", 747);
            AddHand("K7KTK", 920);
            AddHand("388T3", 982);
            AddHand("Q5QQQ", 218);
            AddHand("49J49", 145);
            AddHand("23329", 219);
            AddHand("K97KQ", 736);
            AddHand("7KQJ7", 667);
            AddHand("4KJA6", 323);
            AddHand("422T2", 857);
            AddHand("KJJKT", 746);
            AddHand("T7TTT", 727);
            AddHand("J552T", 304);
            AddHand("77TKK", 107);
            AddHand("K432J", 278);
            AddHand("9JJ99", 988);
            AddHand("6Q8A8", 283);
            AddHand("2A6T8", 890);
            AddHand("28AA5", 601);
            AddHand("T662T", 721);
            AddHand("55J5J", 725);
            AddHand("4QTQ4", 237);
            AddHand("TKK4T", 175);
            AddHand("22682", 471);
            AddHand("J8538", 343);
            AddHand("498J3", 910);
            AddHand("3T548", 850);
            AddHand("33868", 465);
            AddHand("K55K3", 151);
            AddHand("947KJ", 118);
            AddHand("3T9A5", 281);
            AddHand("4T83K", 422);
            AddHand("22K27", 51);
            AddHand("J777J", 80);
            AddHand("A8579", 199);
            AddHand("K5AK5", 972);
            AddHand("77233", 867);
            AddHand("J8J88", 938);
            AddHand("22TJ9", 369);
            AddHand("757A8", 686);
            AddHand("93J2Q", 758);
            AddHand("A79T6", 999);
            AddHand("J6KA9", 239);
            AddHand("J4444", 378);
            AddHand("QJJ62", 59);
            AddHand("JQ6Q3", 928);
            AddHand("KK888", 543);
            AddHand("J636J", 4);
            AddHand("9AA3A", 599);
            AddHand("69J6J", 582);
            AddHand("6JKJK", 110);
            AddHand("Q77QQ", 244);
            AddHand("Q7272", 866);
            AddHand("2346J", 689);
            AddHand("868J8", 892);
            AddHand("969J9", 500);
            AddHand("5T5K5", 619);
            AddHand("27622", 937);
            AddHand("99799", 901);
            AddHand("KK336", 205);
            AddHand("J5566", 975);
            AddHand("2A955", 947);
            AddHand("TKQ57", 52);
            AddHand("99995", 618);
            AddHand("36223", 301);
            AddHand("T4TT8", 90);
            AddHand("A99AA", 49);
            AddHand("29699", 969);
            AddHand("84QTJ", 449);
            AddHand("3T99J", 801);
            AddHand("TQQQQ", 96);
            AddHand("TTK99", 344);
            AddHand("2J282", 98);
            AddHand("7TT79", 152);
            AddHand("J7JJ7", 480);
            AddHand("A288J", 393);
            AddHand("888J8", 411);
            AddHand("689K6", 294);
            AddHand("4T2J4", 787);
            AddHand("Q3344", 307);
            AddHand("2A7TA", 611);
            AddHand("7J86Q", 624);
            AddHand("69J85", 869);
            AddHand("37878", 771);
            AddHand("355QA", 292);
            AddHand("5757J", 357);
            AddHand("844Q8", 156);
            AddHand("8TTTT", 416);
            AddHand("QK9KQ", 299);
            AddHand("4A6K5", 757);
            AddHand("76777", 68);
            AddHand("K692K", 710);
            AddHand("QJ853", 813);
            AddHand("2JJ2J", 256);
            AddHand("7TTKK", 957);
            AddHand("9JK9K", 56);
            AddHand("4A55A", 303);
            AddHand("K6K22", 228);
            AddHand("99747", 767);
            AddHand("6K839", 560);
            AddHand("JA4TA", 248);
            AddHand("JA822", 459);
            AddHand("J6428", 668);
            AddHand("KKT9K", 677);
            AddHand("A7AAA", 65);
            AddHand("38333", 796);
            AddHand("664Q3", 120);
            AddHand("J7432", 36);
            AddHand("A356J", 111);
            AddHand("73Q49", 475);
            AddHand("86TJ2", 899);
            AddHand("24442", 864);
            AddHand("55444", 729);
            AddHand("7K6K4", 753);
            AddHand("7269Q", 960);
            AddHand("77J6J", 194);
            AddHand("8K694", 210);
            AddHand("5338J", 544);
            AddHand("TT99J", 745);
            AddHand("55T5T", 622);
            AddHand("33Q33", 367);
            AddHand("55558", 342);
            AddHand("737J4", 148);
            AddHand("6T6J9", 613);
            AddHand("3AA2T", 296);
            AddHand("434J6", 319);
            AddHand("333T3", 690);
            AddHand("44848", 936);
            AddHand("A98A9", 638);
            AddHand("7K254", 670);
            AddHand("74AJQ", 104);
            AddHand("69698", 266);
            AddHand("76477", 821);
            AddHand("TQJTT", 580);
            AddHand("4TKK4", 909);
            AddHand("45555", 474);
            AddHand("53QQ5", 887);
            AddHand("QJA3Q", 443);
            AddHand("94A85", 136);
            AddHand("3KAAA", 913);
            AddHand("39999", 403);
            AddHand("A4J4Q", 825);
            AddHand("28T5A", 874);
            AddHand("ATTA4", 54);
            AddHand("43JJ4", 226);
            AddHand("T9J48", 509);
            AddHand("K7T23", 945);
            AddHand("AT2Q8", 557);
            AddHand("6Q7J4", 789);
            AddHand("KKK9K", 16);
            AddHand("93J79", 317);
            AddHand("79653", 200);
            AddHand("Q666Q", 335);
            AddHand("AQ33A", 362);
            AddHand("A999J", 765);
            AddHand("5QT47", 441);
            AddHand("JQQQT", 756);
            AddHand("9T325", 122);
            AddHand("4AQ8T", 571);
            AddHand("TQ342", 70);
            AddHand("222JK", 779);
            AddHand("JA443", 300);
            AddHand("J7K88", 990);
            AddHand("646T6", 154);
            AddHand("95569", 842);
            AddHand("4J626", 384);
            AddHand("44KK4", 50);
            AddHand("JQQQQ", 93);
            AddHand("AA6AA", 735);
            AddHand("99888", 929);
            AddHand("4KK9K", 629);
            AddHand("A38K2", 75);
            AddHand("AA2AA", 163);
            AddHand("QQKQQ", 87);
            AddHand("8Q56K", 578);
            AddHand("QK467", 740);
            AddHand("94329", 687);
            AddHand("257A6", 695);
            AddHand("K9929", 345);
            AddHand("777A7", 645);
            AddHand("QA9AA", 57);
            AddHand("KKAAA", 959);
            AddHand("5T5T9", 188);
            AddHand("8JTTJ", 33);
            AddHand("2KKAQ", 371);
            AddHand("47747", 671);
            AddHand("28538", 202);
            AddHand("TJ38A", 881);
            AddHand("656A5", 491);
            AddHand("54Q89", 134);
            AddHand("8Q888", 704);
            AddHand("AKAKK", 415);
            AddHand("J2Q92", 524);
            AddHand("23859", 347);
            AddHand("8K7A6", 20);
            AddHand("JK676", 826);
            AddHand("555K5", 833);
            AddHand("9AK54", 490);
            AddHand("JT658", 793);
            AddHand("79KQ4", 778);
            AddHand("JA7K2", 229);
            AddHand("88883", 840);
            AddHand("87778", 791);
            AddHand("799K7", 712);
            AddHand("4KKKA", 487);
            AddHand("T6JTT", 537);
            AddHand("ATQK2", 76);
            AddHand("Q5679", 392);
            AddHand("7QKQ7", 147);
            AddHand("8KKKK", 568);
            AddHand("772J6", 363);
            AddHand("58545", 41);
            AddHand("A89KT", 696);
            AddHand("Q3T86", 616);
            AddHand("QAK54", 289);
            AddHand("33334", 86);
            AddHand("2AAA2", 158);
            AddHand("A55A5", 83);
            AddHand("TAAT5", 102);
            AddHand("Q9673", 46);
            AddHand("9KKK8", 882);
            AddHand("6JA46", 486);
            AddHand("6A254", 277);
            AddHand("JTTK2", 556);
            AddHand("J76Q2", 269);
            AddHand("9Q888", 781);
            AddHand("J4K4K", 100);
            AddHand("TAATA", 488);
            AddHand("AA9AA", 598);
            AddHand("78TQ3", 722);
            AddHand("A888A", 260);
            AddHand("T675Q", 34);
            AddHand("T3T33", 761);
            AddHand("Q32AQ", 35);
            AddHand("4QA45", 820);
            AddHand("87AAA", 635);
            AddHand("QAA3K", 516);
            AddHand("J5543", 683);
            AddHand("64546", 404);
            AddHand("74T67", 464);
            AddHand("3QT3T", 731);
            AddHand("7TT77", 978);
            AddHand("7J77K", 402);
            AddHand("AAKQ4", 7);
            AddHand("349TA", 251);
            AddHand("682QA", 917);
            AddHand("4357T", 245);
            AddHand("75KT6", 183);
            AddHand("4QQ54", 652);
            AddHand("25QK8", 358);
            AddHand("95KKA", 996);
            AddHand("66556", 407);
            AddHand("ATTKK", 494);
            AddHand("666JK", 612);
            AddHand("3JK5K", 290);
            AddHand("426J2", 522);
            AddHand("JA59J", 823);
            AddHand("5755T", 308);
            AddHand("JTAQK", 831);
            AddHand("T52Q9", 769);
            AddHand("5JKKK", 387);
            AddHand("8QKKK", 333);
            AddHand("5K59K", 541);
            AddHand("44454", 942);
            AddHand("64625", 530);
            AddHand("JK7T9", 361);
            AddHand("2TQQQ", 270);
            AddHand("42AK6", 997);
            AddHand("83T87", 532);
            AddHand("942AQ", 785);
            AddHand("KAQQK", 993);
            AddHand("44666", 750);
            AddHand("43KQ8", 418);
            AddHand("KJ522", 749);
            AddHand("JQQ77", 953);
            AddHand("22J26", 119);
            AddHand("7TK6Q", 531);
            AddHand("8TAK6", 127);
            AddHand("44QQ3", 63);
            AddHand("63236", 853);
            AddHand("66A9A", 873);
            AddHand("K3J3T", 805);
            AddHand("73373", 797);
            AddHand("3J336", 902);
            AddHand("TT67T", 896);
            AddHand("88KKK", 810);
            AddHand("KKKQJ", 139);
            AddHand("69Q58", 718);
            AddHand("9AKQ5", 501);
            AddHand("3223J", 948);
            AddHand("55AJA", 588);
            AddHand("99K59", 434);
            AddHand("86568", 755);
            AddHand("T25AT", 897);
            AddHand("Q2532", 502);
            AddHand("38383", 388);
            AddHand("J2A98", 832);
            AddHand("4TA4A", 606);
            AddHand("32888", 730);
            AddHand("J9TJA", 545);
            AddHand("3328J", 339);
            AddHand("5TTT5", 907);
            AddHand("J3AAJ", 819);
            AddHand("59459", 880);
            AddHand("99933", 255);
            AddHand("3K762", 834);
            AddHand("9T729", 448);
            AddHand("58885", 376);
            AddHand("94449", 6);
            AddHand("J7767", 374);
            AddHand("KKT27", 203);
            AddHand("QQJQA", 133);
            AddHand("A4K82", 326);
            AddHand("TTTK4", 803);
            AddHand("5789J", 661);
            AddHand("67633", 394);
            AddHand("J37KK", 197);
            AddHand("55Q57", 204);
            AddHand("455Q2", 562);
            AddHand("TQ539", 626);
            AddHand("JJ222", 714);
            AddHand("JJJJJ", 728);
            AddHand("98599", 225);
            AddHand("K666K", 971);
            AddHand("QKKK7", 878);
            AddHand("A8888", 664);
            AddHand("99JKJ", 213);
            AddHand("Q332Q", 774);
            AddHand("5J525", 130);
            AddHand("77377", 786);
            AddHand("3KAT4", 637);
            AddHand("48A9A", 891);
            AddHand("7K737", 409);
            AddHand("J8Q89", 525);
            AddHand("TT898", 877);
            AddHand("3Q888", 534);
            AddHand("JTA8A", 886);
            AddHand("J57J8", 991);
            AddHand("9TKA5", 555);
            AddHand("76Q27", 412);
            AddHand("72977", 504);
            AddHand("488TK", 830);
            AddHand("2K748", 162);
            AddHand("T74T9", 566);
            AddHand("K475J", 425);
            AddHand("KK62K", 889);
            AddHand("AJ4TT", 510);
            AddHand("578KT", 124);
            AddHand("666J6", 570);
            AddHand("74968", 331);
            AddHand("2T265", 620);
            AddHand("67677", 439);
            AddHand("AAJ79", 949);
            AddHand("88339", 799);
            AddHand("AJQ9A", 927);
            AddHand("J34T3", 523);
            AddHand("929KK", 551);
            AddHand("2479Q", 227);
            AddHand("KKJKK", 918);
            AddHand("85445", 201);
            AddHand("9Q7J6", 536);
            AddHand("47454", 513);
            AddHand("A2A4Q", 607);
            AddHand("A648J", 759);
            AddHand("A97A7", 514);
            AddHand("J86AA", 240);
            AddHand("Q6374", 576);
            AddHand("4J3Q5", 26);
            AddHand("35A75", 178);
            AddHand("46KQ4", 734);
            AddHand("842A2", 457);
            AddHand("AA4A4", 885);
            AddHand("724Q5", 925);
            AddHand("977J8", 654);
            AddHand("TTT5T", 662);
            AddHand("J3339", 679);
            AddHand("67J67", 548);
            AddHand("QTJJ6", 28);
            AddHand("QTAAJ", 330);
            AddHand("TTJTT", 216);
            AddHand("7A5A7", 483);
            AddHand("5JJ8A", 973);
            AddHand("53AK4", 639);
            AddHand("KKKJT", 640);
            AddHand("43825", 782);
            AddHand("K5KK5", 381);
            AddHand("7QT77", 77);
            AddHand("99777", 40);
            AddHand("777K8", 752);
            AddHand("2QQ77", 811);
            AddHand("J75TQ", 320);
            AddHand("9K552", 445);
            AddHand("A65QA", 125);
            AddHand("Q2379", 469);
            AddHand("74A98", 253);
            AddHand("69969", 340);
            AddHand("QKT84", 748);
            AddHand("TTQQ7", 230);
            AddHand("74QJT", 985);
            AddHand("57757", 311);
            AddHand("Q7T27", 587);
            AddHand("Q8Q99", 135);
            AddHand("6K854", 279);
            AddHand("3TKAA", 717);
            AddHand("77484", 391);
            AddHand("Q2K24", 399);
            AddHand("Q7QQJ", 572);
        }

        class Hand
        {
            public required string Cards { get; set; }
            public int Bid { get; set; }
            public byte TypeRank { get; set; }
            public int Rank { get; set; }
        }
    }
}