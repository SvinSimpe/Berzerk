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
        public Texture2D m_texture;
        public Vector2 m_position;

        public Texture2D Texture
        {
            get { return m_texture; }
        }

        public Vector2 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public StaticTexture( Texture2D texture, Vector2 position)
        {
            m_texture = texture;
            m_position = position;
        }

        void Update(GameTime gameTime)
        { }

        void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_position, Color.White);
        }
        
    }
}
