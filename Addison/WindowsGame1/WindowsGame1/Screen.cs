using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Screen
    {
        protected Game TheGame;
        protected Rectangle bounds;

        public Screen(Game theGame)
        {
            this.TheGame = theGame;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public virtual void LoadContent(Rectangle screenBounds)
        {
            bounds = screenBounds;
        }
    }
}
