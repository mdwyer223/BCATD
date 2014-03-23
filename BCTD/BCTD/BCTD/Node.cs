using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Node()
        {
        }
    }
}
