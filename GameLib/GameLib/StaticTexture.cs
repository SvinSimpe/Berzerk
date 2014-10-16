using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib
{
    public class StaticTexture
    {
        public Texture2D    m_texture;
        public Vector2      m_position;
        public bool         m_isActive;
        public bool         m_isHit;

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

        public Texture2D Texture
        {
            get { return m_texture; }
        }

        public Vector2 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public bool IsActive
        {
            get { return m_isActive; }
            set { m_isActive = value; }
        }

        public bool IsHit
        {
            get { return m_isHit; }
            set { m_isHit = value; }
        }

        public StaticTexture( Texture2D texture, Vector2 position)
        {
            m_texture = texture;
            m_position = position;
            m_isActive = false;
        }

        public void Update(GameTime gameTime, int velocity)
        {
            if(m_isActive)
                m_position.X -= velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_position, Color.White);
        }        
    }
}
