using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class BabyPig : Pig
    {
        public float locY;
        public float velocity = 0.0f;

        public BabyPig(Rectangle srcRect, Rectangle destRect, float dX) 
            : base(srcRect, destRect, dX, 1) 
        {
            locY = destRect.Y;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.velocity += 0.5f;
            //this.DestinationRect.X += 8;
            this.DestinationRect.Y += (int)Math.Round(velocity);
            
            if (this.DestinationRect.X > 2000 || this.DestinationRect.Y > 1000)
            {
                IsDead = true;
            }
        }
    }
}
