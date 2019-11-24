using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;


namespace Dungeon_Generator 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            Random rand = new Random();
            //            MapGenerator generator = new MapGenerator();

            //            generator.PrintStringMap(generator.GenerateMap(generator.map, generator.fillDensity));
            
            int count = rand.Next(1, 8);
            int level = rand.Next(1, 20);
            Console.WriteLine(count + " characters at level " + level);
            EnGen gener = new EnGen(count, level);
            Console.WriteLine("easy is");
            gener.GenerateEasy();
            Console.WriteLine("medium is");
            gener.GenerateMed();
            Console.WriteLine("hard is");
            gener.GenerateHard();
            Console.WriteLine("deadly is");
            gener.GenerateDeadly();

        }
    }
}
