using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class Location
    {
        int row, column;
        Vector2 pos;

        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public int Row
        {
            get { return row; }
        }

        public int Column
        {
            get { return column; }
        }

        public Location(Vector2 gridStart, int row, int column, int size)
        {
            this.pos = new Vector2(column * size, row * size) + gridStart;
            this.row = row;
            this.column = column;
        }
    }
}
