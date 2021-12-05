using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Ship class to represent a ship for battleship. Contains a variable for the ship's length,
/// and variables to hold the X and Y coordinates of the bow. Contains a character for the direction
/// that is instantated when the ship is placed so it can be checked whether or not the ship has been
/// sunk.
/// </summary>
namespace KNeal3Battleship
{
    public class Ship
    {
        //Create variables to hold the length and the X and Y positions of the bow, along with the direction as a character.
        //Will set the direction when placing the ship to know it's orientation.
        public int length;
        public int bowX;
        public int bowY;
        public char direction;
        public string name;

        public Ship(int row, int col, int length)
        {
            this.length = length;
            this.bowX = row;
            this.bowY = col;
            this.direction = ' ';
            SetShipName();
        }//end Ship.

        /// <summary>
        /// Sets the name of the ship using a switch based on the length.
        /// </summary>
        public void SetShipName()
        {
            switch(length)
            {
                case 2:
                    name = "Destroyer";
                    break;
                case 3:
                    name = "Submarine";
                    break;
                case 4:
                    name = "Battleship";
                    break;
                case 5:
                    name = "Carrier";
                    break;
            }
        }//end SetShipName.

        /// <summary>
        /// Properties to set and get the direction the ship is facing.
        /// </summary>
        public char Direction
        {
            get => direction;
            set => value = direction;
        }

        /// <summary>
        /// Property for the X location of the bow of the ship.
        /// </summary>
        public int BowX
        {
            get => bowX;
        }

        /// <summary>
        /// Property for the Y location of the bow of the ship.
        /// </summary>
        public int BowY
        {
            get => bowY;
        }

    }//end Ship.
}