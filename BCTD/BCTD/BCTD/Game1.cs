using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BCTD
{
    public enum GameState { MAIN_MENU, PLAYING, GAME_OVER, CONSTRUCTING }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MainMenu menu;

        Grid grid;
        MouseState mouse;

        KeyboardState keys, oldKeys;

        static ContentManager gameContent;
        public static ContentManager GameContent
        {
            get { return gameContent; }
        }
        static GameState state = GameState.MAIN_MENU;
        public static GameState MainState
        {
            get { return state; }
            set { state = value; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameContent = new ContentManager(Content.ServiceProvider);
            gameContent.RootDirectory = Content.RootDirectory;
        }

        protected override void Initialize()
        {
            grid = new Grid();
            menu = new MainMenu();
            keys = oldKeys = Keyboard.GetState();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        protected override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
            {
                if (state == GameState.PLAYING)
                {
                    state = GameState.CONSTRUCTING;
                }
                else if (state == GameState.CONSTRUCTING)
                {
                    state = GameState.PLAYING;
                }
            }

            if(state == GameState.PLAYING || state == GameState.CONSTRUCTING)
            {
                grid.Update(gameTime);
            }
            else if (state == GameState.MAIN_MENU)
            {
                menu.Update(gameTime);
            }
            oldKeys = keys;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (state == GameState.PLAYING || state == GameState.CONSTRUCTING)
            {
                grid.Draw(spriteBatch);
            }
            else if (state == GameState.MAIN_MENU)
            {
                menu.Draw(spriteBatch);
            }
            spriteBatch.Draw(Content.Load<Texture2D>("cursor"), new Rectangle(mouse.X, mouse.Y, 10, 10), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
