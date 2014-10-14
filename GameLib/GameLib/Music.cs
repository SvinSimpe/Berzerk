using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib
{
    class Music
    {
        private Song song;
        private string name;

        public Song Song
        {
            get { return song; }
        }
        public string Name
        {
            get { return name; }
        }

        public Music(Song song, string name)
        {
            this.song = song;
            this.name = name;
        }

        public void Play()
        {
            MediaPlayer.Play(song);
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }

    }
}
