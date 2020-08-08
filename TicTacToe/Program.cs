using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            int position;
            Console.Write("Pick size of game: ");
            int gameSize = Int32.Parse(Console.ReadLine());
            char[,] board = new char[gameSize, gameSize];
            bool gameComplete = false;
            int row;
            int column;
            var exclude = new HashSet<int>();
            var range = Enumerable.Range(1, gameSize *  gameSize);
            var rand = new System.Random();
            int cpuPosition;
            Console.WriteLine(PrintBoard(board, true));
            while (!gameComplete)
            {
                //player choice
                Console.Write("Choose your position: ");
                position = Int32.Parse(Console.ReadLine()) - 1;
                row = position / gameSize;
                column = position % gameSize;
                Console.WriteLine(row + " " + column);
                board[row, column] = 'x';
                //cpu choice
                exclude.Add(position);
                range = range.Where(i => !exclude.Contains(i));
                cpuPosition = range.ElementAt(rand.Next(0, gameSize * gameSize - exclude.Count)) - 1;
                exclude.Add(cpuPosition);
                board[cpuPosition / gameSize, cpuPosition % gameSize] = 'o';
                Console.WriteLine(PrintBoard(board));
                if (GameWon(row, column, board, gameSize))
                {
                    Console.WriteLine("Game Won");
                    gameComplete = true;
                    Console.Write("Play Again? ");
                    if (Console.ReadLine() == "y")
                        gameComplete = false;
                        Console.Write("Pick size of game: ");
                        gameSize = Int32.Parse(Console.ReadLine());
                        board = new char[gameSize, gameSize];
                        Console.WriteLine(PrintBoard(board, true));
                }
            }
        }

        static string PrintBoard(char[,] board, bool referenceBoard=false)
        {
            string printedBoard = "";
            string data;
            int count = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                printedBoard += " | ";
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (referenceBoard)
                    {
                        count++;
                        data = count.ToString();
                    }
                    else
                        data = board[i, j].ToString();
                    printedBoard = printedBoard + data + " | ";
                }
                printedBoard += "\n";
            }
            return printedBoard;
        }

        static bool GameWon(int row, int col, char[,] board, int gameSize)
        {
            char value = board[row, col];
            bool gameWon = true;
            for (int i = 0; i < board.GetLength(1); i++)
            {
                // walk forwards through columns for current row
                if (value != board[row, i])
                {
                    gameWon = false;
                    break;
                }
            }
            if (gameWon)
                return gameWon;
            gameWon = true;
            // if still false then need to check vertical
            for (int i = 0; i < board.GetLength(0); i++)
            {
                // walk forwards through rows for current column
                if (value != board[i, col])
                {
                    gameWon = false;
                    break;
                }
            }
            if (gameWon)
                return gameWon;
            gameWon = true;
            // if still false need to check diagonals
            // check top left to bottom right
            for (int i = 0; i < gameSize; i++)
            {
                if (value != board[i, i])
                {
                    gameWon = false;
                    break;
                }
            }
            if (gameWon)
                return gameWon;
            gameWon = true;
            // check top right to bottom left
            int tempRow = 0;
            for (int j = gameSize - 1; j >= 0; j--)
            {
                if (value != board[tempRow, j])
                {
                    gameWon = false;
                    break;
                }
                tempRow++;
            }
            if (gameWon)
                return gameWon;
            return false;
        }
    }
}
