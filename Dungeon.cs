using System;

// A dungeon is a list of rooms

namespace Generator {
	public class Generator {

		// Given a sink type, generate the layout of the room
		public void GenerateLayout(string sinkType) {

		}

		void generateRoom(){

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

		struct Hall {
			string lighting;
			Portal portalA, portalB;
			Size size;

			public Hall(Portal portal1, Portal portal2, Size theSize, string theLighting="light") {
				portalA = portal1;
				portalB = portal2;
				size = theSize;
				lighting = theLighting;
			}
		}

		struct Size {
			int x, y;

			public Size(int xIn, int yIn) {
				x = xIn;
				y = yIn;
			}
		}
	}
}