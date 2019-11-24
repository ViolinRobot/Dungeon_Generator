using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;


//Alright Isaac, It's all on you to make the rest of this work, including figuring out how to write everything to a file, Also worth noting that there is some of that in my encounterGenerator.cs too
//Good Luck 
//--Sam

namespace Dungeon_Generator 
{
    class Program 
    {

        static void Main(string[] args) 
        {
            //included randomness for the sake of testing
            Random rand = new Random();
            
            //MapGenerator generator = new MapGenerator();
            //generator.PrintStringMap(generator.GenerateMap(generator.map, generator.fillDensity));
            
            int count = rand.Next(1, 8);
            int level = rand.Next(1, 20);
            string put = "";
            BiomeDescription desc = new BiomeDescription();
            File.WriteAllText("output.txt", desc.description);
            

            EnGen gener = new EnGen(count, level);
            put = "easy difficulty";
            File.AppendAllText("output.txt",put);
            string easy = gener.GenerateEasy().ToString();
            put = "medium difficulty";
            File.AppendAllText("output.txt", put);
            string medium = gener.GenerateMed().ToString();
            put = "hard difficulty";
            File.AppendAllText("output.txt", put);
            string hard = gener.GenerateHard().ToString();
            put = "A dangerous encounter";
            string deadly = gener.GenerateDeadly().ToString();
            File.AppendAllText("output.txt", put);


            File.AppendAllText("output.txt", easy);
            File.AppendAllText("output.txt", medium);
            File.AppendAllText("output.txt", hard);
            File.AppendAllText("output.txt", deadly);

            File.AppendAllText("output.txt", "a magic item that could be rewarded along the way");
            File.AppendAllText("output.txt", gener.FindLoot().ToString());

            Console.WriteLine("Program Complete");
        }
    }
}
