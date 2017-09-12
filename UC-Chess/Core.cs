using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UC_Chess
{
    public class Core : Game
    {
        int windowWidth = 512;
        int windowHeight = 512;
        int tileWidth = 512 / 8;
        int tileHeight = 512 / 8;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //TODO: Move to dedicated assetLoader class
        Texture2D tempTile;
        //Black sprites
        Texture2D blackPawn;
        Texture2D blackCastle;
        Texture2D blackBishop;
        Texture2D blackKnight;
        Texture2D blackQueen;
        Texture2D blackKing;
        //White sprites
        Texture2D whitePawn;
        Texture2D whiteCastle;
        Texture2D whiteBishop;
        Texture2D whiteKnight;
        Texture2D whiteQueen;
        Texture2D whiteKing;

        Chess board;

        MouseState lastMouse;
        Vector2 curSelect;

        public Core()
        {
            graphics = new GraphicsDeviceManager(this);

            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            board = new Chess();

            //Debug board display
            //board.printBoard();

            curSelect = new Vector2(-1, -1);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //TODO: Move to dedicated assetLoader class
            tempTile = Content.Load<Texture2D>("tile");
            blackPawn = Content.Load <Texture2D>("blackPawn");
            blackCastle = Content.Load<Texture2D>("blackRook");
            blackBishop = Content.Load<Texture2D>("blackBishop");
            blackKnight = Content.Load<Texture2D>("blackKnight");
            blackQueen = Content.Load<Texture2D>("blackQueen");
            blackKing = Content.Load<Texture2D>("blackKing");

            whitePawn = Content.Load<Texture2D>("whitePawn");
            whiteCastle = Content.Load<Texture2D>("whiteRook");
            whiteBishop = Content.Load<Texture2D>("whiteBishop");
            whiteKnight = Content.Load<Texture2D>("whiteKnight");
            whiteQueen = Content.Load<Texture2D>("whiteQueen");
            whiteKing = Content.Load<Texture2D>("whiteKing");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Mouse.GetState().LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
            {
                if(Mouse.GetState().Position.X > 0 && Mouse.GetState().Position.X < windowWidth)
                {
                    if (Mouse.GetState().Position.Y > 0 && Mouse.GetState().Position.Y < windowHeight)
                    {
                        int boardPosY = (int)Mouse.GetState().Position.X / tileWidth;
                        int boardPosX = (int)Mouse.GetState().Position.Y / tileHeight;
                        if (curSelect.X == -1)
                        {
                            if (board.getPos(boardPosX, boardPosY) != 0)
                            {
                                curSelect.X = boardPosX;
                                curSelect.Y = boardPosY;
                                System.Console.WriteLine(curSelect.ToString());
                            }
                        }
                        else
                        {
                            if (curSelect != new Vector2(boardPosX, boardPosY))
                            {
                                board.tryMove((int)curSelect.X, (int)curSelect.Y, boardPosX, boardPosY);
                            }
                            curSelect = new Vector2(-1, -1);
                        }
                    }
                }
                
            }

            lastMouse = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: 
            //Move to dedicated render class
            //Support larger graphics sizes
            //Make not terrible/more expandable
            spriteBatch.Begin();
            for(int i = 0; i < 8; i++)
            {
                for(int u = 0; u < 8; u++)
                {
                    if (i % 2 > 0 && u % 2 == 0)
                    {
                        spriteBatch.Draw(tempTile, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.Black);
                    }
                    else if (i % 2 == 0 && u % 2 > 0)
                    {
                        spriteBatch.Draw(tempTile, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.Black);
                    }
                    else
                    {
                        spriteBatch.Draw(tempTile, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                    }

                    switch (board.getPos(u, i))
                    {
                        case 1:
                            spriteBatch.Draw(whitePawn, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(whiteCastle, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(whiteBishop, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 4:
                            spriteBatch.Draw(whiteKnight, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 5:
                            spriteBatch.Draw(whiteQueen, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 6:
                            spriteBatch.Draw(whiteKing, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 7:
                            spriteBatch.Draw(blackPawn, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 8:
                            spriteBatch.Draw(blackCastle, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 9:
                            spriteBatch.Draw(blackBishop, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 10:
                            spriteBatch.Draw(blackKnight, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 11:
                            spriteBatch.Draw(blackQueen, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        case 12:
                            spriteBatch.Draw(blackKing, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                            break;
                        default:
                            break;
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
