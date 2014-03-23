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

        Grid grid;
        MouseState mouse;

        static ContentManager gameContent;
        public static ContentManager GameContent
        {
            get { return gameContent; }
        }
        static GameState state = GameState.CONSTRUCTING;
        public static GameState MainState
        {
            get { return state; }
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        protected override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                state = GameState.PLAYING;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                state = GameState.CONSTRUCTING;
            }
            grid.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            grid.Draw(spriteBatch);
            spriteBatch.Draw(Content.Load<Texture2D>("cursor"), new Rectangle(mouse.X, mouse.Y, 10, 10), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
