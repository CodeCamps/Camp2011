﻿using System;
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

        public int PigHealth;

        public Pig(Rectangle srcRect, Rectangle destRect, float dX, int health) 
            : base(srcRect, destRect, dX) 
        {
            PigHealth = health;
        }

        public bool IsDead = false;
        public bool CanDie = true;

        public int ScoreValue = 50;

        public override void Update(GameTime gameTime)
        {
            locX += deltaX;
            DestinationRect.X = (int)locX;

            if (locX < -DestinationRect.Width || PigHealth <= 0)
            {
                IsDead = true;
            }
        }

    }
}
