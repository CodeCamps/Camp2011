using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    public class GameObject
    {
        public Rectangle SourceRect;
        public Rectangle DestinationRect;

        public Color Tint = Color.White;

        public int Health;

        public virtual void Update(GameTime gameTime)
        {
        }


    }
}
