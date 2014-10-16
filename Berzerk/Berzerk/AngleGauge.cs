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
        private Vector2     m_needlePosition;
        private Texture2D   m_needleTexture;

        private Vector2     m_meterPosition;
        private Texture2D   m_meterTexture;

        private float       m_angle;
        private bool        m_isAngleIncreasing;
        private bool        m_isAngleChosen;
 
        public Vector2 NeedlePosition
        {
            get { return m_needlePosition; }
            set { m_needlePosition = value; }
        }

        public Texture2D NeedleTexture
        {
            get { return m_needleTexture; }
            set { m_needleTexture = value; }
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
            m_needleTexture     = content.Load<Texture2D>( "Graphics/angleGauge1" );
            m_needlePosition    = new Vector2( 150, 300 );

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
                (int)m_needlePosition.X,
                (int)m_needlePosition.Y,
                m_needleTexture.Width,
                m_needleTexture.Height);
            }
        }

        public void Update( GameTime gameTime )
        {
            //===========  INCREASING  ============
            if ( IsAngleIncreasing ) 
                Angle -= 0.01f;

            if (Angle <= -1.2f)
            {
                Angle = -1.2f;
                IsAngleIncreasing = false;
            }

            //===========  DECREASING  ============
            if ( !IsAngleIncreasing )
                Angle += 0.01f;

            if (Angle >= 0.0f)
            {
                Angle = 0.0f;
                IsAngleIncreasing = true;
            }
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            spriteBatch.Draw(m_needleTexture, BoundingBox, null, Color.White, m_angle, new Vector2(-75, 15), SpriteEffects.None, 0);

            if( !IsAngleChosen )
                spriteBatch.Draw(m_meterTexture, m_meterPosition, Color.White);
        }

        #endregion
    }
}
