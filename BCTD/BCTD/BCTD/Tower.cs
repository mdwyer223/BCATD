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

        int bulletTimer, fireRate, damage;
        
        protected int cost = 0;

        public int Cost
        {
            get { return cost; }
        }

        public Tower(Location loc, int size)
            : base(loc, new Rectangle((int)loc.Position.X, (int)loc.Position.Y, size, size), Color.Gray)
        {
            bullets = new List<Bullet>();
            towerColor = Color.Gray;

            bulletTimer = fireRate = 50;

            cost = 100;
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
                        if (bullets[i].OffScreen)
                        {
                            bullets.RemoveAt(i);
                        }
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
            spriteBatch.Draw(texture, new Rectangle(rec.X, rec.Y, rec.Width, 1), Color.Black);
            spriteBatch.Draw(texture, new Rectangle(rec.X, rec.Y + rec.Height - 1, rec.Width, 1), Color.Black);
            spriteBatch.Draw(texture, new Rectangle(rec.X, rec.Y, 1, rec.Height), Color.Black);
            spriteBatch.Draw(texture, new Rectangle(rec.X + rec.Width - 1, rec.Y, 1, rec.Height), Color.Black);

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
