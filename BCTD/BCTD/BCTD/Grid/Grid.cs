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
        Tile[,] grid;
        int rows, columns;
        float tWidth, tHeight;

        public Grid()
        {
            grid = new Tile[12, 21];
        }

        public Grid(int rows, int columns)
        {
            grid = new Tile[rows, columns];
        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
