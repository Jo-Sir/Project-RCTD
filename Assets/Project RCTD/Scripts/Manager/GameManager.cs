using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 
public class GameManager : Singleton<GameManager>
{
    #region Fields
    private int wave;
    private int gold;
    private int life;
    private int upgradeBlackLV;
    private int upgradeWhiteLV;
    #endregion Fields

    #region Properties
    public int Life
    {
        set
        {
            life = value;
            UIManager.Instance.TextUpdate("life", life.ToString());
            //if (life == 0) Debug.Log("게임종료");
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
    private void Awake()
    {
        // 씬 넘어갔을때 초기화 해주기
        gold = 400;
        wave = 0;
        life = 10;
    }
    #endregion UnityEngines

    #region Funcs
    public GameObject ObjectGet(Enum key, Transform parentTransform)
    {
        return ObjectPoolManager.Instance.GetObject(key.ToString(), parentTransform);
    }
    public void ObjectReturn(Enum key, GameObject obj)
    {
        ObjectPoolManager.Instance.ReturnObject(key.ToString(), obj);
    }
    #endregion Funcs
}
