using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameLib;

namespace Berzerk
{
    public class Projectile : StaticTexture
    {
        #region Fields & Properties

        //private Vector2     m_position;
        //private Texture2D   m_texture;
        private float       m_angle;    // The inital angle of the projectile
        private float       m_speed;
        private float       m_xVelocity;
        private float       m_yVelocity;
        private float       m_time;     // The lifetime of the projectiles, increments each loop
        private bool        m_flying;   // True when airborne
        private bool        m_landed;
        private Vector2     m_direction;
        //==============================================================================

        public Vector2 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public Texture2D Texture
        {
            get { return m_texture; }
            set { m_texture = value; }
        }

        public bool Flying
        {
            get { return m_flying; }
            set { m_flying = value; }
        }

        public bool Landed
        {
            get { return m_landed; }
            set { m_landed = value; }
        }

        public float Angle
        {
            get { return m_angle; }
            set { m_angle = value; }
        }



        #endregion
        
        #region Methods

        public Rectangle BoundingBox
	    {
	        get {   return new Rectangle(
	                (int)m_position.X,
	                (int)m_position.Y,
	                m_texture.Width,
	                m_texture.Height);
	        }
	    }

        public Projectile( Vector2 startPosition, ContentManager content )
            :base( content.Load<Texture2D>("Graphics/projectile0"), startPosition )
        {
            m_angle     = MathHelper.ToRadians( 25 );
            m_speed     = 40.0f;
            m_xVelocity = (float)Math.Cos(m_angle) * m_speed;
            m_yVelocity = (float)Math.Sin(m_angle) * m_speed;
            m_time      = 0.0f;
            m_flying    = false;
            m_landed    = false;

            m_direction.X = (float)Math.Cos(m_angle);
            m_direction.Y = (float)Math.Sin(m_angle);
        }

        public void ApplyForce1()
        {
            if (m_speed <= 6.5)
            {
                m_speed = 0;
                m_flying = false;
                m_time = 0;
            }
            else
            {
                m_speed *= (float)0.75;
                m_xVelocity = (float)Math.Cos(m_angle) * m_speed;
                m_yVelocity = (float)Math.Sin(m_angle) * m_speed;
                m_time = 0.0f;

                //TEST
                m_position.Y -= 5.0f;
            }
        }

        public void ApplyForce2()
        {
            m_xVelocity = (float)Math.Cos(m_angle) * m_speed;
            m_yVelocity = (float)Math.Sin(m_angle) * m_speed * (float)0.6;
            m_time = 0.0f;
        }

        public void Update( GameTime gameTime )
        {
            if (m_flying)
            {
                float gravity = 20009.82f;
                float millisecs = gameTime.ElapsedGameTime.Milliseconds;
                millisecs /= 10000;
                m_time += millisecs;

                m_position.X += m_xVelocity;
                m_position.Y -= ( m_yVelocity ) - (gravity * m_time * m_time );
            }                
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            spriteBatch.Draw( m_texture, m_position, Color.White );
        }

        #endregion
    }
}
