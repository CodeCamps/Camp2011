using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Pigatroopers : Pig
    {
        public Pigatroopers(Rectangle srcRect, Rectangle destRect, float dX) 
            : base(srcRect, destRect, dX, 3) 
        {
            locY = destRect.Y;
        }

        public float locY;
        float deltaY = 0.5f;

        public override void Update(GameTime gameTime)
        {
            locY += deltaY;
            DestinationRect.Y = (int)locY;
            base.Update(gameTime);
        }
    }
}
