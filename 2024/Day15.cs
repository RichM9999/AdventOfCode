//https://adventofcode.com/2024/day/15
namespace AdventOfCode.Year2024
{
    class Day15
    {
        int mapSizeY = 50;
        int mapSizeX = 50;
        int currentX = 0;
        int currentY = 0;

        char[,] map;
        string movements = string.Empty;

        public Day15()
        {
            map = new char[mapSizeY, mapSizeX];
        }

        public void Run()
        {
            var start = DateTime.Now;

            movements = string.Empty;
            movements += @"><<vv>><>>^v><^>vv>v><vv>^v<>^v<^v>>v<^<<v^>>v<^>^^^^^v^v><<<<v<<v<^>^^v^vv^^>^>^<v<>v><^<>^^>^>vv<>v><>>^>^><v^>^vv><>><<^>^>^v^<v><v^v>^>v^<^^>^<>^v^<>v^^^>>><v^><v<^>>^<^^vvvv>^v<>>v>^>>v<v><<<vv<vv<v^>v>vvvv<v>^<v^>vv<v<><<^^^>^><^^v>v>^^>^<^v<vv>v<^v^v>v><<><vv<^>^<>^v<>><<^^<<<^v<^vv^^^^>v<<<v^vv<<vv>^^v<^<>><>><v^<<vv<v^<<<>^<vv>^<>^>>>^><<<^><<<v^v<>^^^^<<^^>^vv>^><>^<^<^v^<v<v^^><v^<><<v^>><<^^>v<>v^>>><>^><<<<^^<^<<<<v>v>vv<vv^vvv<^v<v^>>>v^><<<^>vv^>>v^<^vv<><><<^<<^v>vv<^^><>>>^<v<<>^v>>>^v><vv>>vv^v<<v>>^<<>vv><><^v<vvvvv<v><>v<><v>^>vvvv<>>vv><>><^v<<>^>vvv>^v<vv^^>v^<^v<v<^<>>^^v<^>>><<<^^<^<^^^^<<>^^^>^>^><v<v<<<<^^<vv>v<<^^^>v^v<v<><>^^>^^vvvv^>^^><<><^^>>^><<v^vv^<>v<^^<v^^v^>^<^v<<<v^<v<><^v^>>^<<vvv^^^^<<^<^^>vvvv>>^<>^^>^^^v<^>v<^<vv<><^v>^<^<>^>>^^v<>vv>>^<>v^^^^^^><vv<^v<^>^vv>v<<>>^v<v<>>vvv>>>>v><>>^v<vv<>^<<^<v<>>v<v^<^><^^><^v>v><<vvvvv<^<v<^v>^<<><v^<>>v>v>>v>v^><vv><<>v>^^<>v<>>^^><v<<v^>>v^>^v^<>v><^^>>vv>v>v<>v^^^<^>^<v>^<>v<><<><^<<><v<>^>v<><<<<^v^<^^v<";
            movements += @"<^<^^<><>^<v><>^<^v>>>^<<>^>>v^v^<<v^>v<<v^^>vv><<><vv>^v^<>><<>>^^^^<<^vv>><<v^^<>><^^^<<vvv><<v^^^vv^^^>><v>>>>><v<^^^v>>>v<>vv><<vvvv^^>vv<v<^<>v^vv^v><^<<^v<<v^<vvv<v<<>^>v^v>^>>v>><><>vvv>>>^v^<<>^>^<>^><^>^^>>v<^>vv^^v<>>>>>v^><^>^>v>v<v<<vv^^^>><v^^^>v^^<^^><><>>v<>>^>v^^>^><^^<v>>v^>v^>v<^<vv^<^><v^v^v^v>v<^^vvv>>^>v>^v^^>v^><<v<><^^^<>v><^^v^v<v<>>^>^<>^<<>v<^><><v<><^<v<^>>^v^>^^vv<>v>v^v>v<<v^<<>>v^^v<v>^^v>v^<^^v><v>v^>^<<v<><>>^v>vv<>v^v<>^^v>>v<v^<><<><>>>^vv>v<v^^vv^<>>>v<>vvv^>^<<<^>><<>^>>^>^v><>^^v^<^<vv^><><>vv>v<^<v^^>^vv^^>^v>^v^>><v^v>>^^>vv>v<v<^>v<><^>>^><v<<<v<v><v><^<>v^^<<^vv^><>><<>>>v^<><>>^v>^vv<>v<^<v^<>>v^^<<<^^><v<^<^^<^>vvv<v<<>v^^><v^^<>>><^v>^v^>v>v^^v>^v<>v^^vvv>>v<<^><^^v<v<^v<^><vv>v<<<v<>>^<^v<<<><<>^<vv<^>v<>vvv>^^>>^>^>>^>^>vv>>^>v>^v<<>vvv^^<<<><<><<vvv<>><><^>v^><v^>>^>^>>^v^<<^<^^^v^^<<v><^<<vv<>^vv<^v<<^>^><>^<v<<>^v<>><v^<^v>^>>^>vvv<>v><v^vv<vv<<<<^vv^v^v^>>>^<v><^^vv<<<<^<^<^><>v<>v<<v^^<vv>v><^v>^<^^^^vvv>^^vv<<>^>^<<v<v^<<v^^<v<^^<v^v^";
            movements += @">^^>>v>>^^vv^vv>^>><^^>>^v<^<><vv>v<v<v>^>><v<^^v><v^>>vv<<^v>>^v>^<vv>^>v<v><<<>^>v^<>v^>^^^<v<v>v<^>vv>>v<><>v^>>v^<<^><<v^^^>vv<>^><>>^<<<>^^<><<v>>v^^<<>><>v><<v<^>>^>>^v^<^>^<>v^v>v>^v^><><>><>vv<vvv<<v>v>vv>^<^<<vv<>^<<>^v<<><<^v>^>>v<>>vvvv<<^>^><<>>^^v<>^^v>v<<<vv<vv^^>v<<v^^<v^v^^><<v><^vvv>>v^<v^<><<<>v<^<>^>v><^^<^<<>><>>>>v<v<<>v<v^<>>><<^<>^>^^^^<>v^v^<><v<^>vv^<^<^^vv^v^>^v>>v><^<^>>>><^>>^>><vv^<>^v<v><<>>vvvvvv^^^<v<>>v^>>v<^v>^^><>>>v^v>^v>>^>>^><^^<<^^^^><><^^vv^><><>v^^>^^<<^^^<><<>v>>vv^^>^^^v<v^v>^v^^^><<><<><v>vv>v<^^>^<<<^><><^v^<^vv<^^^v<>^>>^<^>><><^vv^^>^<^v>^>v^v>^<>v^v<^>v><v<^^>v^<<^v<>vv^v><>v>>v^<^<v<^>v><^<^<>>>v<^>^vv>>>v>v<^^v<v<>v^v<^vv><v>^v>>v>^<><^v><<<vv>v>>v^v^vv^<<vv^<>><<>>^v<v^v>^^vv<v><<^><^v^^<<^vv^^<<^>^>>v<>^><^^v<v^v^^>^^^>v^<<>^^<v^v>>>v<v^<^v><^vv<>^^vv^^v>>^<>v<>>^><<<vvvv><<<v>>^<^v>^>^^>>v>v^v<<v^^^<><<^v>><<^^<<<^^v<<v>>v<v<<vv<v><^v^>^<vv>^><^<v><v^vv>^v<^>v>>>>>^<<^<^v>>v^>v^>^vv>^v>v^v<v^>v^<^<v<>^<<vvvvv^v>v>>^>^<<v<vv<v>>^^^<^<";
            movements += @"<v^v<>>^<v>vv>><<v><v>^^>v^^v<<v<^<^^^><^^>>^>vv<^<>v^<<<<^<<<><v^^<>>>^v>^>vv^>>><<<>v<vv^>vvv<^v<^^<v<<v^>v^^^vvv^><<v<><>v>v^<<^^^v^>^>v^^<>>><^v<>>><>v<vv^^><>^v<v>>><vvv<^vv><><v<>>^><^vv^>v<^>>v>><>vv^^>^^><^<<>^>v^<><<>v<v^<vvv>^^vv>^<^v^>><v<v>>>v>^vv>^<>^^>^><>v<vv^><<^v^>^<>vv^^^>vv<>^v>^>^^<>><>^v<^^><v^v<^v^<vv^>>><>v<<<^^<v^^^><^v^<<^>vvv<^<v^<>>>^v<<<<v<v<<<v^v^^^>^>^><^v^^^><<<><><v<v>>^v<>^<<v>^>^^v^^<<>^v<<>v>^v^v^<<<vv><<<v>>v>^v^^><vv><<<v>>><^>^<><v>v<<<^<>>^<v^<<>v>vv>v^v>v>><^^vv<>><>^v^<v<v^vv^>^>^^^^^<<v<v^<^<>v>^v>v^v>>>>>>vv<<v>v>^^<<vvv^^^^><^vvv>><^<^>v<v>v>^<>>v<v^<vv^vv>vvv<>v>>v><><>v<^><^<^<v><^v>vv^^^<^v^<<<>^^>>^<^<>>>vv^^vv^>^>vv^>>v>^>>>^>v^<v<>^^v><>><>^^<v>>v<^^>^^v^>^<<^<>>>>^vvv<>>^^v<vv<^^^^>^>^^<v<v<v<^^>^v<^^>>^<^^<>^v<^>>^<<v^^^<>>>>^><<><v>v^^<v>>v>>>v^vv>v^><v^v>>v<^v^^^v^<^v<^>vv<>v>>v^v<vv<<^>>^^vv>^v<^^<><v>^^>>^<>^<^^>vvv^>>>><^^vv>>^<<>^v^>vv<<^vvv^><v<vv<^v^>v^>^<vv<^vv<>^<^>>><>>^v>^<^v<^v><<><<^>><><^><^^^<^>^<v>^v^v^<<^v>>>v>><^<^^";
            movements += @"v>^^<<><>vv^v<<<>v<v<><vv<<v^^^vv<^v<^^<<vv^>>vvv>>vv<v<v<<<>v>>^v<>v^>v<<>^<^><v>>v>>>vv^>^<<^^<vvv^^^>^^<v^<vv^v>^>vvv<v^>v>^v<<^<v^v>>^<vv^<v^^^v^<<<<>^^>^vvv<>v>vv<^^v^>^>v^^>>>v^^>v<><v<^v^^^<vv<<<<<^vvv^^^^>^^v>v^<<^^^vv^<^>>^<^v<^<>>v^^<v^<<<>^vv^^^^<>v<>>^>>v>^v^^^><vv^>v><<vv<<^<v<^v>v^v>>>>v<<<<^v^vv<>^vv>>><<v^><<^^^>>vv^^>>>vv^<>v^<^<><v^^>v>>^>>v<<v<>^>>^v^v<>^<<^<^^>><>^>v>^<><v>>^v^^>>vvv^^^v^^>>>><<>v<><^<v<^>v<^^>><<v><>^<>v<v>v<vv^>^>^<<^^^><^<<<<v^>^<^v^><vvv^>v^^vvv>><^<^<<>v^^>v^><^<v>>^^v<<^<<^vvv^^>^^vvv<<v>>>^v^vv<v^v^>^>^v<<>^>v^<vvv>>vv>^<^>^^>vv<>>^<v^^^<^>^v^<>^><v^>><v<<^<^<>^>^<^<<v^^>><v>^<>>^v^><<^v>^>>>^<vv<<<v^<v><vvv^<>v>^^>>><<><v^^^<v>>v<v^>>^^^^>>^v<^>><^^^vv^<v<>>^vv^>^<>><<^>>>>>><v<>vv^^>v<>>v<^><v<^^<>v>v^^v<>v<<>><^><v<>>v^>>vv<<>>^>v^v<^<^^<v^<>^<>v^<<^><><v^<>>^^v^^<v^^<>^v>>^^v><<v^v><>^>>^vvv><^^<<>vv<>><<><<<<v^^^<v^<^^>^>><^^^v><v^v^<v><<<>^^>^>vv^<<<v><><<^vv<v^<v^^^^>>^<vv<^>v<<^>>^<>^<>>^<v>^<<^^><^><>^v^>>v><><<>^>>v>>>^<>^>^<v<<^^^>";
            movements += @"vv><>^<v^vvv<>><^<<vv^v^<>v>>v>v^^v><^>^<v>^v>vv><^>^^^><<^><>v^>v>^<v<^<vvvvvvv<v<v><^v^<>^>v><>v>>^^>v<vv^<v<v^<><<>v><vv<v<<<<><>>>^<v^^<<<v<>v^v>>>><^v<v^<<vv^><^<<>>^^>>>vv><<v<<>><^<>^<^^v><^>^<vv<^v<<vvvv<^>vvv>^>vv><><<>v^v^<vvv><>v<<^v<vvvv<^^^>^^><<><vv^v<^><><^^<^vv<<vv^v<<vv<<^>><<^<<>^<^>^^v<<^<>><>^>^vv<v^>>^^v<v>>^v<vvv>v^v^<<<>^v<v^<>v^<v^v<^v>>^<^^^^<><v<^>v<<<>^<v<<<><>><v^<v>v><^^^>v^^v>><v<v^^<v<v>>v<^v>>^>>vv>v^v>vvv<>v>v>><><<<<><<^><^v^v^^^>^>vvv^^^^^><<^^^v<v>^^>^<>^^^vv^^<^<>v^v^^^<<v^^^<^<vv>v<^^><^<><<^v>v<><v>^<<^>vv^v<><<<>^v<<<<^>><<^^<^v<v<>>>>^<>><<vv<<^<^vvv>vv^<>^>v>^v>v><<>>v^<^^><<^^<>>v>v>^v>><^>>>vvv>>><<<^<<v^<vvv>>>>^v<v<v^^^>vvv<<^^v^v>>v<v<^><^v><<^vv<vv^<^^<vv^><<<>v^vv^v^^>^v>v><<<<<^v<vvvvv<<^^v>><>v>^v>>^^v><><v>vv<v<>>^v<vvv>v<<v>^^^^>><^<><<v>v<^<<vv^v^<^>><<><^<^><^<><v<^>^>>^>^>^<<<vv<^^<v<vv^<>>>^v><>^<>^>vv^<><^^v^v<<>><v<^>>^v>v>v>^v^>^v^v><<^<^v^><<v>^vv>^<v^^v<v<^<<<>v^>>>vv^<<>>^>^^>>^>><>^^><<>vv<v>v>^v>>^<^vvv<<<^^<v>v^^^>^<^>v^";
            movements += @"^><^v<>>^<<^vv^<v<vv><<^^<^^<><>>^><>><^>^v>>v^v>v><>^vvv><<^>^^v^<vv<v>^>>v><^v<<v>vv^v^>>>>^vv<<v<>^^v^^v<<v>>v<v><v<<>><<vv>>vv>^v>v>^v<<v^<<v>vv<^^<<vv<<<><>>vvv>v^<^<><v>^>^^vv^<^v>><^v<<>>^vv>>v^>^^<<v^v>>v<<v>^^^>^>v<v<v^<>^<^^^v><^vv^><v<<^>>>^><<><<>>vv>^v>v^>v>^<v<<^<>vvv><>><<v^v^>>^>>>^<<v<<v>><^>>>^v^>vv><><^<<v<><<<v<v<^vv>>>><>^>v>v^^^v<^^^v>^^v>v>^<v^<<<v><<vvv<v>^>vv<<v^^^^><>>v<^>^v<><vv<vv<vv>>>^^^v<v<^<v<>><<>v^v>v<<vv<<^v^<v^vv^><^^v^v>>>>^<<<v>^v><^v^><^^>><<<^^<vv>^>^^<<<^>vvv^><vv>><<><>v^<v<v>vv<>v<v>^<vv<^v^<>v<vvv<>v<^<<^<><<><^<v>^<>>>^<v><^>v>^<>^>^v^^<<v<>v><<^v<<vvv<><^>^<>^^><<^v<^^v>^^vv^<<><<>^^v^^>>>vv<^^<^^^>v>^>^<<><<^v^>v^>^^v><^^>^v<v<>^^<vv^^^v>>v<>>^<<<>vvv<^v^^v^^^<<<^>v><>>v>>v>v<v<^>v^<v^<^v^^v>^>vvv^v<^v<>^^><^<><>^^^<v<^<^^^v^<<<vv^v><v><>>^v>^<v^^v>vv>^^^^>^v^>v>>^v^v>v<^<^<v^<<^^>>vvv<v<^<<^<><<^^v^vv^><<<<>v><<<<v<<^v^^>^><>^<v><>><v^v^^>>>>v<<^<vv>^^<>^^v<^^>^v<<^><>v^<<>^<<v^^vv>^vvv>v>^<<>vv><v<>v^<<^vv><v><>^^<^>v<<^>>><^>v<<>^>^><v>";
            movements += @"v^>v<>^<v><><vvvv^>v^>^<^^>>vv^<v^^><<>><v<<<v>^vvv^>v<v^v<><>v<vv>v>>^v^<<>v><^^>>>>v<<>><^><>>^>v>>^vv^<^v^<v<^<<<v<^<v<>>>vv^v<v>^v<^^v<^<>><vvv<>^<v^^><^<<v><^<<>>>v^^<^v><v><<^^<v>v^<^v^<v>>>>vvvv>>vv>>^<^<>>v><>v^^v>v<v^><v<v<<^<^<>^v^>><v>>^<v<v>^<^v><>^>>^v><<<v>vv^v<>^v<v>^<>>^^^vv^>^^<^<<<^><>^^^>^<>>vv><^<^vv<<>>^v<><>v<vv<>^<<^<^v<vv^v<^>vv><>>^^><<<>v>^v^<^v>v<^<><^><v^^><v<v^><<^^>>^v>^>^<>^><>vv<v^<v^<v<>v^v<<>^^^^<<^v>^<<>><^><>v^<<<v<<><>>>>^>>v<>>^^>^^^>><v^<^<vvv<<^^^^v<>><><v^>v^^v<v^^<^>v<<^^v^>^vv^vv>^v>>^^vv>v^<>>^>^<>>vv<>>^^^<^^v>><><<v<v<v>><^<^vvv^^^<><>^>^<v<<v><v><>>>>>>vv^<v><^vv>^<<<>^>v><>v<<<>vv^v<<^>^v<v<<<v<v^>>vv^v>>>v<><^>>>^>v<>vv^><v><<^>^v<v<vv^v>vv><<^<<^<>^<>^>v<^^^v<v<<v^v>v>>v><v>>v<^><^v^<<vv^><^v<><>^<<>^<v<>^v^^v<<^v<<vv>v<>>>vv<^<<v^v<v^<><^vvv^>>^^>>v<^>>^v<<v^^^vv<><>^vv>v^vvv^^v^v<><^^v^^^<<>^<>v^^v^>>>v><^>^><^^v>^>>^v>v<^<>><v^>^><v<>^v>^<vv<>^vv<>^>^<<^>>^<><<^<v^vv>>v>^vv<<<<vvv^<v^v<<vv<>v<v^<^>v<v<^vv<v<^^>>><<^^<v>><<>>^v>^>v^>>";
            movements += @"^^<><^>vv<>>^<>^>^^^v<>>>>>vv^<vv^v>v>>^>^v><^<vv<>><<>>>>^^v^>>^vv<v<v>^v>^^<^vv^vv<><^v>>>><<<<<<<<v<<vv<^<^^vv<vvvv^^^<v<><v<^<vvv<>^v^^>v^v><vv^^^<<^<vv^v>v<v><v^<>v<v^><>vv>v<v<v^vv^^>vv>^>^^><<<<<^<v>>^<<v<^<>vv^vv>>>v<>>v><>^^<^^^>^v^^^<<<<v><<<><<^<v<^vv^vv^v><v^<v<v<v<vv^v<<v^^v>vv^^>v<<<<<vv>^>^v>^^<>^v<^v<<><><vv<^vv^>^v>><^vv>>v<vv<<^>>>v^<<vv>>vvv>><<<<>v>v<<^^>^^><^^^^>^v>vv^^v><<><^^^^>>vv^^<^<<<<^>v^v<^>^>v^^v><>v<v<><>v<<>v^<<vvv^v^><^<><>>^^>>v>^>^v^^<^^v>vv<^^<>vv^><^^vv^>^>vv>>>>v><^<<v<^^v^v^v>>>v<><v<v>>^v^^<>><v^<><^<<^^>^>^^v^>>^v<<<^vv>>>><<<<><v<>>^>vv<vv>v^<>v<>>v<^v>^^^<^<^^^^v<<>vv>v<<^><v>><>>v<^<^>>><^<v^>>^v>v>^vvv^v<><^<v<<^>^^^><^^v^v^^v<v^^v<>>^^<^^^v>>^v^^^^>>>vv<><vv^<vvvvv>^<>>><<v><>>^vv^vvv>^<v^<v^<<v<>^>>>^v>>v>>vv>v^vv<^>>vvv<v<<v>v^>v<<<v>><v><vv>^v><><^^^<><<>v^^^v^vvv^^^vv>^<<<^><v<<>^vv<v><<v<^>^^v^^^^<>^v><>v<^<<<<<^>v<<<^<v^>vv><<>^>>v><><><<<v><vv<<><>^^><<^^^^v<<^>><v>v^^^<^<^<v<<^^><<vv<^^>^^>vv^v<v<^<v>v^<<^><>v<<<<<<<><>>v^>>v<>>^<><";
            movements += @"vv^<><v^<<>v^<>v^^>^^>>><>><<<<^<>vv^v>^<^<<>>>^vvvv<v^^^vv>>>^>^>>^<^v^^v>^^>vv^<><^><>>v>vv^>v>v^v>>><^^<v<>v^>>>>^^^vv><^>>>^v><><<>^^^<v<v<>^v>><>vv^v<<v^^^^^v>^v^v^><^v^vv<<><<^v<vvvvv^<><<>><>^^>>>>>>>^^>^<<^v>v^vv<<^v<><^^v<v<<><v<<^^v^>v<<^^v^v^>>v>^v<>v^v><<>^<^^v^vv>^<>>^>v<v^^v<^v<v<^vv^>v<^<><^^<<<v^^^<>>><<>^v>v<vvv^^vv<>v^>^vv>^v><vvv^><^>>^>^^v>>^<>v>>>vv^<>^^v>v<>>^<v<>>>^^^<v^<^>^v<>v<<v^<><^vv^><><^<<v^^^v^<>^^v<>>^^v^><<<^<vv<^>vv^<>^v^><^<^>v<^>vv<vv^vv<^^><v^<^<><^^>>^<v<<<vvv^v>><>^^<<><>><vv>>>vv>>v^><>vv<>>>vv^><><<v>>><>>><^<>vv>^v<^^^v>><<>><^v<v>^>^><<>^>>>v>^v^^^v><><<^^^v>>>^^>^<v<<v<<v^v>v<^^<><<^><vv>>^v^>^^<<<<^vv^>>><><^^^<^v><v<^<><^>^<><>v><v<<>v^^^<^vvvv<vv^<v>>v<^v>^<<>>^><^<v^v^vvvv>^^v^v<>^>^<^>^v<>v<v^vvv^^>^^>vvv^<>^^<v<<>^<<<>^<><<v^^>^>v<<<><vv^<v^v<<v<<^<^^>^><<^^^^v>>>vv<v^^^>^^^>v>v^^v><v>^^vv^><vv>^v>>v>vv>>>><vvvv^<<v>^>>^vv><>vvv<^>^^>^v>v^^<><^<v<<>v<>v^>^v<^^vvv<v>v>>^>v^vv><>>^<v><>>^^v^>>v>>v<^^<>vv>^>^<v^^<<^>^^^>><^v^^<v>^>^v>^vv^<";
            movements += @"vv>>><^v^>v^>>^<^<v>vv<v^^>^v>>^>>vv<v^^>vv<<<>v^v<<><>^<^^<><^<>^v^vvv><^>><^>>v^vv^>>^^^<><v<<>>^v^>^>^<vvv^v<<<^v<<<^<v^>v>>^><<^<>v>v^>>v<^><v<>>v>v^^><>^<<^^^v^vvv<<^^<^v<<^>^<<<^>>^>^^v>>v^>v<<<^^<<<><^>^>v^^^v<^>>^^vv^^<>>v><v^<v^>vvv>^><>v>v<v><^v><<<v>>^^^<v^><^>^>v<<><^<<>>>v>>>^<<^v<<<^<<<v<>v><^^^^v>^v^^^>>v>>^>>v><<^<<>v^v<<^^>v<>vvv^<vv><vv<<^v><v>v^v<<^v>^><v<v>>vv>>vv>vv^<^^^^v^>^<<^<>^v^>>v^>>^^>>v<vv>^<<<v<<^^<<><v>^>^<v>^v^vv>v>>v^^vvv>^^v>><>v><>^vvvvv^^v^>^^v<>^^^><>v<>^vv<<vvv><v<^><^v>vvv><<vv<<^<>vv^>^vv<>>^<><<v>^^vv<>><^>^^^v>v^<<^vv>v<<^><v>vv^^>^v<>>^v><v<<vvv^<>>>^^<>^^>>v^>^v><v^<>><>^<vv><<>v^<v>>^vv^v^<><vv^>>vv<<>v<vv><>>^<v><>^v><v^v>^<<>^<<<v>>^v>^<^v<>^>><<v<<<<v^>^>vv<<<<<^^<>>v><>^<^v>><vvv^<<<^^<><^>>v<^<<>>vv^>^><>^^<^^>^vvvv><>^<<^^v>>^>vv<v<<^<vv<>v^v^>vv>><^v<vv<v>v<><><v><^<^^v>v<<^v<<>v<<^^^vv<>>^>>^v>>v<<v<^>^^>v<>>^vv<>^><v^>>^>v<v>^^<><^>^v^vv^>vv>^>^^<v^>>><><v><<vv>>v><^^><<^v<>v><v>>v^^>^><>v^<<<^><>v>>>>^^><vvv<^vv>>v<v>^><vv^<<^^vv<<";
            movements += @"^v^vv^<<><^>^v>^v<<<^^^v<<<<<>^>>vv^<<<>v^^<v<^>^^^v>vvv><<<>^^^v>vv<^><v^v^v^><><<vv^v^>><<<>v^^>v>^<>>v^^^vv^<<v<v^>^v^<>v><><<^^>^><<vvvvv^><v<^>^v<vv<^^>vv<^v>v>vv<>^>>>^v<<<<vvvv<^<^><vv^>^><v<^^^>>>>^^<><<>v<^<>>>>v>>>^vv>><vv<^^vv^v^<>vvvv^><^>^<^v<v<>^>^<v>^^><v<<vv<^v>>^v><>>v^>>v>><>><^v^^^<<v>^^v<^v<v><^<^v^>^<^>><>>>v>vv<<<>><<<^^<v>^>^^v>v>>v<v^<^^>^<v>><><v>v^<^<<^>v<>>>>v<^^vv><^v^v>^<v>^>vv^>>><<<<><v>^>><>^^vv>><^<<^^^>v^v^<>v>^v^v^v^<v^>>^v<v^v<<>^^>^<<v^><>^<>v^v^<^^^vv^>v<<v<vv^>>^<^<^<vv>>v<<>^vvv>vvvv^v^>>>^^^v>^^>^<^>v>^v><>v<v^<v><^^v^v^<<>^^>^^v^v<>^>><>^^^>>^^><<^v<>>^vv<^v^vv<v<>^<vv^<^<^>v<<>>>>vv<^v<<<><<><<v>^v<<^<vv<^<v<v>vv^<<<^^v>v><vv^>>^v^v>v^<<<>v^><>v^<>v<vvv>v^v^<<vvv^vv^<<v^><><><><>v^^^v^v<>v>vvv^>><<^>>v^^<v>>v^^<v^^<>v<^^<^<>v^<v>vvvv>^vv<v>^vv<<<><vvvv>><<>>^<><>><<v^<^<>^^>vv>^v<^><<><>^^^^v<vvv><v^<v<>^v<v<>><vv><^>^>^vv<^<<>><><^>^^^>>vv<<<^>>^<v^^>^v^<v><^^v>>^><^vvvvv<<^>><vv^>><vvv<>>^<vvv^><<<<><^vv^<>^^<><><^^^v>>>^>^^>^v><v><<^>^v<>v^";
            movements += @">vv^>>^>v>^<^^<^^v^<>vv^>^<vv<^^<<^^v>>><^<<>v^vv>^<><>>><v^^>>>>vv^>>><>^><<^v<^>v>v^<^>>v^>^^^<<><>vv<<>v<<vv^>^><^<>>^<v^v<<^^v><v^^<v<<v<>v<>>v<<^<vv^<<><<^v<>><>>v^<><<^v^^<>^v<v>^v^><vvv<><<v^<>^<v<>^^>><>vv^>>v^<v^><<>>vv>v^>><^v^^>>^^v<>v^<>^^<<<v^^><^<>^<^<>v>^v<v<v^v>>^<<^v>>^v<^<v<^v><><^>>><^^v<^>^><v>vv<^>>v<v<v>>><>^v<>v^v<v>v>v<^<^^<<>v<^^v^<^^<^vvv^<<v>^^<>>^^v>^>vv><^<vvvv>v^v^>vv^>^<v<>^<^<^^v><^>^v<>><<^>>>v<<<<^^<<^<^<>^^>v^>^v^vv^vv<><vv>>>v^^<^v>v><^<v>><>><v<^v^^<<v<v<v<><^v>><^>>^^>v<^^^<v^><>v^<><>>v<^<<<<<>^^<>><^^v^^^<>v><<v<^<>^<^<<^>^^<^vv>^>^><^^^<vv<>>>>vv>v>v><v^><v<<><><<><^v^^v<^vvv>^>v^v^<^<>^>>^<>^^^^v><^v^vv^v>>^^><<^^v<v<^^><<<^^v^^^v>^>^<v^<^<>><^^<^^><<<<<<>^v>><vv>vv<>^^<v>>^<><>v>v<<<<^^>^v>vv^v<<^>^^<<<^^v<>>vv^><^><^v><<v^>v>>^^>>^^v<<><>>>v^<>>^^>^<>^v^^<v^v<<>^v>v<>>>vvv^^v>v^>><^^>v^^^>v>v^>vvv<^<>v>v>^vv>^vv<<<<<<<<^>v<^<<^<<<<v><>^v><^v^>^vv>v^vv<>v>vv>v^^>^v^<v<v^^>><>v>^<<v>vvv^v>>v^v<>v>><^<<<<^^<<>v<<v^^<<v><vv<>^v<^<v>>v<^^><^^<>v>^";
            movements += @"><>^^^>>>>>><^<>^^v^^v^>><v><<>^<v<<v<>^>^<<^^>v^^v^v>v^^^vvv><>^<<v>vv<v^<vv><v<^>^<v^>>^^<^^<v<^^v^v>>^<v^>v<>>>v^>v>>v^v^>v<>><<>^<^>>>v<<><^v><^^^>^^<vv^<><>v^vv>v<<v^^>vv<vvv<<^^>v<^^><>>v<<>^v^>v^v<^vv^<v^><v<>v<<>^vv<v<vv>^v^v<^>v><>v<<><<^^>>><><^><^v<<>v^^vv^>>>v^>>>>vvv><>>>v^^<<^<v^^v<>vv<>v^^^<v><>vv^>v<vv^^>vvv<^^><><^^><<<><<>>^<vv<<<<<<>^^>vv^^<vvvv<v^><vvv^><>^<>vv^<<<<>><>>>>>>^^>v<v^<<^<<^^<<vv<vv<^<>^^<>^vv<<<^><><v>^<^^^>v<><<>v^^v>^v^v^<>v<>>>v<>v^^>>^<>vvv><v<<>^^^^><<<v^<>>v>>v<<^^^><><<>^><<v>^v>>vv>v<<^vvvv><>vv<^^v>vv>>^<<^<>^v^<>>v>^^<>><<^v<v<<<><>vv^<<^^<^>v<v<vv^>><>^^v<<><v^<v><<^^^vvv<vv>><v>><<v<>^vvv^v><v^v<^>v^v^><<v>>v^^<>>^v<^<^^v^<^^>v<v<<^vv^<^<^v<<<^^<><<<<v^^<^><>v>^v^>^v^^^v^<<>v<><<<>^^^<><<v<^v^v^^>^v>>v>><^><<v<^v<>v^vv<^<>^^<>vv^>>><>>^<^v>^>^^>^^^^^v<v^>v><<v>>^<v>^<^^v>>v^>v^^><^v><vvv<v<>^^v^>v^>>^>^^><<>^<<v<>v>><v>vv^<^^<<<v^v>^^vvv^<<<<^^<v^<vvv>>><>>>^^>v><v<^v<>><^^><<^v^vvvvv^v>>^<<^v^v^<>>v>^>v<v^v<^><<>^vv><^>^v^<v<>>^^^<vv^v>><v";
            movements += @"vvv>^>>><v^>^^>>>>>v^^^v<^v^<<>^v>>v>><<<<<>><><^>>^>>^<v<^^v><^^>v><vvvv^v>^<>v^<>^<<>^^>vv^^^^<v<>vv>>^^^><vv<^>v>^v>^<vv><^vv^^^><^<><^>^^<<v^>^^><v>^><^v<<^v<^<>v^<^v>v>><>>v>^>>v<<^^<>>vv<<<<^>^<v>v<>v<^<>^>^^<<<>^<><<<<v><v^<<^>>^^^vv^v^^^<v^vvv>^><v^v<v<^^><^>vv><^>v<>^>>^v<v>>v^><<><<<<^^>>v>>>>v^>>v^><>>^>^>^>^v>^<v>vv>>><>><^^^v^^vv^^v>>>v^<^v^>v>^v>v>^>>^^>v^^v<vv<^<<<v<>><<vv<v>v<v<<>v>>^v^>^<^>>>>^v>^>v<><>>vv^<<^^>v>>^<vv<><><>^<v>>v<v<>><><^^<<>><^<^<>^^>>vv^vv>v^^^<^><^<^><v<^^<v<^v>>^^^^<>>><><^^<v^<v><v<><v^>^>^v><<v>v>v>^v^<v^vv^>^^^>vv<v^>^^><^<vv<><v<vv<><>><<^>v>v^v>^vv><<>^vvv>v<v<^v^^>>>>^^<^^vv<<^^^><^<^<v>^^^<v>>v^<v^><vv^>^v<<^^^>>^>v<^v^^^<>>^<^<^^>>^v^v>v^<^v<vv<^vvvv<<<^<>^v^^<v<v^<^><<^^<<>^><<<<^<^v<<^>v<vv^>>vv^<^<>^<><<^<>>^>^^v^>^<^>^v<>v><v<v<^^v^^v^><>>><^vv^^<v^>>^<>v>v>>>^<>^^<>^vv><^^^v^>^<^v<vvv>v>>v><<<v>vvvv<v^<^>^<<<<<^vvvv^^><vv<<>v<>^><vv^>>^v^v^^>>^^v^>^<<v^v<vv^vvv>vv^>^vvv^^<v^^>><<^>><>>v<^vv><>v>v>vv<^v<><vvv>>^^v^>>>^>^^^v<>v>v<v^>>^^";
            movements += @"<<>v>v^<v<v^>v<^^>^v^>v>^>><>^v>^^^<v^vv><<v^<v<<>><v<^<^>>>>^^>^v^<^^v^>^<<>^^^^^^v^v^^v^>>v>^^<<v<vv<<<v>><>v<v<v><v<>>v<^<v>^v><<v<vv<>^>^v^^vv<v<v>><v^>>^<>^v><>><><^v^^^^><<<v<^vv<>^>v>^v^>^<>>^><>>>^^v<vv>^^v>vv^>>^^><^<<>>^^<v^vv<<><<^^^v>><>^^v>v>>v<^^vv<>v<^v^^<^v><<^^^^<>>><vv^>^>>><<^^>vvvv<vv<<v>v><^^<^^^v>^>^<>^v<^^<v<^v>^v>v^^<^<<>^>>><v<^<>v^^>vv^v>^^v^v>v^><^>>>v>><><v>>^^^<<vv<^>vvv>^^^v<<v<^<vvv>v>vv^^<^vvv<>^v>^>v><<^v<<<>^v><<<^^><>>v<^v>^^^<^^>>v^^>vv^^^v<<v><><^v>>^>><>v<<>>><v<v>>vv>v>v<>>^v^<<><^^>v<^^v^^^v^v^v<^vvvvvv<><<^><vv^>>v^<<^<<><>^^>^<>><<v<^<>v>^<>v^vv^<v>v^<<<v<^vvv><^><>^vvv<<<v>^v>v>^>v><<^^^<v<>>>^>>>><^v<v>>v<v<>vv^<<vv<v^>^<^<vvv>^>^v<^<^v<^^<v<^>>v>^vv^><vv<v>v^v><<>^^^^<<v<>vv<v^<vvv>^^<<>^<<vv^<>>vv<v>><>><<vv<>vv>v>>^^<>^<>>v<^>^vvv<v<<v<>^<vvvv<vvv^>^vvv>v<>v<^><^^vvv<^>>^<v^v>v>^<<^vv^<^^<v^vv<>v^>>>^<>v>>^^>>v<vv^v^^^>>>>>v>v^vv<>vvv<v^<^v>^><<^>^<>vv>^<v^>^>>vv>^<>>^^>>>^<<vvvv<v>^v><<v>>^^v<^v^v<v<v><^>v<^v<<^<^>v<vv^^^>><^^>v>^><v>^^<v";
            movements += @"v<><<<>>><^^<^^<><>v<<>>>>vv<v^>v<<vvv<^><><vv<>^^<>>>v<v>^><><<^<<<v>>>v^v^^^^^^^>>vv><^<^v>><v<><>>v<vvvv^v^>>>>v^vv^<<v>>^>v<<^>v<^^>^>^<>^<>^<^^<>v^><<<><^^<><v<<<v^v>><<v<>vv>v<>^<^<><^^v^>>v>>v<<<v<><><>^>vvvv<><>>>^>v^><<<<^<v><<v<^^<<^>v<^v<v>v<v^^vvv^<><v^><>>^vv<>>vv>>>><v^>>^>>v>^>v<vv<^v>vv<^><v>^>><<<^^<vvv<^v>v<>^v>v^^><<<v^<v^>^><^^>v>>>>>v<^>v^^^>>>v><>>v>v><^^^^><vv<vv^>^<^>v^vv^<><v<><>><<>>v^vv<<<^^>>><<<v><v^vv>^><><^><<^v><<>v^>v^<^>^<><^><>^v>>^>>^^<<><<<v>^<^^>><^^<v^>v^>>v><vv><vvv^v^v<^<vv<<>>vv>><<><v^vv^>v>^<>>v^<<<^<v<><>^v><<vv<<<v<<v^<><v^>>><><v^>^<>^>^vv<^>^>vv^^vv<<v><vv^<^>^^^^<><<<^^^v^<>vv^v>^<^^>vv>vvv>>>v>^^^>v>^^v>^>^^>>^>><>><^><^<^^><<v^v<<^>v<<^v<^<^v>>>>^<^^<<v^vv^^<^^^<^<>^^^<><>^v<>^^^^>>v>>^v><>^v<^v>^<>^<^v<v^^<>>^>^^<>>v>^^v^>v<>^^^<>>v<v<^><vv>v^>>>^<v^v>^<><vvv>^v<^^<^<>^v><v<><<v^<>v>^><v<<v<^vv<vvvvvv><>^v>><v<<v<<>^vv<^<^^^^<<>v^^^vv^v<v<<^^v^>v>^<>v^^^^>v^v<<^>>vv<<v>>^>^^vvv^<>>^>^^<^>^^^^>>^<^^>>>><>><><<><><<v^^v<vv^<>>^>^<>^<^<^";
            movements += @"vvv><<^>^v^^>v<vv<<^^>><<<>^>^vvv<<^^v<vvvv<v><>v>^v<>v<^<v<<^>v<<v>v><>^^^<><<>^^^v<^^><><v<^<>>^>v><^>^^>vv^v><^>v<^vvv>^>>>v<>^vv^v>v>v^v<>v<^v<^<<>^^^<vv>>><>v>><vv>v^>>^<^<v<<>vv><>^vvv>><>>^<^<^^^^<><>^<>vv><^v^^^v<>v<><>v>v>^>>vv<<><v><v^^vvvv^>vv^<^v>v><<<v>v^>>^>v>^>^>><vv><>>^vv^^>><<><vv^<vv^^v^>v>>vv<><<^^<^>v<>>^^^>^<^<<>^<v<v>>^><<<>^^v<^><vv^><^v^vvv><v^v<<^vv^v^<<v<>^v>v^><^><>vvv^^>>v^^v<^vv<^v><^>vv<^^>v<vv>>v<<<><<^>^>^>v^>><>>v>>v>v>^>v<vv><v^><^<><<<<<<v>^^vv>v>><v^<>^v><vv^>v^^vvvv^>vvvv><>^><^>>^v^<v^>v<^^^v<<<^^<<^^^<>^v>>vvv^v>^^^>^v^<^>>v>^^vv<<<^<>>^<^^^^<^>v>>^v^>>v><^<<<v<v>v>^<<vv^<<>^vvv<^^^v^^^v^v^>v<v<^^>>v^vvv><vv^vv^^<<v<^<^<v>^><v<^<<v^^^<>>>>^v^<vv<v^>>>>v^<<<<v>><<<^v<>>^v>v<>v>v><>^^>v>>v<<^^^^>v<>>>v^>^^<^v^v^^v^v>^^<<^<^<^<<^<>>v>^v>^>^^><v^<>v>>>>>>v^v><^^^><^>>>>^v<>^^v<>><^>^<v>v<<<^<<v<^<>><>>>v<<<<>v^<>v<^>vv^^<^v^^>^<v<<<v>vv^>v<><v>^v<^^^<><<<<<<><><vvv>^>>v^^<<^^^^v<<<>v^^<<v>^>vv^^>v^<v^<<<v<v>^vv^>>^v^<<v<^^v>>vvv<>><v<v>><^><>>^^>v^>v";
            movements += @"v<v<^v>>vvv<^^v^v^>^<><<>>v<<v>vv>v^^>>>^^<>^<v>><>^><<<>^^>>>v<v^<^<>^>^v^>^v>^<^^^^<^>v<vv<^v^v<v<^<v<<>^^v^v>^v<><v^^<^^vv^><^^v<>>>>^v^><<>>^v^<>v<>^>v>>v<^>vvv<^^<<vv<^>>v<<><>>v<vv><<v<v>^><^<<vv><>^<>^vv^v><v^<<v>^^<>v<<<^^>>^^<^vv<<v^v><>vv><v<<vv>^<^<>v<^vv^^<v><v^>v^<>><^vv<v^>v>v>v<^^><<>v<^<^>v>v>^<v^^vvvv<>vv>v^^v^^<><>>^>v<vv<><^v<^vvv<v>^v>vv<v<v<>^>^>^v><^<v^<>^v<^v^v>^<^^^<v<>vv>^>^<^<vvv<>vv>vv>vv>^<>^<<<v<<<vvv^<<<^<<>^>^v>>v^v<>>><>v<>>vv<><^>v><v^>><vv>^<v<<^vv><<v^<>><>>><<vvv<^v<^<<<>v^v>vv>v<<<^v^^>vv^^>vv^<>>^^<>vvv>v><<^v<v>>v^<^v>^>>>^>>^^^vv^>^>^>^v<>^v^vv^<<^<>>v>vv<^>>>vv<^^^v^v<^<^v<<vv>>><<<<<<>^>><^>^><>^<v<><<<^^>^<>^v^>v>^<>^^^<>v<>v^^v<>><v^<v<>vvv<^vvv<^^^^<^><>v<v<<<v<<vvv>^^^<<^v>^>v^v>^>^^vv>^v^>>^<<<v<<><^>v^>^>vv^^<>vv^<vvv^<><^^<v<<^<^vv>^^^>>v>^v<v<<>><>^vv>>^<^<<<v^><v^>^>^<>v>^>v>>v>v^v<^<^>vv^><<v<><v^<><>^^^v^>^<>^<v<^>>^^v<v<^>v<<>>^^>>^v^<^>v^>>^v<><v<^^^^<v>>v^^>vvv><v^>v>^>v^>^><><^v^<v<>v^<^^^^>v>^^^>^^>vv<><v>>^<><<<<<vv^>^v>v<>>v>>";
            movements += @">^^<>>v^>vvv<><v^<vv>>v<<<vv<v<>>>v<>v<<>^^<^v>><v^^vv>vv^v<v<^>><><v>vv<vv>^^>v>vvv^^v^^>v^^^<<v<>>^<v>^<<<^>^>><<<><><v<v>>><v^<v<v^v><>^>>^v^>><^<<^<^><>>>><v^>>><>v<>>vv><><^v>>v<<>vv<>v^>>>><><<><^^v<^>v>v^<>>>^<^v^v^>>vvvv>v>vvvvv^v<><>^<<^><<><<^vv>^^v<>vv^><v<>^<v<v><^<v>v^><vv<vvvvvvv^><<<^^<v>^><><<<vv<<^^^^>v<^>v^^<^>>^>^^^v^^^vvv^^v>^v>v^>><><<^^>>v^<>^<^>^vv^^^v>v><<><^v>v^>^v<v^vv>^<^<<<^^<^vv^<v>^^v^<<v><>^<<>^>^>vv^^v<v<v^vv^^v>^<^v>>^<^v^>v^^<<<>v>v>v<v><>v^>v>><v^^^^v^v<>><><>>>v^v>^><^<v<vv>>^v^v>><<<<<^<>v>v>v>>>>^>vvv><>v><v^<v<>>vv^^><<>>>^<^v>^<<^>v<>v^vv^>^<v^>v<v<<<^v>v<^<^v>^>>vv>><<>^<<v<^v^<<>><^v>vv>^>><<^^>vvv><>>vv>^<<^v^^v^<^v><vv<vv^v^<<<^>v^^v><^v<>^vv<^>>v<<><vv^vv>>vvv^<^<v^v^v>^v^>^^v^^>v^><^^>v>^^>v<^vvv^^>>^<><<v^v^>vv>><^vv^><^v>vvv><<<v^<v>v^<<>v<v<v><>vvvv<<^vvv<<vvv<vv>^v>>>v^>^v><^><^<^>^vv>>^<v<<>>>^vv>><v<^<vv<^<v<^vvvv<^<><><^>^v>^^v>>^>vv<<<v><>>v^v<<vvv>v<>vv>v<v<>v<^<^v^v>>>^<^^^><<>^>v^^v<>^>vv<^^>>v^^v^v<v^>^v^v<^>v^vv><<><><<vv<v<vv>";

            SetupMap();

            var startX = 0;
            var startY = 0;

            // Find starting point
            for (var y = 0; y < mapSizeY; y++)
            {
                for (var x = 0; x < mapSizeX; x++)
                {
                    if (map[y, x] == '@')
                    {
                        startX = x;
                        startY = y;
                        break;
                    }
                }
            }

            currentX = startX;
            currentY = startY;

            for (var m = 0; m < movements.Length; m++)
            {
                var direction = movements[m];

                if (TryToMove(direction, currentX, currentY))
                {
                    // Mark current spot clear
                    map[currentY, currentX] = '.';

                    if (direction == '^')
                    {
                        currentY--;
                    }

                    if (direction == '>')
                    {
                        currentX++;
                    }

                    if (direction == 'v')
                    {
                        currentY++;
                    }

                    if (direction == '<')
                    {
                        currentX--;
                    }
                }
            }
            map[currentY, currentX] = '@';

            var sum = 0;
            for (var y = 0; y < mapSizeY; y++)
            {
                for (var x = 0; x < mapSizeX; x++)
                {
                    if (map[y, x] == 'O' || map[y,x] == '[')
                        sum += y * 100 + x;
                }
            }
            Console.WriteLine($"Sum of GPS: {sum}");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");

            mapSizeX = mapSizeX * 2;
            SetupMap(true);

            startX = 0;
            startY = 0;

            // Find starting point
            for (var y = 0; y < mapSizeY; y++)
            {
                for (var x = 0; x < mapSizeX; x++)
                {
                    if (map[y, x] == '@')
                    {
                        startX = x;
                        startY = y;
                        break;
                    }
                }
            }

            currentX = startX;
            currentY = startY;

            for (var m = 0; m < movements.Length; m++)
            {
                var direction = movements[m];

                if (TryToMove(direction, currentX, currentY))
                {
                    // Mark current spot clear
                    map[currentY, currentX] = '.';

                    if (direction == '^')
                    {
                        currentY--;
                    }

                    if (direction == '>')
                    {
                        currentX++;
                    }

                    if (direction == 'v')
                    {
                        currentY++;
                    }

                    if (direction == '<')
                    {
                        currentX--;
                    }
                }
                map[currentY, currentX] = '@';
            }

            sum = 0;
            for (var y = 0; y < mapSizeY; y++)
            {
                for (var x = 0; x < mapSizeX; x++)
                {
                    if (map[y, x] == 'O' || map[y, x] == '[')
                        sum += y * 100 + x;
                }
            }
            Console.WriteLine($"Sum of GPS with double-wide boxes: {sum}");
            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds}ms");
        }

