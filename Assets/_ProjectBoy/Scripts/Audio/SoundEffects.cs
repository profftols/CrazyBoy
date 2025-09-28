using _ProjectBoy.Scripts.Infostructure.AssetManagement;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _ProjectBoy.Scripts.Audio
{
    public class SoundEffects : MonoBehaviour
    {
        private const string SoundFXTag = "SoundFXSettings";
        private const string Effects = "Effects";
        private AudioClip _attack;
        [SerializeField] private Button[] _buttons;
        private AudioClip _click;
        private AudioClip _dead;
        private AudioClip _defence;
        private AudioClip _getBonus;
        private AudioClip _getHit;

        private Slider _slider;

        [SerializeField] private AudioSource _soundFirst;
        [SerializeField] private AudioSource _soundSecond;
        [SerializeField] private AudioSource _soundThird;
        private AudioClip[] _steps;

        private void Awake()
        {
            _slider = GameObject.FindWithTag(SoundFXTag).GetComponent<Slider>();
            _steps = Resources.LoadAll<AudioClip>(AssetPath.SoundStepsPath);

            _attack = Resources.Load<AudioClip>(AssetPath.SoundAttackPath);
            _defence = Resources.Load<AudioClip>(AssetPath.SoundDefencePath);
            _getHit = Resources.Load<AudioClip>(AssetPath.SoundGetHitPath);
            _click = Resources.Load<AudioClip>(AssetPath.SoundClickPath);
            _getBonus = Resources.Load<AudioClip>(AssetPath.SoundGetBonusPath);
            _dead = Resources.Load<AudioClip>(AssetPath.SoundDeadPath);
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ChangeVolume);

            foreach (var button in _buttons) button.onClick.AddListener(PlayClick);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeVolume);

            foreach (var button in _buttons) button?.onClick.RemoveListener(PlayClick);
        }

        public void PlayPunch()
        {
            PlaySound(_attack);
        }

        public void PlayGetHit()
        {
            PlaySound(_getHit);
        }

        public void PlayDefence()
        {
            PlaySound(_defence);
        }

        public void PlayStep()
        {
            PlaySound(_steps[Random.Range(0, _steps.Length - 1)]);
        }

        public void PlayGetBonus()
        {
            _soundThird.clip = _getBonus;
            _soundThird.Play();
        }

        public void PlayDeath()
        {
            if (_soundThird.isPlaying) _soundThird.Stop();

            _soundThird.clip = _dead;
            _soundThird.Play();
        }

        private void ChangeVolume(float volume)
        {
            MasterSoundSettings.Instance.mixerGroup.audioMixer.SetFloat(Effects,
                Mathf.Lerp(MasterSoundSettings.MinVolume, MasterSoundSettings.MaxVolume, volume));
        }

        private void PlayClick()
        {
            PlaySound(_click);
        }

        private void PlaySound(AudioClip clip)
        {
            if (!_soundFirst.isPlaying)
            {
                _soundFirst.clip = clip;
                _soundFirst.Play();
            }
            else if (!_soundSecond.isPlaying)
            {
                _soundSecond.clip = clip;
                _soundSecond.Play();
            }
        }
    }
}