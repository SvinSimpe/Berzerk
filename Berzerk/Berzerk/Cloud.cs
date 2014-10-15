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
        public Cloud(Vector2 startPosition, ContentManager content)
            :base(content.Load<Texture2D>("Graphics/cloud"), startPosition)
        {
 
        }

        public void Update(GameTime gameTime, int velocity)
        {
            base.Update(gameTime, velocity);
        }
    }
}