        bool TryToMove(char direction, int currentX, int currentY)
        {
            // Check direction and move to next spot if empty
            // Exit if move would go off the grid
            if (direction == '^')
            {
                if (map[currentY - 1, currentX] == '#')
                {
                    return false;
                }

                if (map[currentY - 1, currentX] == '.')
                {
                    return true;
                }

                if (map[currentY - 1, currentX] == 'O')
                {
                    return TryToMoveBoxes(direction, currentX, currentY - 1);
                }

                if (map[currentY - 1, currentX] == '[' || map[currentY - 1, currentX] == ']')
                {
                    return TryToMoveDoubleBoxes(direction, currentX, currentY - 1);
                }
            }

            if (direction == '>')
            {
                if (map[currentY, currentX + 1] == '#')
                {
                    return false;
                }

                if (map[currentY, currentX + 1] == '.')
                {
                    return true;
                }

                if (map[currentY, currentX + 1] == 'O')
                {
                    return TryToMoveBoxes(direction, currentX + 1, currentY);
                }

                if (map[currentY, currentX + 1] == '[' || map[currentY, currentX + 1] == ']')
                {
                    return TryToMoveDoubleBoxes(direction, currentX + 1, currentY);
                }
            }

            if (direction == 'v')
            {
                if (map[currentY + 1, currentX] == '#')
                {
                    return false;
                }

                if (map[currentY + 1, currentX] == '.')
                {
                    return true;
                }

                if (map[currentY + 1, currentX] == 'O')
                {
                    return TryToMoveBoxes(direction, currentX, currentY + 1);
                }

                if (map[currentY + 1, currentX] == '[' || map[currentY + 1, currentX] == ']')
                {
                    return TryToMoveDoubleBoxes(direction, currentX, currentY + 1);
                }
            }

            if (direction == '<')
            {
                if (map[currentY, currentX - 1] == '#')
                {
                    return false;
                }

                if (map[currentY, currentX - 1] == '.')
                {
                    return true;
                }

                if (map[currentY, currentX - 1] == 'O')
                {
                    return TryToMoveBoxes(direction, currentX - 1, currentY);
                }

                if (map[currentY, currentX - 1] == '[' || map[currentY, currentX - 1] == ']')
                {
                    return TryToMoveDoubleBoxes(direction, currentX - 1, currentY);
                }
            }

            return false;
        }

