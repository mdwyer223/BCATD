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

        public Grid()
        {
            grid = new List<List<Tile>>();
            rows = 12;
            columns = 24;

            tHeight =30;
            tWidth = 30;

            generateTiles();
        }

        public Grid(int rows, int columns)
        {
            grid = new List<List<Tile>>();
            this.rows = rows;
            this.columns = columns;
            generateTiles();
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
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                foreach (Tile t in grid[i])
                {
                    if (Game1.MainState != GameState.PLAYING && t != null)
                        t.Draw(spriteBatch);
                }
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
                    Location loc = new Location(new Vector2((x * tWidth), y * tHeight) + startPos, y, x); 
                    grid[x].Add(new Tile(loc, new Rectangle((int)loc.Position.X, (int)loc.Position.Y, (int)tWidth, (int)tHeight), Color.White));
                }
            }
        }
    }
}
