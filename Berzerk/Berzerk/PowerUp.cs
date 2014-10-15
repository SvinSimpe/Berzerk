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
    public class PowerUp : StaticTexture
    {
        #region Fields & Properties

        private int     m_xpReward;
        private bool    m_isTaken;
        //==================================
        public int XpReward
        {
            get { return m_xpReward; }
        }

        public bool IsTaken
        {
            get { return m_isTaken; }
            set { m_isTaken = value; }
        }

        #endregion

        #region Methods

        public PowerUp( Vector2 startPosition, ContentManager content )
            : base( content.Load<Texture2D>("Graphics/powerUp"), startPosition )
        {
            m_xpReward = 10;
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
        { }

        public void Draw(SpriteBatch spriteBatch)
        {
            if( !IsTaken )
                base.Draw( spriteBatch );
        }

        #endregion
    }
}
