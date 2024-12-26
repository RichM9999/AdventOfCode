//https://adventofcode.com/2024/day/14
using System.Drawing;

namespace AdventOfCode.Year2024
{
    class Day14
    {
        private List<RobotInfo> robots;
        const int width = 101;
        const int height = 103;

        public Day14()
        {
            robots = new List<RobotInfo>();
        }

        public void Run()
        {
            var start = DateTime.Now;

            SetupRobots();

            for (var seconds = 1; seconds <= 100; seconds++)
            {
                MoveRobots();
            }

            var factor = CalculateSafetyFactor();

            Console.WriteLine($"Safety factor: {factor}");
            //228457125
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            robots = new List<RobotInfo>();

            SetupRobots();

            for (var seconds = 1; seconds <= 10000; seconds++)
            {
                MoveRobots();
                if (seconds > 6400)
                    DumpRobots(seconds);
                // at 6493 seconds:
                //
                //                  #
                //                  #
                //                                                              #  #           #
                //                                                                 #
                //                                                                #                      #
                //                                                                #   #
                //          #                                                #
                //#   #                            #                              #                       #
                //                                        #                   #                               #    # #
                //                          #                                           #
                //
                //                       #
                //
                //                                                      #
                //                                                                            #         #
                //  #                  #
                //                                                          #
                //                                                                                       #        #
                //                 #
                //                                                        #                           #
                //   #                #            #
                //                    #                                        #      #
                //      #         #                 #                                   ##
                //                        #   #                                                    #
                //
                //                               #                                              #
                //                                              #
                //
                //             #              #                                           #
                //                                                      #                   #
                //                                                            #            #
                //#            #                                                                  #
                //              #
                //
                //                             #
                //                                        ###############################
                //                                        #                             #
                //                                        #                             #
                //                                        #                             #
                //                                        #                             #              #
                //                   #                    #              #              #
                //   #   #       #                        #             ###             #
                //             #                          #            #####            #
                //                                        #           #######           #
                //                                        #          #########          #               #
                //                                        #            #####            #
                //                                        #           #######           #                  #
                //              #                         #          #########          #
                //                                   #    #         ###########         #
                //                                        #        #############        #          #      #
                //                          #             #          #########          #
                //                                        #         ###########         # #
                //                                        #        #############        #
                //                                        #       ###############       #
                //                              # #       #      #################      #
                //                                        #        #############        #
                //                                        #       ###############       #           #
                //      #                                 #      #################      #   #        #
                //#                                       #     ###################     #
                //                                        #    #####################    #               #
                // ##                                     #             ###             #
                //                                        #             ###             #       #
                //                                        #             ###             #          #            #
                //                                        #                             #
                //                                        #                             #
                //                                        #                             #
                //                                        #                             #
                //                                        ###############################
                //
                //                                                       #
                //    #                #                                 #                             #
                //          #                                                                                         #
                //                                                                   #
                //                        #                                 #                  #
                //                                                      #                                #
                //
                //##                                                                                           #
                //                                                          #
                //                       #                                                             #
                //
                //                                  #                           #
                //                                                                    #
                //                #                                                                     #
                //                                                                               #
                //         #         #                                     #               #
                //                                      #
                //                               #        #                                        #
                //                                              #      #                                        #
                //                                                                                                #
                //                                                                                            #
                //     #
                //                 #
                // #                                                           #                    #    #       #
                //                                                                                            #     #
                //                                                                      #                       #
                //                                      #
                //                                                                                   #
                //
                //
                //
                //             #                     #     #
                //                                                                                  #
            }
        }

