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
        protected List<Bullet> bullets;
        protected Color towerColor;
        protected Enemy currentTarget;

        protected int bulletTimer, fireRate, damage = 10;
        
        protected int cost = 0, range;

        public int Cost
        {
            get { return cost; }
        }

        public Tower(Location loc, Grid gr)
            : base(loc, gr)
        {
            bullets = new List<Bullet>();
            towerColor = Color.Gray;

            bulletTimer = fireRate = 50;

            cost = 100;
            range = 200;
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
                    spawnBullet(grid.Enemies);
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
            if(currentTarget != null)
            {
                spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("turret"), new Rectangle((int)this.Center.X, (int)this.Center.Y, 30, 30), 
                    null, Color.White, (float)Math.Atan2((currentTarget.Position.Y - Center.Y), (currentTarget.Position.X - Center.X)), 
                    new Vector2(Game1.GameContent.Load<Texture2D>("turret").Width / 2, Game1.GameContent.Load<Texture2D>("turret").Height / 2), SpriteEffects.None, 0);
            }
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

        protected virtual void spawnBullet(List<Enemy> enemies)
        {
            Enemy target = null;
            float smallestDistance = int.MaxValue;
            foreach (Enemy e in enemies)
            {
                if (!e.IsDead)
                {
                    if (measureDis(Center, e.Position) < smallestDistance)
                    {
                        target = e;
                        smallestDistance = measureDis(Center, e.Position);
                    }
                }
            }
            if (target != null && measureDis(Center, target.Position) < range)
            {
                currentTarget = target;
                bullets.Add(new Bullet(this.Center, target, BulletType.STRAIGHT, damage));
            }
        }
    }
}
