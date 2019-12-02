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

            List<Room> rooms = new List<Room>();

            TrimRegions(regionsFilled, sizeThreshold, 0);
            TrimRegions(regionsSpace, sizeThreshold, 1);

            regionsFilled = GetRegions(1);

            foreach (List<Coord> region in regionsFilled)
            {
                rooms.Add(new Room(region, map));
            }

            rooms.Sort();
            rooms[0].isMainRoom = true;

            map = ConnectClosestRooms(rooms, 3, map);

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

        int[,] ConnectClosestRooms(List<Room> allRooms, int passageWidth, int[,] map, bool forceAccessibleFromMainRoom = false)
        {
            List<Room> roomListA = new List<Room>();
            List<Room> roomListB = new List<Room>();

            if (forceAccessibleFromMainRoom)
            {
                foreach(Room room in allRooms)
                {
                    if (room.isAccessibleFromMainRoom)
                        roomListB.Add(room);
                    else
                        roomListA.Add(room);
                }
            } 
            else
            {
                roomListA = allRooms;
                roomListB = allRooms;
            }

            int bestDistance = 0;
            Coord bestSpaceA = new Coord();
            Coord bestSpaceB = new Coord();
            Room bestRoomA = new Room();
            Room bestRoomB = new Room();
            bool possibleConnectionFound = false;

            foreach (Room roomA in roomListA)
            {
                if (!forceAccessibleFromMainRoom)
                {
                    possibleConnectionFound = false;
                    if (roomA.connectedRooms.Count > 0)
                        continue;
                }

                foreach (Room roomB in roomListB)
                {
                    if (roomA == roomB || roomA.IsConnected(roomB))
                        continue;

                    for (int spaceIndexA = 0; spaceIndexA < roomA.edgeSpaces.Count; spaceIndexA++)
                    {
                        for (int spaceIndexB = 0; spaceIndexB < roomB.edgeSpaces.Count; spaceIndexB++)
                        {
                            Coord spaceA = roomA.edgeSpaces[spaceIndexA];
                            Coord spaceB = roomB.edgeSpaces[spaceIndexB];
                            int distance = (int)MathF.Sqrt(MathF.Pow(spaceA.x - spaceB.x, 2) + MathF.Pow(spaceA.y - spaceB.y, 2));

                            if (distance < bestDistance || !possibleConnectionFound)
                            {
                                bestDistance = distance;
                                possibleConnectionFound = true;
                                bestSpaceA = spaceA;
                                bestSpaceB = spaceB;
                                bestRoomA = roomA;
                                bestRoomB = roomB;
                            }
                        }
                    }

                    if (possibleConnectionFound && !forceAccessibleFromMainRoom)
                        map = CreatePassage(bestRoomA, bestRoomB, bestSpaceA, bestSpaceB, passageWidth, map);
                }

                if (possibleConnectionFound && forceAccessibleFromMainRoom)
                {
                    map = CreatePassage(bestRoomA, bestRoomB, bestSpaceA, bestSpaceB, passageWidth, map);
                    ConnectClosestRooms(allRooms, passageWidth, map, true);
                }

                if (!forceAccessibleFromMainRoom)
                {
                    ConnectClosestRooms(allRooms, passageWidth, map, true);
                }
            }

            return map;
        }

        int[,] CreatePassage(Room roomA, Room roomB, Coord spaceA, Coord spaceB, int passageWidth, int[,] map)
        { 
            Room.ConnectRooms(roomA, roomB);

            List<Coord> path = GetPath(spaceA, spaceB);

            foreach (Coord coord in path)
            {
                map = DrawPath(coord, passageWidth, map);
            }

            return map;
        }

        int[,] DrawPath(Coord coord, int radius, int[,] map)
        {
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    if (x*x + y*y <= radius * radius)
                    {
                        int realX = coord.x + x;
                        int realY = coord.y + x;

                        if (WithinBounds(realX, realY))
                            map[realX, realY] = 1;
                    }
                }
            }

            return map;
        }

        List<Coord> GetPath(Coord a, Coord b)
        {
            List<Coord> path = new List<Coord>();

            int deltaX = b.x - a.x;
            int deltaY = b.y - a.y;
            int x = a.x;
            int y = a.y;

            bool inverted = false;
            int step = Math.Sign(deltaX);
            int gradientStep = Math.Sign(deltaY);

            int longest = Math.Abs(deltaX);
            int shortest = Math.Abs(deltaY);

            if (longest < shortest)
            {
                inverted = true;
                longest = Math.Abs(deltaY);
                shortest = Math.Abs(deltaX);

                step = Math.Sign(deltaY);
                gradientStep = Math.Sign(deltaX);
            }

            int gradientAccumulation = longest / 2;
            for (int i = 0; i < longest; i++)
            {
                path.Add(new Coord(x, y));
                if (inverted)
                    y += step;
                else
                    x += step;

                gradientAccumulation += shortest;
                if (gradientAccumulation >= longest)
                {
                    if (inverted)
                        x += gradientStep;
                    else
                        y += gradientStep;

                    gradientAccumulation -= longest;
                }
            }

            return path;
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

        public string StringMap(int[,] map)
        {
            string stringMap = "";

            for (int i = 0; i < height; i++)
            {
                string row = "";
                
                for (int j = 0; j < width; j++)
                {
                    row += (map[j, i] == 0) ? "000" : " - ";
                }

                stringMap += row + "\n";
            }

            return stringMap;
        }

        public List<string> KerneledStringMap(int[,] map)
        {
            List<string> kerneledStringMap = new List<string>();

            int kernelX = 74;
            int kernelY = 50;

            int numKernels = (int)(Math.Ceiling((width * 3d)/kernelX) * Math.Ceiling((height * 3d)/kernelY));

            Console.WriteLine("Kernel information: " + numKernels + ", " + width * 3 + ", " + height * 3);

            string[,] thingToKernel = new string[width, height];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    thingToKernel[i, j] = (map[i, j] == 0) ? "000" : " - ";
                }
            }

            for (int i = 0; i < numKernels; i++)
            {
                kerneledStringMap.Add(GetKernel(i, width, height, thingToKernel));
            }

            return kerneledStringMap;
        }

        string GetKernel(int index, int kWidth, int kHeight, string[,] thingToKernel)
        {
            string kernel = "";
            int startX;
            int startY;



            return kernel;
        }

        class Room : IComparable<Room> 
        {
            public List<Coord> spaces;
            public List<Coord> edgeSpaces;
            public List<Room> connectedRooms;
            public int roomSize;
            public bool isAccessibleFromMainRoom;
            public bool isMainRoom;

            public Room() {}

            public Room(List<Coord> roomSpaces, int[,] map)
            {
                spaces = roomSpaces;
                roomSize = spaces.Count;
                connectedRooms = new List<Room>();

                edgeSpaces = new List<Coord>();
                foreach (Coord space in spaces)
                {
                    for (int x = space.x-1; x <= space.x+1; x++)
                    {
                        for (int y = space.y-1; y <= space.y+1; y++)
                        {
                            if (WithinBounds(x, y) && (x == space.x || y == space.y))
                            {
                                if (map[x, y] == 0)
                                    edgeSpaces.Add(space);
                            }
                        }
                    }
                }
            }

            public void SetAccessibleFromMainRoom()
            {
                if (!isAccessibleFromMainRoom)
                {
                    isAccessibleFromMainRoom = true;
                    foreach (Room connectedRoom in connectedRooms)
                    {
                        connectedRoom.SetAccessibleFromMainRoom();
                    }
                }
            }

            public static void ConnectRooms(Room roomA, Room roomB)
            {
                if (roomA.isAccessibleFromMainRoom)
                {
                    roomB.SetAccessibleFromMainRoom();
                }
                else if (roomB.isAccessibleFromMainRoom)
                {
                    roomA.SetAccessibleFromMainRoom();
                }
                roomA.connectedRooms.Add(roomB);
                roomB.connectedRooms.Add(roomA);
            }

            public bool IsConnected(Room otherRoom)
            {
                return connectedRooms.Contains(otherRoom);
            }

            public int CompareTo(Room otherRoom)
            {
                return otherRoom.roomSize.CompareTo(roomSize);
            }
            bool WithinBounds(int x, int y)
            {
                return (x >= 0 && x < width && y >= 0 && y < height);
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
