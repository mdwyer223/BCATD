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

        public int TileWidth
        {
            get { return (int)tWidth; }
        }

        public int TileHeight
        {
            get { return (int)tHeight; }
        }

        public Vector2 Position
        {
            get { return startPos; }
        }

        Random rand = new Random();

        public Grid()
        {
            grid = new List<List<Tile>>();
            rows = 12;
            columns = 24;

            tHeight = 30;
            tWidth = 30;            

            generateTiles();

            // this.add(new Entrance(randomLoc);
            // this.add(new Exit(randomLoc);
        }

        public Grid(int rows, int columns)
        {
            grid = new List<List<Tile>>();
            this.rows = rows;
            this.columns = columns;

            generateTiles();

            // this.add(new Entrance(randomLoc);
            // this.add(new Exit(randomLoc);
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
                    Location loc = new Location(y, x); 
                    grid[x].Add(new Tile(loc, this));
                }
            }
        }

        public List<Tile> findPath()
        {
            List<Tile> openL, closedL;
            Tile current = new Tile(Location.Zero, this);

            openL = new List<Tile>();
            closedL = new List<Tile>();

            {


                // path stuff

            }
            while (current.GetType() != typeof(Exit) || openL.Count > 0);


            if (current.GetType() == typeof(Exit))
                return closedL;
            else
                return null;

        }

        public Tile getAdjacent(Location loc)
        {
            return new Tile(Location.Zero, this);
        }
    }
}
