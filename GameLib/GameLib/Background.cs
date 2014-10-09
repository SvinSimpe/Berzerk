using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib
{
    public class Backgrounds
    {
        public Texture2D texture;
        public Rectangle rectangle;

        public Rectangle start;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    public class ScrollingBackground : Backgrounds
    {
        public ScrollingBackground(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
            start = newRectangle;
        }

        public void Update(int velocity)
        {
            rectangle.X -= velocity;
        }

        public void Reset()
        {
            rectangle = start;
        }
    }
}
