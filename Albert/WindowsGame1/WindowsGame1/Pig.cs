using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WindowsGame1
{
    class Pig : BackgroundElement
    {
        public const float DEFAULT_LAYER_DEPTH = 0.4f;
        public float LayerDepth = DEFAULT_LAYER_DEPTH - 0.1f;

        public Pig(Rectangle srcRect, Rectangle destRect, float dX) 
            : base(srcRect, destRect, dX) 
        { 
        }

        public bool IsDead = false;
        public bool CanDie = true;
        public int HealthValue = 2;

        public int ScoreValue = 99;

        public override void Update(GameTime gameTime)
        {
            locX += deltaX;
            DestinationRect.X = (int)locX;

            if (locX < -DestinationRect.Width)
            {
                IsDead = true;
            }

            if (HealthValue <= 0)
            {
                IsDead = true;
            }
        }

    }
}
