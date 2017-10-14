using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace UC_Chess
{
    class Chess
    {
        Piece[,] board;
        public bool count50 = false; //if piece isnt taken it is false. if piece is taken it is true
        public int ruleCounter = 0; //counts the amount of back to back turns without a piece being taken
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
        /// Calculate a list of possible movement positions for a given position
        /// </summary>
        /// <param name="pieceType">(Optional) Override for the pieceType of the square</param>
        /// <returns>An Array of valid movement positions for the piece</returns>
        public Vector2[] getPossibleMoves(int x, int y, string pieceType = "null")
        {
            //Stop if the position contains no piece
            if (board[x, y] == null) return null;
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
                    //Forward 2 squares
                    if (x + 2*dir < 8 && x + 2*dir >= 0)
                    {
                        if (board[x,y].hasMoved == false && board[x+2*dir, y] == null) //Pawn at starting pos and is moving 2 squares
                        {
                            tryPossibleMove(x + 2*dir, y, possibleMoves, board[x, y].playerSide);
                        }
                    }
                    //TODO: En passant
                    break;
                case "bishop":
                    //If true keep checking that diagonal, else ignore it
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
                    //The knight has 8 possible moves
                    if ((y - 2) >= 0) // West
                    {
                        if ((x + 1) < 8) tryPossibleMove(x + 1, y - 2, possibleMoves, board[x, y].playerSide);
                        if ((x - 1) >= 0) tryPossibleMove(x - 1, y - 2, possibleMoves, board[x, y].playerSide);
                    }
                    if ((y + 2) < 8) // East
                    {
                        if ((x + 1) < 8) tryPossibleMove(x + 1, y + 2, possibleMoves, board[x, y].playerSide);
                        if ((x - 1) >= 0) tryPossibleMove(x - 1, y + 2, possibleMoves, board[x, y].playerSide);
                    }
                    if ((x - 2) >= 0) //North
                    {
                        if ((y + 1) < 8) tryPossibleMove(x - 2, y + 1, possibleMoves, board[x, y].playerSide);
                        if ((y - 1) >= 0) tryPossibleMove(x - 2, y - 1, possibleMoves, board[x, y].playerSide);
                    }
                    if ((x + 2) < 8) //South
                    {
                        if ((y + 1) < 8) tryPossibleMove(x + 2, y + 1, possibleMoves, board[x, y].playerSide);
                        if ((y - 1) >= 0) tryPossibleMove(x + 2, y - 1, possibleMoves, board[x, y].playerSide);
                    }
                    break;
                case "castle":
                    for (int i = x + 1; i < 8; i++) //South
                    {
                        //Stop if we hit an invalid move in this direction
                        if (!tryPossibleMove(i, y, possibleMoves, board[x, y].playerSide)) break;
                    }
                    for (int i = x - 1; i >= 0; i--) //North
                    {
                        if (!tryPossibleMove(i, y, possibleMoves, board[x, y].playerSide)) break;
                    }
                    for (int i = y + 1; i < 8; i++) //East
                    {
                        if (!tryPossibleMove(x, i, possibleMoves, board[x, y].playerSide)) break;
                    }
                    for (int i = y - 1; i >= 0; i--) //West
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

        /// <summary>
        /// Add a position to the passed list if it is a valid move
        /// </summary>
        /// <param name="possibleMoves">The output list of possible moves for a piece being appended to</param>
        /// <param name="colour">The playerSide of the piece at X,Y</param>
        /// <returns>True if the square was empty and valid, false if the square was occupied</returns>
        public bool tryPossibleMove(int x, int y, List<Vector2> possibleMoves, int colour)
        {
            //Check the move is valid
            if (isValidMove(colour, x, y))
            {
                possibleMoves.Add(new Vector2(x, y));
                if (board[x, y] != null)
                {
                    //Square contains a piece
                    return false;
                }
            }
            else
            {
                //Move not valid
                return false;
            }
            //Move was valid and the square was empty
            return true;
        }

        /// <summary>
        /// Check if a position is valid for movement
        /// </summary>
        /// <param name="colour">The playerSide of the piece that wants to move</param>
        /// <returns>True if move is valid, false otherwise</returns>
        public bool isValidMove(int colour, int x, int y)
        {
            if (colour == 0) //White piece
            {
                //Square is empty
                if (board[x, y] == null) return true;

                //Square is black
                else if (board[x, y].playerSide == 1) return true;
            }
            else if (colour == 1) //Black piece
            {
                if (board[x, y] == null) return true;

                //Square is white
                else if (board[x, y].playerSide == 0) return true;
            }
            return false;
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
                if (getPossibleMoves(x, y).Contains(new Vector2(newX, newY)))
                {
                    movePiece(x, y, newX, newY);
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
                    board[x, y].hasMoved = true;
                    board[newX, newY] = board[x, y];
                    board[x, y] = null;
                    count50 = false; //piece has not been taken
                    count50Rule();
                }
                else if (board[newX, newY].playerSide == 1)
                {
                    board[x, y].hasMoved = true;
                    board[newX, newY] = board[x, y];
                    board[x, y] = null;
                    count50 = true; //piece has been taken
                    count50Rule();
                }
            }
            else if (board[x, y].playerSide == 1) //Black piece
            {
                if (board[newX, newY] == null)
                {
                    board[x, y].hasMoved = true;
                    board[newX, newY] = board[x, y];
                    board[x, y] = null;
                    count50 = false; //piece has not been taken
                    count50Rule();
                }
                else if (board[newX, newY].playerSide == 0)
                {
                    board[x, y].hasMoved = true;
                    board[newX, newY] = board[x, y];
                    board[x, y] = null;
                    count50 = true; //piece has been taken
                    count50Rule();
                }
            }
        }
        public void count50Rule() //counter class
        {
            if (count50 == true)
            {
                ruleCounter++;
                if (ruleCounter == 50)
                {
                    //game ends in a draw. Maybe a pop up box with the win condiction context in it
                }
            }
            else
            {
                ruleCounter = 0;
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
