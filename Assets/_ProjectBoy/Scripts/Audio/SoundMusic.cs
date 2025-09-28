using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.Audio
{
    public class SoundMusic : MonoBehaviour
    {
        private const string MusicSettingsTag = "MusicSettings";
        private const string Music = "Music";
        private const float OffsetVolume = -15f;
        private const string AudioMusicRunnnermusic = "Audio/Music/RunnnerMusic";
        private const string AudioMusicFightmusic = "Audio/Music/FightMusic";
        private const string AudioMusicMainmenumusic = "Audio/Music/MainMenuMusic";

        private Slider _slider;

        [SerializeField] private AudioSource _sound;

        private void OnEnable()
        {
            _slider = GameObject.FindWithTag(MusicSettingsTag).GetComponentInChildren<Slider>();
            _slider?.onValueChanged.AddListener(ChangeVolume);
        }

        private void OnDisable()
        {
            _slider?.onValueChanged.RemoveListener(ChangeVolume);
        }

        public void ChangeMusicRunner()
        {
            _sound.clip = Resources.Load<AudioClip>(AudioMusicRunnnermusic);
            _sound.Play();
        }

        public void ChangeMusicFighter()
        {
            _sound.clip = Resources.Load<AudioClip>(AudioMusicFightmusic);
            _sound.Play();
        }

        public void ChangeMusicMenu()
        {
            _sound.clip = Resources.Load<AudioClip>(AudioMusicMainmenumusic);
            _sound.Play();
        }

        private void ChangeVolume(float volume)
        {
            MasterSoundSettings.Instance.mixerGroup.audioMixer.SetFloat(Music,
                Mathf.Lerp(MasterSoundSettings.MinVolume, MasterSoundSettings.MaxVolume + OffsetVolume, volume));
        }
    }
}