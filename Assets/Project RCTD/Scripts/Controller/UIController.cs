using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private TowerController towerController;
    [SerializeField] private TextMeshProUGUI curWave;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI life;
    [SerializeField] private GameObject clickTile;
    [SerializeField] private GameObject clickTower;
    #endregion SerializeFields

    #region Fields
    #endregion Fields

    #region UnityEngines
    private void Start()
    {
    }
    #endregion UnityEngines
    #region Funcs
    public void SetActiveClickTileUI()
    {
        clickTile.SetActive(true);
        clickTower.SetActive(false);
    }
    public void SetActiveClickTowerUI()
    {
        clickTower.SetActive(true);
        clickTile.SetActive(false);
    }
    public void CurWaveTextUpdate(string curWave)
    {
        this.curWave.text = curWave;
    }
    public void TimeTextUpdate(string time)
    {
        this.time.text = time;
    }
    public void GoldTextUpdate(string gold)
    {
        this.gold.text = gold;
    }
    public void LifeTextUpdate(string life)
    {
        this.life.text = life;
    }
    #endregion Funcs
}
