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
        SpriteFont font;
        bool drawStats;

        public TowerType Type
        {
            get { return type; }
        }

        public bool DrawStats
        {
            get{return drawStats;}
            set{drawStats = value;}
        }

        public StoreIcon(Vector2 pos, Rectangle rec, TowerType type)
            : base(pos, rec)
        {
            this.type = type;
            font = Game1.GameContent.Load<SpriteFont>("StoreFont");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (type == TowerType.TOWER)
            {
                color = Color.Gray;
            }
            if (drawStats)
            {
                if (type == TowerType.TOWER)
                {
                    spriteBatch.DrawString(font, "Tower\n10\n10m", new Vector2(Rec.X + Rec.Width, rec.Y), Color.Gray);
                }
                else if (type == TowerType.HOMING)
                {
                    spriteBatch.DrawString(font, "Homing\n20\n30m", new Vector2(Rec.X + Rec.Width, rec.Y), Color.Yellow);
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
