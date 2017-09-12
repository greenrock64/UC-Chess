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

        Texture2D tempTile;
        Texture2D tempPieceB;
        Texture2D tempPieceW;

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
            tempTile = Content.Load<Texture2D>("tile");
            tempPieceB = Content.Load <Texture2D>("blackPawn");
            tempPieceW = Content.Load<Texture2D>("whitePawn");
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
                            spriteBatch.Draw(tempPieceW, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
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
