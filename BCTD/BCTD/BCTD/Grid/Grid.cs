using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class Grid
    {
        Vector2 startPos;
        List<List<Tile>> grid;
        List<Enemy> enemies;
        int rows, columns;
        float tWidth, tHeight;

        BuyMenu store;
        int funds = 500;
        SpriteFont font;

        int level = 1;

        public Grid()
        {
            grid = new List<List<Tile>>();
            rows = 12;
            columns = 24;

            tHeight =30;
            tWidth = 30;

            generateTiles();

            store = new BuyMenu(new Vector2(startPos.X, startPos.Y + 10 + (rows * tHeight)));
            font = Game1.GameContent.Load<SpriteFont>("DisplayFont");
        }

        public Grid(int rows, int columns)
        {
            grid = new List<List<Tile>>();
            this.rows = rows;
            this.columns = columns;
            generateTiles();
            store = new BuyMenu(new Vector2(startPos.X, startPos.Y + 10 + (rows * tHeight)));
            font = Game1.GameContent.Load<SpriteFont>("DisplayFont");
        }

        public virtual void Update(GameTime gameTime)
        {
            for (int x = 0; x < grid.Count; x++)
            {
                for (int y = 0; y < grid[x].Count; y++)
                {
                    if (grid[x][y] != null)
                    {
                        grid[x][y].Update(gameTime, this);
                    }
                }
            }

            store.Update(gameTime, this);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                foreach (Tile t in grid[i])
                {
                    if (t.GetType() == typeof(Tile))
                    {
                        if (Game1.MainState != GameState.PLAYING && t != null)
                            t.Draw(spriteBatch);
                    }
                    else
                    {
                        t.Draw(spriteBatch);
                    }
                }
            }

            store.Draw(spriteBatch);
            spriteBatch.DrawString(font, "$" + funds, new Vector2(3, 3), Color.White);
            spriteBatch.DrawString(font, "Level: " + level, new Vector2(800 - font.MeasureString("Level: 9999").X, 3), Color.White);
        }

        public void selectTile(Location loc, Tile check)
        {
            if (check.GetType() == typeof(Tile))
            {
                if (store.BuyType == TowerType.TOWER)
                {
                    Tower tow = new Tower(loc, (int)tHeight);
                    if (funds >= tow.Cost)
                    {
                        grid[loc.Column][loc.Row] = tow;
                        funds -= tow.Cost;
                    }
                }
                
            }
            else
            {
                Tile t = new Tile(loc, new Rectangle((int)loc.Position.X, (int)loc.Position.Y, (int)tWidth, (int)tHeight), Color.White);
                grid[loc.Column][loc.Row] = t;
            }
        }

        private void generateTiles()
        {
            startPos = new Vector2(800 * .05f, 480 * .1f); 
            for (int x = 0; x < columns; x++)
            {
                grid.Add(new List<Tile>());
                for (int y = 0; y < rows; y++)
                {
                    Location loc = new Location(startPos, y, x, (int)tWidth); 
                    grid[x].Add(new Tile(loc, new Rectangle((int)loc.Position.X, (int)loc.Position.Y, (int)tWidth, (int)tHeight), Color.White));
                }
            }
        }
    }
}
