using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Audio
{
    /// <summary>
    /// The audio manager
    /// </summary>
    public static class AudioManager
    {
        private static AudioSource _audioSource;

        private static Dictionary<AudioClipName, AudioClip> _audioClips =
            new Dictionary<AudioClipName, AudioClip>();

        private static bool _initialized = false;

        /// <summary>
        /// Initializes the audio manager
        /// </summary>
        /// <param name="source">audio source</param>
        public static void Initialize(AudioSource source)
        {
            _audioSource = source;
            _audioSource.volume = 0.3f;
            
            if (_initialized) return;
            
            _audioClips.Add(AudioClipName.HitMarble, 
                Resources.Load<AudioClip>(@"Audio\hitMarble"));
            _audioClips.Add(AudioClipName.BackgroundMusic,
                Resources.Load<AudioClip>(@"Audio\bg_music"));
            _audioClips.Add(AudioClipName.HitWall,
                Resources.Load<AudioClip>(@"Audio\hitWall"));


            _initialized = true;
        }

        /// <summary>
        /// Plays the audio clip with the given name
        /// </summary>
        /// <param name="name">name of the audio clip to play</param>
        public static void Play(AudioClipName name)
        {
            _audioSource.PlayOneShot(_audioClips[name]);
        }
    }


    public static class AudioFadeOut {

        public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime) {
            float startVolume = audioSource.volume;

            while ( audioSource.volume > 0 ) {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

    }
}