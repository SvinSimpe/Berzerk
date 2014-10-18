using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Berzerk
{
    public class CloudShadow : StaticTexture
    {
        public CloudShadow(Vector2 startPosition, ContentManager content)
            :base(content.Load<Texture2D>("Graphics/cloudShadow"), startPosition)
        {
 
        }

        public void Update(GameTime gameTime, int velocity, int xPos)
        {
            m_position.X = xPos;
            base.Update(gameTime, velocity);
        }
    }
}
