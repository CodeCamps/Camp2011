using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Tree : Pig
    {
        public Tree(Rectangle srcRect, Rectangle destRect, float dX) 
            : base(srcRect, destRect, dX, 1) 
        { 
            LayerDepth = 0.2f;
            CanDie = false;
        }
    }

    class TreeBranch : Pig
    {
        public TreeBranch(Rectangle srcRect, Rectangle destRect, float dX)
            : base(srcRect, destRect, dX, 1)
        {
            LayerDepth = 0.6f;
            CanDie = false;
        }
    }
}
