using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    class GameScreen : Screen
    {
        protected Player thePlayer1;
        protected Player thePlayer2;
        protected Player thePlayer3;
        protected Player thePlayer4;

        public static GameScreen TheGameScreen;

        public GameScreen(Game theGame)
            : base(theGame)
        {
            TheGameScreen = this;
        }

        int maxX;
        int maxY;
        int restingX;

        Texture2D texBirds;
        Texture2D texSprites;
        Texture2D texClouds;

        SoundEffect effectShoot;
        // --- NEW CODE ---
        SoundEffect effectFall;
        SoundEffect effectSnort;
        SoundEffect effectSqueal;
        SoundEffect effectSquawk;

        SpriteFont fontScore;

        //int locCloudX = 0;
        //Rectangle rectClouds;
        Song songMusic;

        Texture2D texParallax;
        BackgroundElement beHillBack;
        BackgroundElement beHillMid;
        BackgroundElement beHillFront;
        BackgroundElement beSky;
        // ----------------

        public override void LoadContent(Rectangle screenBounds)
        {
            base.LoadContent(screenBounds);

            effectShoot = TheGame.Content.Load<SoundEffect>("spit");
            // --- NEW CODE ---
            effectFall = TheGame.Content.Load<SoundEffect>("fall");
            effectSnort = TheGame.Content.Load<SoundEffect>("snort");
            effectSqueal = TheGame.Content.Load<SoundEffect>("squeal");
            effectSquawk = TheGame.Content.Load<SoundEffect>("squawk");

            fontScore = TheGame.Content.Load<SpriteFont>("scores");
            // ----------------

            texSprites = TheGame.Content.Load<Texture2D>("sprites");
            texBirds = TheGame.Content.Load<Texture2D>("ingamebirds1");
            texClouds = TheGame.Content.Load<Texture2D>("clouds");

            maxX = 2 * bounds.Width / 3;
            restingX = (maxX - 10) / 5 + 10;

            Rectangle rect = new Rectangle(10, 10, 92, 93);

            maxY = bounds.Height - rect.Height - 10;
            thePlayer1 = new Player(PlayerIndex.One, restingX-25, maxX, maxY);
            thePlayer1.DestinationRect = rect;
            thePlayer1.SourceRect = new Rectangle(400, 545, 92, 93);

            thePlayer1.locMouth.X = rect.Width / 2;
            thePlayer1.locMouth.Y = rect.Height / 2;

            rect = new Rectangle(10, 75, 100, 100);
            maxY = bounds.Height - rect.Height - 10;
            thePlayer2 = new Player(PlayerIndex.Two, restingX+25, maxX, maxY);
            thePlayer2.DestinationRect = rect;
            thePlayer2.SourceRect = new Rectangle(142,405, 100, 100);
            thePlayer2.locMouth.X = rect.Width / 2;
            thePlayer2.locMouth.Y = rect.Height / 2;

            // green 285,430,104,75
            rect = new Rectangle(10, 150, 104, 75);
            maxY = bounds.Height - rect.Height - 10;
            thePlayer3 = new Player(PlayerIndex.Three, restingX-25, maxX, maxY);
            thePlayer3.DestinationRect = rect;
            thePlayer3.SourceRect = new Rectangle(285, 430, 104, 75);
            thePlayer3.locMouth.X = rect.Width / 2;
            thePlayer3.locMouth.Y = rect.Height / 2;

            // black 430,430,65,75
            rect = new Rectangle(10, 225, 65, 75);
            maxY = bounds.Height - rect.Height - 10;
            thePlayer4 = new Player(PlayerIndex.Four, restingX+25, maxX, maxY);
            thePlayer4.DestinationRect = rect;
            thePlayer4.SourceRect = new Rectangle(430, 430, 65, 75);
            thePlayer4.locMouth.X = rect.Width / 2;
            thePlayer4.locMouth.Y = rect.Height / 2;

            // --- NEW CODE ---
            //rectClouds = texClouds.Bounds;

            songMusic = TheGame.Content.Load<Song>("music");
            MediaPlayer.Play(songMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.25f;

            texParallax = TheGame.Content.Load<Texture2D>("parallax");
            beHillFront = new BackgroundElement(
                new Rectangle(0, 646, 1024, 378),
                new Rectangle(0, bounds.Height - 350, 1024, 378),
                -2.1f);
            beHillFront.Tint = new Color(0.9f, 0.9f, 1.0f);
            beHillBack = new BackgroundElement(
                new Rectangle(0, 646, 1024, 378),
                new Rectangle(0, bounds.Height - 390, 1536, 378),
                -0.5f);
            beHillMid = new BackgroundElement(
                new Rectangle(0, 353, 1024, 292),
                new Rectangle(0, bounds.Height - 385, 1536, 292),
                -1.4f);
            beSky = new BackgroundElement(
                texClouds.Bounds,
                texClouds.Bounds,
                -0.2f);
            // ----------------
        }

        List<Seed> seeds = new List<Seed>();

        public void AddSeed(Player player)
        {
            var seed = new Seed(player.playerIndex);
            seed.SourceRect = new Rectangle(3, 825, 38, 10);
            seed.DestinationRect = new Rectangle(3, 825, 38, 10);

            seed.DestinationRect.X = 
                player.DestinationRect.X +
                (int)player.locMouth.X;
            seed.DestinationRect.Y = 
                player.DestinationRect.Y +
                (int)player.locMouth.Y;

            seeds.Add(seed);

            effectShoot.Play();
        }

        // --- NEW CODE ---
        List<Egg> eggs = new List<Egg>();

        public void AddEgg(Player player, Color tint)
        {
            var egg = new Egg(player.playerIndex);
            egg.Tint = tint;
            egg.SourceRect = 
            egg.DestinationRect = new Rectangle(666, 818, 50, 59);

            egg.DestinationRect.X = player.DestinationRect.X;
            egg.DestinationRect.Y = player.DestinationRect.Y;

            eggs.Add(egg);

            effectFall.Play();
        }

        List<Pig> pigs = new List<Pig>();

        public void AddPig(Pig pig)
        {
            pigs.Add(pig);
            effectSnort.Play();
        }

        public void AddPig() { AddPig(false); }

        public void AddPig(bool isInTree)
        {
            var pig = new Pig(
                new Rectangle(428,52,104,100),
                new Rectangle(bounds.Width,bounds.Height - 130,104,100),
                -2.1f);

            if (isInTree)
            {
                pig.DestinationRect.Y -= 115;
                pig.ScoreValue = 100;
            }

            AddPig(pig);
            //pigs.Add(pig);
            //effectSnort.Play();
        }

        public void AddTree()
        {
            var tree = new Tree(
                new Rectangle(68, 159, 41, 203),
                new Rectangle(bounds.Width + 31, bounds.Height - 233, 41, 203),
                -2.1f);

            var branch = new TreeBranch(
                new Rectangle(23, 153, 48, 127),
                new Rectangle(bounds.Width, bounds.Height - 233 - 6, 48, 127),
                -2.1f);

            pigs.Add(tree);
            pigs.Add(branch);

            AddPig(true);
        }


        public void AddPigatrooper()
        {
            var pig = new Pigatroopers(
                new Rectangle(286,66,94,87),
                new Rectangle(bounds.Width, bounds.Height - 130, 94,87),
                -2.1f);
            
            pig.DestinationRect.Y = -pig.DestinationRect.Height / 2;
            pig.locY = pig.DestinationRect.Y;
            pig.ScoreValue = 150;

            pigs.Add(pig);

            pig = new Pigatroopers(
                new Rectangle(717,214,140,112),
                new Rectangle(bounds.Width - 20, bounds.Height - 130, 140, 112),
                -2.1f);

            pig.DestinationRect.Y = -pig.DestinationRect.Height;
            pig.locY = pig.DestinationRect.Y;
            pig.CanDie = false;
            pig.LayerDepth = 0.2f;

            pigs.Add(pig);

            effectSnort.Play();
        }

        public void AddKing()
        {
            // king: 0,0,128,153
            // baby: 573,104,52,49
            var king = new KingPig(
                new Rectangle(0,0,128,153),
                new Rectangle(bounds.Width,bounds.Height - 171, 128, 153),
                -2.1f);
            king.ScoreValue = 500;
            pigs.Add(king);
            effectSnort.Play();
        }


        public double nextPigAllowed = 0.0;
        public double nextTreeAllowed = 0.0;
        public double nextTrooperAllowed = 0.0;
        public double nextKingAllowed = 0.0;
        bool isFirstTime = true;

        Random m_Rand = new Random();
        
        // ----------------

        public override void Update(GameTime gameTime)
        {
            if (isFirstTime)
            {
                isFirstTime = false;
                nextTreeAllowed = gameTime.TotalGameTime.TotalSeconds + 12.0;
                nextTrooperAllowed = gameTime.TotalGameTime.TotalSeconds + 18.0;
                nextKingAllowed = gameTime.TotalGameTime.TotalSeconds + 30.0;
            }

            CheckForCollisions(gameTime);

            thePlayer1.Update(gameTime);
            thePlayer2.Update(gameTime);
            thePlayer3.Update(gameTime);
            thePlayer4.Update(gameTime);

            if (gameTime.TotalGameTime.TotalSeconds >= nextPigAllowed)
            {
                nextPigAllowed = gameTime.TotalGameTime.TotalSeconds +
                    1.5 +
                    2.5 * m_Rand.NextDouble();
                AddPig();
            }

            if (gameTime.TotalGameTime.TotalSeconds >= nextTreeAllowed)
            {
                nextTreeAllowed = gameTime.TotalGameTime.TotalSeconds +
                    3.0 +
                    7.0 * m_Rand.NextDouble();
                AddTree();
            }

            if (gameTime.TotalGameTime.TotalSeconds >= nextTrooperAllowed)
            {
                nextTrooperAllowed = gameTime.TotalGameTime.TotalSeconds +
                    3.0 +
                    7.0 * m_Rand.NextDouble();
                AddPigatrooper();
            }

            if (gameTime.TotalGameTime.TotalSeconds >= nextKingAllowed)
            {
                nextKingAllowed = gameTime.TotalGameTime.TotalSeconds +
                    15.0;
                AddKing();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                TheGame.Exit();
            }

            //if (seed != null) { seed.Update(gameTime); }

            foreach (Seed seed in seeds)
            {
                seed.Update(gameTime);
            }

            int index = 0;
            while (index < seeds.Count)
            {
                if (seeds[index].IsDead)
                {
                    seeds.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            // --- NEW CODE ---
            foreach (Egg egg in eggs)
            {
                egg.Update(gameTime);
            }

            index = 0;
            while (index < eggs.Count)
            {
                if (eggs[index].IsDead)
                {
                    eggs.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            int indexPig = 0;
            while (indexPig < pigs.Count)
            {
                pigs[indexPig].Update(gameTime);
                indexPig++;
            }
            //foreach (Pig pig in pigs)
            //{
            //    pig.Update(gameTime);
            //}

            index = 0;
            while (index < pigs.Count)
            {
                if (pigs[index].IsDead)
                {
                    pigs.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            //locCloudX -= 3;
            //if (locCloudX < -rectClouds.Width)
            //{
            //    locCloudX += rectClouds.Width;
            //}
            beHillBack.Update(gameTime);
            beHillMid.Update(gameTime);
            beHillFront.Update(gameTime);
            beSky.Update(gameTime);
            // ----------------


            //if (thePlayer1.DestinationRect.Intersects(thePlayer2.DestinationRect))
            //{
            //    GamePad.SetVibration(thePlayer1.playerIndex, 0.5f, 1.0f);
            //    GamePad.SetVibration(thePlayer2.playerIndex, 0.5f, 1.0f);
            //}
            //else
            //{
            //    GamePad.SetVibration(thePlayer1.playerIndex, 0.0f, 0.0f);
            //    GamePad.SetVibration(thePlayer2.playerIndex, 0.0f, 0.0f);
            //}
        }

        protected void CheckForCollisions(GameTime gameTime)
        {
            // collisions with pigs
            bool pigDied = false;
            foreach (var pig in pigs)
            {
                if (!pig.IsDead && pig.CanDie)
                {
                    foreach (var egg in eggs)
                    {
                        if (egg.DestinationRect.Intersects(pig.DestinationRect))
                        {
                            egg.IsDead = true;
                            pig.IsDead = true;
                            pigDied = true;
                            birds[egg.PlayerIndex].PlayerScore += pig.ScoreValue;
                        }
                    }

                    foreach (var seed in seeds)
                    {
                        if (seed.DestinationRect.Intersects(pig.DestinationRect))
                        {
                            seed.IsDead = true;
                            pig.IsDead = true;
                            pigDied = true;
                            birds[seed.PlayerIndex].PlayerScore += pig.ScoreValue;
                        }
                    }
                }
            }

            // collisions with players
            bool birdDied = false;
            if (birds.Count == 0)
            {
                birds.Add(thePlayer1.playerIndex, thePlayer1);
                birds.Add(thePlayer2.playerIndex, thePlayer2);
                birds.Add(thePlayer3.playerIndex, thePlayer3);
                birds.Add(thePlayer4.playerIndex, thePlayer4);
            }

            foreach (var player in birds.Values)
            {
                foreach (var pig in pigs)
                {
                    if (pig.CanDie)
                    {
                        if (player.DestinationRect.Intersects(pig.DestinationRect))
                        {
                            pig.IsDead = true;
                            pigDied = true;
                            player.PlayerScore /= 2;
                            // TODO: make transparent for a bit.
                            player.DestinationRect.X =
                                player.DestinationRect.Y = 10;
                            birdDied = true;
                        }
                    }
                }
            }

            if (pigDied) { effectSqueal.Play(); }
            if (birdDied) { effectSquawk.Play(); }

        }

        private Dictionary<PlayerIndex,Player> birds = 
            new Dictionary<PlayerIndex,Player>();

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Crimson);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // --- NEW CODE ---
            //Rectangle rect = rectClouds;
            //rect.X = locCloudX;
            //while (rect.X <= bounds.Width)
            //{
            //    spriteBatch.Draw(
            //        texClouds,
            //        rect,
            //        rectClouds,
            //        Color.White);
            //    rect.X += rectClouds.Width;
            //}

            Rectangle rect = beSky.DestinationRect;
            while (rect.X < bounds.Width)
            {
                spriteBatch.Draw(
                    texClouds,
                    rect,
                    beSky.SourceRect,
                    beSky.Tint);
                rect.X += beSky.DestinationRect.Width;
            }

            rect = beHillBack.DestinationRect;
            while (rect.X < bounds.Width)
            {
                spriteBatch.Draw(
                    texParallax,
                    rect,
                    beHillBack.SourceRect,
                    beHillBack.Tint);
                rect.X += beHillBack.DestinationRect.Width;
            }

            rect = beHillMid.DestinationRect;
            while (rect.X < bounds.Width)
            {
                spriteBatch.Draw(
                    texParallax,
                    rect,
                    beHillMid.SourceRect,
                    beHillMid.Tint);
                rect.X += beHillMid.DestinationRect.Width;
            }

            rect = beHillFront.DestinationRect;
            while (rect.X < bounds.Width)
            {
                spriteBatch.Draw(
                    texParallax,
                    rect,
                    beHillFront.SourceRect,
                    beHillFront.Tint);
                rect.X += beHillFront.DestinationRect.Width;
            }

            foreach (Egg egg in eggs)
            {
                spriteBatch.Draw(
                    texBirds,
                    egg.DestinationRect,
                    egg.SourceRect,
                    egg.Tint);
            }

            // ----------------

            // --- MOVED SEEDS BEFORE PLAYERS
            foreach (Seed seed in seeds)
            {
                spriteBatch.Draw(
                    texBirds,
                    seed.DestinationRect,
                    seed.SourceRect,
                    seed.Tint);
            }

            Vector2 loc = Vector2.Zero;
            loc.X = 10;
            Rectangle rectBird = new Rectangle(0, 0, 30, 30);
            foreach (var player in birds.Values)
            {
                rectBird.X = (int)loc.X;
                spriteBatch.Draw(
                    player.playerIndex == PlayerIndex.One ? texBirds : texSprites, 
                    rectBird, 
                    player.SourceRect, 
                    player.Tint);

                loc.X += 42;
                loc.Y += 2;
                spriteBatch.DrawString(
                    fontScore,
                    player.PlayerScore.ToString(),
                    loc,
                    Color.Black);

                loc.X -= 2;
                loc.Y -= 2;
                spriteBatch.DrawString(
                    fontScore,
                    player.PlayerScore.ToString(),
                    loc,
                    Color.White);

                loc.X += bounds.Width / 4 - 75;
            }
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            foreach (Pig pig in pigs)
            {
                //spriteBatch.Draw(
                //    texSprites,
                //    pig.DestinationRect,
                //    pig.SourceRect,
                //    pig.Tint);

                spriteBatch.Draw(
                    texSprites,
                    pig.DestinationRect,
                    pig.SourceRect,
                    pig.Tint,
                    0.0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    pig.LayerDepth);
            }

            //spriteBatch.Draw(
            //    texBirds,
            //    thePlayer1.DestinationRect,
            //    thePlayer1.SourceRect,
            //    thePlayer1.Tint);

            spriteBatch.Draw(
                texBirds,
                thePlayer1.DestinationRect,
                thePlayer1.SourceRect,
                thePlayer1.Tint,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                Pig.DEFAULT_LAYER_DEPTH);

            spriteBatch.Draw(
                texSprites,
                thePlayer2.DestinationRect,
                thePlayer2.SourceRect,
                thePlayer2.Tint,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                Pig.DEFAULT_LAYER_DEPTH);

            spriteBatch.Draw(
                texSprites,
                thePlayer3.DestinationRect,
                thePlayer3.SourceRect,
                thePlayer3.Tint,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                Pig.DEFAULT_LAYER_DEPTH);

            spriteBatch.Draw(
                texSprites,
                thePlayer4.DestinationRect,
                thePlayer4.SourceRect,
                thePlayer4.Tint,
                0.0f,
                Vector2.Zero,
                SpriteEffects.None,
                Pig.DEFAULT_LAYER_DEPTH);
            
            //spriteBatch.Draw(
            //    texSprites,
            //    thePlayer2.DestinationRect,
            //    thePlayer2.SourceRect,
            //    thePlayer2.Tint);

            //spriteBatch.Draw(
            //    texSprites,
            //    thePlayer3.DestinationRect,
            //    thePlayer3.SourceRect,
            //    thePlayer3.Tint);

            //spriteBatch.Draw(
            //    texSprites,
            //    thePlayer4.DestinationRect,
            //    thePlayer4.SourceRect,
            //    thePlayer4.Tint);

            spriteBatch.End();
        }
    }
}
