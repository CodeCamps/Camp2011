using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class KingPig : Pig
    {
        public KingPig(Rectangle srcRect, Rectangle destRect, float dX) 
            : base(srcRect, destRect, dX) 
        {
            HealthValue = 5;

            ScoreValue = 5000;
        }

        public double nextPigAllowed = 0.0;

        Random m_Rand = new Random();

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //Toss baby pigs here!
            if (gameTime.TotalGameTime.TotalSeconds >= nextPigAllowed)
            {
                nextPigAllowed = gameTime.TotalGameTime.TotalSeconds + 1.0;
                var rect = new Rectangle(
                    DestinationRect.X + DestinationRect.Width / 2 - 26,
                    DestinationRect.Y + DestinationRect.Height / 2 -25,
                    52,49);
                var pig = new BabyPig(
                    new Rectangle(573, 104, 52, 49),
                    rect,
                    (float)(m_Rand.NextDouble() * 8.0 - 4.0));
                pig.velocity = -18;
                pig.ScoreValue = 1000;
                GameScreen.TheGameScreen.AddPig(pig);
            }
        }
    }
}
