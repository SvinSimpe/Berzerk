using System;
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
        GraphicsDevice graphics;
        Camera2D camera;
        Vector2 cameraPosition;

        AnimatedTexture batter;
        float rotation = 0.0f;
        float scale = 1.0f;
        float depth = 0.5f;
        Vector2 batterPos;
        int frames = 2;
        int framesPerSec = 3;

        Rectangle groundRect = new Rectangle(0, 712, 1280, 7);  /// Checks intersection with projectile

        ScrollingBackground back1;
        ScrollingBackground back2;
        ScrollingBackground back3;
        ScrollingBackground back4;
        ScrollingBackground plateau;
        int velocity;

        //========= PROJECTILE TEST =============
        Projectile m_projectile;
        int m_groundHitCounter = 0;
        //========= PROJECTILE TEST =============

        //========= WASP TEST =============
        Wasp m_wasp;
        //========= WASP TEST =============

        //========= SLIME TEST =============
        Slime m_slime;
        //========= SLIME TEST =============

        //========= MINE TEST =============
        Mine m_mine;
        //========= MINE TEST =============

        //========= POWER UP TEST =============
        PowerUp m_powerUp;
        //========= POWER UP TEST =============

        //========= ANGLE GAUGE TEST =============
        AngleGauge m_angleGauge;
        //========= ANGLE GAUGE TEST =============

        //========= POWER GAUGE TEST =============
        PowerGauge m_powerGauge;
        //========= POWER GAUGE TEST =============


        public TestGame(GraphicsDevice graphics)
        {
            // Sound init
            SoundEngine.Initialize();

            this.graphics = graphics;
            batter = new AnimatedTexture(Vector2.Zero, rotation, scale, depth);

            camera = new Camera2D(graphics.Viewport);
            cameraPosition = new Vector2(graphics.Viewport.Width / 2, 0);
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

            //========= PROJECTILE TEST =============
            Vector2 projectilePosition = new Vector2(graphics.Viewport.Width / 2, 512);
            m_projectile = new Projectile(projectilePosition, content);
            //========= PROJECTILE TEST =============

            //========= PROJECTILE TEST =============
            m_wasp = new Wasp( new Vector2(700, 400), content );
            //========= PROJECTILE TEST =============

            //========= SLIME TEST =============
            m_slime = new Slime(new Vector2(1000, 650), content);
            //========= SLIME TEST =============

            //========= MINE TEST =============
            m_mine = new Mine(new Vector2(700, 680), content);
            //========= MINE TEST =============


            //========= MINE TEST =============
            m_powerUp = new PowerUp(new Vector2(900, 350), content);
            //========= MINE TEST =============

            //========= LOAD SOUNDS =============
           // SoundEngine.AddSoundEffect(content.Load<SoundEffect>("bee"), "bee", 0.1f);
            //========= LOAD SOUNDS =============

            //========= ANGLE GAUGE =============
            m_angleGauge = new AngleGauge( content );
            //========= ANGLE GAUGE =============

            //========= POWER GAUGE =============
            m_powerGauge = new PowerGauge( content );
            //========= POWER GAUGE =============

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
                cameraPosition.X = graphics.Viewport.Width / 2;
                cameraPosition.Y = 0;
            }

            if (back1.rectangle.X + back1.texture.Width <= 0)
                back1.rectangle.X = back4.rectangle.X + back4.texture.Width;
            if (back2.rectangle.X + back2.texture.Width <= 0)
                back2.rectangle.X = back1.rectangle.X + back3.texture.Width;
            if (back3.rectangle.X + back3.texture.Width <= 0)
                back3.rectangle.X = back2.rectangle.X + back2.texture.Width;
            if (back4.rectangle.X + back4.texture.Width <= 0)
                back4.rectangle.X = back3.rectangle.X + back3.texture.Width;

            if (m_projectile.Flying)
            {
                back1.Update((int)m_projectile.XVelocity);
                back2.Update((int)m_projectile.XVelocity);
                back3.Update((int)m_projectile.XVelocity);
                back4.Update((int)m_projectile.XVelocity);
                if (plateau.rectangle.X + plateau.texture.Width > 0)
                    plateau.Update((int)m_projectile.XVelocity);
            }

            //back1.Update(velocity);
            //back2.Update(velocity);
            //back3.Update(velocity);
            //back4.Update(velocity);
            //if( plateau.rectangle.X + plateau.texture.Width > 0 )
            //    plateau.Update(velocity);
            /////////////////////////////////////////////////////////////////

            ///////////////////////////// CAMERA ////////////////////////////
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                cameraPosition.Y--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                cameraPosition.Y++;
            }

            if (m_projectile.Position.Y >= 0)
            {
                camera.Update(gameTime, 0);
            }
            else
                camera.Update(gameTime, m_projectile.Position.Y);
            /////////////////////////////////////////////////////////////////

            ///////////////////////////   PROJECTILE   ///////////////////////////
            m_projectile.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                m_projectile.Flying = true;
            }

            if (m_projectile.Flying)
            {
                //Check collision with ground
                if (groundRect.Intersects(m_projectile.BoundingBox))
                {
                    m_groundHitCounter++;
                    m_projectile.ApplyForce1();
                }
            }

            ///////////////////////////   WASP   ///////////////////////////
            m_wasp.Update( gameTime );

            //Check collision with wasp
            if (m_projectile.BoundingBox.Intersects(m_wasp.BoundingBox))
            {
                m_projectile.ApplyForce2();
               // SoundEngine.PlaySoundEffect("bee");
            }

            ///////////////////////////   SLIME   ///////////////////////////
            m_slime.Update( gameTime );

            //Check collision with slime
            if (m_projectile.BoundingBox.Intersects(m_slime.BoundingBox))
                m_projectile.ApplyForce3();

            ///////////////////////////   MINE   ///////////////////////////
            m_mine.Update(gameTime);

            //Check collision with mine
            if (m_projectile.BoundingBox.Intersects(m_mine.BoundingBox))
                m_projectile.ApplyForce4();

            ///////////////////////////   POWER UP   ///////////////////////////
            m_powerUp.Update(gameTime);

            //Check collision with mine
            if (m_projectile.BoundingBox.Intersects(m_powerUp.BoundingBox))
            {
                //Player receivs XP-reward from Power Up
                //...
                m_powerUp.IsTaken = true;
            }

            ///////////////////////////   ANGLE GAUGE  ///////////////////////////
            m_angleGauge.Update( gameTime );

            ///////////////////////////   ANGLE GAUGE  ///////////////////////////
            m_powerGauge.Update( gameTime );

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transforms);
            
            // BACKGROUND
            back1.Draw(spriteBatch);
            back2.Draw(spriteBatch);
            back3.Draw(spriteBatch);
            back4.Draw(spriteBatch);
            plateau.Draw(spriteBatch);

            // BATTER
            batter.DrawFrame(spriteBatch, batterPos);

            // PROJECTILE
            m_projectile.Draw(spriteBatch);

            // WASP
            m_wasp.Draw( spriteBatch );

            // SLIME
            m_slime.Draw( spriteBatch );

            // SLIME
            m_mine.Draw(spriteBatch);

            // POWER UP
            m_powerUp.Draw(spriteBatch);

            // ANGLE GAUGE
            m_angleGauge.Draw( spriteBatch );

            // POWER GAUGE
            m_powerGauge.Draw(spriteBatch);

            spriteBatch.End();

        }
    }
}
