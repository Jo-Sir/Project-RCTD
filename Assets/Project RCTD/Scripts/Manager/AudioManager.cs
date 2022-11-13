using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{

    #region Fields
    // �����Ŭ�� ��ũ���ͺ� ������Ʈ�� �����ͼ� �ֱ�
    private AudioClip[] bgmsData = null;
    public AudioMixer audioMixer;
    #endregion
    #region Property
    public float Pitch { get => Time.timeScale; }
    #endregion

    #region UnityEngine
    private new void Awake()
    {
        base.Awake();
        audioMixer = Resources.Load<AudioMixer>("AudioMixer/Master");
        if (bgmsData == null)
        {
            // bgmsData  = Resources.Load<AudioMixer>("Data/BgmsData").GetComponent<AudioDatas>().Data;
        }

    }
    #endregion

    #region Funcs
    public void AoudioControl(string name, float value)
    {
        audioMixer.SetFloat(name, value);
    }
    public void AoudioMuteControl(string name, bool mute)
    {
        float value = mute ? -80 : 0;
        audioMixer.SetFloat(name, value);
    }
    #endregion
}
