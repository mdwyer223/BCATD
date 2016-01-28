using System;
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
            get { return new Rectangle((int)position.X, (int) position.Y, rec.Width, rec.Height); }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(Rec.X + (rec.Width / 2),
                    Rec.Y + (rec.Height / 2));
            }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public BaseSprite(Vector2 pos, Rectangle rec)
        {
            this.position = pos;
            this.rec = rec;
            this.color = Color.White;

            texture = Game1.GameContent.Load<Texture2D>("Particle");
        }

        public virtual void Update(GameTime gameTime, Grid grid)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, Rec, color);
            }
        }

        public float measureDis(Vector2 point1, Vector2 point2)
        {
            return (float)Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        public float measureDis(Vector2 point)
        {
            return this.measureDis(this.position, point);
        }

    }
}
