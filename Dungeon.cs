using System;

namespace Generator {
	public class Room {
    	bool floor;
	    bool walls;
    	bool ceiling;
	    bool floor;
    	string shape;
	    string lighting;
    	// Size size;
	    // Portal[] portals;

    	public Room() { 
    	}

	    // Reuturn some sort of graphical layout
    	void GenerateLayout() {

    	}
	}

	public struct Portal {
		public Room roomA, roomB;

		public Portal() {
			roomA = room1;
			roomB = room2;	
		}
	}
}
