using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class BackgroundElement : GameObject
    {
        protected float deltaX;
        protected float locX;

        public BackgroundElement(Rectangle sourceRect, Rectangle destRect, float dX)
            : base()
        {
            SourceRect = sourceRect;
            DestinationRect = destRect;
            deltaX = dX;
            locX = destRect.X;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            locX += deltaX;
            if (locX < -DestinationRect.Width)
            {
                locX += DestinationRect.Width;
            }
            DestinationRect.X = (int)locX;
        }

    }
}
