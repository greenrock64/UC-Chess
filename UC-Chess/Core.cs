using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UC_Chess
{
    public class Core : Game
    {
        //Graphics Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Temporary variables for screen sizes
        int windowWidth = 512;
        int windowHeight = 512;
        int tileWidth = 512 / 8;
        int tileHeight = 512 / 8;

        //Logic Variables
        Chess board;

        //Input Variables
        MouseState lastMouse;
        Vector2 curSelect;

        public Core()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.ApplyChanges();

            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            board = new Chess();

            //Debug board display
            //board.printBoard();

            //Which tile the user has selected
            //Defaults to off board when nothing selected
            curSelect = new Vector2(-1, -1);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            AssetManager.load(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            //Input
            //TODO: Input handler class?
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(Mouse.GetState().LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
            {
                //If mouse within window
                if(Mouse.GetState().Position.X > 0 && Mouse.GetState().Position.X < windowWidth)
                {
                    if (Mouse.GetState().Position.Y > 0 && Mouse.GetState().Position.Y < windowHeight)
                    {
                        //Convert mouse position to tile position
                        int boardPosY = (int)Mouse.GetState().Position.X / tileWidth;
                        int boardPosX = (int)Mouse.GetState().Position.Y / tileHeight;
                        if (curSelect.X == -1) //No piece selected
                        {
                            if (board.getPos(boardPosX, boardPosY) != 0)
                            {
                                curSelect.X = boardPosX;
                                curSelect.Y = boardPosY;
                                System.Console.WriteLine(curSelect.ToString());
                            }
                        }
                        else //Piece previously selected
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
                    //If the tile is selected by the user, make it crimson
                    if (u == curSelect.X && i == curSelect.Y)
                    {
                        spriteBatch.Draw(AssetManager.getTex("tile"), new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.Crimson);
                    }

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
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
