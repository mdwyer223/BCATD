using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BCTD
{
    public class Node
    {
        public int F
        {
            get { return G + H; }
        }

        public int G
        {
            get;
            set;
        }

        public int H
        {
            get;
            set;
        }

        public Node parent
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public Location Loc
        {
            get;
            protected set;
        }

        public Node(Vector2 pos, Location loc)
        {
            Position = pos;
            Loc = loc;
        }

        public override bool Equals(object obj)
        {
            Node node = (Node)obj;
            return this.Loc.Equals(node.Loc);
        }
    }
}
