using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
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
            char[,] referenceBoard = new char[,] { 
            { '1', '2', '3' },
            { '4', '5', '6'},
            { '7', '8', '9'}};
            char[,] board = new char[gameSize, gameSize];
            bool gameComplete = false;
            int row;
            int column;
            Console.WriteLine(PrintBoard(board, true));
            while (!gameComplete)
            {
                Console.Write("Choose your position: ");
                position = Int32.Parse(Console.ReadLine()) - 1;
                row = position / 3;
                column = position % 3;
                Console.WriteLine(row + " " + column);
                board[row, column] = 'x';
                Console.WriteLine(PrintBoard(board));
                if (GameWon(row, column, board))
                {
                    Console.WriteLine("Game Won");
                    gameComplete = true;
                    Console.WriteLine("Play Again?");
                    if (Console.ReadLine() == "yes")
                        gameComplete = false;
                    board = new char[3, 3];
                }
            }
        }

        static (int, int) getRowColumn(int position)
        {
            return (position / 3, position % 3);
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

        static bool GameWon(int row, int col, char[,] board)
        {
            char value = board[row, col];
            int count = 0;
            for (int i = col + 1; i < board.GetLength(1); i++)
            {
                // walk forwards through columns for current row
                if (value == board[row, i])
                    count++;
                else
                    break;
            }
            if (count < 2)
            {
                // if 2 weren't found then walk backwards through columns for current row
                for (int i = col - 1; i >= 0; i--)
                {
                    if (value == board[row, i])
                        count++;
                    else
                        break;
                }
            }
            else
                return true;
            if (count == 2)
                return true;
            else
            {
                //Need to check vertical
                count = 0;
                for (int i = row + 1; i < board.GetLength(0); i++)
                {
                    // walk forwards through rows for current column
                    if (value == board[i, col])
                        count++;
                    else
                        break;
                }
                if (count < 2)
                {
                    // if 2 weren't found then walk backwards through rows for current column
                    for (int i = row - 1; i >= 0; i--)
                    {
                        if (value == board[i, col])
                            count++;
                        else
                            break;
                    }
                }
                else
                    return true;
                if (count == 2)
                    return true;
                else
                {
                    //Need to check diagonal
                    if ((row == 0 && col == 0) || (row == 0 && col == 2) )
                    {
                        if (col == 0)
                        {
                            for (int i = 1; i < 3; i++)
                            {
                                if (value != board[row + i, col + i])
                                    return false;
                            }
                            return true;
                        }
                        else
                        {
                            for (int i = 1; i < 3; i++)
                            {
                                if (value != board[row + i, col - i])
                                    return false;
                            }
                            return true;
                        }
                    }
                    else if (row == 1 && col == 1)
                    {
                        if (value == board[row - 1, col - 1] && value == board[row + 1, col + 1])
                            return true;
                        else
                            return false;
                    }
                    else if ((row == 2 && col == 0) || (row == 2 && col == 2))
                    {
                        if (col == 0)
                        {
                            for (int i = 1; i < 3; i++)
                            {
                                if (value != board[row - i, col + i])
                                    return false;
                            }
                            return true;
                        }
                        else
                        {
                            for (int i = 1; i < 3; i++)
                            {
                                if (value != board[row - i, col - i])
                                    return false;
                            }
                            return true;
                        }
                    }
                    else
                        return false;
                }
            }
        }
    }
}
