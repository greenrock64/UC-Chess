using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UC_Chess
{
    //Test Comment
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
            graphics.PreferredBackBufferWidth = 512;
            graphics.PreferredBackBufferHeight = 512;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            board = new Chess();
            board.printBoard();
            curSelect = new Vector2(-1, -1);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tempTile = Content.Load<Texture2D>("tile");
            tempPieceB = Content.Load <Texture2D>("blackPawn");
            tempPieceW = Content.Load<Texture2D>("whitePawn");
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                board.tryMove(1, 0, 3, 0);
            }

            if(Mouse.GetState().LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
            {
                //TODO: No hard coding window size
                if(Mouse.GetState().Position.X > 0 && Mouse.GetState().Position.X < windowWidth)
                {
                    if (Mouse.GetState().Position.Y > 0 && Mouse.GetState().Position.Y < windowHeight)
                    {
                        int boardPosY = (int)Mouse.GetState().Position.X / tileWidth;
                        int boardPosX = (int)Mouse.GetState().Position.Y / tileHeight;
                        if (curSelect.X == -1)
                        {
                            curSelect.X = boardPosX;
                            curSelect.Y = boardPosY;
                            System.Console.WriteLine(curSelect.ToString());
                        }
                        else
                        {
                            board.tryMove((int)curSelect.X, (int)curSelect.Y, boardPosX, boardPosY);
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

                    if (board.getBoard()[u,i] == 1)
                    {
                        //spriteBatch.Draw(tempPieceB, new Rectangle(i * 32, u * 32, 32, 32), Color.White);
                    }

                    if (board.getBoard()[u, i] == 1)
                    {
                        spriteBatch.Draw(tempPieceW, new Rectangle(i * tileWidth, u * tileHeight, tileWidth, tileHeight), Color.White);
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
