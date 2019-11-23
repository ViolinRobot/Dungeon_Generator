using System;

namespace Dungeon_Generator
{
    public class StringMapShenanigansCave
    {
        public string[,] map = new string[50, 50];
        public int fillDensity = 50;
        public int smoothing = 1;

        public string[,] GenerateStringMap(string[,] map, int fillDensity)
        {
            var rand = new Random();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = (rand.Next(0, 100) < fillDensity) ? "000" : " - ";
                }
            }



            return SmoothMap(map, smoothing);
        }

        string[,] SmoothMap(string[,] map, int smoothing)
        {
            for (int i = 0; i < smoothing; i++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    for (int y = 0; y < map.GetLength(1); y++)
                    {
                        int surroundingSpaces = CountSurroundingSpaces(map, x, y);
                        if (surroundingSpaces > 4)
                            map[x, y] = " - ";
                        else if (surroundingSpaces < 4)
                            map[x, y] = "000";
                    }
                }
            }

            return map;
        }

        int CountSurroundingSpaces(string[,] map, int posX, int posY)
        {
            int spaces = 0;

            for (int x = posX-1; x < posX+2; x++)
            {
                for (int y = posY-1; y < posY+2; y++)
                {
                    if (x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1))
                    {
                        if (map[x, y] == " - " && (x != posX || y != posY))
                            spaces++;
                    }
                }
            }

            return spaces;
        }

        public void PrintStringMap(string[,] map)
        {
            for (int i = 0; i < map.GetLength(1); i++)
            {
                string row = "";

                for (int j = 0; j < map.GetLength(0); j++)
                {
                    row += map[j, i];
                }

                Console.WriteLine(row);
            }
        }
    }
}
