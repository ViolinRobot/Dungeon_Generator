using System;

// By Emily Peterson. This class randomly generates a description and biome for the dungeon generator. There are three biomes: Desert, Forest, and Arctic, each with 13^10 different possible descriptions. The description is intended for the DM to read to describe the dungeon, or to draw inspiration from as he or she invents their own description. 
namespace Dungeon_Generator
{
    class BiomeDescription
    {
        public string description;
        public BiomeDescription()
        {
            // instantiating our random variable
            var rand = new Random();

            // a three-dimensional array of biomes  
            string[,,] biome = new string[3, 13, 8]
           {

           // arctic

            {{"arctic","","","","","","",""},
            {"","cool","pine","wind whistling","cool","dead animals","twitch your nose.","dark"},
            {"","frigid","ice","dripping icicles","frigid","inexplicable smoke","cough.","dim"},
            {"","frostbitten","winter air","arctic animals","frostbitten","pines","sneeze.","bright"},
            {"","chill","winter","wind howling","chill","winter air","feel uncomfortable.","dark"},
            {"","freezing","cold","pines creaking","freezing","something burning","place your hand on your weapon.","dim"},
            {"","icy","frost","ice cracking","icy","ice","rub your eyes.","bright"},
            {"","frozen","snow","an animal crying out","frozen","cold stone","bite your lip.","dark"},
            {"","frosty","frigid air","the slow trickle of a half-frozen stream","frosty","cool water","recall a forgotten memory.","dim"},
            {"","glacial","the promise of snow","something crashing to the ground","cracked","frosty ground","squint at something in the distance.","bright"},
            {"","wintry","a coming storm","an echo","frozen","cold metal","straighten your stance.","dark"},
            {"","cruel","the bite of winter","a howling beast","slippery","snow","rub your arms.","dim"},
            {"","sharp","winter's wrath","metal hitting ice","sharp","something sharp","perk up.","bright"}
            },
  

          // desert

            {{"desert","","","","","","",""},
            {"","hot","sand","wind whistling","unstable","sagebrush","sneeze several times.","bright"},
            {"","dry","dust","an approaching sandstorm howling","sandy","sand","rub your eyes in futility.","dim"},
            {"","sandy","a far-off oasis","the chatter of desert animals","gritty","the inexplicable promise of rain","twitch your nose.","dark"},
            {"","dusty","hot wind","the trickle of a hidden spring","coarse","a distant wellspring","feel uncomfortable.",""},
            {"","warm","aromatic scent of creosote","coyotes howling","cracked","aromatic creosote","place your hand on your weapon.",""},
            {"","gritty","sagebrush","the hoot of a small owl","rocky","clay","recall a forgotten memory.",""},
            {"","heat-scorched","clay","desert insects chittering","stony","sunbaked stone","squint at something in the distance.",""},
            {"","arid","sunbathed basalt","dust blowing through the air","hot","dry earth","straighten your stance.",""},
            {"","parched","hot air","the creak of a nearby bush","burning","parched dust","perk up.",""},
            {"","thirsty for moisture","the faint sweetness of blooming desert flowers","the harrumph of a camel","made of fine sand","the faint sweetness of blooming desert flowers","rub your arms.",""},
            {"","dried-up","dry earth","the swish of birds' wings","threatening to trap your feet","dried flesh strangely","feel uncomfortable.",""},
            {"","dehydrated","nearby salt flats","sand whispering as it moves","windblown","decay","cough",""}},

        // forest

            {{"forest","","","","","","",""},
            {"","musty","trees","the creak of thick branches","wet","must","twitch your nose.","dark"},
            {"","damp","leaves","the rustle of leaves","squishy","mushrooms","cough.","dim"},
            {"","aromatic","wood","the snuffle of a small animal","carpeted with small plants","bark","sneeze","bright"},
            {"","wet","rain","the crunch of your footsteps on the leaf-coated ground","thick with undergrowth","aromatic plants","feel uncomfortable.",""},
            {"","ancient","water","the sudden flapping of wings","swollen with water","rotting flesh","place your hand on your weapon.",""},
            {"","decaying","oak","something moving in the underbrush","earthy","wet fur","rub your eyes.",""},
            {"","arboreous","pine","the rushing of wind in the trees","musty","lichen","bite your lip.",""},
            {"","leafy","maple","a threat of a downpour","half-rotten","moss","recall a forgotten memory.",""},
            {"","woody","elm","the howl of a strange beast","littered with leaves","wet stone","squint at something in the distance.",""},
            {"","rain-washed","underbrush","a branch cracking and falling","rich and dark","soil","straighten your stance.",""},
            {"","stinky","musty soil","the whoosh of a strong wind","fertile","earth","rub your arms.",""},
            {"","thick","wet earth","a tree toppling to the ground","fresh with the scent of clean soil","trees","perk up.",""}

            }};

            // this variable is a randomly generated integer between 1 and 3 to select a biome's 2d array from the 3d array
            var choose = rand.Next(3);

            // a selection of strings which will become our description, with crucial parts of the sentence randomly generated using rand (which was declared at the beginning of the class)
            string one = "The air in this ";
            string biomeChoice = biome[choose, 0, 0];
            string two = " area is ";
            string airOne = biome[choose, rand.Next(1, 13), 1];
            string three = " and ";
            string airTwo = biome[choose, rand.Next(1, 13), 1];
            string four = ". You taste ";
            string taste = biome[choose, rand.Next(1, 13), 2];
            string five = " on your tongue, and hear the low noise of ";
            string sound = biome[choose, rand.Next(1, 13), 3];
            string six = " in the distance. The ground under your feet is ";
            string groundOne = biome[choose, rand.Next(1, 13), 4];
            string groundTwo = biome[choose, rand.Next(1, 13), 4];
            string seven = "; the smell of ";
            string smell = biome[choose, rand.Next(1, 13), 5];
            string eight = " permeates the air, causing you to ";
            string action = biome[choose, rand.Next(1, 13), 6];
            string nine = " Your eyes adjust to the ";
            string light = biome[choose, rand.Next(1, 3), 7];
            string ten = " light. What do you do?";

            // the strings are concatonated into a single "description" string
            description = one + biomeChoice + two + airOne + three + airTwo + four + taste + five + sound + six + groundOne + three + groundTwo + seven + smell + eight + action + nine + light + ten;

            // description is printed to the console
            //Console.WriteLine(description);

        }

    }
}