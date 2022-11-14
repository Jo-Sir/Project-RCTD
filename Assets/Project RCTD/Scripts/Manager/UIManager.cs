using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    #region Fields
    private UIController uIController;
    private float preTimeScale = 1f;
    #endregion Fields

    #region Proprety
    public float PreTimeScale { set => preTimeScale = value; get => preTimeScale; }
    #endregion

    #region UnityEngines
    private new void Awake()
    {
        if (uIController == null)
        {
            uIController = GameObject.Find("UIController").GetComponent<UIController>();
        }
    }
    #endregion UnityEngines

    #region Funcs
    public void ClickTileUI()
    {
        uIController.SetActiveClickTileUI();
    }
    public void ClickTowerUI()
    {
        uIController.SetActiveClickTowerUI();
    }
    public void ClickGroundUI()
    {
        uIController.SetActiveClickGroundUI();
    }
    public void TextUpdate(string changetextName, string text)
    {
        switch (changetextName)
        {
            case "curWave":
                uIController.CurWaveTextUpdate(text);
                break;
            case "time":
                uIController.TimeTextUpdate(text);
                break;
            case "gold":
                uIController.GoldTextUpdate(text);
                break;
            case "life":
                uIController.LifeTextUpdate(text);
                break;
        }
    }
    public void TowerInfoUpdate()
    {
        uIController.InfoTextUpdate();
    }
    public void GameResult(string result)
    { 
        uIController.SetResultUI(result);
    }
    #endregion Funcs
}
