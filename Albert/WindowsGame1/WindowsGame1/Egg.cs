using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    class Egg : Seed
    {
        public Egg(PlayerIndex index) : base(index) { }

        float velocity = 0.0f;
        public override void Update(GameTime gameTime)
        {
            this.velocity += 0.5f;
            this.DestinationRect.X += 8;
            this.DestinationRect.Y += (int)Math.Round(velocity);
            if (this.DestinationRect.X > 2000 || this.DestinationRect.Y > 1000)
            {
                IsDead = true;
            }

            //base.Update(gameTime);
        }
    }
}
