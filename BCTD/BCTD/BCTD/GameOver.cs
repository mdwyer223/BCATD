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
    public class GameOver
    {
        SpriteFont header, display;

        public bool SpacePressed
        {
            get;
            protected set;
        }

        public GameOver()
        {
            header = Game1.GameContent.Load<SpriteFont>("Header");
            display = Game1.GameContent.Load<SpriteFont>("DisplayFont");
        }

        public void Update(GameTime gameTime)
        {
            SpacePressed = Keyboard.GetState().IsKeyDown(Keys.Space);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(display, "Press space to reset!", new Vector2(400 - (display.MeasureString("Press space to reset!").X / 2),
                240 + 1), Color.White);

            spriteBatch.DrawString(header, "Game Over!", new Vector2(400 - (header.MeasureString("Game Over!").X / 2), 100), Color.White);
        }
    }
}
