﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameLib;

namespace Berzerk
{
    public class Mine : StaticTexture
    {

        #region Methods

        public Mine( Vector2 startPosition, ContentManager content )
            : base( content.Load<Texture2D>("Graphics/mine"), startPosition )
        {

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
