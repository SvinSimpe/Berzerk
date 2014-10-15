using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameLib;

namespace Berzerk
{
    public class PowerGauge
    {
        #region Fields & Properties
        private Vector2     m_framePosition;
        private Texture2D   m_frameTexture;

        private Vector2     m_needlePosition;
        private Texture2D   m_needleTexture;

        private float       m_power;
        private bool        m_isPowerIncreasing;

        public Vector2 FramePosition
        {
            get { return m_framePosition; }
            set { m_framePosition = value; }
        }

        public Vector2 NeedlePosition
        {
            get { return m_needlePosition; }
            set { m_needlePosition = value; }
        }

        public float Power
        {
            get { return m_power; }
            set { m_power = value; }
        }

        public bool IsPowerIncreasing
        {
            get { return m_isPowerIncreasing; }
            set { m_isPowerIncreasing = value; }
        }
        #endregion

        #region Methods
        public PowerGauge( ContentManager content )
        {
            m_framePosition     = new Vector2( 300, 100 );
            m_frameTexture      = content.Load<Texture2D>( "Graphics/powerGaugeFrame" );
            m_needlePosition    = new Vector2( 302, 196 );
            m_needleTexture     = content.Load<Texture2D>( "Graphics/powerGaugeNeedle" );
            m_power             = 98;
            m_isPowerIncreasing = true;
        }

        public void Update(GameTime gameTime)
        {
            if (IsPowerIncreasing)
            {
                Power += (float)5.0f;
                m_needlePosition.Y -= (float)5.0f;
            }

            if (m_needlePosition.Y <= 100.0f)
                IsPowerIncreasing = false;

            if (!IsPowerIncreasing)
            {
                Power -= (float)5.0f;
                m_needlePosition.Y += (float)5.0f;
            }

            if (m_needlePosition.Y >= 296.0f)
                IsPowerIncreasing = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw( m_frameTexture, m_framePosition, Color.White );
            spriteBatch.Draw( m_needleTexture, m_needlePosition, Color.White );
        }
        #endregion
    }
}
