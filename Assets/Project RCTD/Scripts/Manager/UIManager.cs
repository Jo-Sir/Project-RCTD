using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    #region Fields
    public UIController uIController;
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
    { }
    public void ClickTowerUI()
    { }
    #endregion Funcs
}
