using System;
using System.Collections.Generic;
/// <summary>
/// Runs the logic for the battleship game. First implements a new Gameboard, creates a list of ships, then instantiates
/// 5 ships, using Random to generate their location on the board. Displays a menu to the user asking for their input,
/// and places the ships on the board if they chose to play the game. Prompts the user for X and Y inputs, and uses an Attack
/// method to determine if the play is a hit or a miss on the board. When the ships are placed on the board, they are added
/// to the ship list, and if the list is empty, meaning they sunk all the ships, the program tells them they have won and re-prompts
/// them with the menu.
/// </summary>
namespace KNeal3Battleship
{
    public class Game
    {
        //Create a new instance of Gameboard.
        static Gameboard gBoard = new Gameboard();

        //Create a list of ships to keep track of the ships we have and the ships that have been destryoed.
        public static List<Ship> ships = new List<Ship>();

        //Create a random instance to get a random location for the ships on the game board.
        static Random random = new Random();

        //Create ships by creating instances of the ship class and passing in the values for the locations and the lengths.
        static Ship destroyerOne = new Ship(random.Next(gBoard.BoardWidth), random.Next(gBoard.BoardHeight), 2);
        static Ship destroyerTwo = new Ship(random.Next(gBoard.BoardWidth), random.Next(gBoard.BoardHeight), 2);

        static Ship subOne = new Ship(random.Next(gBoard.BoardWidth), random.Next(gBoard.BoardHeight), 3);
        static Ship subTwo = new Ship(random.Next(gBoard.BoardWidth), random.Next(gBoard.BoardHeight), 3);

        static Ship battleship = new Ship(random.Next(gBoard.BoardWidth), random.Next(gBoard.BoardHeight), 4);

        static Ship carrier = new Ship(random.Next(gBoard.BoardWidth), random.Next(gBoard.BoardHeight), 5);

        #region Menu Methods

        /// <summary>
        /// Method to run the game by calling the appropriate methods.
        /// </summary>
        public void RunGame()
        {
            //Display the game menu to the user.
            DisplayGameMenu();

            //Gets the user's results from the game menu.
            MenuResults();
        }

        /// <summary>
        /// Method to get the results from the main menu
        /// </summary>
        private void MenuResults()
        {
            switch (GetMenuInput())
            {
                //Start the game by calling PlayGame.
                case 1:
                    PlayGame();
                    break;

                //Exit the game
                case 2:
                    Console.WriteLine("Goodbye");
                    break;

                //Default case.
                default:
                    Console.WriteLine("Not sure how you even got here, but you should\n probably exit now.");
                    break;
            }
        }// end MenuResults.

        /// <summary>
        /// Welcomes the user and displays the menu for the game choices. 1 will start the game and 2 will exit the program.
        /// </summary>
        private void DisplayGameMenu()
        {
            Console.WriteLine("Welcome to Battleship. Please select a menu option");
            Console.WriteLine("1.  Start Game");
            Console.WriteLine("2.  Exit");
        }//end DisplayGameMenu.

        /// <summary>
        /// Method to get the input from the DisplayGameMenu prompt and perform the appropriate action using a switch statement.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private int GetMenuInput()
        {
            //Variable to make sure the user has inputted a valid response to the menu.
            bool isValid = false;

            while (!isValid)
            {
                //Get the answer from the user by parsing the line.
                int input = int.Parse(Console.ReadLine());

                //If the input is valid return the user's input.
                if (input == 1 || input == 2)
                {
                    return input;
                }

                //Otherwise tell the user their input is invalid and redisplay the menu and ask them to prompt again.
                else
                {
                    Console.WriteLine("Invalid input. Please try again");
                    DisplayGameMenu();
                }
            }

            return -1;
        }//end GetMenuInput

        #endregion

        #region Prompt For User Attack Methods

