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
        Renderer render;
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
            render = new Renderer();
            render.setWindowSize(windowWidth, windowHeight);
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
                                render.setHighlights(new Vector2[] {curSelect});
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
                            render.setHighlights(new Vector2[] { curSelect });
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

            spriteBatch.Begin();
            render.renderBoard(board, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
