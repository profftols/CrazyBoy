using UnityEngine;
using UnityEngine.UI;

public class SoundEffects : MasterSettings
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private Button[] _buttons;
    
    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeVolume);
        
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].onClick.AddListener(PlayClick);
        }
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(ChangeVolume);

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].onClick.RemoveListener(PlayClick);
        }
    }

    private void ChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat(EffectsVolume, Mathf.Lerp(MinVolume, MaxVolume, volume));
    }

    private void PlayClick()
    {
        _sound.Play();
    }
}
