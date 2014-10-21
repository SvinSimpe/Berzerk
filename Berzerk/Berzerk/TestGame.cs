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

        GUI m_gui;
        bool isFinish = false;
        private float tempDistance;

        //FIRE CONTROL
        private bool m_isAngleChosen;
        private bool m_isPowerChosen;

        KeyboardState m_prevState;
        KeyboardState m_currentState;

        Level level;

        Rectangle groundRect = new Rectangle(0, 620, 1280, 5);  /// Checks intersection with projectile

        //========= PROJECTILE TEST =============
        Projectile m_projectile;
        int m_groundHitCounter = 0;
        //========= PROJECTILE TEST =============

        //========= ANGLE GAUGE TEST =============
        AngleGauge m_angleGauge;
        //========= ANGLE GAUGE TEST =============

        //========= POWER GAUGE TEST =============
        PowerGauge m_powerGauge;
        //========= POWER GAUGE TEST =============

        //========= POWER HIT TEST =============
        AnimatedTexture m_powerHit;
        Vector2 m_powerHitPos;
        bool drawPowerHit = false;
        int powerHitDrawTime = 2;
        float powerHitElapsed = 0;
        //========= POWER HIT TEST =============


        public TestGame(GraphicsDevice graphics)
        {
            // Sound init
            SoundEngine.Initialize();

            this.graphics = graphics;

            level = new Level(graphics);

            camera = new Camera2D(graphics.Viewport);
            cameraPosition = new Vector2(graphics.Viewport.Width / 2, 0);

            m_isAngleChosen = false;
            m_isPowerChosen = false;

            tempDistance = 0.0f;

            //========= POWER HIT TEST =============
            m_powerHit = new AnimatedTexture(Vector2.Zero, 0.0f, 1.0f, 0.5f);
            m_powerHitPos = new Vector2(0, 35);
            //========= POWER HIT TEST =============
        }

        public void LoadContent(ContentManager content)
        {
            level.LoadContent(content);

            this.content = content;

            //========= PROJECTILE TEST =============
            Vector2 projectilePosition = new Vector2(200, 250);
            m_projectile = new Projectile(projectilePosition, content);
            //========= PROJECTILE TEST =============

            //========= LOAD SOUNDS =============
            //SoundEngine.AddSoundEffect(content.Load<SoundEffect>("bee"), "bee", 0.1f);
            SoundEngine.AddSoundEffect(content.Load<SoundEffect>("coin"), "coin", 0.1f);
            //SoundEngine.AddSong(content.Load<Song>("theme"), "theme");
            //SoundEngine.PlaySong("theme");
            //========= LOAD SOUNDS =============

            //========= PLAYER =============
            Player.Initialize();
            //========= PLAYER =============

            //========= ANGLE GAUGE =============
            m_angleGauge = new AngleGauge( content );
            //========= ANGLE GAUGE =============

            //========= POWER GAUGE =============
            m_powerGauge = new PowerGauge( content );

            // Let NeedleSpeed depend on Player LVL
            if (Player.LVL > 1)
                m_powerGauge.NeedleSpeed *= (float)(Math.Pow( 0.95f, Player.LVL));
            //========= POWER GAUGE =============

            //========= GUI =============
            m_gui = new GUI( content ) ;
            m_gui.HighscoreString = "Highscore: " + '\n' + "  " + Player.Highscore.ToString();
            //========= GUI =============

            //========= POWER HIT TEST =============
            m_powerHit.Load(content, "Graphics/hit", 3, 3);
            //========= POWER HIT TEST =============
        }

        public void Update(GameTime gameTime)
        {
            m_currentState = Keyboard.GetState();

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

            ///////////////////////////   PROJECTILE   ///////////////////////////
            m_projectile.Update(gameTime);

            if (m_projectile.Flying)
            {
                //Check collision with ground
                if (groundRect.Intersects(m_projectile.BoundingBox) || m_projectile.BoundingBox.Bottom >= groundRect.Top )
                {
                    m_groundHitCounter++;
                    m_projectile.ApplyGroundForce();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                level.ResetLevel();
                ResetGame();
            }

            ///////////////////////////   ANGLE GAUGE  ///////////////////////////
            if (!m_isAngleChosen && !m_projectile.Flying)
                m_angleGauge.Update( gameTime );

            ///////////////////////////   POWER GAUGE  ///////////////////////////
            if( m_isAngleChosen && !m_isPowerChosen && !m_projectile.Flying )
                m_powerGauge.Update( gameTime );


            ///////////////////////////   FIRE CONTROL  ///////////////////////////
            if (m_currentState.IsKeyDown(Keys.Space) && m_prevState.IsKeyUp(Keys.Space) && m_isAngleChosen)
            {
                if (m_powerGauge.Power < 5.0f)
                    m_powerGauge.Power = 5.0f;
                m_isPowerChosen = true;
                if (m_powerGauge.NeedlePosition.Y <= 105.0f) // PERFECT HIT
                {
                    drawPowerHit = true;
                    m_projectile.Speed = m_powerGauge.Power / 2 * (Player.Modifier * (float)2.0f);
                }
                else
                    m_projectile.Speed = m_powerGauge.Power / 2 * Player.Modifier;
                {
                    //drawPowerHit = true;
                    m_projectile.Fire();
                }
            }

            if (m_currentState.IsKeyDown(Keys.Space) && m_prevState.IsKeyUp(Keys.Space) && !m_isAngleChosen)
            {
                m_isAngleChosen = true;
                m_angleGauge.IsAngleChosen = true;
                m_projectile.Angle = m_angleGauge.Angle * (float)-1;
               
            }

            //========= POWER HIT TEST =============
            m_powerHit.UpdateFrame((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            if (drawPowerHit)
            {
                powerHitElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (powerHitElapsed > powerHitDrawTime)
                {
                    powerHitElapsed = 0;
                    drawPowerHit = false;
                }
            }
            //========= POWER HIT TEST =============

            UpdateGUI();

            m_prevState = m_currentState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transforms);
            
            // Level
            level.Draw(spriteBatch);

            // PROJECTILE
            m_projectile.Draw(spriteBatch);

            // ANGLE GAUGE
            if(  !m_projectile.Flying && !m_projectile.Landed && !m_isAngleChosen)
                m_angleGauge.Draw( spriteBatch );

            // POWER GAUGE
            if (m_isAngleChosen && !m_projectile.Flying && !m_projectile.Landed)
                m_powerGauge.Draw(spriteBatch);



            spriteBatch.End();

            spriteBatch.Begin();

            // GUI
            if(m_projectile.m_position.Y > -19760)
                m_gui.Draw(spriteBatch, Color.Black, m_projectile.Landed);
            else
                m_gui.Draw(spriteBatch, Color.White, m_projectile.Landed);

            //========= POWER HIT TEST =============
            if (drawPowerHit)
                m_powerHit.DrawFrame(spriteBatch, m_powerHitPos);

            spriteBatch.End();
        }

        private void UpdateGUI()
        {
            //Update GUI Info
            //===============
            if (m_projectile.Flying)
            {
                tempDistance += m_projectile.Speed / 8;
                m_gui.DistanceString = "Distance: " + '\n' + tempDistance.ToString();
                m_gui.HeightString = "Height:" + '\n' + "  " + (((int)m_projectile.Position.Y * -1 + 617) / 20).ToString();
            }

            // When landed
            if (m_projectile.Landed && !isFinish)
            {
                Player.CurrentDistance = (int)tempDistance;
                Player.CheckPlayerLvl();

                if (Player.Highscore < (int)tempDistance)
                    m_gui.HighscoreString = "Highscore: " + '\n' + tempDistance.ToString();

                Player.WriteFile();
                m_gui.HeightString      = "Height:" + '\n' + "   0";
                m_gui.LvlString = "Level: " + '\n' + " " + Player.LVL.ToString();
                m_gui.CurrXpString = " XP: " + '\n' + Player.XP.ToString();
                m_gui.XpToNextString = "To next: " + '\n' + " " + (Player.XpLimit - Player.XP).ToString();
                isFinish = true;
                
            }
        }

        private void ResetGame()
        {
            m_angleGauge.IsAngleChosen  = false;
            m_isAngleChosen             = false;
            m_isPowerChosen             = false;
            m_projectile.Flying         = false;
            m_projectile.Landed         = false;
            Player.CurrentDistance      = 0;
            tempDistance                = 0;
            m_gui.DistanceString        = "Distance:" + '\n' + "    0";
            m_projectile.m_position     = m_projectile.InitPosition;
            Player.NrOfPowerUps         = 0;
            isFinish                    = false;
        }
    }
}
