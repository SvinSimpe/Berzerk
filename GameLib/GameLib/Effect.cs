using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib
{
    class Effect
    {
        private SoundEffect soundEffect;
        private string name;
        private SoundEffectInstance effectControll;
        private float volume;

        public SoundEffect SoundEffect
        {
            get { return soundEffect; }
        }

        public string Name
        {
            get { return name; }
        }

        public Effect(SoundEffect effect, string name)
        {
            soundEffect = effect;
            this.name = name;
            effectControll = soundEffect.CreateInstance();
            volume = 1.0f;
        }

        public Effect(SoundEffect effect, string name, float volume)
        {
            soundEffect = effect;
            this.name = name;
            effectControll = soundEffect.CreateInstance();
            this.volume = volume;
        }

        public void Play()
        {
            soundEffect.Play(volume, 0.0f, 0.0f);
        }
    }
}
