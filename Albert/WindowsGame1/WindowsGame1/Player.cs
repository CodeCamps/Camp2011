using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    public class Player : GameObject
    {
        public PlayerIndex playerIndex;
        public int restingX;
        int maxX;
        int maxY;

        public int PlayerScore = 0;

        public Vector2 locMouth = Vector2.Zero;

        public int playerhealth;
        public int playerlife;
        
        public Player(PlayerIndex index, int restX, int mX, int mY)
            : base()
        {
            this.playerIndex = index;
            this.restingX = restX;
            this.maxX = mX;
            this.maxY = mY;

            playerhealth = 3;
            playerlife = 3;
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            var state = GamePad.GetState(playerIndex);

            if (state.ThumbSticks.Left.X < 0.0f)
            {
                DestinationRect.X -= 3;
            }
            else if (state.ThumbSticks.Left.X > 0.0f)
            {
                DestinationRect.X += 3;
            }
            else
            {
                if (DestinationRect.X > restingX)
                {
                    DestinationRect.X -= 1;
                }
                else if (DestinationRect.X < restingX)
                {
                    DestinationRect.X += 1;
                }
            }

            if (state.ThumbSticks.Left.Y < 0.0f)
            {
                DestinationRect.Y += 3;
            }
            else if (state.ThumbSticks.Left.Y > 0.0f)
            {
                DestinationRect.Y -= 3;
            }

            if (DestinationRect.X < 10)
            {
                DestinationRect.X = 10;
            }

            if (DestinationRect.Y < 10)
            {
                DestinationRect.Y = 10;
            }

            if (DestinationRect.Y > maxY)
            {
                DestinationRect.Y = maxY;
            }

            if (DestinationRect.X > maxX)
            {
                DestinationRect.X = maxX;
            }

            if (GamePad.GetState(playerIndex).Buttons.A == ButtonState.Pressed)
            {
                if (gameTime.TotalGameTime.TotalSeconds >= nextShotAllowed)
                {
                    nextShotAllowed = gameTime.TotalGameTime.TotalSeconds + 0.33;
                    GameScreen.TheGameScreen.AddSeed(this);
                }
            }

            if (GamePad.GetState(playerIndex).Buttons.X == ButtonState.Pressed)
            {
                if (gameTime.TotalGameTime.TotalSeconds >= nextShotAllowedInter)
                {
                    nextShotAllowedInter = gameTime.TotalGameTime.TotalSeconds + 0.15;
                    GameScreen.TheGameScreen.AddIntercepter(this);
                }
            }

            // --- NEW CODE ---
            if (GamePad.GetState(playerIndex).Buttons.B == ButtonState.Pressed)
            {
                if (gameTime.TotalGameTime.TotalSeconds >= nextShotAllowed2)
                {
                    nextShotAllowed2 = gameTime.TotalGameTime.TotalSeconds + 1.5;
                    GameScreen.TheGameScreen.AddEgg(this, Color.White);
                }
            }

            if (GamePad.GetState(playerIndex).Buttons.Y == ButtonState.Pressed)
            {
                if (gameTime.TotalGameTime.TotalSeconds >= nextShotAllowedLaser)
                {
                    nextShotAllowedLaser = gameTime.TotalGameTime.TotalSeconds + 0.5;
                    GameScreen.TheGameScreen.AddLaser(this, Color.White);
                }
            }
            // ----------------

            base.Update(gameTime);
        }

        public double nextShotAllowed = 0.0;
        public double nextShotAllowed2 = 0.0;
        public double nextShotAllowedInter = 0.0;
        public double nextShotAllowedLaser = 0.0;
    }
}
