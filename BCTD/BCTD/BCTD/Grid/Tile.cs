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

        Node node;

        public Location Location
        {
            get { return loc; }
        }

        public Node TNode
        {
            get { return node; }
            set { node = value; }
        }

        public bool Open
        {
            get { return this.GetType() == typeof(Tile); }
        }

        public Tile(Location loc, Grid gr)
            : base(new Vector2(loc.Column * gr.TileWidth + gr.Position.X, loc.Row * gr.TileHeight + gr.Position.Y),
                new Rectangle((int)(loc.Column * gr.TileWidth + gr.Position.X), (int)(loc.Row * gr.TileHeight + gr.Position.Y), gr.TileWidth, gr.TileHeight))
        {
            this.loc = loc;
            node = new Node(Position, loc);
        }

        public override void Update(GameTime gameTime, Grid grid)
        {
            mouse = Mouse.GetState();
            if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(this.rec))
            {
                if(Game1.MainState == GameState.CONSTRUCTING)
                    if (this.GetType() == typeof(Tile))
                    {
                        this.color = Color.Green;
                        if (mouse.LeftButton.Equals(ButtonState.Pressed))
                        {
                            if (grid.testPath(node) != null)
                            {
                                grid.selectTile(loc, this);
                            }
                        }
                    }
                    else
                    {
                        this.color = Color.Red;
                        if (mouse.RightButton.Equals(ButtonState.Pressed))
                        {
                            grid.selectTile(loc, this);
                        }
                    }
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
