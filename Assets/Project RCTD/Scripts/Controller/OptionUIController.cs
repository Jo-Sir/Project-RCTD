using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private void Update()
    {

    }
    #endregion
    #region Funcs
    public void CloseOption()
    {
        Time.timeScale = UIManager.Instance.PreTimeScale;
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
        AudioManager.Instance.AoudioControl(name, value);
    }
    public void MuteButton(string name)
    {
        bool muteValue = false;
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
                break;
        }
        AudioManager.Instance.AoudioMuteControl(name, muteValue);
    }
    #endregion
}
