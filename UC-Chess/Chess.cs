using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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

        //
        //
        //WIP ZONE
        //
        //

        public Vector2[] getPossibleMoves(int x, int y, string pieceType = "null")
        {
            List<Vector2> possibleMoves = new List<Vector2>();
            pieceType = (pieceType != "null") ? pieceType : board[x, y].pieceType;
            switch (pieceType)
            {
                case "pawn":
                    //Set which direction the pawn can go (black/white piece)
                    int dir = (board[x,y].playerSide == 0) ? 1:-1;
                    //Check board boundaries
                    if (x + dir < 8 && x + dir >= 0)
                    {
                        //Forward 1 square
                        if (board[x + dir, y] == null)//No piece ahead
                        {
                            tryPossibleMove(x + dir, y, possibleMoves, board[x, y].playerSide);
                        }
                        //Diagonals
                        if (y - 1 >= 0)
                        {
                            if (board[x + dir, y - 1] != null) //Piece ahead
                            {
                                tryPossibleMove(x + dir, y - 1, possibleMoves, board[x, y].playerSide);
                            }
                        }
                        if (y + 1 < 8)
                        {
                            if (board[x + dir, y + 1] != null)
                            {
                                tryPossibleMove(x + dir, y + 1, possibleMoves, board[x, y].playerSide);
                            }
                        }
                    }
                    /*TODO: Add hasMoved flag to piece class
                    //Forward 2 squares
                    if (x + 2*dir < 8 && x + 2*dir >= 0)
                    {
                        if (Piece.hasMoved == false) //Pawn at starting pos and is moving 2 squares
                        {
                            tryPossibleMove(x + 2, y, possibleMoves, board[x, y].playerSide);
                        }
                    }*/
                    break;
                case "bishop":
                    bool SE = true, SW = true, NE = true, NW = true;
                    for (int i = 1; i < 8; i++)
                    {
                        if (x + i < 8) //S
                        {
                            if (y + i < 8 && SE) //E
                            {
                                SE = tryPossibleMove(x+i, y+i, possibleMoves, board[x, y].playerSide);
                            }
                            if (y - i >= 0 && SW) //W
                            {
                                SW = tryPossibleMove(x + i, y - i, possibleMoves, board[x, y].playerSide);
                            }
                        }
                        if (x - i >= 0) //N
                        {
                            if (y + i < 8 && NE) //E
                            {
                                NE = tryPossibleMove(x - i, y + i, possibleMoves, board[x, y].playerSide);
                            }
                            if (y - i >= 0 && NW) //W
                            {
                                NW = tryPossibleMove(x - i, y - i, possibleMoves, board[x, y].playerSide);
                            }
                        }
                    }
                    break;
                case "knight":
                    if ((y - 2) >= 0)
                    {
                        if ((x + 1) < 8) tryPossibleMove(x + 1, y - 2, possibleMoves, board[x, y].playerSide);
                        if ((x - 1) >= 0) tryPossibleMove(x - 1, y - 2, possibleMoves, board[x, y].playerSide);
                    }
                    if ((y + 2) < 8)
                    {
                        if ((x + 1) < 8) tryPossibleMove(x + 1, y + 2, possibleMoves, board[x, y].playerSide);
                        if ((x - 1) >= 0) tryPossibleMove(x - 1, y + 2, possibleMoves, board[x, y].playerSide);
                    }
                    if ((x - 2) >= 0)
                    {
                        if ((y + 1) < 8) tryPossibleMove(x - 2, y + 1, possibleMoves, board[x, y].playerSide);
                        if ((y - 1) >= 0) tryPossibleMove(x - 2, y - 1, possibleMoves, board[x, y].playerSide);
                    }
                    if ((x + 2) < 8)
                    {
                        if ((y + 1) < 8) tryPossibleMove(x + 2, y + 1, possibleMoves, board[x, y].playerSide);
                        if ((y - 1) >= 0) tryPossibleMove(x + 2, y - 1, possibleMoves, board[x, y].playerSide);
                    }
                    break;
                case "castle":
                    for (int i = x + 1; i < 8; i++)
                    {
                        if (!tryPossibleMove(i, y, possibleMoves, board[x, y].playerSide)) break;
                    }
                    for (int i = x - 1; i >= 0; i--)
                    {
                        if (!tryPossibleMove(i, y, possibleMoves, board[x, y].playerSide)) break;
                    }
                    for (int i = y + 1; i < 8; i++)
                    {
                        if (!tryPossibleMove(x, i, possibleMoves, board[x, y].playerSide)) break;
                    }
                    for (int i = y - 1; i >= 0; i--)
                    {
                        if (!tryPossibleMove(x, i, possibleMoves, board[x, y].playerSide)) break;
                    }
                    break;
                case "king":
                    //Look at all 9 squares centered on the king
                    for (int i = -1; i < 2; i++)
                    {
                        for (int u = -1; u < 2; u++)
                        {
                            //Make sure it's inside the board
                            if ((x + i < 8 && x + i >= 0) && (y + u < 8 && y + u >= 0))
                            {
                                tryPossibleMove(x + i, y + u, possibleMoves, board[x, y].playerSide);
                            }
                        }
                    }
                    //TODO: Castling
                    break;
                case "queen":
                    //Queen is the castle and Bishop rolled up into one
                    //Recursively run getPossibleMoves for castle and bishop
                    possibleMoves.AddRange(getPossibleMoves(x, y, "castle"));
                    possibleMoves.AddRange(getPossibleMoves(x, y, "bishop"));
                    break;
            }
            return possibleMoves.ToArray();
        }

        public bool tryPossibleMove(int x, int y, List<Vector2> possibleMoves, int colour)
        {
            if (isValidMove(colour, x, y))
            {
                possibleMoves.Add(new Vector2(x, y));
                if (board[x, y] != null)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool isValidMove(int colour, int x, int y)
        {
            if (colour == 0) //white piece
            {
                if (board[x, y] == null)
                {
                    return true;
                }
                else if (board[x, y].playerSide == 1)
                {
                    return true;
                }
            }
            else if (colour == 1) //Black piece
            {
                if (board[x, y] == null)
                {
                    return true;
                }
                else if (board[x, y].playerSide == 0)
                {
                    return true;
                }
            }
            return false;
        }

        //
        //
        //END WIP ZONE
        //
        //


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
        /// Moves a piece to a new position
        /// </summary>
        private void movePiece(int x, int y, int newX, int newY)
        {
            if (board[x, y].playerSide == 0) //white piece
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
            }
            else if (board[x, y].playerSide == 1) //Black piece
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
            }
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
            else if (newX == x - 1 && Math.Abs(y-newY) == 1)//Diagonal
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
