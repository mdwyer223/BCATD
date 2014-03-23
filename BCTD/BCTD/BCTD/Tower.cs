using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class Tower : Tile
    {
        List<Bullet> bullets;
        Color towerColor;

        int bulletTimer, fireRate;

        public Tower(Location loc, int size)
            : base(loc, new Rectangle((int)loc.Position.X, (int)loc.Position.Y, size, size), Color.Gray)
        {
            bullets = new List<Bullet>();
            towerColor = Color.Gray;

            bulletTimer = fireRate = 50;
        }

        public override void Update(GameTime gameTime, Grid grid)
        {
            if (Game1.MainState == GameState.PLAYING)
            {
                if (bulletTimer < fireRate)
                {
                    bulletTimer++;
                }
                else
                {
                    bulletTimer = 0;
                    spawnBullet(null);
                }

                color = towerColor;

                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i] != null)
                    {
                        bullets[i].Update(gameTime, grid);
                    }
                }
            }
            else if (Game1.MainState != GameState.PLAYING)
            {
                base.Update(gameTime, grid);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rec, color);

            foreach (Bullet b in bullets)
            {
                if (b != null)
                    b.Draw(spriteBatch);
            }
        }

        protected virtual void spawnBullet(Enemy e)
        {
            bullets.Add(new Bullet(this.Center, null));
        }
    }
}
