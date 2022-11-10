using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private TextMeshProUGUI curWave;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI life;
    [SerializeField] private TextMeshProUGUI gameSpeed;
    [SerializeField] private GameObject clickTile;
    [SerializeField] private GameObject clickTower;
    [SerializeField] private GameObject clickGround;
    #endregion SerializeFields

    #region Fields
    #endregion Fields

    #region UnityEngines
    private void Awake()
    {
    }
    #endregion UnityEngines

    #region Funcs
    public void SetActiveClickTileUI()
    {
        clickTile.SetActive(true);
        clickTower.SetActive(false);
        clickGround.SetActive(false);
    }
    public void SetActiveClickTowerUI()
    {
        clickTower.SetActive(true);
        clickTile.SetActive(false);
        clickGround.SetActive(false);
    }
    public void SetActiveClickGroundUI()
    {
        clickGround.SetActive(true);
        clickTower.SetActive(false);
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
    public void ChangeGameSpeed()
    {
        if (Time.timeScale > 1f)
        {
            Time.timeScale = 1f;
            gameSpeed.text = "1.0x";
        }
        else if (Time.timeScale <= 1f)
        {
            Time.timeScale = 2f;
            gameSpeed.text = "2.0x";
        }
    }
    #endregion Funcs
}
