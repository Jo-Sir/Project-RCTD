using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{

    #region Fields
    private AudioMixer audioMixer;
    private AudioSource goldSound;
    #endregion

    #region Property
    public float Pitch { get => Time.timeScale; }
    public AudioSource GoldSound { get => goldSound; set => goldSound = value; }
    #endregion

    #region UnityEngine
    private new void Awake()
    {
        base.Awake();
        audioMixer = Resources.Load<AudioMixer>("AudioMixer/Master");
    }
    #endregion

    #region Funcs
    public void SetAudioMixerValue(string name, float value)
    {
        if (value <=-40) { value = -80; }
        audioMixer.SetFloat(name, value);
    }
    public void SetAudioMixerMute(string name, bool mute)
    {
        float value = mute ? -80 : 0;
        audioMixer.SetFloat(name, value);
    }
    public float GetAudioMixerValue(string name)
    {
        float audioMixervalue;
        audioMixer.GetFloat(name, out audioMixervalue);
        return audioMixervalue;
    }
    #endregion
}
