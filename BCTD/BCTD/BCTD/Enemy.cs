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
        public Enemy()
            : base(Vector2.Zero, new Rectangle(0, 0, 0, 0), Color.White)
        {

        }
    }
}
