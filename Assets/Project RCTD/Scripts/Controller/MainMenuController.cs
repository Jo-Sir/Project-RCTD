using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    #region UnityEngine
    private void Awake()
    {
        GameManager.Instance.Init();
    }
    #endregion
    #region Funcs
    public void GameStart()
    {
        GameManager.Instance.GameStart();
    }
    #endregion
}