        /// <summary>
        /// Prompt the user for the position on the board they would like to attack.
        /// </summary>
        public int PromptXInput()
        {
            bool validInput = false;
            Console.WriteLine(" ");
            Console.Write("Enter in the X location of where you would like to attack > ");

            while (!validInput)
            {
                int xLocation = int.Parse(Console.ReadLine());

                //If the user enters 24 for X and 25 for Y, hacks are enabled so allow 24 as an acceptable input for the Y location.
                if ((xLocation <= 10 && xLocation >= 1) || xLocation == 24)
                {
                    return xLocation;
                }

                else
                {
                    Console.Write("Invalid location. Re-enter your choice > ");
                }
            }

            return -1;

        }//PromptXInput.

        /// <summary>
        /// Prompts the user for the Y location they would like to attack.
        /// </summary>
        /// <returns></returns>
        public int PromptYInput()
        {
            bool validInput = false;
            Console.WriteLine(" ");
            Console.Write("Enter in the Y location of where you would like to attack > ");

            while (!validInput)
            {
                int yLocation = int.Parse(Console.ReadLine());

                //If the user enters 24 for X and 25 for Y, hacks are enabled so allow 25 as an acceptable input for the Y location.
                if ((yLocation <= 10 && yLocation >= 1) || yLocation == 25)
                {
                    return yLocation;
                }

                else
                {
                    Console.Write("Invalid location. Re-enter your choice > ");
                }
            }

            return -1;

        }//PromptYInput.

        #endregion

        #region Ship Placement Methods

        /// <summary>
        /// Method to place ships on the board. Takes in a Ship as a parameter so we can call this later to place ships of different lengths.
        /// </summary>
        /// <param name="placedShip"></param>
        public static void Place(Ship placedShip)
        {
            bool validLocation = false;
            char direction = ' ';

            while(!validLocation)
            {
                //Checks if there is a ship at the current location.
                if (gBoard.GetHiddenChar(placedShip.bowY, placedShip.bowX) == ' ')
                {
                    //Loops 4 times, one for each direction in which the ship can be oriented.
                    for (int j = 0; j < 4; j++)
                    {
                        //Loops through the spaces where the ship could be placed based on the length of the ship.
                        for (int k = 0; k < placedShip.length; k++)
                        {
                            //Switch statement for the orientation of the ship.
                            switch(j)
                            {
                                case 0:
                                    //Orients the ship facing to the left by incrementing the X with k.
                                    //Checks if the along the X axis up to the length of the ship are empty and can hold the ship.
                                    if (LocationEmpty(placedShip.bowY, placedShip.bowX + k) == true)
                                    {
                                        //If the for loop is finished looping through.
                                        if(k == placedShip.length - 1)
                                        {
                                            //Save the direction and sets validLocation to true to break out of the loop.
                                            direction = '0';
                                            validLocation = true;
                                        }
                                       
                                    }

                                    break;

                                case 1:
                                    //Orients the ship facing down by decrementing the Y with k.
                                    //Checks if the Y axis up to the length of the ship are empty and can hold the ship.
                                    if(LocationEmpty(placedShip.bowY - k, placedShip.bowX) == true)
                                    {
                                        //If the for is finished looping.
                                        if (k == placedShip.length - 1)
                                        {
                                            //Save the direction and set validLocation to true to break out of the loop.
                                            direction = '1';
                                            validLocation = true;
                                        }
                                    }

                                    break;

                                case 2:
                                    //Orients the ship facing up by incrementing the Y with k.
                                    //Checks if the Y axis up to the length of the ship are empty and can hold the ship.
                                    if (LocationEmpty(placedShip.bowY + k, placedShip.bowX) == true)
                                    {
                                        //If the for is finished looping.
                                        if (k == placedShip.length - 1)
                                        {
                                            //Save the direction and set validLocation to true to break out of the loop.
                                            direction = '2';
                                            validLocation = true;
                                        }
                                    }

                                    break;

                                case 3:
                                    //Orients the ship facing left by decrementing the X with k.
                                    //Checks if the X axis up to the length of the ship are empty and can hold the ship.
                                    if (LocationEmpty(placedShip.bowY, placedShip.bowX - k) == true)
                                    {
                                        //If the for is finished looping.
                                        if (k == placedShip.length - 1)
                                        {
                                            //Save the direction and set validLocation to true to break out of the loop.
                                            direction = '3';
                                            validLocation = true;
                                        }
                                    }
                                                               
                                    break;                     
                                                               
                                default:                       
                                    break;                     
                            }//end switch case for j.          
                        }//end for loop for the length of the ship.
                    }//end for loop to loop through the directions.

                    //Call PlaceShipHere to place the ship at the correct location after one has been found.
                    PlaceShipHere(placedShip, direction);

                }//end if the initial location is blank.       
                                                               
                else                                           
                {                                              
                    //Gets a new location for the ship.        
                    placedShip.bowX = random.Next(gBoard.BoardWidth);
                    placedShip.bowY = random.Next(gBoard.BoardHeight);
                }

            }//end while validLocation is false.
        }//end Place.

