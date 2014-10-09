using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib
{
    public class AnimatedTexture
    {
        // Members
        private int         frameCount;
        private int         frame;
        private float       timePerFrame;
        private float       totalElapsed;
        private bool        isPaused;
        private Texture2D   texture;
        public float        rotation, scale, depth;
        public Vector2      origin;

        //Constructor
        public AnimatedTexture(Vector2 origin, float rotation, float scale, float depth)
        {
            this.origin     = origin;
            this.rotation   = rotation;
            this.scale      = scale;
            this.depth      = depth;
        }

        // Methods
        public void Load(ContentManager content, string asset, int frameCount, int framePerSec)
        {
            this.frameCount = frameCount;
            this.texture = content.Load<Texture2D>(asset);
            this.timePerFrame = (float)1 / framePerSec;
            this.frame = 0;
            this.totalElapsed = 0;
            this.isPaused = false;
        }

        public void UpdateFrame(float elapsed)
        {
            if (isPaused)
                return;
            totalElapsed += elapsed;
            if (totalElapsed > timePerFrame)
            {
                frame++;
                // Keep the frame between 0 and the total frames, minus one.
                frame = frame % frameCount;
                totalElapsed -= timePerFrame;
            }
        }

        public void DrawFrame(SpriteBatch spriteBatch, Vector2 screenPos)
        {
            DrawFrame(spriteBatch, frame, screenPos);
        }
        public void DrawFrame(SpriteBatch spriteBatch, int frame, Vector2 screenPos)
        {
            int frameWidth = texture.Width / frameCount;
            Rectangle sourceRect = new Rectangle(frameWidth * frame, 0, frameWidth, texture.Height);
            spriteBatch.Draw(texture, screenPos, sourceRect, Color.White, rotation, origin, scale, SpriteEffects.None, depth);
        }

        public bool IsPaused
        {
            get { return isPaused; }
            set { isPaused = value; }
        }
        public void Reset()
        {
            frame = 0;
            totalElapsed = 0.0f;
        }
        public void Stop()
        {
            Pause();
            Reset();
        }
        public void Play()
        {
            isPaused = false;
        }
        public void Pause()
        {
            isPaused = true;
        }

    }
}
