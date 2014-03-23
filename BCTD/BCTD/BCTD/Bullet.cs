using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class Bullet : BaseSprite
    {
        protected Vector2 velo;
        protected int speed = 3;

        public Bullet(Vector2 center, Enemy target)
            : base(new Vector2(center.X, center.Y), new Rectangle((int)center.X,(int)center.Y,3,3), Color.Orange)
        {
            velo = new Vector2(-1, 0);
            Vector2 fakeTarget = new Vector2(400, 240);
            velo = fakeTarget - center;
            velo.Normalize();
        }

        public override void Update(GameTime gameTime, Grid grid)
        {
            this.position += (velo * speed);
            //check collision
            base.Update(gameTime, grid);
        }
    }
}
