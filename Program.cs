using System;
using System.Collections.Generic;

namespace Dungeon_Generator 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            MapGenerator generator = new MapGenerator();

            generator.PrintStringMap(generator.GenerateMap(generator.map, generator.fillDensity));
        }
    }
}
