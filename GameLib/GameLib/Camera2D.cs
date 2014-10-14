using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib
{
    public class Camera2D
    {
        public Matrix transforms;
        Viewport view;
        Vector2 center;

        public Camera2D(Viewport view)
        {
            this.view = view;
        }

        public void Update(GameTime gameTime, Vector2 objectPosition)
        {
            center = new Vector2(view.X / 2, objectPosition.Y);
            transforms = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                         Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}
