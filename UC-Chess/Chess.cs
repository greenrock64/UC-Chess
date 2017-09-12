using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 1 - Pawn
 * 2 - Castle
 * 3 - Bishop
 * 4 - Knight
 * 5 - Queen
 * 6 - King
 */ 

namespace UC_Chess
{
    class Chess
    {
        int[,] board;
        public Chess()
        {
            board = new int[8, 8];
            setBoard(board);
        }
        public int getPos(int x, int y)
        {
            return board[x, y];
        }
        public int[,] getBoard()
        {
            return board;
        }
        public bool tryMove(int x, int y, int newX, int newY)
        {
            if(board[x,y] != 0)
            {
                board[newX, newY] = board[x, y];
                board[x, y] = 0;
            }
            return false;
        }
        public void setBoard(int[,] board)
        {
            //Row
            for (int i = 0; i < 8; i++)
            {
                //Col
                for (int u = 0; u < 8; u++)
                {
                    if (i == 1 || i == 6)
                    {
                        board[i, u] = 1;
                    }
                    if(i == 0 || i == 7)
                    {
                        setFrontRow(board, i);
                    }
                }
            }
        }
        public void setFrontRow(int[,] board, int i)
        {
            board[i, 0] = 2;
            board[i, 1] = 3;
            board[i, 2] = 4;
            if(i == 0)
            {
                board[i, 3] = 5;
                board[i, 4] = 6;
            }else
            {
                board[i, 3] = 6;
                board[i, 4] = 5;
            }
            board[i, 5] = 4;
            board[i, 6] = 3;
            board[i, 7] = 2;
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
