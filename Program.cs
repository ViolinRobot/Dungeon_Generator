using System;
using System.IO;


//Alright Isaac, It's all on you to make the rest of this work, including figuring out how to write everything to a file, Also worth noting that there is some of that in my encounterGenerator.cs too
//Good Luck 
//--Sam

namespace Dungeon_Generator 
{
    class Program 
    {

        static void Main(string[] args) 
        {
            string outputFileName = "output.txt";

            //included randomness for the sake of testing
            Random rand = new Random();

            // Create the output file
            File.WriteAllText(outputFileName, "");

            MapGenerator generator = new MapGenerator();

            string map = generator.StringMap(generator.GenerateMap(generator.map, generator.fillDensity));
            File.AppendAllText(outputFileName, map);

            generator.KerneledStringMap(generator.map);

            File.AppendAllText(outputFileName, "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");

            int count = rand.Next(1, 8);
            int level = rand.Next(1, 20);
            string put = "";
            BiomeDescription desc = new BiomeDescription();
            File.AppendAllText(outputFileName, desc.description);
            

            EnGen gener = new EnGen(count, level);
            put = "easy difficulty";
            File.AppendAllText(outputFileName,put);
            string easy = gener.GenerateEasy().ToString();
            put = "medium difficulty";
            File.AppendAllText(outputFileName, put);
            string medium = gener.GenerateMed().ToString();
            put = "hard difficulty";
            File.AppendAllText(outputFileName, put);
            string hard = gener.GenerateHard().ToString();
            put = "A dangerous encounter";
            string deadly = gener.GenerateDeadly().ToString();
            File.AppendAllText(outputFileName, put);


            File.AppendAllText(outputFileName, easy);
            File.AppendAllText(outputFileName, medium);
            File.AppendAllText(outputFileName, hard);
            File.AppendAllText(outputFileName, deadly);

            File.AppendAllText(outputFileName, "a magic item that could be rewarded along the way");
            File.AppendAllText(outputFileName, gener.FindLoot().ToString());


            string path = Directory.GetCurrentDirectory();

            Console.WriteLine("Program Complete");
            Console.WriteLine();
            Console.WriteLine("Data file can be found under: \n" + path + "\\" + outputFileName);
            Console.WriteLine();
            Console.WriteLine("Note: this software assumes a font of: \nConsolas, 11pt. \nPlease change your text editor font settings to reflect this specification.");
        }
    }
}
