using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 
public class GameManager : Singleton<GameManager>
{
    #region Fields
    public ROUND_TYPE CUR_ROUND_TYPE;
    public int Gold;
    public int round;
    #endregion Fields

    #region UnityEngines
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        { 
        }
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
