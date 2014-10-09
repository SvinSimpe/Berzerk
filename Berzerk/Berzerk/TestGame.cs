﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameLib;

namespace Berzerk
{
    public class TestGame
    {
        ContentManager content;

        AnimatedTexture batter;
        float rotation = 0.0f;
        float scale = 1.0f;
        float depth = 0.5f;
        Vector2 batterPos;
        int frames = 2;
        int framesPerSec = 3;

        ScrollingBackground back1;
        ScrollingBackground back2;
        ScrollingBackground back3;
        ScrollingBackground back4;
        ScrollingBackground plateau;
        int velocity;

        public TestGame()
        {
            batter = new AnimatedTexture(Vector2.Zero, rotation, scale, depth);
        }

        public void LoadContent(ContentManager content)
        {
            batter.Load(content, "Graphics/BatterIdle", frames, framesPerSec);
            batterPos = new Vector2(100, 240);

            back1 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back1"), new Rectangle(0, 0, 640, 720));
            back2 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back2"), new Rectangle(640, 0, 640, 720));
            back3 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back3"), new Rectangle(1280, 0, 640, 720));
            back4 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back4"), new Rectangle(1920, 0, 640, 720));
            plateau = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/Plateau"), new Rectangle(0, 360, 360, 360));
            velocity = 0;

            this.content = content;
        }

        public void Update(GameTime gameTime)
        {

            ///////////////////////////   BATTER   ///////////////////////////
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                if (batter.IsPaused)
                    batter.Play();
                else
                    batter.Pause();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                batter.Load(content, "Graphics/BatterHit", frames, 1);
                batter.IsPaused = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                batter.IsPaused = false;
                
                velocity = 50;
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            batter.UpdateFrame(elapsed);
            batterPos.X -= velocity;

            /////////////////////////// BACKGROUND ///////////////////////////
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velocity--;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velocity++;
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                back1.Reset();
                back2.Reset();
                back3.Reset();
                back4.Reset();
                plateau.Reset();
                batterPos.X = 100;
                velocity = 0;
            }

            if (back1.rectangle.X + back1.texture.Width <= 0)
                back1.rectangle.X = back4.rectangle.X + back4.texture.Width;
            if (back2.rectangle.X + back2.texture.Width <= 0)
                back2.rectangle.X = back1.rectangle.X + back3.texture.Width;
            if (back3.rectangle.X + back3.texture.Width <= 0)
                back3.rectangle.X = back2.rectangle.X + back2.texture.Width;
            if (back4.rectangle.X + back4.texture.Width <= 0)
                back4.rectangle.X = back3.rectangle.X + back3.texture.Width;

            back1.Update(velocity);
            back2.Update(velocity);
            back3.Update(velocity);
            back4.Update(velocity);
            if( plateau.rectangle.X + plateau.texture.Width > 0 )
                plateau.Update(velocity);
            /////////////////////////////////////////////////////////////////
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // BACKGROUND
            back1.Draw(spriteBatch);
            back2.Draw(spriteBatch);
            back3.Draw(spriteBatch);
            back4.Draw(spriteBatch);
            plateau.Draw(spriteBatch);

            // BATTER
            batter.DrawFrame(spriteBatch, batterPos);
        }
    }
}
