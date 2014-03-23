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

        public static Location Zero
        {
            get { return new Location(0, 0); }
        }

        public Location(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public override bool Equals(object obj)
        {
            Location loc = (Location)obj;
            return loc.Row == this.Row && loc.Column == this.Column;
        }

    }
}
