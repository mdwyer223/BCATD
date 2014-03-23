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
            Price = 20;
            path = gr.findPath();
            this.position = e.Center;
            color = Color.LightGreen;

            maxHealth = health = 20;
            texture = Game1.GameContent.Load<Texture2D>("Enemy1");
        }

        public override void Update(GameTime gameTime, Grid grid)
        {
            if (health <= 0)
                return;

            if (path.Count > 0)
            {
                path[path.Count - 1].Position = getNodePos(grid, path.Count - 1);

                velo = path[path.Count - 1].Position - this.position;
                foreach (Tile tile in grid.getAdjacent(path[path.Count - 1].Loc))
                {
                    if (tile.GetType() == typeof(Rock) && Rec.Intersects(tile.Rec))
                    {
                        velo *= .3f;
                    }
                }

                if (!path[path.Count - 1].Position.Equals(position))
                {
                    velo.Normalize();
                    velo *= 2;
                }
                else
                    velo = Vector2.Zero;

                this.position += velo;

                //if in range of node remove node;
                if (measureDis(path[path.Count - 1].Position) < 2)
                {
                    path.RemoveAt(path.Count - 1);
                    if (path.Count == 0)
                        atEnd = true;
                }
            }

            base.Update(gameTime, grid);
        }
    }
}
