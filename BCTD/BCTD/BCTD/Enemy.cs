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
        protected int speed;

        protected List<Node> path;

        protected Vector2 velo;

        public Vector2 Velocity
        {
            get { return velo; }
        }

        public bool IsDead
        {
            get { return health <= 0 || path.Count == 0; }
        }

        public bool atEnd
        {
            get;
            set;
        }

        public int Price
        {
            get;
            protected set;
        }

        public Enemy(Grid gr, Entrance e)
            : base(Vector2.Zero, new Rectangle(0, 0, 15, 15))
        {
            Price = 25;
            path = gr.findPath();

            this.position.X = e.Rec.X;
            this.position.Y = e.Rec.Y;
            color = Color.Purple;

            maxHealth = health = 70;

            texture = Game1.GameContent.Load<Texture2D>("Enemy3");
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
                    velo.Normalize();
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

        public void Update(Vector2 velo)
        {
            this.position += velo;
        }

        protected Vector2 getNodePos(Grid grid, int pathIndex)
        {
            return new Vector2(path[pathIndex].Loc.Column * grid.TileWidth + grid.Position.X + (grid.TileWidth / 2),
                                path[pathIndex].Loc.Row * grid.TileHeight + grid.Position.Y + (grid.TileHeight / 2));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (health <= 0)
                return;

            base.Draw(spriteBatch);
            spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("Particle"), new Rectangle(Rec.X, Rec.Y - 2, (int)(Rec.Width * ((float)health / (float)maxHealth) + .5f), 2), Color.Red);
        }

        public void setPath(Grid gr)
        {
            path = gr.findPath();
            
            int nodeDis = int.MaxValue;
            int closeNodeIndex = 0;
            //start from the closest part of the grid
            for (int i = path.Count - 1; i >= 0; i--)
            {
                // get the pathIndex that i'm closest to
                if (measureDis(getNodePos(gr, i)) < nodeDis)
                {
                    nodeDis = (int)measureDis(getNodePos(gr, i));
                    closeNodeIndex = i;
                }
            }

            //removes all parts before that index
            for (int i = path.Count - 1; i > closeNodeIndex; i--)
            {
                path.RemoveAt(i);
            }
        }

        public void reset(Location loc)
        {
            this.position.X = 0;
            this.health = maxHealth;
        }

        public void damage(int d)
        {
            health -= d;
            if (health < 0)
            {
                health = 0;
            }
        }
    }
}