        bool TryToMoveBoxes(char direction, int firstBoxX, int firstBoxY)
        {
            var lastBoxX = firstBoxX;
            var lastBoxY = firstBoxY;

            if (direction == '>')
            {
                while (lastBoxX < mapSizeX - 1)
                {
                    lastBoxX++;
                    
                    // Found open spot, shift boxes and make space
                    if (map[firstBoxY, lastBoxX] == '.')
                    {
                        for (var x = lastBoxX; x > firstBoxX; x--)
                        {
                            map[firstBoxY, x] = map[firstBoxY, x - 1];
                        }
                        map[firstBoxY, firstBoxX] = '.';
                        return true;
                    }

                    // No open spot, found wall, can't move
                    if (map[firstBoxY, lastBoxX] == '#')
                    {
                        return false;
                    }
                }
                return false;
            }

            if (direction == 'v')
            {
                while (lastBoxY < mapSizeY - 1)
                {
                    lastBoxY++;

                    // Found open spot, shift boxes and make space
                    if (map[lastBoxY, firstBoxX] == '.')
                    {
                        for (var y = lastBoxY; y > firstBoxY; y--)
                        {
                            map[y, firstBoxX] = map[y - 1, firstBoxX];
                        }
                        map[firstBoxY, firstBoxX] = '.';
                        return true;
                    }

                    // No open spot, found wall, can't move
                    if (map[lastBoxY, firstBoxX] == '#')
                    {
                        return false;
                    }
                }
                return false;
            }

            if (direction == '<')
            {
                while (lastBoxX > 0)
                {
                    lastBoxX--;

                    // Found open spot, shift boxes and make space
                    if (map[firstBoxY, lastBoxX] == '.')
                    {
                        for (var x = lastBoxX; x <= firstBoxX; x++)
                        {
                            map[firstBoxY, x] = map[firstBoxY, x + 1];
                        }
                        map[firstBoxY, firstBoxX] = '.';
                        return true;
                    }

                    // No open spot, found wall, can't move
                    if (map[firstBoxY, lastBoxX] == '#')
                    {
                        return false;
                    }
                }
                return false;
            }

            if (direction == '^')
            {
                while (lastBoxY > 0)
                {
                    lastBoxY--;

                    // Found open spot, shift boxes and make space
                    if (map[lastBoxY, firstBoxX] == '.')
                    {
                        for (var y = lastBoxY; y <= firstBoxY; y++)
                        {
                            map[y, firstBoxX] = map[y +1, firstBoxX];
                        }
                        map[firstBoxY, firstBoxX] = '.';
                        return true;
                    }

                    // No open spot, found wall, can't move
                    if (map[lastBoxY, firstBoxX] == '#')
                    {
                        return false;
                    }
                }
                return false;
            }

            return false;
        }

