using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib
{
    class StaticTexture
    {
        Texture2D texture;
        Vector2 position;

        public Texture2D Texture
        {
            get { return texture; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public StaticTexture( Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        void Update(GameTime gameTime)
        { }

        void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
        
    }
}
