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
        // Ground
        private ScrollingBackground m_back1;
        private ScrollingBackground m_back2;
        private ScrollingBackground m_back3;
        private ScrollingBackground m_back4;
        private ScrollingBackground m_back5;
        private ScrollingBackground m_back6;
        private ScrollingBackground m_back7;
        private ScrollingBackground m_back8;
        // Sky
        private ScrollingBackground m_sky1;
        private ScrollingBackground m_sky2;
        private ScrollingBackground m_sky3;
        private ScrollingBackground m_sky4;
        private ScrollingBackground m_sky5;
        private ScrollingBackground m_sky6;
        private ScrollingBackground m_sky7;
        private ScrollingBackground m_sky8;
        private ScrollingBackground m_sky9;
        // Space
        private ScrollingBackground m_space1;

        private ScrollingBackground m_plateau;

        // Batter
        AnimatedTexture     m_batter;
        Vector2             m_batterPos;

        // Projectile
        Projectile m_projectile;
 
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
            // Ground
            m_back1 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back1"), new Rectangle(0, -1328, 640, 2048));
            m_back2 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back2"), new Rectangle(640, -1328, 640, 2048));
            m_back3 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back3"), new Rectangle(1280, -1328, 640, 2048));
            m_back4 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back4"), new Rectangle(1920, -1328, 640, 2048));
            m_back5 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back5"), new Rectangle(2560, -1328, 640, 2048));
            m_back6 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back6"), new Rectangle(3200, -1328, 640, 2048));
            m_back7 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back7"), new Rectangle(3840, -1328, 640, 2048));
            m_back8 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/back8"), new Rectangle(4480, -1328, 640, 2048));
            // Sky1
            m_sky1 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/sky"), new Rectangle(0, -3376, 1280, 2048));
            m_sky2 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/sky"), new Rectangle(0, -5424, 1280, 2048));
            m_sky3 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/sky"), new Rectangle(0, -7472, 1280, 2048));
            m_sky4 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/sky"), new Rectangle(0, -9520, 1280, 2048));
            m_sky5 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/sky"), new Rectangle(0, -11560, 1280, 2048));
            m_sky6 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/sky"), new Rectangle(0, -13606, 1280, 2048));
            m_sky7 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/sky"), new Rectangle(0, -15654, 1280, 2048));
            m_sky8 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/sky"), new Rectangle(0, -17702, 1280, 2048));
            m_sky9 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/sky"), new Rectangle(0, -19750, 1280, 2048));
            // Space
            m_space1 = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/space"), new Rectangle(0, -21798, 1280, 2048));

            m_plateau = new ScrollingBackground(content.Load<Texture2D>("Graphics/Backgrounds/Plateau"), new Rectangle(0, 360, 360, 360));



            // Batter
            m_batter.Load(content, "Graphics/BatterIdle", 2, 3);

            // StaticTextures
            mineText  = content.Load<Texture2D>("Graphics/mine");
            slimeText = content.Load<Texture2D>("Graphics/slime");
            cloudText = content.Load<Texture2D>("Graphics/cloud");
            waspText  = content.Load<Texture2D>("Graphics/wasp");
            powerText = content.Load<Texture2D>("Graphics/powerUp");

            boom = new StaticTexture(content.Load<Texture2D>("Graphics/boom"), new Vector2(m_graphics.Viewport.Width + 100, 625 - 120));

            rand = new Random();
            textures = new StaticTexture[NUM_TEXTURES];
            
            textures[0] = new Mine( new Vector2(m_graphics.Viewport.Width  + mineText.Width, 625 - mineText.Height), content);
            textures[1] = new Mine(new Vector2(m_graphics.Viewport.Width + mineText.Width, 625 - mineText.Height), content);
            textures[2] = new Cloud( new Vector2(m_graphics.Viewport.Width + cloudText.Width, cloudText.Height + 10), content);
            textures[3] = new Wasp( new Vector2(m_graphics.Viewport.Width  + waspText.Width, 300), content);
            textures[4] = new Mine(new Vector2(m_graphics.Viewport.Width + mineText.Width, 625 - mineText.Height), content);
            textures[5] = new Slime(new Vector2(m_graphics.Viewport.Width + slimeText.Width, 625 - (mineText.Height * 2)), content);
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

            textures[20] = new Mine(new Vector2(m_graphics.Viewport.Width + mineText.Width, 625 - mineText.Height), content);
            textures[21] = new Cloud(new Vector2(m_graphics.Viewport.Width + cloudText.Width, cloudText.Height + 10), content);
            textures[22] = new Cloud(new Vector2(m_graphics.Viewport.Width + cloudText.Width, cloudText.Height + 10), content);
            textures[23] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[24] = new Mine(new Vector2(m_graphics.Viewport.Width + mineText.Width, 625 - mineText.Height), content);
            textures[25] = new Slime(new Vector2(m_graphics.Viewport.Width + slimeText.Width, 625 - (mineText.Height * 2)), content);
            textures[26] = new Cloud(new Vector2(m_graphics.Viewport.Width + cloudText.Width, cloudText.Height + 10), content);
            textures[27] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[28] = new Wasp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
            textures[29] = new PowerUp(new Vector2(m_graphics.Viewport.Width + waspText.Width, 300), content);
        }

        private void UpdateBackgrounds()
        {
            // Backgrounds
            // Ground
            if (m_back1.rectangle.X + m_back1.texture.Width <= 0)
                m_back1.rectangle.X = m_back8.rectangle.X + m_back8.texture.Width;
            if (m_back2.rectangle.X + m_back2.texture.Width <= 0)
                m_back2.rectangle.X = m_back1.rectangle.X + m_back3.texture.Width;
            if (m_back3.rectangle.X + m_back3.texture.Width <= 0)
                m_back3.rectangle.X = m_back2.rectangle.X + m_back2.texture.Width;
            if (m_back4.rectangle.X + m_back4.texture.Width <= 0)
                m_back4.rectangle.X = m_back3.rectangle.X + m_back3.texture.Width;
           
            if (m_back5.rectangle.X + m_back5.texture.Width <= 0)
                m_back5.rectangle.X = m_back4.rectangle.X + m_back4.texture.Width;
            if (m_back6.rectangle.X + m_back6.texture.Width <= 0)
                m_back6.rectangle.X = m_back5.rectangle.X + m_back5.texture.Width;
            if (m_back7.rectangle.X + m_back7.texture.Width <= 0)
                m_back7.rectangle.X = m_back6.rectangle.X + m_back6.texture.Width;
            if (m_back8.rectangle.X + m_back8.texture.Width <= 0)
                m_back8.rectangle.X = m_back7.rectangle.X + m_back7.texture.Width;
        }

        public void Update(GameTime gameTime, Projectile projectile)
        {
            m_projectile = projectile;
            // Backgrounds
            UpdateBackgrounds();

            if (projectile.Flying)
            {
                m_back1.Update((int)projectile.XVelocity);
                m_back2.Update((int)projectile.XVelocity);
                m_back3.Update((int)projectile.XVelocity);
                m_back4.Update((int)projectile.XVelocity);
                m_back5.Update((int)projectile.XVelocity);
                m_back6.Update((int)projectile.XVelocity);
                m_back7.Update((int)projectile.XVelocity);
                m_back8.Update((int)projectile.XVelocity);
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
            if (projectile.Flying)
            {
                CheckTexturePositions();

                if (projectile.XVelocity > 0)
                {
                    if (time >= 1)
                    {
                        SpawnTexture();
                        time = 0;
                    }

                    for (int i = 0; i < NUM_TEXTURES; i++)
                    {
                        if (boom.m_isActive)
                            boom.Update(gameTime, (int)projectile.XVelocity);

                        if (textures[i].GetType() == typeof(Wasp))
                            ((Wasp)textures[i]).Update(gameTime, (int)projectile.XVelocity);
                        else
                            textures[i].Update(gameTime, (int)projectile.XVelocity);
                    }

                    if (boom.m_position.X + boom.m_texture.Width <= 0)
                    {
                        boom.m_isActive = false;
                    }
                }
                /////////////////////////////////////////////////////

                ApplyPhysics(projectile);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Backgrounds
            // Ground
            m_back1.Draw(spriteBatch);
            m_back2.Draw(spriteBatch);
            m_back3.Draw(spriteBatch);
            m_back4.Draw(spriteBatch);
            m_back5.Draw(spriteBatch);
            m_back6.Draw(spriteBatch);
            m_back7.Draw(spriteBatch);
            m_back8.Draw(spriteBatch);
            // Sky
            m_sky1.Draw(spriteBatch);
            m_sky2.Draw(spriteBatch);
            m_sky3.Draw(spriteBatch);
            m_sky4.Draw(spriteBatch);
            m_sky5.Draw(spriteBatch);
            m_sky6.Draw(spriteBatch);
            m_sky7.Draw(spriteBatch);
            m_sky8.Draw(spriteBatch);
            m_sky9.Draw(spriteBatch);
            // Space
            m_space1.Draw(spriteBatch);

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
            m_back5.Reset();
            m_back6.Reset();
            m_back7.Reset();
            m_back8.Reset();
            m_plateau.Reset();
            m_batterPos.X = 100;

            // Batter
            m_batter.Load(m_content, "Graphics/BatterIdle", 2, 3);

            for (int i = 0; i < NUM_TEXTURES; i++)
                textures[i].Reset();
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

        // SPAWNER
        private void SpawnTexture()
        {
            bool valid = false;
            int num = 0;
            while (!valid)
            {
                num = rand.Next(0, 29);
                if (textures[num].m_isActive == false)
                {
                    if (textures[num].GetType() == typeof(PowerUp))
                    {
                        if (m_projectile.m_position.Y < -17712)
                        {
                            valid = true;
                        }
                    }
                    else
                        valid = true;
                }
            }
  
            if (textures[num].GetType() == typeof(Mine))
            {
                textures[num].m_isActive = true;
            }
            if (textures[num].GetType() == typeof(Slime))
            {
                textures[num].m_isActive = true;
                textures[num].IsHit = false;
            }
            if (textures[num].GetType() == typeof(Cloud))       // CLOUD
            {
                int offset;
                int spawnPos;
                if (m_projectile.m_position.Y > 500)
                    spawnPos = rand.Next(0, 100);
                else
                {
                    offset = rand.Next(-360, 360);
                    spawnPos = (int)m_projectile.m_position.Y + offset;
                    if (spawnPos > 100)
                        spawnPos = 100;
                }

                textures[num].m_isActive = true;
                textures[num].m_position.Y = spawnPos;
            }
            if (textures[num].GetType() == typeof(Wasp))
            {
                int offset = rand.Next(-360, 360);
                int spawnPos = (int)m_projectile.m_position.Y + offset;
                if (spawnPos > 368)
                {
                    offset = rand.Next(-70, 10);
                    spawnPos = 368 + offset;
                }

                textures[num].m_isActive = true;
                textures[num].m_position.Y = spawnPos;
                textures[num].IsHit = false;
            }
            if (textures[num].GetType() == typeof(PowerUp))
            {
                int offset = rand.Next(-500, 500);
                int spawnPos = (int)m_projectile.m_position.Y + offset;
                textures[num].m_isActive = true;
                textures[num].m_position.Y = spawnPos;
                textures[num].IsHit = false;
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
                        if (textures[i].GetType() == typeof(Wasp) && !textures[i].IsHit)
                        {
                            projectile.ApplyWaspForce();
                            textures[i].IsHit = true;
                        }
                        // Slime
                        if (textures[i].GetType() == typeof(Slime) && !textures[i].IsHit )
                        {
                            projectile.ApplySlimeForce();
                            textures[i].IsHit = true;
                        }
                        // Mine
                        if (textures[i].GetType() == typeof(Mine))
                        {
                            projectile.ApplyMineForce();
                            boom.m_isActive = true;
                            boom.m_position.X = textures[i].m_position.X;
                        }
                        // Powerup
                        if (textures[i].GetType() == typeof(PowerUp) && !((PowerUp)textures[i]).IsTaken )
                        {
                            ( (PowerUp)textures[i] ).IsTaken = true;
                            Player.NrOfPowerUps++;                        
                        }
                    }
                }
            }
        }

    }
}
