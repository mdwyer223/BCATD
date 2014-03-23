using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class SpeedyEnemy : Enemy
    {
        public SpeedyEnemy(Grid gr, Entrance e)
            : base(gr, e)
        {
            Price = 25;
            path = gr.findPath();
            this.position = e.Center;
            color = Color.LightGreen;

            maxHealth = health = 30;
        }

        public override void Update(GameTime gameTime, Grid grid)
        {
            if (health <= 0)
                return;
            if (path.Count > 0)
            {
                path[0].Position = new Vector2(path[0].Loc.Column * grid.TileWidth + grid.Position.X + (grid.TileWidth / 2), 
                                    path[0].Loc.Row * grid.TileHeight + grid.Position.Y + (grid.TileHeight / 2));
                                
                velo = path[0].Position - this.position;
                if (!path[0].Position.Equals(position))
                {
                    velo.Normalize();
                    velo *= 2;
                }
                else
                    velo = Vector2.Zero;

                this.position += velo;

                //if in range of node remove node;
                if (measureDis(path[0].Position) < 2)
                {
                    path.RemoveAt(0);
                }                               

                // create method for moveing(tile on), death(position)
            }

            base.Update(gameTime, grid);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (health <= 0)
                return;

            base.Draw(spriteBatch);
            spriteBatch.Draw(texture, new Rectangle(Rec.X, Rec.Y - 2, (int)(Rec.Width * ((float)health / (float)maxHealth) + .5f), 2), Color.Red);
        }
    }
}
