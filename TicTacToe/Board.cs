using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Board
    {
        char[,] board;
        public bool gameComplete;
        int size;
        int spaces;
        public int turns;
        List<int> options;
        Random rand = new System.Random();

        public Board(int gameSize)
        {
            size = gameSize;
            board = new char[size, size];
            spaces = size * size;
            options = Enumerable.Range(0, spaces).ToList();
            gameComplete = false;
            turns = 0;
            PrintBoard(true);
        }

        public void PrintBoard(bool referenceBoard = false)
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
            Console.WriteLine(printedBoard);
        }

        private bool IsPositionTaken(int position)
        {
            return !options.Contains(position);
        }

        private bool RemainingOptions()
        {
            if (options.Count == 0)
            {
                PrintBoard();
                Console.WriteLine("Game over, tie.");
                return false;
            }
            return true;
        }

        public bool PlacePosition(int position, char symbol)
        {
            if (IsPositionTaken(position))
                return false;
            turns++;
            int row = position / size;
            int column = position % size;
            Console.WriteLine(row + " " + column);
            board[row, column] = symbol;
            options.Remove(position);
            gameComplete = GameWon(position) || !RemainingOptions();
            return true;
        }

        public void PlaceCpuPosition(char symbol)
        {
            int position = options[rand.Next(0, options.Count)];
            options.Remove(position);
            int row = position / size;
            int column = position % size;
            board[row, column] = 'o';
        }

        public bool GameWon(int position)
        {
            int row = position / size;
            int col = position % size;
            char value = board[row, col];
            gameComplete = true;
            for (int i = 0; i < board.GetLength(1); i++)
            {
                // walk forwards through columns for current row
                if (value != board[row, i])
                {
                    gameComplete = false;
                    break;
                }
            }
            if (gameComplete)
                return gameComplete;
            gameComplete = true;
            // if still false then need to check vertical
            for (int i = 0; i < board.GetLength(0); i++)
            {
                // walk forwards through rows for current column
                if (value != board[i, col])
                {
                    gameComplete = false;
                    break;
                }
            }
            if (gameComplete)
                return gameComplete;
            gameComplete = true;
            // if still false need to check diagonals
            // check top left to bottom right
            for (int i = 0; i < size; i++)
            {
                if (value != board[i, i])
                {
                    gameComplete = false;
                    break;
                }
            }
            if (gameComplete)
                return gameComplete;
            gameComplete = true;
            // check top right to bottom left
            int tempRow = 0;
            for (int j = size - 1; j >= 0; j--)
            {
                if (value != board[tempRow, j])
                {
                    gameComplete = false;
                    break;
                }
                tempRow++;
            }
            if (gameComplete)
                return gameComplete;
            return false;
        }
    }
}
