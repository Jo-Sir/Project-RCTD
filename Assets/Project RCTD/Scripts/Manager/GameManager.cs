using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    public UnityAction returnAllObj;
    private int wave;
    private int gold;
    private int life;
    private int upgradeBlackLV;
    private int upgradeWhiteLV;
    private bool gameOver;
    private FadeController fadeController = null;
    #endregion Fields

    #region Properties
    public int Life
    {
        set
        {
            life = value;
            if (life <= 0) { life = 0; }
            UIManager.Instance.TextUpdate("life", life.ToString());
            if (life <= 0) GameResult(false);
        }
        get => life;
    }
    public int Gold
    {
        set 
        {
            gold = value;
            UIManager.Instance.TextUpdate("gold", gold.ToString());
        }
        get => gold;
    }
    public int Wave
    {
        set
        {
            wave = value;
            UIManager.Instance.TextUpdate("curWave", wave.ToString());
            if(wave != 1) Gold += 400;
        }
        get => wave;
    }
    public int UpgradeWhiteLV { get => upgradeWhiteLV; set => upgradeWhiteLV = value; }
    public int UpgradeBlackLV { get => upgradeBlackLV; set => upgradeBlackLV = value; }
    public bool GameOver { get => gameOver; set => gameOver = value; }
    #endregion

    #region UnityEngines
    private new void Awake()
    {
        base.Awake();
        if (fadeController == null)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Controller/Fade"));
            fadeController = GameObject.Find("Fade(Clone)").GetComponent<FadeController>();
        }
        GameOver = false;
        AudioManager.Instance.SetAudioMixerMute("Master", false);
    }
    #endregion UnityEngines

    #region Funcs
    public void Init()
    {
        ObjectPoolManager.Instance.Init();
    }
    public void GameStart()
    {
        returnAllObj?.Invoke();
        gold = 400;
        wave = 0;
        life = 20;
        gameOver = false;
        StartCoroutine(FadeOutTerm("GameScenes"));
    }
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        GameOver = true;
        StartCoroutine(FadeOutTerm("MainScenes"));
        returnAllObj?.Invoke();
    }
    public void GameExit() 
    {
        Application.Quit();
    }
    /// <summary>
    /// true = Game Clear, false = Game Over
    /// </summary>
    /// <param value="result"></param>
    public void GameResult(bool result)
    {
        if (result)
        {
            UIManager.Instance.GameResult("Game Clear");
        }
        else
        {
            UIManager.Instance.GameResult("Game Over");
        }
    }
    public GameObject ObjectGet(Enum key, Transform parentTransform)
    {
        return ObjectPoolManager.Instance.GetObject(key.ToString(), parentTransform);
    }
    public void ObjectReturn(Enum key, GameObject obj)
    {
        ObjectPoolManager.Instance.ReturnObject(key.ToString(), obj);
    }
    #endregion

    #region IEnumerator
    IEnumerator FadeOutTerm(string scenesName)
    {
        fadeController.FadeImageSetActive(true);
        fadeController.FadeOut();
        yield return new WaitForSeconds(0.9f);
        if (scenesName != null)
        {
            SceneManager.LoadScene(scenesName);
        }
        StartCoroutine(FadeInTerm());
    }
    IEnumerator FadeInTerm()
    {
        fadeController.FadeIn();
        yield return new WaitForSeconds(0.5f);
        fadeController.FadeImageSetActive(false);
    }
    #endregion
}
