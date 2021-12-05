using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Gameboard class to represent the board for battleship. Creates 2 10x10 2D arrays of characters that can hold either an X, O,
/// or S for miss, hit, and a ship location when hacks are implemented. Contains a display method to show the gameboard.
/// Contains two gameboards. One that is shown to the user and tracks their plays, and the other that has the location
/// of the ships and is hidden from the user unless they enable hacks.
/// </summary>
namespace KNeal3Battleship
{
    public class Gameboard
    {
        //Creates a 10x10 2D array of characters for the gameboard.
        private char [,] board = new char[10, 10];
        private char[,] hiddenBoard = new char[10, 10];

        #region Show Player View Game Board

        /// <summary>
        /// Method to display the player view game board with for loops.
        /// </summary>
        public void Display()
        {
            //Displays the headings of the rows and columns.
            Console.WriteLine("    1 2 3 4 5 6 7 8 9 10");
            for (int row = 0; row < board.GetLength(0); row++)
            {
                //10 is 2 characters long so if it's at the last row just add one space to the end to now throw off the spacing.
                if(row == 9)
                {
                    Console.Write(row + 1 + " ");
                }

                else
                {
                    Console.Write(row + 1 + "  ");
                }
                
                Console.Write("|");

                for (int col = 0; col < board.GetLength(1); col++)
                {
                    //Write the value at board[row, col] to the console to display the board to the user.
                    Console.Write(board[row, col] + "|");
                }

                Console.WriteLine();
            }
        }//end Display.

        /// <summary>
        /// Method to clear out the player view game board back to a blank board.
        /// </summary>
        public void Clear()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = ' ';
                }
            }
        }//end clear.

        #endregion

        #region Show Hidden View Game Board With Hacks

        /// <summary>
        /// Method to show the hidden board and display an S on every spot that contains a ship.
        /// </summary>
        public void HackerMan()
        {
            Console.WriteLine("Hello friend. You've enabled hacks.\nNow you can see the full gameboard.");
            Console.WriteLine(" ");

            //Displays the headings for the rows and columns.
            Console.WriteLine("    1 2 3 4 5 6 7 8 9 10");
            for (int row = 0; row < hiddenBoard.GetLength(0); row++)
            {
                //10 is 2 characters long and will throw off the spacing if we don't include this.
                if (row == 9)
                {
                    Console.Write(row + 1 + " ");
                }

                else
                {
                    Console.Write(row + 1 + "  ");
                }

                Console.Write("|");

                for (int col = 0; col < hiddenBoard.GetLength(1); col++)
                {
                    //Write the value at board[row, col] to the console to display the board to the user.
                    Console.Write(hiddenBoard[row, col] + "|");
                }

                Console.WriteLine();
            }
        }//end HackerMan

        /// <summary>
        /// Same as the clear method for the player view board, but to set all the spots in the hidden board to an empty char.
        /// </summary>
        public void ClearHiddenBoard()
        {
            for (int row = 0; row < hiddenBoard.GetLength(0); row++)
            {
                for (int col = 0; col < hiddenBoard.GetLength(1); col++)
                {
                    hiddenBoard[row, col] = ' ';
                }
            }
        }

        #endregion

        #region Hit Or Miss On Game Board
        /// <summary>
        /// Called if the player hits one of the ships on the board. Displays an O in the location if there's a hit.
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// </summary>
        public void Hit(int row, int col)
        {
            PlaceChar(row, col, 'O');
            PlaceHiddenChar(row, col, 'O');
        }//end Hit.

        /// <summary>
        /// Called if the player misses a ship on the board when they attack. Displays an X in the location.
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// </summary>
        public void Miss(int row, int col)
        {
            PlaceChar(row, col, 'X');
            PlaceHiddenChar(row, col, 'X');
        }//end Miss.

        #endregion

        #region Properties For Width and Height
        /// <summary>
        /// Property for the width of the board.
        /// </summary>
        public int BoardWidth
        {
            get => board.GetLength(0);
        }

        /// <summary>
        /// Property for the height of the board.
        /// </summary>
        public int BoardHeight
        {
            get => board.GetLength(1);
        }

        #endregion

        #region Place And Get Chars For Player View Board
        /// <summary>
        /// Place a char on the player view board.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="toPlace"></param>
        public void PlaceChar(int row, int col, char toPlace)
        {
            board[row, col] = toPlace;
        }

        /// <summary>
        /// Get a character from the player view board.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public char GetChar(int row, int col)
        {
            return board[row, col];
        }

        #endregion

        #region Place And Get Chars For Hidden View Board

        /// <summary>
        /// Place a char on the hidden view board.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="toPlace"></param>
        public void PlaceHiddenChar(int row, int col, char toPlace)
        {
            hiddenBoard[row, col] = toPlace;
        }

        /// <summary>
        /// Get a character from the hidden view board.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public char GetHiddenChar(int row, int col)
        {
            return hiddenBoard[row, col];
        }

        #endregion
    }
}