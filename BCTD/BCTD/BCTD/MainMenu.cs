using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BCTD
{
    public class MainMenu
    {
        Rectangle newGame;
        Color c;
        SpriteFont head, display;
        MouseState mouse;

        public MainMenu()
        {
            head = Game1.GameContent.Load<SpriteFont>("Header");
            display = Game1.GameContent.Load<SpriteFont>("DisplayFont");
            int width = 200, height = 50;
            int x = 300, y = 265;
            newGame = new Rectangle(x, y, width, height);
        }

        public void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();

            if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(newGame))
            {
                c = Color.Red;
                if (mouse.LeftButton.Equals(ButtonState.Pressed))
                {
                    Game1.MainState = GameState.CONSTRUCTING;
                }
            }
            else
            {
                c = Color.Black;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Particle"), newGame, c);
            spriteBatch.DrawString(display, "Press to start!", new Vector2(newGame.Center.X - (display.MeasureString("Press to Start!").X / 2),
                newGame.Y + 1), Color.White);

            spriteBatch.DrawString(head, "BCTD", new Vector2(400 - (head.MeasureString("BCTD").X / 2), 100), Color.White);
        }
    }
}
