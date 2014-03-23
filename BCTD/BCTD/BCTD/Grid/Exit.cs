using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class Exit : Tile
    {
        public Exit(Location loc, Grid gr)
            : base(loc, gr)
        {
            texture = Game1.GameContent.Load<Texture2D>("Gate");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rec, color);
        }
    }
}
