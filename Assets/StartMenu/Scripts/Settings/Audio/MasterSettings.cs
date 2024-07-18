using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MasterSettings : MonoBehaviour
{
    [SerializeField] protected AudioMixerGroup Mixer;
    [SerializeField] private Toggle _toggle;

    protected const string EffectsVolume = "Effects";
    protected const string MusicVolume = "Music";
    
    private const string MasterVolume = "Master";

    protected float MinVolume = -80f;
    protected float MaxVolume = 0f;
    protected float CurrentValue;
    
    private bool _isEnableSound = true;

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(Disable);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(Disable);
    }

    protected virtual void Disable(bool enable)
    {
        _isEnableSound = enable;
        
        if (enable)
        {
            Mixer.audioMixer.SetFloat(MasterVolume, CurrentValue);
        }
        else
        {
            GetMasterAudio();
            Mixer.audioMixer.SetFloat(MasterVolume, MinVolume);
        }
    }

    private void GetMasterAudio()
    {
        Mixer.audioMixer.GetFloat(MasterVolume, out float value);
        CurrentValue = value;
    }
}