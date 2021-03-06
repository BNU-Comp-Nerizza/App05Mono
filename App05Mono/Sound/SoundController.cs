using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace App05Mono.Sound
{
    /// <summary>
    /// Sound Controller will manage any sounds or music in the game.
    /// </summary>
    /// <author>
    /// Andrei Cruceru
    /// modified by Nerizza 
    /// </author>
    public static class SoundController
    {
        private static readonly Dictionary<string, Song> Songs =
            new Dictionary<string, Song>();

        private static readonly Dictionary<string, SoundEffect> SoundEffects =
            new Dictionary<string, SoundEffect>();

        /// <summary>
        /// Load songs and sound effects.
        /// </summary>
        /// <param name="content"></param>
        public static void LoadContent(ContentManager content)
        {
            Songs.Add("Klee!", content.Load<Song>("Klee!"));

            SoundEffects.Add("Fire", content.Load<SoundEffect>("FireSound"));
            SoundEffects.Add("Explode", content.Load<SoundEffect>("ExplodeSound"));
        }
        /// <summary>
        /// Get a sound effect from the collection.
        /// </summary>
        /// <param name="effect">A string type key assigned to a sound effect.</param>
        /// <returns>A SoundEffect object assigned to the string key.</returns>
        public static SoundEffect GetSoundEffect(string effect)
        {
            return SoundEffects[effect];
        }
        /// <summary>
        /// Play a song
        /// </summary>
        /// <param name="song">A string type key assigned to a song.</param>
        public static void PlaySong(string song)
        {
            MediaPlayer.IsRepeating = true;

            MediaPlayer.Play(Songs[song]);
            MediaPlayer.Volume = 0.05f;
        }

        public static void PauseSong()
        {
            MediaPlayer.Pause();
        }

        public static void ResumeSong()
        {
            MediaPlayer.Resume();
        }
    }
}
