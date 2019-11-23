using System;
using System.Collections.Generic;

namespace Dungeon_Generator
{
    class MapGenerator
    {
        static int width = 75;
        static int height = 75;

        public int[,] map = new int[width, height];
        public int fillDensity = 50;
        public int smoothing = 1;
        public int minRegionSize = 8;

        public int[,] GenerateMap(int[,] map, int fillDensity)
        {
            var rand = new Random();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i, j] = (rand.Next(0, 100) < fillDensity) ? 0 : 1;
                }
            }

            return ProcessMap(SmoothMap(map, smoothing), minRegionSize);
        }

        int[,] SmoothMap(int[,] map, int smoothing)
        {
            for (int i = 0; i < smoothing; i++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int surroundingSpaces = CountSurroundingSpaces(map, x, y);
                        if (surroundingSpaces > 4)
                            map[x, y] = 1;
                        else if (surroundingSpaces < 4)
                            map[x, y] = 0;
                    }
                }
            }

            return map;
        }

        int CountSurroundingSpaces(int[,] map, int posX, int posY)
        {
            int spaces = 0;

            for (int x = posX - 1; x < posX + 2; x++)
            {
                for (int y = posY - 1; y < posY + 2; y++)
                {
                    if (WithinBounds(x, y))
                    {
                        if (map[x, y] == 1 && (x != posX || y != posY))
                            spaces++;
                    }
                }
            }

            return spaces;
        }

        List<Coord> GetRegion(int xStart, int yStart)
        {
            List<Coord> region = new List<Coord>();
            int[,] mapTraversed = new int[width, height];
            int regionType = map[xStart, yStart];

            Queue<Coord> processQueue = new Queue<Coord>();
            processQueue.Enqueue(new Coord(xStart, yStart));
            mapTraversed[xStart, yStart] = 1;

            while (processQueue.Count > 0)
            {
                Coord square = processQueue.Dequeue();
                region.Add(square);

                for (int x = square.x-1; x <= square.x+1; x++)
                {
                    for (int y = square.y-1; y <= square.y+1; y++)
                    {
                        if (WithinBounds(x, y) && (y == square.y || x == square.x))
                        {
                            if (mapTraversed[x, y] == 0 && map[x, y] == regionType)
                            {
                                mapTraversed[x, y] = 1;
                                processQueue.Enqueue(new Coord(x, y));
                            }
                        }
                    }
                }
            }

            return region;
        }

        List<List<Coord>> GetRegions (int type)
        {
            List<List<Coord>> regions = new List<List<Coord>>();
            int[,] mapTraversed = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (mapTraversed[x, y] == 0 && map[x, y] == type)
                    {
                        List<Coord> newRegion = GetRegion(x, y);
                        regions.Add(newRegion);

                        foreach (Coord coord in newRegion)
                        {
                            mapTraversed[coord.x, coord.y] = 1;
                        }
                    }
                }
            }

            return regions;
        }

        int[,] ProcessMap(int[,] map, int sizeThreshold)
        {
            List<List<Coord>> regionsFilled = GetRegions(1);
            List<List<Coord>> regionsSpace = GetRegions(0);

            TrimRegions(regionsFilled, sizeThreshold, 0);
            TrimRegions(regionsSpace, sizeThreshold, 1);

            return map;
        }

        void TrimRegions(List<List<Coord>> regions, int sizeThreshold, int antiType)
        {
            foreach (List<Coord> region in regions)
            {
                if (region.Count < sizeThreshold)
                {
                    foreach (Coord position in region)
                    {
                        map[position.x, position.y] = antiType;
                    }
                }
            }
        }

        bool WithinBounds(int x, int y)
        {
            return (x >= 0 && x < width && y >= 0 && y < height);
        }

        public void PrintStringMap(int[,] map)
        {
            for (int i = 0; i < height; i++)
            {
                string row = "";

                for (int j = 0; j < width; j++)
                {
                    row += (map[j, i] == 0) ? "000" : " - ";
                }

                Console.WriteLine(row);
            }
        }

        struct Coord
        {
            public int x, y;

            public Coord (int x_in, int y_in)
            {
                x = x_in;
                y = y_in;
            }
        }
    }
}
