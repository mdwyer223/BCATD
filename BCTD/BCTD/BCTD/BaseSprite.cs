﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class BaseSprite
    {
        protected Rectangle rec;
        protected Texture2D texture;
        protected Vector2 position;
        protected Color color;

        public Rectangle Rec
        {
            get { return rec; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public BaseSprite(Vector2 pos, Rectangle rec, Color color)
        {
            this.position = pos;
            this.rec = rec;
            this.color = color;

            texture = Game1.GameContent.Load<Texture2D>("Particle");
        }

        public virtual void Update(GameTime gameTime, Grid grid)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, rec, color);
            }
        }
    }
}