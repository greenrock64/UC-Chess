using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Current piece values (subject to change)
 * 1 - W Pawn
 * 2 - W Castle
 * 3 - W Bishop
 * 4 - W Knight
 * 5 - W Queen
 * 6 - W King
 * 
 * 7 - B Pawn
 * 8 - B Castle
 * 9 - B Bishop
 * 10 - B Knight
 * 11 - B Queen
 * 12 - B King
 */ 

namespace UC_Chess
{
    class Chess
    {
        int[,] board;
        public Chess()
        {
            //Instantiate the board as an 8x8 array
            board = new int[8, 8];
            setBoard(board);
        }

        /// <summary>
        /// Returns the piece at location X,Y
        /// </summary>
        public int getPos(int x, int y)
        {
            return board[x, y];
        }

        /// <summary>
        /// Attempt to move contents of a square to a new square
        /// </summary>
        /// <returns>Wether the move was successful</returns>
        public bool tryMove(int x, int y, int newX, int newY)
        {
            //If there is a piece to move
            if(board[x,y] != 0)
            {
                //Successfully move piece
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Set the initial pieces on the board
        /// </summary>
        public void setBoard(int[,] board)
        {
            //Row
            for (int i = 0; i < 8; i++)
            {
                //Col
                for (int u = 0; u < 8; u++)
                {
                    //Set pawns
                    if (i == 1 || i == 6)
                    {
                        if (i == 1)
                        {
                            board[i, u] = 1;
                        }
                        else
                        {
                            board[i, u] = 7;
                        }
                    }
                    //Set core pieces
                    if(i == 0 || i == 7)
                    {
                        setFrontRow(board, i);
                    }
                }
            }
        }
        public void setFrontRow(int[,] board, int i)
        {
            if(i == 0) //White Pieces
            {
                board[i, 0] = 2;
                board[i, 1] = 3;
                board[i, 2] = 4;
                board[i, 3] = 5;
                board[i, 4] = 6;
                board[i, 5] = 4;
                board[i, 6] = 3;
                board[i, 7] = 2;
            }
            else //Black Pieces
            {
                board[i, 0] = 8;
                board[i, 1] = 9;
                board[i, 2] = 10;
                board[i, 3] = 11;
                board[i, 4] = 12;
                board[i, 5] = 10;
                board[i, 6] = 9;
                board[i, 7] = 8;
            }

        }

        //Test methods
        public void printBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                //Col
                for (int u = 0; u < 8; u++)
                {
                    Console.Write(board[i, u]);
                }
                Console.WriteLine();
            }
        }
    }
}
