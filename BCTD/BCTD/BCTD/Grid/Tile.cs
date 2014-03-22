using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class Tile : BaseSprite
    {
        protected Location loc;

        public Location Location
        {
            get { return loc; }
        }
    }
}
