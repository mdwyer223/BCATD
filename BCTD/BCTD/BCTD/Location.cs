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

        public Location(Vector2 pos, int row, int column)
        {
            this.pos = pos;
            this.row = row;
            this.column = column;
        }
    }
}
