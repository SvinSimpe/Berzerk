using GameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berzerk
{
    public class Cloud : StaticTexture
    {

        CloudShadow m_shadow;
        Vector2 m_shadowPos;

        public Cloud(Vector2 startPosition, ContentManager content)
            :base(content.Load<Texture2D>("Graphics/cloud"), startPosition)
        {
           m_shadowPos = new Vector2(m_position.X, 580 );
           m_shadow = new CloudShadow(m_shadowPos, content);
        }

        public void Update(GameTime gameTime, int velocity)
        {
            ((CloudShadow)m_shadow).Update(gameTime, velocity, (int)m_position.X);
            base.Update(gameTime, velocity);
        }

        public void Draw( SpriteBatch spriteBatch)
        {
            ((CloudShadow)m_shadow).Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