        public long CalculateSafetyFactor()
        {
            var quad1 = 0;
            var quad2 = 0;
            var quad3 = 0;
            var quad4 = 0;

            // Quad 1
            for (var quadx = 0; quadx < ((width - 1) / 2); quadx++)
            {
                for (var quady = 0; quady < ((height - 1) / 2); quady++)
                {
                    foreach (var robot in robots)
                    {
                        if (robot.positionX == quadx && robot.positionY == quady)
                        {
                            quad1++;
                        }
                    }
                }
            }

            // Quad 2
            for (var quadx = ((width - 1) / 2) + 1; quadx < width; quadx++)
            {
                for (var quady = 0; quady < ((height - 1) / 2); quady++)
                {
                    foreach (var robot in robots)
                    {
                        if (robot.positionX == quadx && robot.positionY == quady)
                        {
                            quad2++;
                        }
                    }
                }
            }

            // Quad 3
            for (var quadx = 0; quadx < ((width - 1) / 2); quadx++)
            {
                for (var quady = ((height - 1) / 2) + 1; quady < height; quady++)
                {
                    foreach (var robot in robots)
                    {
                        if (robot.positionX == quadx && robot.positionY == quady)
                        {
                            quad3++;
                        }
                    }
                }
            }

            // Quad 3
            for (var quadx = ((width - 1) / 2) + 1; quadx < width; quadx++)
            {
                for (var quady = ((height - 1) / 2) + 1; quady < height; quady++)
                {
                    foreach (var robot in robots)
                    {
                        if (robot.positionX == quadx && robot.positionY == quady)
                        {
                            quad4++;
                        }
                    }
                }
            }

            return quad1 * quad2 * quad3 * quad4;
        }
        public void MoveRobots()
        {
            foreach (var robot in robots)
            {
                robot.positionX += robot.velocityX;
                robot.positionY += robot.velocityY;

                if (robot.positionX > width - 1)
                {
                    robot.positionX = robot.positionX - width;
                }

                if (robot.positionY > height - 1)
                {
                    robot.positionY = robot.positionY - height;
                }

                if (robot.positionX < 0)
                {
                    robot.positionX = width + robot.positionX;
                }

                if (robot.positionY < 0)
                {
                    robot.positionY = height + robot.positionY;
                }
            }
        }

