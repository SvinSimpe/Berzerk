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

        Level level;

        Rectangle groundRect = new Rectangle(0, 712, 1280, 7);  /// Checks intersection with projectile

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

        public TestGame(GraphicsDevice graphics)
        {
            // Sound init
            SoundEngine.Initialize();

            this.graphics = graphics;

            level = new Level(graphics);

            camera = new Camera2D(graphics.Viewport);
            cameraPosition = new Vector2(graphics.Viewport.Width / 2, 0);
        }

        public void LoadContent(ContentManager content)
        {
            level.LoadContent(content);

            this.content = content;

            //========= PROJECTILE TEST =============
            Vector2 projectilePosition = new Vector2(200, 250);
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

            //========= LOAD SOUNDS =============
            SoundEngine.AddSoundEffect(content.Load<SoundEffect>("bee"), "bee", 0.1f);
            //========= LOAD SOUNDS =============
        }

        public void Update(GameTime gameTime)
        {
            ///////////////////////////   LEVEL   ///////////////////////////
            level.Update(gameTime, m_projectile);

            ///////////////////////////   BATTER   ///////////////////////////
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                level.BatterCharge();
            }

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
                    m_projectile.ApplyGroundForce();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                level.ResetLevel();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transforms);
            
            // Level
            level.Draw(spriteBatch);

            // PROJECTILE
            m_projectile.Draw(spriteBatch);

            // WASP
            m_wasp.Draw( spriteBatch );

            // SLIME
            m_slime.Draw( spriteBatch );

            // SLIME
            m_mine.Draw(spriteBatch);

            spriteBatch.End();

        }
    }
}
