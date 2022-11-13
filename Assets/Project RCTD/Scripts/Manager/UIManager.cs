using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    #region Fields
    private UIController uIController;
    private float preTimeScale;
    #endregion Fields

    #region Proprety
    public float PreTimeScale { set => preTimeScale = value; get => preTimeScale; }
    #endregion

    #region UnityEngines
    private new void Awake()
    {
        base.Awake();
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
    #endregion Funcs
}