        public void SetupRobots()
        {
            AddRobot(9, 89, -73, -15);
            AddRobot(0, 11, -26, -93);
            AddRobot(42, 38, 30, 1);
            AddRobot(53, 4, 49, 38);
            AddRobot(42, 83, 63, -60);
            AddRobot(42, 62, 70, -3);
            AddRobot(67, 18, 21, -58);
            AddRobot(10, 89, 19, 94);
            AddRobot(59, 101, -77, 67);
            AddRobot(0, 19, 54, -73);
            AddRobot(76, 75, -10, 96);
            AddRobot(7, 29, -95, -25);
            AddRobot(69, 65, -70, -5);
            AddRobot(40, 81, 41, -67);
            AddRobot(40, 81, -20, -37);
            AddRobot(79, 28, -48, -65);
            AddRobot(50, 31, 7, -45);
            AddRobot(68, 93, -4, 91);
            AddRobot(96, 30, 12, 31);
            AddRobot(73, 40, 79, -62);
            AddRobot(6, 57, 61, -1);
            AddRobot(21, 85, 40, -11);
            AddRobot(52, 15, 25, 8);
            AddRobot(42, 53, -76, -51);
            AddRobot(98, 30, 6, -45);
            AddRobot(60, 95, 59, 16);
            AddRobot(52, 48, -84, -76);
            AddRobot(64, 64, 66, 63);
            AddRobot(79, 13, 55, 61);
            AddRobot(33, 85, -49, -96);
            AddRobot(12, 34, -96, 33);
            AddRobot(80, 62, -8, -58);
            AddRobot(29, 0, -32, 79);
            AddRobot(80, 39, 60, 57);
            AddRobot(83, 41, 28, 20);
            AddRobot(49, 79, 63, 23);
            AddRobot(75, 46, -1, -48);
            AddRobot(61, 35, -55, 66);
            AddRobot(60, 84, -7, -8);
            AddRobot(24, 81, -27, -29);
            AddRobot(22, 47, -34, -80);
            AddRobot(21, 47, 32, -49);
            AddRobot(44, 95, 77, 64);
            AddRobot(59, 84, 21, 99);
            AddRobot(32, 22, -20, 83);
            AddRobot(32, 78, 18, 72);
            AddRobot(81, 70, 27, 46);
            AddRobot(23, 91, -44, -8);
            AddRobot(33, 83, 81, -32);
            AddRobot(72, 44, -62, 63);
            AddRobot(6, 2, 66, -3);
            AddRobot(95, 53, 30, 50);
            AddRobot(15, 87, -51, -87);
            AddRobot(79, 88, 20, -34);
            AddRobot(11, 86, -89, 15);
            AddRobot(10, 53, 8, 79);
            AddRobot(83, 81, -99, -85);
            AddRobot(60, 75, -95, -5);
            AddRobot(32, 66, -41, -82);
            AddRobot(7, 34, -48, 26);
            AddRobot(62, 79, -84, -82);
            AddRobot(32, 33, 72, 87);
            AddRobot(54, 96, -94, -15);
            AddRobot(43, 98, -52, -62);
            AddRobot(34, 64, -65, -62);
            AddRobot(61, 51, 55, -73);
            AddRobot(26, 64, -20, -2);
            AddRobot(85, 16, 92, -17);
            AddRobot(55, 6, -98, -36);
            AddRobot(35, 18, 53, -96);
            AddRobot(84, 31, -81, 89);
            AddRobot(5, 84, -89, -4);
            AddRobot(85, 42, -94, -47);
            AddRobot(78, 16, 38, -90);
            AddRobot(38, 59, -76, 52);
            AddRobot(5, 37, -61, -97);
            AddRobot(96, 15, 89, -17);
            AddRobot(71, 65, 17, 99);
            AddRobot(89, 58, -29, 54);
            AddRobot(19, 87, 50, 16);
            AddRobot(10, 20, -68, -70);
            AddRobot(16, 57, -65, 25);
            AddRobot(35, 9, -37, -46);
            AddRobot(82, 10, 86, 36);
            AddRobot(45, 41, -69, -86);
            AddRobot(5, 21, -3, -87);
            AddRobot(35, 11, 88, 32);
            AddRobot(39, 94, -61, 92);
            AddRobot(81, 88, 24, 19);
            AddRobot(33, 10, -48, 15);
            AddRobot(14, 42, 71, -77);
            AddRobot(33, 99, 67, -85);
            AddRobot(67, 77, 52, -7);
            AddRobot(9, 61, -61, 48);
            AddRobot(57, 68, 52, 74);
            AddRobot(89, 58, -29, -77);
            AddRobot(54, 5, -75, 78);
            AddRobot(25, 94, 74, 94);
            AddRobot(30, 65, -86, -54);
            AddRobot(47, 45, 46, 16);
            AddRobot(98, 19, -35, -37);
            AddRobot(97, 71, -92, 46);
            AddRobot(64, 40, 57, -21);
            AddRobot(100, 32, -51, -73);
            AddRobot(80, 91, 87, -42);
            AddRobot(86, 82, 90, -9);
            AddRobot(56, 11, 94, 87);
            AddRobot(31, 66, 74, -54);
            AddRobot(52, 70, -84, -1);
            AddRobot(87, 85, -18, -85);
            AddRobot(6, 102, -40, 14);
            AddRobot(15, 2, -37, 60);
            AddRobot(39, 90, -97, 67);
            AddRobot(26, 74, -34, 97);
            AddRobot(33, 79, 74, -84);
            AddRobot(71, 27, 83, 58);
            AddRobot(83, 31, -32, 33);
            AddRobot(98, 15, 9, 88);
            AddRobot(27, 44, 36, 38);
            AddRobot(86, 96, 90, -15);
            AddRobot(80, 67, 87, -27);
            AddRobot(73, 99, 48, -11);
            AddRobot(12, 82, 95, -87);
            AddRobot(54, 79, -73, 21);
            AddRobot(100, 55, -33, 29);
            AddRobot(18, 4, 12, -12);
            AddRobot(72, 33, 79, 53);
            AddRobot(42, 81, -16, -99);
            AddRobot(26, 91, 29, 20);
            AddRobot(75, 26, 55, 58);
            AddRobot(17, 86, 78, 45);
            AddRobot(27, 54, -41, 78);
            AddRobot(25, 87, 50, 70);
            AddRobot(35, 73, -66, 97);
            AddRobot(100, 40, -79, -55);
            AddRobot(38, 82, -76, -32);
            AddRobot(16, 69, 78, 20);
            AddRobot(85, 34, 13, 83);
            AddRobot(22, 60, 6, 34);
            AddRobot(38, 11, 32, 63);
            AddRobot(42, 93, 4, -12);
            AddRobot(9, 71, 5, -55);
            AddRobot(100, 3, -83, -83);
            AddRobot(68, 102, 48, -55);
            AddRobot(19, 75, 46, -7);
            AddRobot(23, 49, -79, 57);
            AddRobot(51, 90, 63, -59);
            AddRobot(32, 67, -33, 3);
            AddRobot(40, 86, 91, -51);
            AddRobot(32, 98, -27, -11);
            AddRobot(48, 60, 27, -12);
            AddRobot(34, 80, 67, 72);
            AddRobot(87, 48, 58, -78);
            AddRobot(73, 76, -43, -98);
            AddRobot(18, 69, -97, -41);
            AddRobot(5, 30, 40, 3);
            AddRobot(81, 82, -88, -35);
            AddRobot(75, 82, -26, 37);
            AddRobot(92, 40, -31, -72);
            AddRobot(6, 28, -75, -71);
            AddRobot(14, 54, -75, -8);
            AddRobot(65, 72, 71, -90);
            AddRobot(51, 27, -80, 34);
            AddRobot(21, 93, 78, 43);
            AddRobot(19, 20, 54, 8);
            AddRobot(78, 53, -56, 48);
            AddRobot(20, 49, 48, 93);
            AddRobot(12, 6, -75, -44);
            AddRobot(34, 30, 81, 53);
            AddRobot(54, 22, 63, -42);
            AddRobot(84, 35, 8, 68);
            AddRobot(37, 32, 53, -96);
            AddRobot(97, 14, 79, 49);
            AddRobot(78, 51, 79, 30);
            AddRobot(1, 90, -68, -8);
            AddRobot(54, 19, 56, 87);
            AddRobot(26, 57, 88, 78);
            AddRobot(56, 75, -7, -6);
            AddRobot(79, 77, -71, -81);
            AddRobot(82, 81, 27, -37);
            AddRobot(68, 30, -21, -42);
            AddRobot(42, 8, -38, 13);
            AddRobot(1, 34, -10, 77);
            AddRobot(21, 70, -52, 67);
            AddRobot(77, 37, -8, -46);
            AddRobot(57, 65, 73, -81);
            AddRobot(33, 77, -33, 95);
            AddRobot(33, 28, -34, -19);
            AddRobot(74, 21, -53, -96);
            AddRobot(56, 102, -66, -68);
            AddRobot(12, 52, 96, -4);
            AddRobot(96, 92, 2, -9);
            AddRobot(27, 31, 1, 33);
            AddRobot(81, 56, -77, -27);
            AddRobot(3, 27, 76, -3);
            AddRobot(99, 97, 44, 68);
            AddRobot(73, 7, 80, 66);
            AddRobot(25, 65, -97, 23);
            AddRobot(33, 85, 51, 28);
            AddRobot(8, 99, -75, 15);
            AddRobot(16, 74, 40, 42);
            AddRobot(30, 80, 67, -3);
            AddRobot(88, 38, -25, 59);
            AddRobot(21, 20, -58, 8);
            AddRobot(45, 33, -31, -17);
            AddRobot(34, 37, 25, -23);
            AddRobot(37, 99, -76, 38);
            AddRobot(77, 65, 13, -4);
            AddRobot(98, 16, -77, 45);
            AddRobot(24, 61, 11, -52);
            AddRobot(86, 101, -1, -39);
            AddRobot(50, 43, 70, 29);
            AddRobot(41, 10, 84, -92);
            AddRobot(31, 61, 88, -32);
            AddRobot(42, 50, 42, 2);
            AddRobot(31, 43, 24, -57);
            AddRobot(90, 61, 42, -88);
            AddRobot(64, 83, -21, 70);
            AddRobot(42, 57, 51, 43);
            AddRobot(12, 1, 78, 86);
            AddRobot(62, 56, 59, 76);
            AddRobot(46, 79, -91, -66);
            AddRobot(97, 19, 2, 86);
            AddRobot(88, 25, 86, 34);
            AddRobot(83, 89, -94, 60);
            AddRobot(71, 2, 36, -60);
            AddRobot(99, 19, -49, 96);
            AddRobot(29, 92, 67, 97);
            AddRobot(72, 99, -75, -76);
            AddRobot(98, 22, 90, -83);
            AddRobot(52, 80, -28, -83);
            AddRobot(43, 11, -21, -43);
            AddRobot(80, 52, -46, 27);
            AddRobot(6, 96, 68, -63);
            AddRobot(67, 50, -69, -69);
            AddRobot(53, 20, -33, -8);
            AddRobot(21, 12, 40, -69);
            AddRobot(43, 21, 87, 81);
            AddRobot(22, 96, -55, -15);
            AddRobot(44, 27, 91, -37);
            AddRobot(34, 23, 74, 32);
            AddRobot(49, 35, 74, -20);
            AddRobot(9, 42, 3, 89);
            AddRobot(36, 26, -10, 84);
            AddRobot(52, 37, -45, 81);
            AddRobot(48, 16, 63, -43);
            AddRobot(23, 25, -20, 86);
            AddRobot(27, 85, -97, -30);
            AddRobot(37, 40, -58, 23);
            AddRobot(33, 44, -65, 6);
            AddRobot(35, 16, 3, -12);
            AddRobot(57, 77, 45, 72);
            AddRobot(39, 46, 32, -52);
            AddRobot(68, 33, -14, -17);
            AddRobot(42, 39, 36, -6);
            AddRobot(56, 100, 98, -67);
            AddRobot(10, 11, -9, 36);
            AddRobot(20, 78, -50, -61);
            AddRobot(18, 88, 61, 71);
            AddRobot(64, 35, 7, 95);
            AddRobot(90, 69, -50, -53);
            AddRobot(20, 88, 50, -17);
            AddRobot(84, 58, -53, 83);
            AddRobot(75, 82, 55, -79);
            AddRobot(61, 27, -59, 63);
            AddRobot(26, 56, -27, -78);
            AddRobot(98, 13, -68, -20);
            AddRobot(7, 101, 26, 15);
            AddRobot(26, 5, -93, -63);
            AddRobot(32, 30, -79, 61);
            AddRobot(83, 56, 41, -51);
            AddRobot(71, 62, 27, 25);
            AddRobot(5, 61, 51, 13);
            AddRobot(95, 4, 86, -77);
            AddRobot(65, 61, -26, -92);
            AddRobot(32, 54, -41, 27);
            AddRobot(72, 102, -86, 30);
            AddRobot(87, 75, -81, -57);
            AddRobot(31, 32, -55, 81);
            AddRobot(99, 51, -1, -50);
            AddRobot(5, 23, 4, 77);
            AddRobot(7, 52, 96, -11);
            AddRobot(5, 30, 84, -36);
            AddRobot(56, 0, 35, 64);
            AddRobot(53, 78, -14, -58);
            AddRobot(73, 56, 72, 85);
            AddRobot(24, 38, 40, 89);
            AddRobot(88, 2, 13, 68);
            AddRobot(30, 68, -83, 49);
            AddRobot(13, 95, -16, 90);
            AddRobot(48, 27, 34, 89);
            AddRobot(77, 80, 55, -9);
            AddRobot(69, 28, -39, 85);
            AddRobot(19, 52, -51, -80);
            AddRobot(10, 28, -18, -88);
            AddRobot(85, 32, 80, 32);
            AddRobot(55, 86, 11, 10);
            AddRobot(64, 34, 52, -22);
            AddRobot(88, 72, 68, -4);
            AddRobot(87, 22, 58, -68);
            AddRobot(58, 67, 26, 11);
            AddRobot(85, 45, 76, -28);
            AddRobot(39, 64, -3, -29);
            AddRobot(22, 97, -9, -41);
            AddRobot(5, 60, 40, -55);
            AddRobot(61, 1, -14, -37);
            AddRobot(10, 14, -2, -43);
            AddRobot(50, 0, -87, 40);
            AddRobot(97, 8, -47, 89);
            AddRobot(61, 94, -70, -61);
            AddRobot(98, 66, -47, 23);
            AddRobot(81, 66, -85, -52);
            AddRobot(76, 47, -39, 52);
            AddRobot(2, 58, -67, -45);
            AddRobot(8, 22, 40, -46);
            AddRobot(87, 65, -66, -14);
            AddRobot(4, 6, 34, -92);
            AddRobot(98, 26, -19, 84);
            AddRobot(45, 4, 77, 61);
            AddRobot(86, 29, 38, 45);
            AddRobot(27, 15, 57, 11);
            AddRobot(20, 6, -76, 20);
            AddRobot(95, 21, 37, -46);
            AddRobot(76, 90, 51, -86);
            AddRobot(13, 15, 88, -39);
            AddRobot(48, 101, 77, -89);
            AddRobot(26, 59, -86, -78);
            AddRobot(76, 3, -53, -19);
            AddRobot(53, 66, 38, 52);
            AddRobot(27, 31, 36, 33);
            AddRobot(8, 48, -79, -2);
            AddRobot(2, 20, -26, 60);
            AddRobot(77, 32, -67, 84);
            AddRobot(25, 85, 8, 69);
            AddRobot(30, 3, 74, 92);
            AddRobot(18, 5, -86, -66);
            AddRobot(78, 57, -56, -75);
            AddRobot(12, 28, -71, 52);
            AddRobot(48, 60, 14, -79);
            AddRobot(81, 71, -81, 96);
            AddRobot(64, 90, -21, -60);
            AddRobot(83, 89, -39, -39);
            AddRobot(15, 56, 71, -26);
            AddRobot(76, 39, 65, 25);
            AddRobot(70, 41, 15, -28);
            AddRobot(72, 40, 74, -85);
            AddRobot(30, 16, -48, 61);
            AddRobot(30, 16, -62, -94);
            AddRobot(97, 18, -5, 10);
            AddRobot(31, 25, 71, -16);
            AddRobot(98, 46, -95, 55);
            AddRobot(34, 79, 46, 72);
            AddRobot(21, 29, 45, 78);
            AddRobot(75, 15, 13, 31);
            AddRobot(2, 94, 68, -10);
            AddRobot(71, 44, -63, -36);
            AddRobot(82, 66, -95, 21);
            AddRobot(38, 75, 47, 9);
            AddRobot(39, 64, 49, 68);
            AddRobot(31, 92, 71, -62);
            AddRobot(3, 15, 61, -93);
            AddRobot(12, 98, 95, 55);
            AddRobot(7, 48, 37, 82);
            AddRobot(42, 80, 81, -51);
            AddRobot(10, 17, 12, 10);
            AddRobot(72, 83, 76, 45);
            AddRobot(73, 70, 80, -7);
            AddRobot(13, 49, -45, -34);
            AddRobot(31, 67, 39, -29);
            AddRobot(41, 69, -95, 45);
            AddRobot(43, 98, 38, -96);
            AddRobot(14, 11, -12, -68);
            AddRobot(1, 18, 9, -39);
            AddRobot(30, 53, 1, -25);
            AddRobot(79, 17, -41, -88);
            AddRobot(48, 5, 7, -18);
            AddRobot(72, 87, 31, 96);
            AddRobot(56, 34, -40, 2);
            AddRobot(21, 11, 8, 60);
            AddRobot(33, 99, 77, 10);
            AddRobot(7, 20, -72, -42);
            AddRobot(14, 43, 14, -5);
            AddRobot(4, 46, 50, -1);
            AddRobot(41, 66, 39, 75);
            AddRobot(93, 100, 37, -14);
            AddRobot(5, 57, -32, 56);
            AddRobot(36, 42, -75, -99);
            AddRobot(12, 28, -6, -49);
            AddRobot(68, 93, -56, 16);
            AddRobot(92, 29, -23, -37);
            AddRobot(82, 73, 17, -34);
            AddRobot(85, 4, 78, 5);
            AddRobot(4, 60, -43, 51);
            AddRobot(6, 38, 68, 29);
            AddRobot(94, 57, -55, -28);
            AddRobot(22, 95, -93, 13);
            AddRobot(66, 14, -81, -65);
            AddRobot(49, 74, 21, -83);
            AddRobot(36, 77, 12, 64);
            AddRobot(2, 84, -37, 91);
            AddRobot(49, 36, -59, -49);
            AddRobot(38, 79, 18, 45);
            AddRobot(77, 91, 44, 66);
            AddRobot(94, 11, -57, 10);
            AddRobot(47, 37, 84, -47);
            AddRobot(15, 56, -16, 98);
            AddRobot(13, 68, 46, -74);
            AddRobot(4, 4, -97, -68);
            AddRobot(27, 43, -86, -58);
            AddRobot(59, 9, 66, 27);
            AddRobot(18, 51, -23, 79);
            AddRobot(20, 96, 39, -11);
            AddRobot(23, 14, -93, 59);
            AddRobot(36, 100, 20, -7);
            AddRobot(65, 52, -39, 4);
            AddRobot(23, 13, -44, -1);
            AddRobot(91, 96, -57, -35);
            AddRobot(57, 10, 66, -91);
            AddRobot(79, 4, 62, -65);
            AddRobot(50, 45, 7, -20);
            AddRobot(76, 81, -50, 48);
            AddRobot(10, 14, -68, 60);
            AddRobot(9, 45, -86, -26);
            AddRobot(37, 59, -54, 38);
            AddRobot(22, 22, 22, 57);
            AddRobot(17, 102, 43, 14);
            AddRobot(51, 44, 89, 55);
            AddRobot(39, 73, -76, -31);
            AddRobot(22, 19, -79, -18);
            AddRobot(70, 85, -78, 44);
            AddRobot(74, 102, -28, -89);
            AddRobot(45, 87, -27, 15);
            AddRobot(20, 94, 15, 94);
            AddRobot(74, 79, 24, -31);
            AddRobot(85, 25, -4, 35);
            AddRobot(63, 35, 38, 84);
            AddRobot(92, 52, 44, 28);
            AddRobot(75, 56, -95, 3);
            AddRobot(75, 18, -91, 87);
            AddRobot(48, 19, -21, 36);
            AddRobot(68, 95, -95, -65);
            AddRobot(21, 53, -61, 44);
            AddRobot(98, 5, -20, 70);
            AddRobot(3, 77, -36, 97);
            AddRobot(5, 47, -58, -48);
            AddRobot(76, 61, -95, -79);
            AddRobot(51, 89, -14, -86);
            AddRobot(68, 22, 6, 58);
            AddRobot(89, 82, 46, -25);
            AddRobot(68, 93, 14, -64);
            AddRobot(63, 39, -76, 6);
            AddRobot(87, 100, -18, 13);
            AddRobot(14, 80, -37, -5);
            AddRobot(45, 68, 70, 76);
            AddRobot(41, 36, -83, 55);
            AddRobot(95, 67, -57, -24);
            AddRobot(45, 78, 56, 18);
            AddRobot(37, 97, 18, 65);
            AddRobot(81, 49, 82, -90);
            AddRobot(17, 62, 85, -2);
            AddRobot(20, 93, -2, 42);
            AddRobot(15, 54, -6, -48);
            AddRobot(51, 34, -87, -47);
            AddRobot(98, 80, 10, -28);
            AddRobot(32, 75, 74, -57);
            AddRobot(38, 34, 25, -72);
            AddRobot(27, 39, 64, -72);
            AddRobot(52, 2, 14, -43);
            AddRobot(9, 0, -1, -4);
            AddRobot(8, 96, -85, -7);
            AddRobot(27, 74, -58, -5);
            AddRobot(8, 8, -26, 89);
            AddRobot(83, 28, -88, -73);
            AddRobot(19, 21, 22, 61);
            AddRobot(69, 35, 38, 81);
            AddRobot(62, 65, 17, 50);
            AddRobot(89, 44, -50, -51);
            AddRobot(58, 19, -7, -93);
            AddRobot(22, 32, -86, -73);
            AddRobot(60, 23, -84, -66);
            AddRobot(94, 68, 34, 75);
            AddRobot(82, 42, -36, -73);
            AddRobot(73, 19, -21, 56);
            AddRobot(62, 42, 73, 55);
            AddRobot(60, 62, -35, 51);
            AddRobot(54, 13, -38, -12);
            AddRobot(89, 86, -1, 12);
            AddRobot(93, 72, 60, -59);
            AddRobot(3, 99, 79, -89);
            AddRobot(9, 58, 9, 46);
            AddRobot(72, 33, 79, 1);
            AddRobot(67, 30, -39, 59);
            AddRobot(31, 71, 37, 6);
            AddRobot(32, 84, -89, 49);
            AddRobot(93, 18, 82, 56);
            AddRobot(60, 86, -77, 18);
            AddRobot(95, 101, 94, 42);
            AddRobot(35, 27, -27, 59);
            AddRobot(49, 46, 49, 1);
            AddRobot(52, 4, -66, -15);
        }

