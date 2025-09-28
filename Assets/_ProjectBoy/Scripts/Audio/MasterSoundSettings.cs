using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.Audio
{
    public class MasterSoundSettings : MonoBehaviour
    {
        private const string MasterSoundTag = "MasterSoundSettings";

        public const float MaxVolume = 0f;
        public const float MinVolume = -80f;

        private const string Master = "Master";

        private float _currentValue;

        private Toggle mute;

        [field: SerializeField] public AudioMixerGroup mixerGroup { get; private set; }
        [field: SerializeField] public SoundMusic soundMusic { get; private set; }
        [field: SerializeField] public SoundEffects soundEffects { get; private set; }

        public static MasterSoundSettings Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            mute = GameObject.FindWithTag(MasterSoundTag).GetComponentInChildren<Toggle>();
            ;
            mute?.onValueChanged.AddListener(ChangeMute);
        }

        private void OnDisable()
        {
            mute?.onValueChanged.RemoveListener(ChangeMute);
        }

        private void ChangeMute(bool enable)
        {
            if (!enable)
            {
                mixerGroup.audioMixer.SetFloat(Master, _currentValue);
            }
            else
            {
                mixerGroup.audioMixer.GetFloat(Master, out var value);
                _currentValue = value;
                mixerGroup.audioMixer.SetFloat(Master, MinVolume);
            }
        }
    }
}