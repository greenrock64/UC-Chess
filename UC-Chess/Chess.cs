﻿using System;
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
            if (board[x, y] != 0) //y==x x==y
            {
                //to-do classes for each piece
                switch (board[x, y])
                {
                    case 1: //pawn movement. to-do move to own class for all cases. to-do taking pieces 
                        pawn(x, y, newX, newY);
                        return true;
                        break;
                    case 2: //castle movement. to-do check for movement being blocked
                        castle(x, y, newX, newY);
                        return true;
                        break;
                    case 3: //Bishop movement
                        bishop(x, y, newX, newY);
                        return true;
                        break;
                    case 4: //Knight movement 
                        knight(x, y, newX, newY);
                        return true;
                        break;
                    case 5: //queen movement
                        queen(x, y, newX, newY);
                        return true;
                        break;
                    case 6: //king movement
                        king(x, y, newX, newY);
                        break;
                    case 7: //black pawn
                        blackpawn(x, y, newX, newY);
                        return true;
                        break;
                    case 8: //black castle
                        castle(x, y, newX, newY);
                        return true;
                        break;
                    case 9: //black Bishop
                        bishop(x, y, newX, newY);
                        return true;
                        break;
                    case 10: // black knight
                        knight(x, y, newX, newY);
                        return true;
                        break;
                    case 11: // black queen
                        queen(x, y, newX, newY);
                        return true;
                        break;
                    case 12: //black king
                        king(x, y, newX, newY);
                        return true;
                        break;
                }

            }
            return false;
        }
        public void pawn(int x, int y, int newX, int newY)
        {
            if (x == 1) //  need to fix
            {
                if ((x == newX - 1 || x == newX - 2) && y == newY) //the possible double move for that pawns first move
                {
                    board[newX, newY] = board[x, y];
                    board[x, y] = 0;
                }
            }
            else if (x == newX - 1 && y == newY) //normal movement
            {
                if (x == newX - 1 && y == newY)
                {
                    board[newX, newY] = board[x, y];
                    board[x, y] = 0;
                }
            }
        }
        public void blackpawn(int x, int y, int newX, int newY) //need to fix
        {
            /*
            if (board[(x + 1), (y + 1)] != 0 || board[(x + 1), (y - 1)] != 0)
            {
                if (board[(x + 1), (y + 1)] <= 6 || board[(x + 1), (y - 1)] <= 6)
                {
                    
                }
            } */
            if (x == 6)
            {
                if ((x == newX + 1 || x == newX + 2) && y == newY)
                {
                    board[newX, newY] = board[x, y];
                    board[x, y] = 0;
                }
            }
            else if (x == newX + 1 && y == newY)
            {
                if (x == newX + 1 && y == newY)
                {
                    board[newX, newY] = board[x, y];
                    board[x, y] = 0;
                }
            }
        }
        public void castle(int x, int y, int newX, int newY)
        {
            if (y != newY && x == newX) //for moving left and right
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
            if (x != newX && y == newY) //for up and down
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
        }
        public void bishop(int x, int y, int newX, int newY)
        {
            int n, k;
            n = newX - x;
            k = newY - y;
            n = Math.Abs(n); //take absolute value because negatives mess things up
            k = Math.Abs(k);
            if (n == k) //makes sure that things stay on the diagonal
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
        }
        public void queen(int x, int y, int newX, int newY)
        {
            int n, k; //queen is castle and bishop rolled up into one
            n = newX - x;
            k = newY - y;
            n = Math.Abs(n);
            k = Math.Abs(k);
            if (n == k)
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
            if (y != newY && x == newX) //for moving left and right.Probably could be put into one if
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
            if (x != newX && y == newY) //for up and down
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
        }
        public void king(int x, int y, int newX, int newY)
        {
            if (x >= -1 && x <= 1 && y >= -1 && y <= 1)
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
        }
        public void knight(int x, int y, int newX, int newY)
        {
            if (y == newY - 2 && (x == newX - 1 || x == newX + 1)) //this one is a b***h. Could do with some fixing up
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
            else if (y == newY + 2 && (x == newX - 1 || x == newX + 1))
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
            else if (x == newX + 2 && (y == newY - 1 || y == newY + 1))
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
            else if (x == newX - 2 && (y == newY - 1 || y == newY + 1))
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
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
                    if (i == 0 || i == 7)
                    {
                        setFrontRow(board, i);
                    }
                }
            }
        }
        public void setFrontRow(int[,] board, int i)
        {
            if (i == 0) //White Pieces
            {
                board[i, 0] = 2;
                board[i, 1] = 4;
                board[i, 2] = 3;
                board[i, 3] = 5;
                board[i, 4] = 6;
                board[i, 5] = 3;
                board[i, 6] = 4;
                board[i, 7] = 2;
            }
            else //Black Pieces
            {
                board[i, 0] = 8;
                board[i, 1] = 10;
                board[i, 2] = 9;
                board[i, 3] = 11;
                board[i, 4] = 12;
                board[i, 5] = 9;
                board[i, 6] = 10;
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
