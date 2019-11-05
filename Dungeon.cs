using System;

// A dungeon is a list of rooms

namespace Generator {
	public class Generator {

		// int, bool --> Room[]
		// Given an integer (number of rooms) and a boolean (determines the generation of a boss room), generate a dungeon
		public Room[] GenerateLayout(int size, bool bossRoom) {
			Room[] theDungeon = new Room[size];

			// Use recursion
			return generateDungeon(theDungeon, bossRoom);
		}

		// Room[], bool --> Room[]
		// Given a list of rooms (the empty dungeon) and a boolean (determines the generation of a boss room), generate a dungeon recursively
		Room[] generateDungeon(Room[] theDungeon, bool bossRoom) {
		}

		// Room[], ... --> Room
		// Return a room
		Room generateRoom(Room[] theDungeon) {
			return new Room();
		}

		// Room[], Bool, ... --> Room
		// Return a hall-like room
		Room generateHall(Room[] theDungeon, bool twoExits) {
			return new Room();
		}

		struct Room {
			bool floor;
			bool walls;
			bool ceiling;
			string shape;
			string lighting;
			Size size;
			Portal[] portals;

			public Room(bool theFloor=true, bool theWalls=true, bool theCeiling=true, string theShape="rectangle", string theLighting="light", Size theSize, Portal[] thePortals) {
				floor = theFloor;
				walls = theWalls;
				ceiling = theCeiling;
				shape = theShape;
				lighting = theLighting;
				size = theSize;
				portals = thePortals;
			}			
		}

		struct Portal {
			Room roomA, roomB;

			public Portal(Room room1, Room room2) {
				roomA = room1;
				roomB = room2;	
			}
		}

		struct Size {
			int x, y;

			public Size(int xIn, int yIn) {
				x = xIn;
				y = yIn;
			}
		}

		// Need to test this...
		bool isFull(object[] theArray) {
			for (int i = 0; i < theArray.Length(); i++) {
				if(theArray[i].GetType() != theArray.GetType())
					return false;
			}

			return true;
		}
	}
}