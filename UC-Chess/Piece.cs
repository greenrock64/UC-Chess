using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UC_Chess
{
    class Piece
    {
        //Rendering variables
        public string textureName; //The texture name for this piece
        //Logic Variables
        public string pieceType;
        public int playerSide; //0 - White, 1 - Black

        /// <summary>
        /// Piece class to store data related to individual pieces on the board.
        /// </summary>
        /// <param name="type">What kind of chess piece this is (EG knight, king, queen)</param>
        /// <param name="playerSide">Wether this piece is white (0) or black (1)</param>
        public Piece(string type, int playerSide)
        {
            this.playerSide = playerSide;
            setPiece(type.ToLower());
        }

        public Piece setPiece(string type)
        {
            this.pieceType = type;
            if (playerSide == 0)
            {
                this.textureName = "white" + pieceType.Substring(0,1).ToUpper() + pieceType.Substring(1); 
            }
            else
            {
                this.textureName = "black" + pieceType.Substring(0, 1).ToUpper() + pieceType.Substring(1); 
            }
            return this;
        }
    }
}
