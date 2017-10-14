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
        KeyboardState lastKey;
        Vector2 curSelect;

        //Input state variables (test)
        int state;
        //Temp GUI Elements
        Rectangle continueButton = new Rectangle(231, 175, 50, 25);
        Rectangle resetButton = new Rectangle(231, 225, 50, 25);
        Rectangle quitButton = new Rectangle(231, 275, 50, 25);

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

            //Input state variables (test)
            state = 0;

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && lastKey.IsKeyUp(Keys.Escape))
                if (state == 1)
                {
                    state = 0;
                }
                else if (state == 0)
                {
                    state = 1;
                }
                //Exit();

            if(Mouse.GetState().LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
            {
                //If mouse within window
                if (Mouse.GetState().Position.X > 0 && Mouse.GetState().Position.X < windowWidth)
                {
                    if (Mouse.GetState().Position.Y > 0 && Mouse.GetState().Position.Y < windowHeight)
                    {
                        if (state == 0) //Play game
                        {
                            //Convert mouse position to tile position
                            int boardPosY = (int)Mouse.GetState().Position.X / tileWidth;
                            int boardPosX = (int)Mouse.GetState().Position.Y / tileHeight;
                            if (curSelect.X == -1) //No piece selected
                            {
                                if (board.getPos(boardPosX, boardPosY) != null)
                                {
                                    curSelect.X = boardPosX;
                                    curSelect.Y = boardPosY;
                                    //render.setHighlights(new Vector2[] { curSelect });
                                    render.setHighlights(board.getPossibleMoves(boardPosX, boardPosY));
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
                        else if (state == 1)//In-game menu
                        {
                            if(continueButton.Intersects(new Rectangle(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 1,1))){
                                state = 0;
                            }
                            if (resetButton.Intersects(new Rectangle(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 1, 1)))
                            {
                                board = new Chess();
                                curSelect = new Vector2(-1, -1);
                                render.setHighlights(new Vector2[] {curSelect});
                                state = 0;
                            }
                            if (quitButton.Intersects(new Rectangle(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 1, 1)))
                            {
                                Exit();
                            }
                        }
                    }
                }         
            }

            lastKey = Keyboard.GetState();
            lastMouse = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred,null, SamplerState.AnisotropicWrap,null,null,null,null);
            render.renderBoard(board, spriteBatch);
            //GUI Render test
            if (state == 1)
            {
                //render.renderGUI(guiElements?, spriteBatch);
                spriteBatch.Draw(AssetManager.getTex("tile"), new Rectangle(0,0,windowWidth,windowHeight), new Color(50,50,50,150));
                spriteBatch.Draw(AssetManager.getTex("tile"), continueButton, Color.ForestGreen);
                spriteBatch.Draw(AssetManager.getTex("tile"), resetButton, Color.Orange);
                spriteBatch.Draw(AssetManager.getTex("tile"), quitButton, Color.Red);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
