using GameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berzerk
{
    public class Level
    {
        ContentManager     m_content;
        GraphicsDevice     m_graphics;

        Rectangle groundRect;

        // Backgrounds
        private ScrollingBackground m_back1;
        private ScrollingBackground m_back2;
        private ScrollingBackground m_back3;
        private ScrollingBackground m_back4;
        private ScrollingBackground m_plateau;

        // Batter
        AnimatedTexture     m_batter;
        Vector2             m_batterPos;

        // Projectile
        //Projectile m_projectile;
 
        // StaticTextures
        Texture2D mineText;
        Texture2D slimeText;
        Texture2D cloudText;
        Texture2D waspText;
        Texture2D powerText;
        StaticTexture boom;
        
        //Timer
        float time = 0;

        Random rand;
        Cloud m_cloud;
        Vector2 m_cloudPos;
        StaticTexture[] textures;
        const int NUM_TEXTURES = 30;


        // Properties
        public AnimatedTexture Batter
        {
            get { return m_batter; }
            set { m_batter = value; }
        }


        public Level(GraphicsDevice graphics)
        {
            m_graphics = graphics;

            // Batter
            m_batter = new AnimatedTexture(Vector2.Zero, 0.0f, 1.0f, 0.5f);
            m_batterPos = new Vector2(100, 240);

            groundRect = new Rectangle(0, 712, 1280, 7);  /// Checks intersection with projectile
        }

        public void LoadContent(ContentManager content)
        {
            m_content = content;
            // Load background pictures
            m_back1 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back1"), new Rectangle(0, -720, 640, 1440));
            m_back2 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back2"), new Rectangle(640, -720, 640, 1440));
            m_back3 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back3"), new Rectangle(1280, -720, 640, 1440));
            m_back4 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back4"), new Rectangle(1920, -720, 640, 1440));
            m_plateau = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/Plateau"), new Rectangle(0, 360, 360, 360));

            // Batter
            m_batter.Load(content, "Graphics/BatterIdle", 2, 3);

            // StaticTextures
            mineText  = content.Load<Texture2D>("Graphics/mine");
            slimeText = content.Load<Texture2D>("Graphics/slime");
            cloudText = content.Load<Texture2D>("Graphics/cloud");
            waspText  = content.Load<Texture2D>("Graphics/wasp");
            powerText = content.Load<Texture2D>("Graphics/powerUp");

            boom = new StaticTexture(content.Load<Texture2D>("Graphics/boom"), new Vector2(m_graphics.Viewport.Width + 100, m_graphics.Viewport.Height - 120));

            rand = new Random();
            textures = new StaticTexture[NUM_TEXTURES];
            
            textures[0] = new Mine( new Vector2(m_graphics.Viewport.Width  + mineText.Width, m_graphics.Viewport.Height - mineText.Height), content);
            textures[1] = new Cloud( new Vector2(m_graphics.Viewport.Width + cloudText.Width, cloudText.Height + 10), content);
            textures[2] = new Cloud( new Vector2(m_graphics.Viewport.Width + cloudText.Width, cloudText.Height + 10), content);
            textures[3] = new Wasp( new Vector2(m_graphics.Viewport.Width  + waspText.Width, 300), content);
            textures[4] = new Mine( new Vector2(m_graphics.Viewport.Width  + mineText.Width, m_graphics.Viewport.Height - mineText.Height), content);
            textures[5] = new Slime( new Vector2(m_graphics.Viewport.Width + slimeText.Width, m_graphics.Viewport.Height - (mineText.Height * 2)), content);
            textures[6] = new Cloud( new Vector2(m_graphics.Viewport.Width + cloudText.Width, cloudText.Height + 10), content);
            textures[7] = new Wasp( new Vector2(m_graphics.Viewport.Width  + waspText.Width, 300), content);
            textures[8] = new Wasp( new Vector2(m_graphics.Viewport.Width  + waspText.Width, 300), content);
            textures[9] = new PowerUp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);

            textures[10] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[11] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[12] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[13] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[14] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[15] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[16] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[17] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[18] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[19] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);

            textures[20] = new Mine(new Vector2(m_graphics.Viewport.Width + mineText.Width, m_graphics.Viewport.Height - mineText.Height), content);
            textures[21] = new Cloud(new Vector2(m_graphics.Viewport.Width + cloudText.Width, cloudText.Height + 10), content);
            textures[22] = new Cloud(new Vector2(m_graphics.Viewport.Width + cloudText.Width, cloudText.Height + 10), content);
            textures[23] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[24] = new Mine(new Vector2(m_graphics.Viewport.Width + mineText.Width, m_graphics.Viewport.Height - mineText.Height), content);
            textures[25] = new Slime(new Vector2(m_graphics.Viewport.Width + slimeText.Width, m_graphics.Viewport.Height - (mineText.Height * 2)), content);
            textures[26] = new Cloud(new Vector2(m_graphics.Viewport.Width + cloudText.Width, cloudText.Height + 10), content);
            textures[27] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[28] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[29] = new PowerUp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
        }

        public void Update(GameTime gameTime, Projectile projectile)
        {
            // Backgrounds
            if (m_back1.rectangle.X + m_back1.texture.Width <= 0)
                m_back1.rectangle.X = m_back4.rectangle.X + m_back4.texture.Width;
            if (m_back2.rectangle.X + m_back2.texture.Width <= 0)
                m_back2.rectangle.X = m_back1.rectangle.X + m_back3.texture.Width;
            if (m_back3.rectangle.X + m_back3.texture.Width <= 0)
                m_back3.rectangle.X = m_back2.rectangle.X + m_back2.texture.Width;
            if (m_back4.rectangle.X + m_back4.texture.Width <= 0)
                m_back4.rectangle.X = m_back3.rectangle.X + m_back3.texture.Width;

            if (projectile.Flying)
            {
                m_back1.Update((int)projectile.XVelocity);
                m_back2.Update((int)projectile.XVelocity);
                m_back3.Update((int)projectile.XVelocity);
                m_back4.Update((int)projectile.XVelocity);
                if (m_plateau.rectangle.X + m_plateau.texture.Width > 0)
                {
                    m_plateau.Update((int)projectile.XVelocity);
                    m_batterPos.X -= projectile.XVelocity;
                }
            }

            // Batter
            m_batter.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Textures
            if( projectile.Flying)
            {
                CheckTexturePositions();

                // Fixa att textures åker när allt är stilla, när simon fixat mätaren
                if (projectile.XVelocity > 0)
                {
                    if (time >= 1)
                    {
                        SpawnTexture();
                        time = 0;
                    }

                    for (int i = 0; i < NUM_TEXTURES; i++)
                        textures[i].Update(gameTime, (int)projectile.XVelocity);

                    if (boom.m_isActive)
                        boom.Update(gameTime, (int)projectile.XVelocity);
                }

                if (boom.m_position.X + boom.m_texture.Width <= 0)
                {
                    boom.m_isActive = false;
                }
            }
            /////////////////////////////////////////////////////

            ApplyPhysics(projectile);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Backgrounds
            m_back1.Draw(spriteBatch);
            m_back2.Draw(spriteBatch);
            m_back3.Draw(spriteBatch);
            m_back4.Draw(spriteBatch);
            m_plateau.Draw(spriteBatch);

            // Batter
            m_batter.DrawFrame(spriteBatch, m_batterPos);

            // Textures
            for (int i = 0; i < NUM_TEXTURES; i++)
                textures[i].Draw(spriteBatch);

            if (boom.m_isActive)
                boom.Draw(spriteBatch);
        }

        public void ResetLevel()
        {
            // Backgrounds
            m_back1.Reset();
            m_back2.Reset();
            m_back3.Reset();
            m_back4.Reset();
            m_plateau.Reset();
            m_batterPos.X = 100;

            // Batter
            m_batter.Load(m_content, "Graphics/BatterIdle", 2, 3);
        }

        public void BatterCharge()
        {
            m_batter.IsPaused = true;
            m_batter.Load(m_content, "Graphics/BatterHit", 2, 3);
        }

        public void BatterHit()
        {
            m_batter.IsPaused = false;
            m_batter.Load(m_content, "Graphics/BatterHit", 2, 3);
        }

        private void SpawnTexture()
        {
            bool valid = false;
            int num = 0;
            int randPos= 0;
            while (!valid)
            {
                num = rand.Next(0, 29);
                if (textures[num].m_isActive == false)
                    valid = true;
            }
  
            if (textures[num].GetType() == typeof(Mine))
            {
                textures[num].m_isActive = true;
            }
            if (textures[num].GetType() == typeof(Slime))
            {
                textures[num].m_isActive = true;
            }
            if (textures[num].GetType() == typeof(Cloud))
            {
                textures[num].m_isActive = true;
                randPos = rand.Next( 0, 400 );
                textures[num].m_position.Y = randPos;
            }
            if (textures[num].GetType() == typeof(Wasp))
            {
                textures[num].m_isActive = true;
                randPos = rand.Next(0, 600);
                textures[num].m_position.Y = randPos;
            }
            if (textures[num].GetType() == typeof(PowerUp))
            {
                textures[num].m_isActive = true;
                randPos = rand.Next(0, 600);
                textures[num].m_position.Y = randPos;
            }
        }

        private void CheckTexturePositions()
        {
            for( int i = 0; i < NUM_TEXTURES; i++)
            {
                if (textures[i].m_position.X + textures[i].m_texture.Width <= 0)
                {
                    textures[i].m_isActive = false;
                    textures[i].m_position.X = m_graphics.Viewport.Width;
                }
            }
        }

        private void ApplyPhysics(Projectile projectile)
        {
            if (projectile.Flying)
            {
                for (int i = 0; i < NUM_TEXTURES; i++)
                {
                    if (projectile.BoundingBox.Intersects(textures[i].BoundingBox))
                    {
                        // Wasp
                        if (textures[i].GetType() == typeof(Wasp))
                            projectile.ApplyWaspForce();
                        // Slime
                        if (textures[i].GetType() == typeof(Slime))
                            projectile.ApplySlimeForce();
                        // Mine
                        if (textures[i].GetType() == typeof(Mine))
                        {
                            projectile.ApplyMineForce();
                            boom.m_isActive = true;
                            boom.m_position.X = textures[i].m_position.X;
                        }
                        // Powerup
                        if (textures[i].GetType() == typeof(PowerUp))
                        {
                            // isTaken = true;
                        }
                    }
                }
            }
        }

    }
}
