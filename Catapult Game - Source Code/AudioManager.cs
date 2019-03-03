
#region File Description
//-----------------------------------------------------------------------------
// AudioManager.cs
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace CatapultGame
{
    /// <summary>
    /// Component that manages audio playback for all sounds.
    /// </summary>
    public class AudioManager : GameComponent
    {

        public static void Initialize(Game game)
        {
            audioManager = new AudioManager(game);

            if (game != null)
            {
                game.Components.Add(audioManager);
            }
        }

        public static void LoadSounds()
        {
            string soundLocation = "Sounds/";
            audioManager.soundNames = new string[,] { 
                    {"CatapultExplosion", "catapultExplosion"}, 
                    {"Lose", "gameOver_Lose"},
                    {"Win", "gameOver_Win"},
                    {"BoulderHit", "boulderHit"},
                    {"CatapultFire", "catapultFire"},
                    {"RopeStretch", "ropeStretch"}};

            audioManager.soundBank = new Dictionary<string, SoundEffectInstance>();

            for (int i = 0; i < audioManager.soundNames.GetLength(0); i++)
            {
                SoundEffect se = audioManager.Game.Content.Load<SoundEffect>(
                    soundLocation + audioManager.soundNames[i, 0]);
                audioManager.soundBank.Add(
                    audioManager.soundNames[i, 1], se.CreateInstance());
            }
        }

        public static void PlaySound(string soundName)
        {
            // if the music sound exists, start it
            if (audioManager.soundBank.ContainsKey(soundName))
                audioManager.soundBank[soundName].Play();
        }
        public static void PlaySound(string soundName, bool isLooped)
        {
            // If the sound exists, start it
            if (audioManager.soundBank.ContainsKey(soundName))
            {
                if (audioManager.soundBank[soundName].IsLooped != isLooped)
                    audioManager.soundBank[soundName].IsLooped = isLooped;

                audioManager.soundBank[soundName].Play();
            }
        }
        public static void StopSound(string soundName)
        {
            // if the music sound exists, start it
            if (audioManager.soundBank.ContainsKey(soundName))
                audioManager.soundBank[soundName].Stop();
        }
        public static void StopSounds()
        {
            var soundEffectInstances = from sound in audioManager.soundBank.Values
                                       where sound.State != SoundState.Stopped
                                       select sound;

            foreach (var soundeffectInstance in soundEffectInstances)
                soundeffectInstance.Stop();
        }

        public static void PauseResumeSounds(bool isPause)
        {
            SoundState state = isPause ? SoundState.Paused : SoundState.Playing;

            var soundEffectInstances = from sound in audioManager.soundBank.Values
                                       where sound.State == state
                                       select sound;

            foreach (var soundeffectInstance in soundEffectInstances)
            {
                if (isPause)
                    soundeffectInstance.Play();
                else
                    soundeffectInstance.Pause();
            }
        }
        public static void PlayMusic(string musicSoundName)
        {
            // stop the old music sound
            if (audioManager.musicSound != null)
                audioManager.musicSound.Stop(true);

            // if the music sound exists
            if (audioManager.soundBank.ContainsKey(musicSoundName))
            {
                // get the instance and start it
                audioManager.musicSound = audioManager.soundBank[musicSoundName];
                if (!audioManager.musicSound.IsLooped)
                    audioManager.musicSound.IsLooped = true;
                audioManager.musicSound.Play();
            }
        }



        #region Singleton
        /// <summary>
        /// The singleton for this type.
        /// </summary>
        private static AudioManager audioManager = null;
        #endregion

        #region Audio Data
        private SoundEffectInstance musicSound;
        private Dictionary<string, SoundEffectInstance> soundBank;
        private string[,] soundNames;
        #endregion

        #region Initialization Methods

		private AudioManager(Game game)
			: base(game) { }

        #endregion
        #region Sound Methods
        
        #endregion

        #region Instance Disposal Methods
        /// <summary>
        /// Clean up the component when it is disposing.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    foreach (var item in soundBank)
                    {
                        item.Value.Dispose();
                    }
                    soundBank.Clear();
                    soundBank = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        #endregion
    }
}
