using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionUIController : MonoBehaviour
{
    #region SerializeField
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Button masterMuteButton;
    [SerializeField] Button bgmMuteButton;
    [SerializeField] Button sfxMuteButton;
    [SerializeField] Sprite muteSprite;
    [SerializeField] Sprite nanMuteSprite;
    #endregion

    #region Fields
    private bool masterVolumeMute = false;
    private bool bgmVolumeMute = false;
    private bool sfxVolumeMute = false;
    #endregion

    #region UnityEngie
    private void OnEnable()
    {
        masterSlider.value = AudioManager.Instance.GetAudioMixerValue("Master");
        bgmSlider.value = AudioManager.Instance.GetAudioMixerValue("BGM");
        sfxSlider.value = AudioManager.Instance.GetAudioMixerValue("SFX");
    }
    #endregion

    #region Funcs
    public void CloseOption()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "GameScenes")
        { Time.timeScale = UIManager.Instance.PreTimeScale; }
        gameObject.SetActive(false);
    }
    public void SliderControll(string name)
    {
        float value = 0;
        switch (name)
        {
            case "Master":
                value = masterSlider.value;
                break;
            case "BGM":
                value = bgmSlider.value;
                break;
            case "SFX":
                value = sfxSlider.value;
                break;
        }
        AudioManager.Instance.SetAudioMixerValue(name, value);
    }
    public void MuteButton(string name)
    {
        bool muteValue = false;
        Slider curslider = null;
        switch (name)
        {
            case "Master":
                if (masterVolumeMute == true)
                {
                    masterVolumeMute = false;
                    masterMuteButton.image.sprite = nanMuteSprite;
                }
                else
                {
                    masterVolumeMute = true;
                    masterMuteButton.image.sprite = muteSprite;
                }
                muteValue = masterVolumeMute;
                curslider = masterSlider;
                break;
            case "BGM":
                if (bgmVolumeMute == true)
                {
                    bgmVolumeMute = false;
                    bgmMuteButton.image.sprite = nanMuteSprite;
                }
                else
                {
                    bgmVolumeMute = true;
                    bgmMuteButton.image.sprite = muteSprite;
                }
                muteValue = bgmVolumeMute;
                curslider = bgmSlider;
                break;
            case "SFX":
                if (sfxVolumeMute == true)
                {
                    sfxVolumeMute = false;
                    sfxMuteButton.image.sprite = nanMuteSprite;
                }
                else
                {
                    sfxVolumeMute = true;
                    sfxMuteButton.image.sprite = muteSprite;
                }
                muteValue = sfxVolumeMute;
                curslider = sfxSlider;
                break;
        }
        AudioManager.Instance.SetAudioMixerMute(name, muteValue, curslider);
    }
    public void BackToMainMenu()
    {
        GameManager.Instance.BackToMainMenu();
    }
    public void ExitGame()
    {
        GameManager.Instance.GameExit();
    }
    #endregion
}
