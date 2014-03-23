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
    public class Tile : BaseSprite
    {
        protected Location loc;
        protected MouseState mouse;

        public Location Location
        {
            get { return loc; }
        }

        public Vector2 Center
        {
            get { return new Vector2((loc.Column * rec.Width) + (rec.Width / 2) + position.X, 
                (loc.Row * rec.Height) + (rec.Height / 2) + position.Y); }
        }

        public bool Open
        {
            get { return this.GetType() == typeof(Tile); }
        }

        public Tile(Location loc, Rectangle rec, Color color)
            :base (loc.Position, rec, color)
        {
            this.loc = loc;
        }

        public override void Update(GameTime gameTime, Grid grid)
        {
            mouse = Mouse.GetState();
            if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(this.rec))
            {
                if (this.GetType() == typeof(Tile))
                {
                    this.color = Color.Green;
                    if (mouse.LeftButton.Equals(ButtonState.Pressed))
                    {
                        grid.selectTile(this);
                    }
                }
                else
                    this.color = Color.Red;
            }
            else
            {
                color = Color.White;
            }
            base.Update(gameTime, grid);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.MainState == GameState.CONSTRUCTING)
            {
                spriteBatch.Draw(texture, rec, color);
            }
            else if (Game1.MainState != GameState.PLAYING)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
