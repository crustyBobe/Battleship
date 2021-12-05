using System;

/// <summary>
/// 
/// Kobe Neal
/// Assignment 1
/// 2/22/2021
/// 
/// Battleship game using a ship, gameboard, and game class. The game class runs the game logic and instantiates
/// a gameboard, then instantiates ships and places them on the board.
/// </summary>
namespace KNeal3Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instantiate a new Game object and call the RunGame() method to start playing battleship.
            Game game = new Game();
            game.RunGame();
        }//end Main.
    }//end Program.
}