        bool TryToMoveDoubleBoxes(char direction, int firstBoxX, int firstBoxY)
        {
            var lastBoxX = firstBoxX;
            var lastBoxY = firstBoxY;

            var originalSpaces = new List<(int x, int y)>();
            var spacesToMove = new List<(int x, int y)>();

            if (direction == '>')
            {
                while (lastBoxX < mapSizeX - 1)
                {
                    lastBoxX++;

                    // Found open spot, shift boxes and make space
                    if (map[firstBoxY, lastBoxX] == '.')
                    {
                        for (var x = lastBoxX; x > firstBoxX; x--)
                        {
                            map[firstBoxY, x] = map[firstBoxY, x - 1];
                        }
                        map[firstBoxY, firstBoxX] = '.';
                        return true;
                    }

                    // No open spot, found wall, can't move
                    if (map[firstBoxY, lastBoxX] == '#')
                    {
                        return false;
                    }
                }
                return false;
            }

            if (direction == '<')
            {
                while (lastBoxX > 0)
                {
                    lastBoxX--;

                    // Found open spot, shift boxes and make space
                    if (map[firstBoxY, lastBoxX] == '.')
                    {
                        for (var x = lastBoxX; x <= firstBoxX; x++)
                        {
                            map[firstBoxY, x] = map[firstBoxY, x + 1];
                        }
                        map[firstBoxY, firstBoxX] = '.';
                        return true;
                    }

                    // No open spot, found wall, can't move
                    if (map[firstBoxY, lastBoxX] == '#')
                    {
                        return false;
                    }
                }
                return false;
            }

            if (direction == 'v')
            {
                var leftEdge = firstBoxX;
                var rightEdge = firstBoxX + 1;
                if (map[firstBoxY, firstBoxX] == ']')
                {
                    leftEdge = firstBoxX - 1;
                    rightEdge = firstBoxX;
                }

                // Replace original box with empty space at the end
                originalSpaces.Add((leftEdge, firstBoxY));
                originalSpaces.Add((rightEdge, firstBoxY));

                // Move original box
                for (var x = leftEdge; x <= rightEdge; x++)
                {
                    spacesToMove.Add((x, lastBoxY));
                }

                var boxSpans = new List<(int leftEdge, int rightEdge)>
                {
                    (leftEdge, rightEdge)
                };

                while (lastBoxY < mapSizeY - 1)
                {
                    lastBoxY++;

                    var allOpen = true;
                    foreach (var span in boxSpans)
                    {
                        for (var x = span.leftEdge; x <= span.rightEdge; x++)
                        {
                            if (map[lastBoxY, x] != '.')
                            {
                                allOpen = false;
                                break;
                            }
                        }
                    }

                    // Found open spot, shift boxes and make space
                    if (allOpen)
                    {
                        foreach (var space in spacesToMove.OrderByDescending(m => m.y))
                        {
                            map[space.y + 1, space.x] = map[space.y, space.x];
                            map[space.y, space.x] = '.';
                        }

                        foreach (var space in originalSpaces)
                        {
                            map[space.y, space.x] = '.';
                        }

                        return true;
                    }

                    var newBoxSpans = new List<(int leftEdge, int rightEdge)>();

                    foreach (var span in boxSpans)
                    {
                        // No open spot, found wall, can't move
                        for (var x = span.leftEdge; x <= span.rightEdge; x++)
                        {
                            if (map[lastBoxY, x] == '#')
                            {
                                return false;
                            }
                        }

                        leftEdge = span.leftEdge;
                        rightEdge = span.rightEdge;

                        if (map[lastBoxY, leftEdge] == ']')
                            leftEdge--;
                        if (map[lastBoxY, rightEdge] == '[')
                            rightEdge++;

                        var startOfNewSpan = -1;

                        for (var x = leftEdge; x <= rightEdge; x++)
                        {
                            if (map[lastBoxY, x] == '[')
                            {
                                spacesToMove.Add((x, lastBoxY));
                                if (startOfNewSpan == -1)
                                {
                                    startOfNewSpan = x;
                                }
                            }

                            if (map[lastBoxY, x] == ']')
                            {
                                spacesToMove.Add((x, lastBoxY));
                                if (x == rightEdge || map[lastBoxY, x + 1] == '.')
                                {
                                    newBoxSpans.Add((startOfNewSpan, x));
                                    startOfNewSpan = -1;
                                }
                            }
                        }
                    }

                    boxSpans = newBoxSpans;
                }
                return false;
            }

            if (direction == '^')
            {
                var leftEdge = firstBoxX;
                var rightEdge = firstBoxX + 1;
                if (map[firstBoxY, firstBoxX] == ']')
                {
                    leftEdge = firstBoxX - 1;
                    rightEdge = firstBoxX;
                }

                // Replace original box with empty space at the end
                originalSpaces.Add((leftEdge, firstBoxY));
                originalSpaces.Add((rightEdge, firstBoxY));

                // Move original box
                for (var x = leftEdge; x <= rightEdge; x++)
                {
                    spacesToMove.Add((x, lastBoxY));
                }

                var boxSpans = new List<(int leftEdge, int rightEdge)>
                {
                    (leftEdge, rightEdge)
                };

                while (lastBoxY > 0)
                {
                    lastBoxY--;

                    var allOpen = true;
                    foreach (var span in boxSpans)
                    {
                        for (var x = span.leftEdge; x <= span.rightEdge; x++)
                        {
                            if (map[lastBoxY, x] != '.')
                            {
                                allOpen = false;
                                break;
                            }
                        }
                    }

                    // Found open spot, shift boxes and make space
                    if (allOpen)
                    {
                        foreach (var space in spacesToMove.OrderBy(m => m.y))
                        {
                            map[space.y - 1, space.x] = map[space.y, space.x];
                            map[space.y, space.x] = '.';
                        }

                        foreach (var space in originalSpaces)
                        {
                            map[space.y, space.x] = '.';
                        }

                        return true;
                    }

                    var newBoxSpans = new List<(int leftEdge, int rightEdge)>();

                    foreach (var span in boxSpans)
                    {
                        // No open spot, found wall, can't move
                        for (var x = span.leftEdge; x <= span.rightEdge; x++)
                        {
                            if (map[lastBoxY, x] == '#')
                            {
                                return false;
                            }
                        }

                        leftEdge = span.leftEdge;
                        rightEdge = span.rightEdge;

                        if (map[lastBoxY, leftEdge] == ']')
                            leftEdge--;
                        if (map[lastBoxY, rightEdge] == '[')
                            rightEdge++;

                        var startOfNewSpan = -1;

                        for (var x = leftEdge; x <= rightEdge; x++)
                        {
                            if (map[lastBoxY, x] == '[')
                            {
                                spacesToMove.Add((x, lastBoxY));
                                if (startOfNewSpan == -1)
                                {
                                    startOfNewSpan = x;
                                }
                            }

                            if (map[lastBoxY, x] == ']')
                            {
                                spacesToMove.Add((x, lastBoxY));
                                if (x == rightEdge || map[lastBoxY, x + 1] == '.')
                                {
                                    newBoxSpans.Add((startOfNewSpan, x));
                                    startOfNewSpan = -1;
                                }
                            }
                        }
                    }

                    boxSpans = newBoxSpans;
                }
                return false;
            }

            return false;
        }

