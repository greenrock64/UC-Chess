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
        public static Dictionary<string, Texture2D> textureDictionary = new Dictionary<string, Texture2D>();
        public static void load(ContentManager Content)
        {
            add("tile", Content.Load<Texture2D>("tile"));
            add("blackPawn", Content.Load<Texture2D>("blackPawn"));
            add("blackCastle", Content.Load<Texture2D>("blackRook"));
            add("blackBishop", Content.Load<Texture2D>("blackBishop"));
            add("blackKnight", Content.Load<Texture2D>("blackKnight"));
            add("blackQueen", Content.Load<Texture2D>("blackQueen"));
            add("blackKing", Content.Load<Texture2D>("blackKing"));

            add("whitePawn", Content.Load<Texture2D>("whitePawn"));
            add("whiteCastle", Content.Load<Texture2D>("whiteRook"));
            add("whiteBishop", Content.Load<Texture2D>("whiteBishop"));
            add("whiteKnight", Content.Load<Texture2D>("whiteKnight"));
            add("whiteQueen", Content.Load<Texture2D>("whiteQueen"));
            add("whiteKing", Content.Load<Texture2D>("whiteKing"));
        }
        public static void add(String name, Texture2D tex)
        {
            textureDictionary.Add(name, tex);
        }
        public static Texture2D getTex(String name)
        {
            if (textureDictionary.ContainsKey(name))
            {
                return textureDictionary[name];
            }
            else
            {
                //TODO: Add an error texture, this just crashes
                return textureDictionary["error"];
            }
        }
    }
}
