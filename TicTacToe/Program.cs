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
            char[,] board = InitializeGame();
            int gameSize = board.GetLength(0);
            bool gameComplete = false;
            int row;
            int column;
            List<int> options = Enumerable.Range(0, gameSize * gameSize).ToList();
            var rand = new System.Random();
            while (!gameComplete)
            {
                //player choice
                Console.Write("Choose your position: ");
                position = Int32.Parse(Console.ReadLine()) - 1;
                if (!options.Contains(position))
                    Console.WriteLine("Position taken. Please choose another position.");
                else
                {
                    row = position / gameSize;
                    column = position % gameSize;
                    Console.WriteLine(row + " " + column);
                    board[row, column] = 'x';
                    options.Remove(position);
                    if (GameWon(row, column, board, gameSize))
                    {
                        Console.WriteLine(PrintBoard(board));
                        Console.WriteLine("x won");
                        gameComplete = true;
                        Console.Write("Play Again? ");
                        if (Console.ReadLine() == "y")
                            gameComplete = false;
                        board = InitializeGame();
                        gameSize = board.GetLength(0);
                        options = Enumerable.Range(0, gameSize * gameSize).ToList();
                    }
                    else
                    {
                        if (options.Count == 0)
                        {
                            Console.WriteLine(PrintBoard(board));
                            Console.WriteLine("Game over, tie.");
                            Console.Write("Play Again? ");
                            if (Console.ReadLine() == "y")
                                gameComplete = false;
                            board = InitializeGame();
                            gameSize = board.GetLength(0);
                            options = Enumerable.Range(0, gameSize * gameSize).ToList();
                        }
                        else
                        {
                            //cpu choice
                            position = options[rand.Next(0, options.Count)];
                            options.Remove(position);
                            row = position / gameSize;
                            column = position % gameSize;
                            board[row, column] = 'o';
                            Console.WriteLine(PrintBoard(board));

                            if (GameWon(row, column, board, gameSize))
                            {
                                Console.WriteLine("o won");
                                gameComplete = true;
                                Console.Write("Play Again? ");
                                if (Console.ReadLine() == "y")
                                    gameComplete = false;
                                board = InitializeGame();
                                gameSize = board.GetLength(0);
                                options = Enumerable.Range(0, gameSize * gameSize).ToList();
                            }
                        }
                    }
                }
            }
        }

        static char[,] InitializeGame()
        {
            Console.Write("Pick size of game: ");
            int gameSize = Int32.Parse(Console.ReadLine());
            char[,] board = new char[gameSize, gameSize];
            Console.WriteLine(PrintBoard(board, true));
            return board;
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
