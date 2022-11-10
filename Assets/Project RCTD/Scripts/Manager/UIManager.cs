using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    #region Fields
    private UIController uIController;
    #endregion Fields

    #region UnityEngines
    private void Awake()
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
    #endregion Funcs
}
