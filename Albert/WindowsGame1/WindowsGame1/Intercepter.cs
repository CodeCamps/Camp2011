using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class Intercepter : GameObject
    {
        public Intercepter(PlayerIndex index)
            : base()
        {
            this.PlayerIndex = index;
        }

        public PlayerIndex PlayerIndex;
        public bool IsDead;

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            this.DestinationRect.X +=50;
            if (this.DestinationRect.X > 2000 || this.DestinationRect.Y > 1000)
            {
                IsDead = true;
            }

            base.Update(gameTime);
        }
    }
}
