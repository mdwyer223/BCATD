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
    public enum TowerType { TOWER, AA, HOMING };

    public class BuyMenu
    {
        List<StoreIcon> icons;
        SpriteFont font;
        MouseState mouse;

        TowerType buyType;
        public TowerType BuyType
        {
            get { return buyType; }
        }

        public BuyMenu(Vector2 bottomOfGrid)
        {
            icons = new List<StoreIcon>();
            font = Game1.GameContent.Load<SpriteFont>("StoreFont");

            icons.Add(new StoreIcon(bottomOfGrid, new Rectangle((int)bottomOfGrid.X, (int)bottomOfGrid.Y, 30, 30), TowerType.TOWER));
            icons.Add(new StoreIcon(new Vector2((bottomOfGrid.X + 80), bottomOfGrid.Y), new Rectangle((int)(bottomOfGrid.X + 80), (int)bottomOfGrid.Y, 30,30), TowerType.HOMING));
        }

        public void Update(GameTime gameTime, Grid grid)
        {
            mouse = Mouse.GetState();

            for(int i = 0; i < icons.Count; i ++)
            {
                if(icons[i] != null)
                {
                    if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(icons[i].Rec))
                    {
                        icons[i].DrawStats = true;
                        if (mouse.LeftButton.Equals(ButtonState.Pressed))
                        {
                            buyType = icons[i].Type;
                        }
                    }
                    else
                        icons[i].DrawStats = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (StoreIcon s in icons)
                s.Draw(spriteBatch);
        }
    }
}
