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
    public class AngleGauge
    {
        #region Fields & Properties
        private Vector2     m_position;
        private Texture2D   m_texture;
        private Vector2     m_meterPosition;
        private Texture2D   m_meterTexture;
        private float       m_angle;
        private bool        m_isAngleIncreasing;
        private bool        m_isAngleChosen;
 
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

        public float Angle
        {
            get { return m_angle; }
            set { m_angle = value; }
        }

        public bool IsAngleIncreasing
        {
            get { return m_isAngleIncreasing; }
            set { m_isAngleIncreasing = value; }
        }

        public bool IsAngleChosen
        {
            get { return m_isAngleChosen; }
            set { m_isAngleChosen = value; }
        }
        #endregion

        #region Method

        public AngleGauge( ContentManager content )
        {
            m_texture           = content.Load<Texture2D>( "Graphics/angleGauge" );
            m_position          = new Vector2( 150, 300 );
            m_meterTexture      = content.Load<Texture2D>("Graphics/angleMeter");
            m_meterPosition     = new Vector2(200, 150);
            m_angle             = MathHelper.ToRadians( 0 );
            m_isAngleIncreasing = true;
            m_isAngleChosen     = false;
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                (int)m_position.X,
                (int)m_position.Y,
                m_texture.Width,
                m_texture.Height);
            }
        }

        public void Update( GameTime gameTime )
        {
            if ( IsAngleIncreasing ) 
                Angle -= (float)0.01;

            if (Angle <= -1.0f)
                IsAngleIncreasing = false;

            if ( !IsAngleIncreasing )
                Angle += (float)0.01;

            if (Angle >= 0.0f)
                IsAngleIncreasing = true;
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            spriteBatch.Draw(m_texture, BoundingBox, null, Color.White, m_angle, new Vector2(-100, 15), SpriteEffects.None, 0);

            if( !IsAngleChosen )
                spriteBatch.Draw(m_meterTexture, m_meterPosition, Color.White);
        }

        #endregion
    }
}
