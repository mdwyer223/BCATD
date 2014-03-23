using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public enum TowerType { TOWER, AA, HOMING };

    public class BuyMenu
    {
        List<StoreIcon> icons;
        SpriteFont font;

        public BuyMenu(Vector2 bottomOfGrid)
        {
            icons = new List<StoreIcon>();
            font = Game1.GameContent.Load<SpriteFont>("StoreFont");
        }
    }
}
