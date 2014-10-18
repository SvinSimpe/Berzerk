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
    public class Gold : StaticTexture
    {
        #region Fields & Properties
        private int     m_amount;
        private bool    m_isTaken;
        //==================================
        public int Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
        }

        public bool IsTaken
        {
            get { return m_isTaken; }
            set { m_isTaken = value; }
        }
        #endregion

        #region Methods
        public Gold(Vector2 startPosition, ContentManager content)
            : base(content.Load<Texture2D>("Graphics/gold"), startPosition)
        {
            m_amount = 100;
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

        public void Update(GameTime gameTime, int velocity)
        {
            base.Update(gameTime, velocity);
        }
        #endregion
    }
}
