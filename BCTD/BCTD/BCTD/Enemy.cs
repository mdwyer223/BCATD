using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class Enemy : BaseSprite
    {
        protected int health, maxHealth;
        Rectangle healthRec;


        private List<Tile> path;

        private Vector2 velo;

        public Vector2 Velocity
        {
            get { return velo; }
        }

        public int Price
        {
            get;
            protected set;
        }

        public Enemy(Grid gr)
            : base(Vector2.Zero, new Rectangle(0, 0, 0, 0))
        {
            Price = 1;
            path = gr.findPath();
        }

        public override void Update(GameTime gameTime, Grid grid)
        {
            if (health <= 0)
                return;
            //moveTo path(0)
            //if in range of node path.remove(0);

            // create method for moveing(tile on), death(position),

            base.Update(gameTime, grid);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (health <= 0)
                return;

            base.Draw(spriteBatch);
            spriteBatch.Draw(texture, new Rectangle(Rec.X, Rec.Y - 10, (int)(Rec.Width * (health / maxHealth) + .5f), 10), Color.Red);
        }
    }
}
