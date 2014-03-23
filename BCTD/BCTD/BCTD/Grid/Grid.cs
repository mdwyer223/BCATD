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

        Entrance enter;
        Exit exit;

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
            int row;

            row = rand.Next(rows);
            grid[row][0] = enter = new Entrance(new Location(row, 0),this);

            row = rand.Next(rows);
            grid[row][columns - 1] = exit = new Exit(new Location(row, columns - 1), this);
        }

        public Grid(int rows, int columns)
        {
            grid = new List<List<Tile>>();
            this.rows = rows;
            this.columns = columns;

            generateTiles();

            int row;
            row = rand.Next(rows);
            grid[row][0] = enter = new Entrance(new Location(row, 0), this);

            row = rand.Next(rows);
            grid[row][columns - 1] = exit = new Exit(new Location(row, columns - 1), this);
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

        public List<Node> findPath()
        {
            List<Node> openL, closedL;
            Tile current = enter;

            openL = new List<Node>();
            closedL = new List<Node>();

            {               

                foreach (Tile tile in getAdjacent(current.Location))
                {
                    if (!closedL.Contains(tile.TNode) && tile.GetType() == typeof(Tile))
                        if (!openL.Contains(tile.TNode))
                        {
                            Node node = tile.TNode;
                            node.parent = current.TNode;
                            node.G = 10 + current.TNode.G;

                            int xDis = Math.Abs(node.Loc.Column - exit.Location.Column);
                            int yDis = Math.Abs(node.Loc.Row - exit.Location.Row);

                            node.H = xDis + yDis;
                            openL.Add(node);
                        }
                        else
                        {
                            if (1 + current.TNode.G > tile.TNode.G)
                            {
                                tile.TNode.parent = current.TNode;
                                tile.TNode.G = 1 + tile.TNode.G;
                            }
                        }
                }

                //get lowest F on openL
                //current = LowestF
                //closedL.add(current)

            }
            while (current.GetType() != typeof(Exit) || openL.Count > 0);


            if (current.GetType() == typeof(Exit))
                return closedL;
            else
                return null;

        }

        public List<Tile> getAdjacent(Location loc)
        {
            List<Tile> adj = new List<Tile>();
            if (isValid(new Location(loc.Row + 1, loc.Column)))
                adj.Add(this.grid[loc.Row + 1][ loc.Column]);

            if (isValid(new Location(loc.Row - 1, loc.Column)))
                adj.Add(this.grid[loc.Row + 1][loc.Column]);

            if (isValid(new Location(loc.Row, loc.Column + 1)))
                adj.Add(this.grid[loc.Row + 1][loc.Column]);

            if (isValid(new Location(loc.Row, loc.Column - 1)))
                adj.Add(this.grid[loc.Row + 1][loc.Column]);

            return adj;
        }

        public bool isValid(Location loc)
        {
            return loc.Column >= 0 && loc.Column < columns && loc.Row >= 0 && loc.Row < rows;
        }

    }
}
