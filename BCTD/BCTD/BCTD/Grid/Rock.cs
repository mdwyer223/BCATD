﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class Rock : Tile
    {
        public Rock(Location loc, Grid gr)
            : base(loc, gr)
        {
            texture = Game1.GameContent.Load<Texture2D>("rock");
        }

        public override void Update(GameTime gameTime, Grid grid)
        {
            color = Color.Crimson;     
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rec, Color.White);
            //spriteBatch.Draw(texture, new Rectangle(rec.X, rec.Y, rec.Width, 1), Color.Black);
            //spriteBatch.Draw(texture, new Rectangle(rec.X, rec.Y + rec.Height - 1, rec.Width, 1), Color.Black);
            //spriteBatch.Draw(texture, new Rectangle(rec.X, rec.Y, 1, rec.Height), Color.Black);
            //spriteBatch.Draw(texture, new Rectangle(rec.X + rec.Width - 1, rec.Y, 1, rec.Height), Color.Black);
        }
    }
}