        /// <summary>
        /// Method to check if the location is within the array for placing the ships.
        /// </summary>
        public static bool LocationEmpty(int bowX, int bowY)
        {
            //If the X and Y are between 0 and 9 and the location on the board is not S.
            if(bowX > 0 && bowY > 0 && bowX < 9 && bowY < 9 && gBoard.GetHiddenChar(bowY, bowX) != 'S')
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method to place ships on a location that has been determined to be able to hold the ship.
        /// </summary>
        /// <param name="toPlace"></param>
        /// <param name="direction"></param>
        public static void PlaceShipHere(Ship toPlace, char direction)
        {
            //Switch to go through the different directions and place the ships based on it.
            switch(direction)
            {
                case '0':
                    for (int k = 0; k < toPlace.length; k++)
                    {
                       gBoard.PlaceHiddenChar(toPlace.bowY, toPlace.bowX + k, 'S');
                    }

                    //Use a property to set the direction of the ship so we can check later if it's been sunk or not.
                    toPlace.direction = '0';

                    break;

                case '1':
                    for (int k = 0; k < toPlace.length; k++)
                    {
                        gBoard.PlaceHiddenChar(toPlace.bowY - k, toPlace.bowX, 'S');
                    }

                    //Use a property to set the direction of the ship so we can check later if it's been sunk or not.
                    toPlace.direction = '1';

                    break;

                case '2':
                    for (int k = 0; k < toPlace.length; k++)
                    {
                        gBoard.PlaceHiddenChar(toPlace.bowY + k, toPlace.bowX, 'S');
                    }

                    //Use a property to set the direction of the ship so we can check later if it's been sunk or not.
                    toPlace.direction = '2';

                    break;

                case '3':
                    for (int k = 0; k < toPlace.length; k++)
                    {
                        gBoard.PlaceHiddenChar(toPlace.bowY, toPlace.bowX - k, 'S');
                    }

                    //Use a property to set the direction of the ship so we can check later if it's been sunk or not.
                    toPlace.direction = '3';

                    break;
            }
        }//end PlaceShipHere.

        /// <summary>
        /// Method to place the ships on the board by calling Place
        /// </summary>
        private void PlaceTheShips()
        {
            gBoard.ClearHiddenBoard();

            Place(subOne);
            Place(subTwo);
            Place(destroyerOne);
            Place(destroyerTwo);
            Place(battleship);
            Place(carrier);
        }//PlaceTheShips.

        #endregion

        #region ShipHitsOrMisses

        /// <summary>
        /// Method to attack the board and call miss if the character isn't an S and call Hit if it is.
        /// If hit is called, call the CheckSunk method to see if a ship was sunk during the play.
        /// </summary>
        public void Attack(int xLoc, int yLoc)
        {
            //Checks if there is a ship at the spot, or if the spot has already been hit.
            if (gBoard.GetHiddenChar(yLoc, xLoc) == 'S' || gBoard.GetHiddenChar(yLoc, xLoc) == 'O')
            {
                gBoard.Hit(yLoc, xLoc);

                //Check each ship to see if it's been sunk by calling CheckSunk. For some reason a for each loop wouldn't work here.
                for (int i = 0; i < ships.Count; i++)
                {
                    CheckSunk(ships[i]);
                }
                
            }

            //There was no S or O on the location, so call Miss.
            else
            {
                gBoard.Miss(yLoc, xLoc);
            }
        }//end Attack.

        /// <summary>
        /// Checks if a passed ship has been sunk by using a switch to check it's orientation and seeing if everything along
        ///  it's length in the orientation has been set to O. If it has, the ship has been sunk so call the Sink method.
        /// </summary>
        /// <param name="checkShip"></param>
        public void CheckSunk(Ship checkShip)
        {
            //Switch to go through the different directions and place the ships based on it.
            switch (checkShip.direction)
            {
                case '0':
                    for (int k = 0; k < checkShip.length; k++)
                    {
                        //If the character at the X + k position has been hit and the for loop is over, sink the ship by calling
                        // Sink.
                        if (gBoard.GetHiddenChar(checkShip.bowY, checkShip.bowX + k) == 'O')
                        {
                            if (k == checkShip.length - 1)
                            {
                                Sink(checkShip);
                                break;
                            }
                        }
                    }

                    break;

                case '1':
                    for (int k = 0; k < checkShip.length; k++)
                    {
                        //If the character at the Y - k position has been hit and the for loop is over, sink the ship by calling
                        // Sink.
                        if (gBoard.GetHiddenChar(checkShip.bowY - k, checkShip.bowX) == 'O')
                        {
                            if (k == checkShip.length - 1)
                            {
                                Sink(checkShip);
                                break;
                            }
                        }
                    }

                    break;

                case '2':
                    for (int k = 0; k < checkShip.length; k++)
                    {
                        //If the character at the Y + k position has been hit and the for loop is over, sink the ship by calling
                        // Sink.
                        if (gBoard.GetHiddenChar(checkShip.bowY + k, checkShip.bowX) == 'O')
                        {
                            if (k == checkShip.length - 1)
                            {
                                Sink(checkShip);
                                break;
                            }
                        }
                    }

                    break;

                case '3':
                    for (int k = 0; k < checkShip.length; k++)
                    {
                        //If the character at the X - k position has been hit and the for loop is over, sink the ship by calling
                        // Sink.
                        if (gBoard.GetHiddenChar(checkShip.bowY, checkShip.bowX - k) == 'O')
                        {
                            if (k == checkShip.length - 1)
                            {
                                Sink(checkShip);
                                break;
                            }
                        }
                    }

                    break;
            }//end switch.

        }//end CheckSunk.

        /// <summary>
        /// Displays a message that a ship has been sunk and removes it from the ships list.
        /// </summary>
        /// <param name="sunkShip"></param>
        public void Sink(Ship sunkShip)
        {
            Console.WriteLine(" ");
            Console.WriteLine($"You sunk the {sunkShip.name}!");
            Console.WriteLine(" ");

            ships.Remove(sunkShip);
        }//end Sink

        #endregion

        #region Game Play

        /// <summary>
        /// Method to play the game
        /// </summary>
        private void PlayGame()
        {
            //Clear the gameboard to make sure the game is reset.
            gBoard.Clear();
            
            //Place the ships by calling the correct method.
            PlaceTheShips();

            //Adds the ships to the ship list. I have absolutely no idea why but this wouldn't
            // work anywhere else so I added them to the list here.
            ships.Add(destroyerOne);
            ships.Add(destroyerTwo);
            ships.Add(subOne);
            ships.Add(subTwo);
            ships.Add(battleship);
            ships.Add(carrier);

            //Display the gameboard to the user.
            gBoard.Display();

            //While there are still ships in the ship list.
            while (ships.Count != 0)
            {
                //Prompt the user for the X and Y location they would like to attack.
                int xLoc = PromptXInput();
                int yLoc = PromptYInput();

                //You wanna know what's funnier than 24? 25. Enable for hacks.
                if(xLoc == 24 && yLoc == 25)
                {
                    gBoard.HackerMan();
                    Console.WriteLine(" ");
                }

                //Call attack to check the location at X - 1 and Y - 1.
                else
                {
                    Attack(xLoc - 1, yLoc - 1);
                }

                gBoard.Display();
            }//end while ship count is greater than 0.

            //If the list length is 0, then all ships have been sunk, so display a message to the user and call RunGame()
            // to ask if they want to play again or quit.
            Console.WriteLine("You won! What would you like to do next?");
            RunGame();

        }//end PlayGame.

        #endregion

    }//end Game.
}