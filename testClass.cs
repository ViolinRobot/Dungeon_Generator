using System;
using Generator;

namespace Generator {
    public class testClass {
        public static void Main() {
            Generator dungeonGenerator = new Generator();
            List<Room> theDungeon = dungeonGenerator.GenerateLayout(10, false);

            foreach (Room room in theDungeon)
            {
                Console.WriteLine(room.size.x + ", " + room.size.y);
            }
        }
    }
}