using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
namespace UC_Chess
{
    class Renderer
    {
        private int windowWidth, windowHeight, tileWidth, tileHeight;
        private Vector2[] highlights;
        public Renderer()
        {
            setWindowSize(512, 512);
            highlights = new Vector2[0];
        }
        public void setWindowSize(int windowWidth, int windowHeight)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            tileWidth = windowWidth / 8;
            tileHeight = windowHeight / 8;
        }

        //Set squares to be highlighted (current selection, possible movements)
        public void setHighlights(Vector2[] highlights)
        {
            this.highlights = highlights;
        }

        public void renderBoard(Chess board, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int u = 0; u < 8; u++)
                {
                    //Math to alternate tile checkering
                    if (i % 2 > 0 && u % 2 == 0)
                    {
                        spriteBatch.Draw(AssetManager.getTex("tile"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.Black);
                    }
                    else if (i % 2 == 0 && u % 2 > 0)
                    {
                        spriteBatch.Draw(AssetManager.getTex("tile"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.Black);
                    }
                    else
                    {
                        spriteBatch.Draw(AssetManager.getTex("tile"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                    }

                    if (highlights.Count() > 0)
                    {
                        if (highlights.Contains(new Vector2(u, i)))
                        {
                            spriteBatch.Draw(AssetManager.getTex("tile"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.Crimson);
                        }
                    }
                    //If the tile is selected by the user, make it crimson
                    //if (u == curSelect.X && i == curSelect.Y)
                    //{

                    //}

                    //TODO: More efficient (piece class?)
                    switch (board.getPos(u, i))
                    {
                        case 1:
                            spriteBatch.Draw(AssetManager.getTex("whitePawn"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(AssetManager.getTex("whiteCastle"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(AssetManager.getTex("whiteBishop"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 4:
                            spriteBatch.Draw(AssetManager.getTex("whiteKnight"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 5:
                            spriteBatch.Draw(AssetManager.getTex("whiteQueen"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 6:
                            spriteBatch.Draw(AssetManager.getTex("whiteKing"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 7:
                            spriteBatch.Draw(AssetManager.getTex("blackPawn"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 8:
                            spriteBatch.Draw(AssetManager.getTex("blackCastle"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 9:
                            spriteBatch.Draw(AssetManager.getTex("blackBishop"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 10:
                            spriteBatch.Draw(AssetManager.getTex("blackKnight"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 11:
                            spriteBatch.Draw(AssetManager.getTex("blackQueen"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 12:
                            spriteBatch.Draw(AssetManager.getTex("blackKing"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
