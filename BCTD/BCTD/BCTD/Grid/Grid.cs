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

        double timer = 0;
        int spawnTime = 3, spawnLimit, spawncounter;

        BuyMenu store;
        int funds = 500;
        SpriteFont font;

        int level = 1, timeBetweenLevels = 10;
        float levelTimer = 0;
        int lives = 10;

        Entrance enter;
        Exit exit;

        int delay = 50, delayTimer = 0;

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

        public List<Enemy> Enemies
        {
            get { return enemies; }
        }

        Random rand = new Random();

        public Grid()
        {
            grid = new List<List<Tile>>();
            enemies = new List<Enemy>();
            rows = 12;
            columns = 24;

            tHeight = 30;
            tWidth = 30;            

            generateTiles();
            randomizeLandscape();

            store = new BuyMenu(new Vector2(startPos.X, startPos.Y + 10 + (rows * tHeight)));
            font = Game1.GameContent.Load<SpriteFont>("DisplayFont");

            int row;
            row = rand.Next(rows);
            grid[0][row] = enter = new Entrance(new Location(row, 0),this);

            row = rand.Next(rows);
            grid[columns - 1][row] = exit = new Exit(new Location(row, columns - 1), this);

            enemies.Add(new Enemy(this, enter));
        }

        public Grid(int rows, int columns)
        {
            grid = new List<List<Tile>>();
            enemies = new List<Enemy>();
            this.rows = rows;
            this.columns = columns;

            generateTiles();
            randomizeLandscape();

            store = new BuyMenu(new Vector2(startPos.X, startPos.Y + 10 + (rows * tHeight)));
            font = Game1.GameContent.Load<SpriteFont>("DisplayFont");

            int row;
            row = rand.Next(rows);
            grid[0][row] = enter = new Entrance(new Location(row, 0), this);

            row = rand.Next(rows);
            grid[columns - 1][row] = exit = new Exit(new Location(row, columns - 1), this);

            enemies.Add(new Enemy(this, enter));
        }

        public virtual void Update(GameTime gameTime)
        {
            if (delayTimer >= delay)
            {
                delayTimer = delay;
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
                if (Game1.MainState == GameState.PLAYING)
                {
                    spawner(gameTime);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i] != null)
                        {
                            enemies[i].Update(gameTime, this);
                            if (enemies[i].IsDead)
                            {
                                funds += enemies[i].Price;
                                enemies.RemoveAt(i);
                            }
                        }
                    }
                }
           
                store.Update(gameTime, this);
            }
            else
            {
                delayTimer++;
            }
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

            
            foreach (Enemy e in enemies)
            {
                if (e != null)
                {
                    e.Draw(spriteBatch);
                }
            }


            store.Draw(spriteBatch);
            spriteBatch.DrawString(font, "$" + funds, new Vector2(3, 3), Color.White);
            spriteBatch.DrawString(font, "Level: " + level, new Vector2(800 - font.MeasureString("Level: 9999").X, 3), Color.White);
            spriteBatch.DrawString(font, "Lives: " + lives, new Vector2(400 - (font.MeasureString("Lives: 111").X / 2), 3), Color.White);
        }

        public void selectTile(Location loc, Tile check)
        {
            if (Game1.MainState == GameState.CONSTRUCTING)
            {
                if (check.GetType() == typeof(Tile))
                {
                    if (store.BuyType == TowerType.TOWER)
                    {
                        Tower tow = new Tower(loc, this);
                        if (funds >= tow.Cost)
                        {
                            grid[loc.Column][loc.Row] = tow;
                            funds -= tow.Cost;
                        }
                    }
                else if (store.BuyType == TowerType.HOMING)
                {
                    HomingTower tow = new HomingTower(loc, this);
                    if (funds >= tow.Cost)
                    {
                        grid[loc.Column][loc.Row] = tow;
                        funds -= tow.Cost;
                    }
                }
                }
                else
                {
                    Tile t = new Tile(loc, this);
                    grid[loc.Column][loc.Row] = t;
                }

                foreach (Enemy e in enemies)
                {
                    if (e != null)
                    {
                        e.setPath(this);
                    }
                }
            }
        }

        public void tempSelectTile(Location loc, bool addingTower)
        {
            if (addingTower)
            {
                Tower tow = new Tower(loc, this);
                grid[loc.Column][loc.Row] = tow;
            }
            else
            {
                Tile t = new Tile(loc, this);
                grid[loc.Column][loc.Row] = t;
            }
        }

        public void spawner(GameTime gametime)
        {
            if (level >= 31)
                spawnLimit = 50;
            else if (level == 30)
                spawnLimit = 5;
            else if (level >= 25)
                spawnLimit = 45;
            else if (level >= 20)
                spawnLimit = 40;
            else if (level >= 16)
                spawnLimit = 30;
            else if (level == 15)
                spawnLimit = 2;
            else if (level >= 10)
                spawnLimit = 25;
            else if (level >= 5)
                spawnLimit = 15;
            else
                spawnLimit = 10;
            //check timer
            timer += gametime.ElapsedGameTime.TotalSeconds;
            if (timer >= spawnTime && spawncounter < spawnLimit)
            {
                if (level == 30)
                {
                    enemies.Add(new Boss(this, enter));
                }
                else if (level >= 20)
                {
                    enemies.Add(new Enemy(this, enter));
                    enemies.Add(new SpeedyEnemy(this, enter));
                    enemies.Add(new TankEnemy(this, enter));
                }
                else if (level >= 16)
                {
                    //enemies.Add(new Enemy(this, enter));
                    enemies.Add(new SpeedyEnemy(this, enter));
                    enemies.Add(new TankEnemy(this, enter));
                }
                else if (level == 15)
                {
                    enemies.Add(new Boss(this, enter));
                }
                else if (level >= 10)
                {
                    enemies.Add(new Enemy(this, enter));
                    //enemies.Add(new SpeedyEnemy(this, enter));
                    enemies.Add(new TankEnemy(this, enter));
                }
                else if (level >= 5)
                {
                    enemies.Add(new Enemy(this, enter));
                    enemies.Add(new SpeedyEnemy(this, enter));
                    //enemies.Add(new TankEnemy(this, enter));
                }
                else
                {
                    enemies.Add(new Enemy(this, enter));
                }
                spawnTime = spawnTime - (level / 30);
                spawncounter++;
                timer = 0;
            }
            else if (spawncounter >= spawnLimit && enemies.Count == 0)
            {
                level++;
                spawncounter = 0;
                Game1.MainState = GameState.CONSTRUCTING;
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

        private void randomizeLandscape()
        {
            int numObjects = rand.Next(3, 11);

            for (int i = 0; i < numObjects; i++)
            {
                if (rand.Next(1, 3) == 1)
                {
                    int y = rand.Next(23), x = rand.Next(10);
                    grid[y][x] = new Rock(grid[y][x].Location, this);
                }
                else
                {
                    int y = rand.Next(23), x = rand.Next(10);
                    grid[y][x] = new Mountain(grid[y][x].Location, this);
                }
            }
        }

        public List<Node> findPath()
        {
            List<Node> openL, closedL;
            Node current = enter.TNode;

            openL = new List<Node>();
            closedL = new List<Node>();

            openL.Add(current);
            clearNodes();

            while (!listContains(closedL, exit.TNode) && openL.Count != 0) 
            {
                int lowF = int.MaxValue;
                Node lowNode = new Node(Vector2.Zero, Location.Zero);
                foreach (Node node in openL)
                {
                    if (node.F < lowF && !listContains(closedL, node))
                    {
                        lowNode = node;
                        lowF = node.F;
                    }
                }
                current = lowNode;
                openL.Remove(current);
                closedL.Add(current);

                foreach (Tile tile in getAdjacent(current.Loc))
                {
                    if (!listContains(closedL, tile.TNode) && (tile.GetType() == typeof(Tile) || tile.GetType() == typeof(Entrance) || tile.GetType() == typeof(Exit) || tile.GetType() == typeof(Rock)))
                    {
                        if (!listContains(openL, tile.TNode))
                        {

                            Node node = tile.TNode;
                            node.parent = current;
                            node.G = 1 + current.G;

                            int xDis = Math.Abs(node.Loc.Column - exit.Location.Column);
                            int yDis = Math.Abs(node.Loc.Row - exit.Location.Row);

                            node.H = xDis + yDis;
                            openL.Add(node);
                            tile.TNode = node;
                        }
                        else
                        {
                            if (1 + current.G < tile.TNode.G)
                            {
                                tile.TNode.parent = current;
                                tile.TNode.G = 1 + tile.TNode.G;
                            }
                        }
                    }
                }
            }
            

            if (listContains(closedL, exit.TNode))
            {
                List<Node> path = new List<Node>();

                path.Add(exit.TNode);

                Node parent = current;
                path.Add(current);
                do
                {
                    if (parent.parent != null)
                        path.Add(parent.parent);
                    parent = parent.parent;
                }
                while (parent != null);
                return path;
            }
            else
                return null;

        }

        public List<Node> testPath(Node blockedNode)
        {
            List<Node> openL, closedL;
            Node current = enter.TNode;

            openL = new List<Node>();
            closedL = new List<Node>();

            closedL.Add(blockedNode);
            openL.Add(current);
            clearNodes();

            while (!listContains(closedL, exit.TNode) && openL.Count != 0)
            {
                int lowF = int.MaxValue;
                Node lowNode = new Node(Vector2.Zero, Location.Zero);
                foreach (Node node in openL)
                {
                    if (node.F < lowF && !listContains(closedL, node))
                    {
                        lowNode = node;
                        lowF = node.F;
                    }
                }
                current = lowNode;
                openL.Remove(current);
                closedL.Add(current);

                foreach (Tile tile in getAdjacent(current.Loc))
                {
                    if (!listContains(closedL, tile.TNode) && (tile.GetType() == typeof(Tile) || tile.GetType() == typeof(Entrance) || tile.GetType() == typeof(Exit) || tile.GetType() == typeof(Rock)))
                    {
                        if (!listContains(openL, tile.TNode))
                        {

                            Node node = tile.TNode;
                            node.parent = current;
                            node.G = 1 + current.G;

                            int xDis = Math.Abs(node.Loc.Column - exit.Location.Column);
                            int yDis = Math.Abs(node.Loc.Row - exit.Location.Row);

                            node.H = xDis + yDis;
                            openL.Add(node);
                            tile.TNode = node;
                        }
                        else
                        {
                            if (1 + current.G < tile.TNode.G)
                            {
                                tile.TNode.parent = current;
                                tile.TNode.G = 1 + tile.TNode.G;
                            }
                        }
                    }
                }
            }


            if (listContains(closedL, exit.TNode))
            {
                List<Node> path = new List<Node>();

                path.Add(exit.TNode);

                Node parent = current;
                path.Add(current);
                do
                {
                    if (parent.parent != null)
                        path.Add(parent.parent);
                    parent = parent.parent;
                }
                while (parent != null);
                return path;
            }
            else
                return null;
        }

        public void clearNodes()
        {
            for (int x = 0; x < columns; x++)
            {
                grid.Add(new List<Tile>());
                for (int y = 0; y < rows; y++)
                {
                    grid[x][y].TNode = new Node(Vector2.Zero, grid[x][y].Location);
                }
            }
        }

        public List<Tile> getAdjacent(Location loc)
        {
            List<Tile> adj = new List<Tile>();
            if (isValid(new Location(loc.Row + 1, loc.Column)))
                adj.Add(this.grid[loc.Column][loc.Row + 1]);

            if (isValid(new Location(loc.Row - 1, loc.Column)))
                adj.Add(this.grid[loc.Column][loc.Row - 1]);

            if (isValid(new Location(loc.Row, loc.Column + 1)))
                adj.Add(this.grid[loc.Column + 1][loc.Row]);

            if (isValid(new Location(loc.Row, loc.Column - 1)))
                adj.Add(this.grid[loc.Column - 1][loc.Row]);

            return adj;
        }

        public Tile get(Location loc)
        {
            return grid[loc.Column][loc.Row];
        }

        public bool isValid(Location loc)
        {
            return loc.Column >= 0 && loc.Column < columns && loc.Row >= 0 && loc.Row < rows;
        }

        private bool listContains(List<Node> list, Node node)
        {
            foreach (Node n in list)
            {
                if (n.Equals(node))
                    return true;
            }
            return false;
        }

    }
}
