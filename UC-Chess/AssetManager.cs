using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace UC_Chess
{
    public static class AssetManager
    {
        //Texture and Sprite Dictionaries
        public static Dictionary<string, Texture2D> spriteSheetDictionary = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Vector2> spriteSheetData = new Dictionary<string, Vector2>();
        public static Dictionary<string, Rectangle> spriteData = new Dictionary<string, Rectangle>();

        public static void load(ContentManager Content)
        {
            addTex("tile", Content.Load<Texture2D>("tile"), new Vector2(1,1));
            addTex("error", Content.Load<Texture2D>("missingTexture"), new Vector2(1,1));

            addTex("pieceSprites", Content.Load<Texture2D>("PieceSpriteSheet"), new Vector2(6,2));
            addSprite("whitePawn", "pieceSprites", new Vector2(5,0));
            addSprite("whiteCastle", "pieceSprites", new Vector2(4,0));
            addSprite("whiteKnight", "pieceSprites", new Vector2(3,0));
            addSprite("whiteBishop", "pieceSprites", new Vector2(2,0));
            addSprite("whiteQueen", "pieceSprites", new Vector2(1,0));
            addSprite("whiteKing", "pieceSprites", new Vector2(0,0));
            addSprite("blackPawn", "pieceSprites", new Vector2(5,1));
            addSprite("blackCastle", "pieceSprites", new Vector2(4,1));
            addSprite("blackKnight", "pieceSprites", new Vector2(3,1));
            addSprite("blackBishop", "pieceSprites", new Vector2(2,1));
            addSprite("blackQueen",  "pieceSprites", new Vector2(1,1));
            addSprite("blackKing", "pieceSprites", new Vector2(0,1));
        }
        //Add a new spritesheet
        public static void addTex(String name, Texture2D tex, Vector2 sheetData)
        {
            spriteSheetDictionary.Add(name, tex);
            spriteSheetData.Add(name, sheetData);
        }
        //Declare a new sprite in an existing spritesheet
        public static void addSprite(String name, String spritesheet, Vector2 pos)
        {
            int width = spriteSheetDictionary[spritesheet].Width/(int)spriteSheetData[spritesheet].X;
            int height = spriteSheetDictionary[spritesheet].Height/(int)spriteSheetData[spritesheet].Y;
            Rectangle temp = new Rectangle((int)pos.X * width, (int)pos.Y * height, width, height);
            spriteData.Add(name, temp);
        }
        //Get a spritesheet
        public static Texture2D getTex(String name)
        {
            if (spriteSheetDictionary.ContainsKey(name))
            {
                return spriteSheetDictionary[name];
            }
            else
            {
                return spriteSheetDictionary["error"];
            }
        }
        //Get the position of a declared sprite on it's spritesheet
        public static Rectangle getSpritePos(String name)
        {
            return spriteData[name];
        }
    }
}
