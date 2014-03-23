using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public enum BulletType { STRAIGHT, HOMING, SHATTER };

    public class Bullet : BaseSprite
    {
        protected BulletType type;
        protected Vector2 velo;
        protected Enemy target;
        protected int timeToHit = 2, damage = 0;

        int timer;

        Random rand = new Random();

        bool hitTarget;

        public bool OffScreen
        {
            get { return position.X < 0 || position.Y < 0 || position.X > 800 || position.Y > 480 || hitTarget || timer > 240; }
        }


        public Bullet(Vector2 center, Enemy target, BulletType t, int damage)
            : base(center, new Rectangle((int)center.X,(int)center.Y,3,3))
        {
            type = t;
            this.target = target;
            this.damage = damage;
            if (t == BulletType.STRAIGHT)
            {
                Vector2 tPos = target.Position + (target.Velocity * (timeToHit * 16));
                velo = new Vector2(tPos.X - center.X, tPos.Y - center.Y);
                velo = velo / (timeToHit * 16);

            }
            color = Color.Orange;
            //velo.Normalize();
        }

        public override void Update(GameTime gameTime, Grid grid)
        {
            timer++;
            if (type == BulletType.HOMING)
            {
                velo = new Vector2(target.Position.X - this.position.X, target.Position.Y - this.position.Y);
                velo.Normalize();
                velo *= 2.5f;
            }
            this.position += (velo);
            //check collision
            if (this.Rec.Intersects(target.Rec))
            {
                target.damage(damage);
                hitTarget = true;
            }
            base.Update(gameTime, grid);
        }

        public virtual void Update()
        {
            if (type == BulletType.HOMING)
            {
                velo = new Vector2(target.Position.X - this.position.X, target.Position.Y - this.position.Y);
                velo.Normalize();
                velo *= 1.25f;
            }
            this.position += (velo);
            //check collision
            if (this.Rec.Intersects(target.Rec))
            {
                target.damage(damage);
                hitTarget = true;
            }
        }
    }
}
