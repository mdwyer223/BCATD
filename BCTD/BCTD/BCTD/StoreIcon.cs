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
        Texture2D top;
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
                texture = Game1.GameContent.Load<Texture2D>("Base");
                top = Game1.GameContent.Load<Texture2D>("GatlingGun");
            }
            else if (type == TowerType.HOMING)
            {
                texture = Game1.GameContent.Load<Texture2D>("Base2");
                top = Game1.GameContent.Load<Texture2D>("Turret2");
            }
            if (drawStats)
            {
                if (type == TowerType.TOWER)
                {
                    spriteBatch.DrawString(font, "Tower\n10d\n$" + 100, new Vector2(Rec.X + Rec.Width, rec.Y), Color.Gray);
                }
                else if (type == TowerType.HOMING)
                {
                    spriteBatch.DrawString(font, "Homing\n20d\n$" + 200, new Vector2(Rec.X + Rec.Width, rec.Y), Color.Yellow);
                }
            }
            spriteBatch.Draw(texture, rec, color);
            //spriteBatch.Draw(top, new Rectangle(Rec.X, Rec.Y, 30, 15), color);
        }
    }
}
