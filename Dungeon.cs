using System;

namespace Generator {
	public class Generator {

		public void GenerateLayout() {
		}

		public struct Room {
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

		public struct Portal {
			public Room roomA, roomB;

			public Portal(Room room1, Room room2) {
				roomA = room1;
				roomB = room2;	
			}
		}

		public struct Hall {
			public string lighting;
			public Portal portalA, portalB;
			public Size size;

			public Hall(string theLighting="light", Portal portal1, Portal portal2, Size theSize) {
				portalA = portal1;
				portalB = portal2;
				size = theSize;
				lighting = theLighting;
			}
		}

		public struct Size {
			public int x, y;

			public Size(int xIn, int yIn) {
				x = xIn;
				y = yIn;
			}
		}
	}
}