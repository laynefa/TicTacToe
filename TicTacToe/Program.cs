using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = InitializeGame();
            bool gameComplete = false;
            int position;
            while (!gameComplete)
            {
                Console.Write("Choose your position: ");
                position = Int32.Parse(Console.ReadLine()) - 1;
                if (!board.PlacePlayerPosition(position, 'x'))
                    Console.WriteLine("Position either invalid or taken. Please choose another position.");
                else
                {
                    if (board.gameComplete)
                    {
                        board.PrintBoard();
                        Console.WriteLine("x won in " + board.turns + " turns");
                        gameComplete = RestartGame();
                        if (!gameComplete)
                            board = InitializeGame();
                    }
                    else
                    {
                        board.PlaceCpuPosition('o');
                        board.PrintBoard();
                        if (board.gameComplete)
                        {
                            Console.WriteLine("o won in " + board.turns + " turns");
                            gameComplete = RestartGame();
                            if (!gameComplete)
                                board = InitializeGame();
                        }
                    }
                }
            }
        }

        static Board InitializeGame()
        {
            int gameSize;
            Console.Write("Pick game size: ");
            gameSize = Int32.Parse(Console.ReadLine());
            Board board = new Board(gameSize);
            return board;
        }

        static bool RestartGame()
        {
            Console.Write("Play Again? ");
            if (Console.ReadLine() == "y")
                return false;
            return true;
        }
    }
}
