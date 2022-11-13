using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Fields
    private int wave;
    private int gold;
    private int life;
    private int upgradeBlackLV;
    private int upgradeWhiteLV;
    private FadeController fadeController = null;
    #endregion Fields

    #region Properties
    public int Life
    {
        set
        {
            life = value;
            UIManager.Instance.TextUpdate("life", life.ToString());
            if (life == 0) Debug.Log("게임종료");
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
        AudioManager.Instance.AoudioMuteControl("Master", false);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion UnityEngines

    #region Funcs
    public void Init()
    {
        ObjectPoolManager.Instance.Init();
    }
    public void GameStart()
    {
        gold = 400;
        wave = 0;
        life = 10;
        StartCoroutine(FadeOutTerm("GameScenes"));
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeInTerm());

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
        yield return new WaitForSeconds(1f);
        if (scenesName != null)
        {
            SceneManager.LoadScene(scenesName);
        }
    }
    IEnumerator FadeInTerm()
    {
        fadeController.FadeIn();
        yield return new WaitForSeconds(1f);
        fadeController.FadeImageSetActive(false);
    }
    #endregion
}
