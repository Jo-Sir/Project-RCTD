using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private GameObject optionUI;
    [SerializeField] private GameObject explanationUI;
    #endregion
    #region UnityEngine
    private void Awake()
    {
        if (GameObject.Find("GameManager") == null)
        {
            GameManager.Instance.Init();
        }
    }
    #endregion
    #region Funcs
    public void GameStart()
    {
        GameManager.Instance.GameStart();
    }
    public void SetActiveOptionUI()
    {
        optionUI.SetActive(true);
    }
    public void SetActiveExplanationUI()
    {
        explanationUI.SetActive(true);
    }
    #endregion
}
