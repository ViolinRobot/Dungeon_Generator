using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Dungeon_Generator
{
    class EnGen
    {
        //setting up tables and variables to be used within the generation of encounters
        public int chars { get; set; }
        public int lvl { get; set; }
        private Random rand = new Random();
        private int[] ezEXP = { 0, 25, 50, 75, 125, 250, 300, 350, 450, 550, 600, 800, 1000, 1100, 1250, 1400, 1600, 2000, 2100, 2400, 2800 };
        private int[] medEXP = { 0, 50, 100, 150, 250, 500, 600, 750, 900, 1100, 1200, 1600, 2000, 2200, 2500, 2800, 3200, 3900, 4200, 4900, 5700 };
        private int[] hardEXP = { 0, 75, 150, 225, 375, 750, 900, 1100, 1400, 1600, 1900, 2400, 3000, 3400, 3800, 4300, 4800, 5900, 6300, 7300, 8500 };
        private int[] deadEXP = { 0, 100, 200, 400, 500, 1100, 1400, 1700, 2100, 2400, 2800, 3600, 4500, 5100, 5700, 6400, 7200, 8800, 9500, 10900, 12700 };
        private double[] mult = { 0.0, 1.0, 1.5, 2.0, 2.0, 2.0, 2.0, 2.5, 2.5, 2.5, 2.5, 3.0, 3.0, 3.0, 3.0, 4.0 };

        private int[,] crEXP = { { 0, 10 }, { -8, 25 }, { -4, 50 }, { -2, 100 }, { 1, 200 }, { 2, 450 }, { 3, 700 }, { 4, 1100 }, { 5, 1800 }, { 6, 2300 }, { 7, 2900 }, { 8, 3900 }, { 9, 5000 }, { 10, 5900 }, { 11, 7200 }, { 12, 8400 }, { 13, 10000 }, { 14, 11500 }, { 15, 13000 }, { 16, 15000 }, { 17, 18000 }, { 18, 20000 }, { 19, 22000 }, { 20, 25000 }, { 21, 33000 }, { 22, 41000 }, { 23, 50000 }, { 24, 62000 }, { 30, 155000 } };

        //constructor
        public EnGen(int characters, int level)
        {
            chars = characters;
            lvl = level;
        }

        public dynamic GenerateEasy()
        {
            int exp = ezEXP[lvl] * chars;
            int count = rand.Next(1, 2);
            int i = 0;
            dynamic monster;

            //calculating a specific CR of monster to use, given the randomly generated number of monsters being used
            while (crEXP[i, 1] * count <= exp) {
                i++;
            }
            //grabbing a random monster with the given CR for the party
            if (i == 1)
            {
                monster = findRanMon("1%2F8");
            }
            else if (i == 2)
            {
                monster = findRanMon("1%2F4");
            }
            else if (i == 3)
            {
                monster = findRanMon("1 % 2F2");
            }
            else
            {
                monster = findRanMon(crEXP[i, 0]);
            }
            //displaying the monster being used
            if (count == 1)
            {
                Console.Write("there is " + count + " ");
                Console.WriteLine(monster.name);
            }
            else {
                Console.Write("there are " + count + " ");
                Console.Write(monster.name);
                Console.WriteLine("s");
            }

            return monster;
        }
        public dynamic GenerateMed()
        {
            int exp = medEXP[lvl] * chars;
            int count = rand.Next(1, 5);
            int i = 0;
            dynamic monster;

            //calculating a specific CR of monster to use, given the randomly generated number of monsters being used
            while (crEXP[i, 1] * count <= exp)
            {
                i++;
            }
            //grabbing a random monster with the given CR for the party
            if (i == 1)
            {
                monster = findRanMon("1%2F8");
            }
            else if (i == 2)
            {
                monster = findRanMon("1%2F4");
            }
            else if (i == 3)
            {
                monster = findRanMon("1 % 2F2");
            }
            else
            {
                monster = findRanMon(crEXP[i, 0]);
            }
            //displaying the monster being used
            if (count == 1)
            {
                Console.Write("there is " + count + " ");
                Console.WriteLine(monster.name);
            }
            else
            {
                Console.Write("there are " + count + " ");
                Console.Write(monster.name);
                Console.WriteLine("s");
            }

            return monster;
        }
        public dynamic GenerateHard()
        {
            int exp = deadEXP[lvl] * chars;
            int count = rand.Next(1, 5);
            int i = 0;
            dynamic monster;

            //calculating a specific CR of monster to use, given the randomly generated number of monsters being used
            while (crEXP[i, 1] * count <= exp)
            {
                i++;
            }
            //grabbing a random monster with the given CR for the party
            if (i == 1)
            {
                monster = findRanMon("1%2F8");
            }
            else if (i == 2)
            {
                monster = findRanMon("1%2F4");
            }
            else if (i == 3)
            {
                monster = findRanMon("1 % 2F2");
            }
            else
            {
                monster = findRanMon(crEXP[i, 0]);
            }
            //displaying the monster being used
            if (count == 1)
            {
                Console.Write("there is " + count + " ");
                Console.WriteLine(monster.name);
            }
            else
            {
                Console.Write("there are " + count + " ");
                Console.Write(monster.name);
                Console.WriteLine("s");
            }

            return monster;
        }
        public dynamic GenerateDeadly()
        {
            int exp = hardEXP[lvl] * chars;
            int count = rand.Next(1, 5);
            int i = 0;
            dynamic monster;

            //calculating a specific CR of monster to use, given the randomly generated number of monsters being used
            while (crEXP[i, 1] * count <= exp)
            {
                i++;
            }
            //grabbing a random monster with the given CR for the party
            if (i == 1)
            {
                monster = findRanMon("1%2F8");
            }
            else if (i == 2)
            {
                monster = findRanMon("1%2F4");
            }
            else if (i == 3)
            {
                monster = findRanMon("1 % 2F2");
            }
            else
            {
                monster = findRanMon(crEXP[i, 0]);
            }
            //displaying the monster being used
            if (count == 1)
            {
                Console.Write("there is " + count + " ");
                Console.WriteLine(monster.name);
            }
            else
            {
                Console.Write("there are " + count + " ");
                Console.Write(monster.name);
                Console.WriteLine("s");
            }


            return findRanMon(crEXP[i, 0]);
        }
        public dynamic findRanMon(int CR)
        {
            //setting up the connection to the API for monster Generation
            RestClient rClient = new RestClient();
            string nPage = "https://api.open5e.com/monsters/?challenge_rating=" + CR.ToString();
            rClient.endPoint = nPage;

            //Console.WriteLine("Rest Client Created");  //checking to ensure connection is setup to API
            //pulling JSON data and making it usable to C#
            string Results = (string)rClient.makeRequest();
            var jPerson = JsonConvert.DeserializeObject<dynamic>(Results);

            //setting up to randomly grab a monster based off of the CR
            int cnt = jPerson.count;
            int str = 1;
            int spot = rand.Next(0, cnt);

            //loop because given JSON pages with up to 50 monsters at a time, and can grab from any random page
            while (spot / (50 * str) >= 1)
            {
                //grabbing the link to the next page if the monster is not on the current one
                nPage = jPerson.next;
                rClient.endPoint = nPage;
                //pulling and making the next page readable
                Results = (string)rClient.makeRequest();
                jPerson = JsonConvert.DeserializeObject<dynamic>(Results);

                str++;
            }

            return jPerson.results[spot % 50];
        }

        //same thing as above but for strings (needed for 1/8, 1/4, and 1/2 CR monsters
        public dynamic findRanMon(string CR)
        {
            //setting up the connection to the API for monster Generation
            RestClient rClient = new RestClient();
            string nPage = "https://api.open5e.com/monsters/?challenge_rating=" + CR.ToString();
            rClient.endPoint = nPage;

            //Console.WriteLine("Rest Client Created");  //checking to ensure connection is setup to API
            //pulling JSON data and making it usable to C#
            string Results = (string)rClient.makeRequest();
            var jPerson = JsonConvert.DeserializeObject<dynamic>(Results);

            //setting up to randomly grab a monster based off of the CR
            int cnt = jPerson.count;
            int str = 1;
            int spot = rand.Next(0, cnt + 1);

            //loop because given JSON pages with up to 50 monsters at a time, and can grab from any random page
            while (spot / (50 * str) >= 1)
            {
                //grabbing the link to the next page if the monster is not on the current one
                nPage = jPerson.next;
                rClient.endPoint = nPage;
                //pulling and making the next page readable
                Results = (string)rClient.makeRequest();
                jPerson = JsonConvert.DeserializeObject<dynamic>(Results);

                str++;
            }

            return jPerson.results[spot % 50];
        }
    }
}
