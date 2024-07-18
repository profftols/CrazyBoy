using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundMusic : MasterSettings
{
    [SerializeField] protected Slider _slider;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat(MusicVolume, Mathf.Lerp(MinVolume, MaxVolume, volume));
    }
}