        void AddMapRow(int row, bool doubleWide, string data)
        {
            var x = 0;
            for (var i = 0; i < data.Length; i++)
            {
                x = i * (doubleWide ? 2 : 1);
                var space = data[i];
                map[row, x] = space;
                if (doubleWide)
                {
                    if (space == '#')
                    {
                        map[row, x + 1] = '#';
                    }
                    if (space == '.')
                    {
                        map[row, x + 1] = '.';
                    }
                    if (space == 'O')
                    {
                        map[row, x] = '[';
                        map[row, x + 1] = ']';
                    }
                    if (space == '@')
                    {
                        map[row, x + 1] = '.';
                    }
                }
            }
        }

        void SetupMap(bool doubleWide = false)
        {
            map = new char[mapSizeY, mapSizeX];

            AddMapRow(0, doubleWide, "##################################################");
            AddMapRow(1, doubleWide, "#.#...O...O..#O.O...O..O...#....O...#O...O...OO..#");
            AddMapRow(2, doubleWide, "##OO.....O#....#...O..OO.OOOO.O#......O...O......#");
            AddMapRow(3, doubleWide, "#..........#O#...O..O.O......OOO#......O#.O.OO...#");
            AddMapRow(4, doubleWide, "#........O..OO#...#O....O##O...O....OO.#.O.OO.O..#");
            AddMapRow(5, doubleWide, "#O..O..##O...O.O.O......OO.O...O.........#..O...O#");
            AddMapRow(6, doubleWide, "#..O...#..O....O.O..O#...#.OO..O....O.....OOO...##");
            AddMapRow(7, doubleWide, "#..O.O.O.O..#...O.............O...#O.O.O..O.OOO.O#");
            AddMapRow(8, doubleWide, "#....O............OOO.O..#.#..#..#...O.OO.#....O.#");
            AddMapRow(9, doubleWide, "#.#.O.O.#.OOO........O...OOOO#......OO.#O..#.#O..#");
            AddMapRow(10, doubleWide, "#.O....O...............O.O.O.OO..OOO..OO#.O##O..O#");
            AddMapRow(11, doubleWide, "###.OO.......O...O.O..O..O..O.O.O..OO.O.OO....O#.#");
            AddMapRow(12, doubleWide, "#...O.O...#.O..O....O...O.O.O...O..OOO.........O.#");
            AddMapRow(13, doubleWide, "#.O....O...........O.....OO..OO#..OOO....#...O..O#");
            AddMapRow(14, doubleWide, "#....OOOO.O.#...#....#............O......OO#.O.O.#");
            AddMapRow(15, doubleWide, "##OOO.O...O.O..O.O.....OO....#....OOOO...O.O....##");
            AddMapRow(16, doubleWide, "#O......#.O..O...O...O..O.O..O...OOO#O.#.O..O....#");
            AddMapRow(17, doubleWide, "#.OOOO.O......OO.OOO....#.....O.O.OO.......O...#.#");
            AddMapRow(18, doubleWide, "#..............O.O....O...#.O.......OO...........#");
            AddMapRow(19, doubleWide, "##O..O.#O.....O.#.O..O#...O..#.O....O#.O.O.......#");
            AddMapRow(20, doubleWide, "#.O.#.OO.....O..#....O.O....O....O....#..O##.O...#");
            AddMapRow(21, doubleWide, "#.##.O.O.O.OOO.O.O.........OO.##.O...O...OOO..OO.#");
            AddMapRow(22, doubleWide, "#O#.##.#..##.#......O...#O.O.O..#..O.OO.....O..OO#");
            AddMapRow(23, doubleWide, "#O....#.##....O...O.##OO.O..O..........O.....O.#.#");
            AddMapRow(24, doubleWide, "#O..O.#O...O....O..O....@.OO...O...O...O..O.O..OO#");
            AddMapRow(25, doubleWide, "#O.OO..O...O......O#.##OO.#..OO.....O..O.O.O...O.#");
            AddMapRow(26, doubleWide, "#..O##..OO..OO.O...OOO...O...O..OO..O.OOO...O....#");
            AddMapRow(27, doubleWide, "##..OO..OOO.....#OOO.O..#O.O.O..OO.O.OOO..O..#OOO#");
            AddMapRow(28, doubleWide, "#O.##.##.OOOO..#.OO.....O...O.#OOO..O..O.#.O.OO#.#");
            AddMapRow(29, doubleWide, "#..OOO.#.O..O....O#..#.OO..#.O.O..OO..#O...O...#.#");
            AddMapRow(30, doubleWide, "#..O.......O.....O.O..O.O#..#...#..O..#.OO..O.O..#");
            AddMapRow(31, doubleWide, "#O..O..#.#O.O..O..O...O...OO....O.O.OO.O.O#....#.#");
            AddMapRow(32, doubleWide, "#.......#.O...O..#...O.O.OO..O.O.O...#..........O#");
            AddMapRow(33, doubleWide, "#.OO....O.OO#...O..O.O..O...O..OO..OO.O...O......#");
            AddMapRow(34, doubleWide, "#O.O.O...O...OO.O.OO.OO.OO.#...O.O.....#.O.O.....#");
            AddMapRow(35, doubleWide, "#O..O.O...O..#............O.#..#..O.OO..O#...O.O.#");
            AddMapRow(36, doubleWide, "##O...O......O....OO.O.....O...OO.O..OOO..OOO...O#");
            AddMapRow(37, doubleWide, "#......#..O.OO..#O...OO.....O..OO...OO.....O.OO.O#");
            AddMapRow(38, doubleWide, "#...#.O.#.O..O..#...#.......#...........O.O...O..#");
            AddMapRow(39, doubleWide, "#.O.O.....OO..OOO...O..O.O..O..#..O###.O.....OO..#");
            AddMapRow(40, doubleWide, "#.O.....O.#........OO.......O...O....#O.....OO#O.#");
            AddMapRow(41, doubleWide, "#O#OO.....O....O..O........O..#O#.OO.O..O......O##");
            AddMapRow(42, doubleWide, "#...#...O....O.O.O.OOO......OO..........OOO...O..#");
            AddMapRow(43, doubleWide, "#.O.O.....OOO...#.O...OO#....O....O.##O..O...O..O#");
            AddMapRow(44, doubleWide, "#...O.......O#.O..#.O.OO......##O.O..O..OO..O....#");
            AddMapRow(45, doubleWide, "#....O.....O.......#.OO...O..O.....O.O...O..O.OO##");
            AddMapRow(46, doubleWide, "#..O..OO#O#..O..O.......O..........O.........O...#");
            AddMapRow(47, doubleWide, "#....O..#.O........O...#OO...O..O.O...O...O#.....#");
            AddMapRow(48, doubleWide, "#.....###...........O..O....O........O.....##O..O#");
            AddMapRow(49, doubleWide, "##################################################");
        }

        void DumpMap(bool doubleWide = false)
        {
            for (var y = 0; y < mapSizeY; y++)
            {
                for (var x = 0; x < mapSizeX; x++)
                {
                    Console.SetCursorPosition(x, y);
                    if (y == currentY && x == currentX)
                    {
                        Console.Write('@');
                    } 
                    else 
                    {
                        Console.Write(map[y, x]);
                    }
                }
            }
        }
    }
}
