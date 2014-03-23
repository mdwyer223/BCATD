using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class StoreIcon : BaseSprite
    {
        TowerType type;

        public TowerType Type
        {
            get { return type; }
        }

        public StoreIcon(Vector2 pos, Rectangle rec, TowerType type)
            : base(pos, rec, Color.White)
        {
            this.type = type;
        }
    }
}
