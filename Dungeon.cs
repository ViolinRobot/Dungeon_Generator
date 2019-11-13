using System;
using System.Collections.Generic;

// A dungeon is a list of rooms

namespace Generator {
	public class Generator {

		// int, bool --> Room[]
		// Given an integer (number of rooms) and a boolean (determines the generation of a boss room), generate a dungeon
		public List<Room> GenerateLayout(int size, bool bossRoom) {
			List<Room> theDungeon = new List<Room>();

			// Use recursion or something along those lines
			return assignPortals(generateRooms(theDungeon, size, bossRoom));
		}

		// List<Room>, int, bool, ... --> Room
		// Given a list of rooms (the dungeon), an integer (the number of rooms remaining), and a boolean (determines the generation of a boss room), generate a room and follow along its portals
		List<Room> generateRooms(List<Room> theDungeon, int size, bool bossRoom) {
			if (size == 1 && bossRoom) {
				size -= 2;
				theDungeon.Add(generateRoom(generateRandomSize(5, 10), 2)); // Make the boss room
				theDungeon.Add(generateRoom(generateRandomSize(3, 6), 1)); // Make the final room
				return theDungeon;
			} else if (size == 0) {
				size --;
				theDungeon.Add(generateRoom(generateRandomSize(3, 6), 1)); // Make the final room
				return theDungeon;
			} else {
				size --;
				theDungeon.Add(generateRoom(generateRandomSize(2, 10), generateRandomPortalNumber())); // Generate a normal room
				for (int i = 1; i <= theDungeon[theDungeon.Count-1].portals.GetLength(); i++) {
					theDungeon.Add(generateRooms(theDungeon, size, bossRoom)); // Generate normal rooms for each portal in the room
				}
			}

			return theDungeon;
		}

		Size generateRandomSize(int min, int max) {
			var rand = new Random();
			return new Size(rand.Next(min, max), rand.Next(min, max));
		}

		int generateRandomPortalNumber() {
			var rand = new Random();
			return rand.Next(3);
		}

		// Return a room
		Room generateRoom(Size size, int numPortals) {
			Portal[] thePortals = new Portal[numPortals];
			return new Room(true, true, true, "rectangle", "light", size, thePortals);
		}

		// Return a hall-like room
		Room generateHall() {
			return new Room();
		}

		List<Room> assignPortals(List<Room> theDungeon) {
			return theDungeon;
		}

		struct Room {
			bool floor;
			bool walls;
			bool ceiling;
			string shape;
			string lighting;
			Size size;
			public Portal[] portals;

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