        public void AddRobot(int px, int py, int vx, int vy)
        {
            robots.Add(new RobotInfo { positionX = px, positionY = py, velocityX = vx, velocityY = vy });
        }

        public void DumpRobots(int seconds)
        {
            Console.WriteLine($"Seconds: {seconds}");

            // Look for a line of 4 across connected to 4 down
            var foundlines = 0;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (x < width - 4 && y < height - 4)
                    {
                        if (robots.Any(r => r.positionX == x && r.positionY == y)
                            && robots.Any(r => r.positionX == x+1 && r.positionY == y)
                            && robots.Any(r => r.positionX == x+2 && r.positionY == y)
                            && robots.Any(r => r.positionX == x+3 && r.positionY == y)
                            && robots.Any(r => r.positionX == x && r.positionY == y)
                            && robots.Any(r => r.positionX == x && r.positionY == y + 1)
                            && robots.Any(r => r.positionX == x && r.positionY == y + 2)
                            && robots.Any(r => r.positionX == x && r.positionY == y + 3))
                        {
                            foundlines++;
                        }
                    }

                }
            }

            if (foundlines > 5)
            {
                Console.WriteLine("******************");
                var bmp = new Bitmap(width, height);

                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        if (robots.Any(r => r.positionX == x && r.positionY == y))
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write('#');
                            bmp.SetPixel(x, y, Color.White);
                        }
                        else
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write(' ');
                            bmp.SetPixel(x, y, Color.Black);
                        }
                    }
                }

                bmp.Save(@$"e:\Temp\Downloads\{seconds}.png");
            }
        }

        class RobotInfo
        {
            public int positionX { get; set; }
            public int positionY { get; set; }
            public int velocityX { get; set; }
            public int velocityY { get; set; }
        }
    }
}
