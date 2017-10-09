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
 * 5 - W King
 * 6 - W Queen
 * 
 * 7 - B Pawn
 * 8 - B Castle
 * 9 - B Bishop
 * 10 - B Knight
 * 11 - B King
 * 12 - B Queen
 */

namespace UC_Chess
{
    class Chess
    {
        Piece[,] board;
        public Chess()
        {
            //Instantiate the board as an 8x8 array
            board = new Piece[8, 8];
            setBoard(board);
        }

        /// <summary>
        /// Returns the piece at location X,Y
        /// </summary>
        public Piece getPos(int x, int y)
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
            if (board[x, y] != null) //y==x x==y
            {
                //to-do classes for each piece
                switch (board[x, y].pieceType)
                {
                    case "pawn":
                        if (board[x, y].playerSide == 0)
                        {
                            pawn(x, y, newX, newY);
                        }
                        else
                        {
                            blackpawn(x, y, newX, newY);
                        }
                        break;
                    case "bishop":
                        bishop(x, y, newX, newY);
                        break;
                    case "knight":
                        knight(x, y, newX, newY);
                        break;
                    case "castle":
                        castle(x, y, newX, newY);
                        break;
                    case "king":
                        king(x, y, newX, newY);
                        break;
                    case "queen":
                        queen(x, y, newX, newY);
                        break;
                }

            }
            return false;
        }
        /// <summary>
        /// Moves a piece to a new position & turn conditions
        /// </summary>
        /// 
        public bool whiteTurn = true; //white goes first
        private void movePiece(int x, int y, int newX, int newY)
        {
            
            if (board[x, y].playerSide == 0 && whiteTurn == true) //white piece
            {
                if (board[newX, newY] == null)
                {
                    board[newX, newY] = board[x, y];
                    board[x, y] = null;
                }
                else if (board[newX, newY].playerSide == 1)
                {
                    board[newX, newY] = board[x, y];
                    board[x, y] = null;
                }
                //changes to black and writes a console line
                whiteTurn = !whiteTurn;
            }
            else
             if (board[x, y].playerSide == 1 && whiteTurn == false) //Black piece
            {
                if (board[newX, newY] == null)
                {
                    board[newX, newY] = board[x, y];
                    board[x, y] = null;
                }
                else if (board[newX, newY].playerSide == 0)
                {
                    board[newX, newY] = board[x, y];
                    board[x, y] = null;
                }
                //changes to white and writes console line
                whiteTurn = !whiteTurn;
            }
            string str;
            if(whiteTurn==true)
            {
                str = "White's turn ";
            }
            else
            {
                str = "Black's turn ";
            }

            System.Windows.Forms.MessageBox.Show(str);
        }
        
        public void pawn(int x, int y, int newX, int newY)
        {
            if (newX == x + 1 && newY == y)//Forward 1 square
            {
                if (board[newX, newY] == null)//No piece ahead
                {
                    movePiece(x, y, newX, newY);
                }
            }
            else if (x == 1 && (newX == x + 2 && newY == y)) //Pawn at starting pos and is moving 2 squares
            {
                movePiece(x, y, newX, newY);
            }
            else if (newX == x + 1 && Math.Abs(y - newY) == 1)//Diagonal
            {
                if (board[newX, newY] != null)//Is an occupied tile
                {
                    movePiece(x, y, newX, newY);
                }
            }
        }
        public void blackpawn(int x, int y, int newX, int newY)
        {
            if (newX == x - 1 && newY == y)//Forward 1 square
            {
                if (board[newX, newY] == null)//No piece ahead
                {
                    movePiece(x, y, newX, newY);
                }
            }
            else if (x == 6 && (newX == x - 2 && newY == y)) //Pawn at starting pos and is moving 2 squares
            {
                movePiece(x, y, newX, newY);
            }
            else if (newX == x - 1 && Math.Abs(y - newY) == 1)//Diagonal
            {
                if (board[newX, newY] != null)//Is an occupied tile
                {
                    movePiece(x, y, newX, newY);
                }
            }
        }
        public void castle(int x, int y, int newX, int newY)
        {
            if ((y != newY && x == newX) || (x != newX && y == newY)) //for moving left and right and up and down
            {
                movePiece(x, y, newX, newY);
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
                movePiece(x, y, newX, newY);
            }
        }
        public void queen(int x, int y, int newX, int newY)
        {
            int n, k; //queen is castle and bishop rolled up into one
            n = newX - x;
            k = newY - y;
            n = Math.Abs(n);
            k = Math.Abs(k);
            if ((n == k) || (y != newY && x == newX) || (x != newX && y == newY))
            {
                movePiece(x, y, newX, newY);
            }
        }
        public void king(int x, int y, int newX, int newY)
        {
            if ((Math.Abs(x - newX) == 1 && Math.Abs(y - newY) == 1) || (Math.Abs(x - newX) == 1 && newY == y) || (Math.Abs(y - newY) == 1 && newX == x)) //not broken but it looks bad
            {
                movePiece(x, y, newX, newY);
            }
        }
        public void knight(int x, int y, int newX, int newY)
        {
            if (y == newY - 2 && (x == newX - 1 || x == newX + 1)) //this one is a b***h. Could do with some fixing up
            {
                movePiece(x, y, newX, newY);
            }
            else if (y == newY + 2 && (x == newX - 1 || x == newX + 1))
            {
                movePiece(x, y, newX, newY);
            }
            else if (x == newX + 2 && (y == newY - 1 || y == newY + 1))
            {
                movePiece(x, y, newX, newY);
            }
            else if (x == newX - 2 && (y == newY - 1 || y == newY + 1))
            {
                movePiece(x, y, newX, newY);
            }
        }
        /// <summary>
        /// Set the initial pieces on the board
        /// </summary>
        public void setBoard(Piece[,] board)
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
                            board[i, u] = new Piece("pawn", 0);
                        }
                        else
                        {
                            board[i, u] = new Piece("pawn", 1);
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
        public void setFrontRow(Piece[,] board, int i)
        {
            if (i == 0) //White Pieces
            {
                board[i, 0] = new Piece("castle", 0);
                board[i, 1] = new Piece("knight", 0);
                board[i, 2] = new Piece("bishop", 0);
                board[i, 3] = new Piece("king", 0);
                board[i, 4] = new Piece("queen", 0);
                board[i, 5] = new Piece("bishop", 0);
                board[i, 6] = new Piece("knight", 0);
                board[i, 7] = new Piece("castle", 0);
            }
            else //Black Pieces
            {
                board[i, 0] = new Piece("castle", 1);
                board[i, 1] = new Piece("knight", 1);
                board[i, 2] = new Piece("bishop", 1);
                board[i, 3] = new Piece("king", 1);
                board[i, 4] = new Piece("queen", 1);
                board[i, 5] = new Piece("bishop", 1);
                board[i, 6] = new Piece("knight", 1);
                board[i, 7] = new Piece("castle", 1);
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
