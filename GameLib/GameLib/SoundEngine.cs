using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib
{
    public static class SoundEngine
    {
        private static List<Music> songs;
        private static List<Effect> soundEffects;

        public static void Initialize()
        {
            songs = new List<Music>();
            soundEffects = new List<Effect>();
        }

        public static void AddSoundEffect(SoundEffect effect, string name)
        {
            soundEffects.Add(new Effect(effect, name));
        }

        public static void AddSoundEffect(SoundEffect effect, string name, float volume)
        {
            soundEffects.Add(new Effect(effect, name, volume));
        }

        public static void AddSong(Song song, string name)
        {
            songs.Add(new Music(song, name));
        }

        public static void PlaySong(string name)
        {
            foreach (Music song in songs)
            {
                if (song.Name == name)
                {
                    song.Play();
                    return;
                }
            }
        }

        public static void PlaySoundEffect(string name)
        {
            foreach (Effect effect in soundEffects)
            {
                if (effect.Name == name)
                {
                    effect.Play();
                    return;
                }
            }
        }
    }
}
