using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    namespace Managers
    {
        public enum SoundType { playerWalk, playerShoot, enemyHit }

        public class SoundManager : Singleton<SoundManager>
        {
            [Header("Source References")]
            [SerializeField] private AudioSource sfxSource;

            [Header("SFX Values")]
            [SerializeField] private float defaultPitch = 1f;
            [SerializeField] private float defaultVolume = 1f;
            [SerializeField] private float pitchRandomModifier = 0.1f;

            [Header("Sounds")]
            public List<Sound> sounds = new List<Sound>();

            private Dictionary<SoundType, float> _soundTimerDictionary;

            protected override void Awake()
            {
                base.Awake();

                Initialize();
            }

            private void Initialize()
            {
                _soundTimerDictionary = new Dictionary<SoundType, float>()
                {
                    [SoundType.playerWalk] = 0f
                };
            }

            public void PlaySound(SoundType st)
            {
                if (!CanPlay(st))
                    return;

                sfxSource.pitch = Random.Range(defaultPitch - pitchRandomModifier, defaultPitch + pitchRandomModifier);
                sfxSource.PlayOneShot(SearchSound(st), defaultVolume * defaultVolume);
            }

            public void PlaySound(SoundType st, float newVolume)
            {
                if (!CanPlay(st))
                    return;

                sfxSource.pitch = Random.Range(defaultPitch - pitchRandomModifier, defaultPitch + pitchRandomModifier);
                sfxSource.PlayOneShot(SearchSound(st), defaultVolume * newVolume);
            }

            public void PlaySound(SoundType st, float newVolume, float newPitch)
            {
                if (!CanPlay(st))
                    return;

                sfxSource.pitch = Random.Range(newPitch - pitchRandomModifier, newPitch + pitchRandomModifier);
                sfxSource.PlayOneShot(SearchSound(st), defaultVolume * defaultVolume);
            }

            private bool CanPlay(SoundType st)
            {
                switch(st)
                {
                    default:
                        return true;
                    case SoundType.playerWalk:
                        if (!_soundTimerDictionary.ContainsKey(st))
                            return true;

                        float lastTimePlayed = _soundTimerDictionary[st];
                        float playerMoveTimerMax = .25f;
                        if (lastTimePlayed + playerMoveTimerMax >= Time.time)
                            return false;

                        _soundTimerDictionary[st] = Time.time;
                        return true;
                }
            }

            private AudioClip SearchSound(SoundType st)
            {
                foreach (Sound s in sounds)
                {
                    if (s.soundType == st)
                        return s.soundClip;
                }

                Debug.LogError("Sound Not Found!");
                return null;
            }
        }

        [System.Serializable]
        public class Sound
        {
            public SoundType soundType;
            public AudioClip soundClip;
        }
    }
}
