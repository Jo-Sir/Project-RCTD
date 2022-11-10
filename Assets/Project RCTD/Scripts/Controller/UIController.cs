using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private TextMeshProUGUI curWaveText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI gameSpeed;
    [SerializeField] private TextMeshProUGUI upgradeBlackLVText;
    [SerializeField] private TextMeshProUGUI upgradeWhiteLVText;
    [SerializeField] private TextMeshProUGUI upgradeBlackPriceText;
    [SerializeField] private TextMeshProUGUI upgradeWhitePriceText;
    [SerializeField] private TextMeshProUGUI infoNameText;
    [SerializeField] private TextMeshProUGUI infoCurATKText;
    [SerializeField] private TextMeshProUGUI infoSkillProbabilityText;
    [SerializeField] private GameObject clickTile;
    [SerializeField] private GameObject clickTower;
    [SerializeField] private GameObject clickGround;
    [SerializeField] private GameObject clickMission;
    [SerializeField] private GameObject clickUpgrade;
    [SerializeField] private GameObject clickInfo;
    [SerializeField] private Button buttonMissionOne;
    [SerializeField] private Button buttonMissionTwo;
    [SerializeField] private Button buttonMissionThree;
    #endregion SerializeFields

    #region Fields
    private StageController stageController;
    private TowerController towerController;
    private int upgradeBlackPrice = 100;
    private int upgradeWhitePrice = 100;
    #endregion Fields

    #region UnityEngines
    private void Awake()
    {
        towerController = GameObject.Find("TowerController").GetComponent<TowerController>();
        stageController = GameObject.Find("StageController").GetComponent<StageController>();
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
    public void CurWaveTextUpdate(string curWaveText)
    {
        this.curWaveText.text = curWaveText;
    }
    public void TimeTextUpdate(string timeText)
    {
        this.timeText.text = timeText;
    }
    public void GoldTextUpdate(string goldText)
    {
        this.goldText.text = goldText;
    }
    public void LifeTextUpdate(string lifeText)
    {
        this.lifeText.text = lifeText;
    }
    private void UpgradeTextUpdate(COLOR_TYPE cOLOR_TYPE)
    {
        switch (cOLOR_TYPE)
        {
            case COLOR_TYPE.BLACK:
                upgradeBlackLVText.text = GameManager.Instance.UpgradeBlackLV.ToString();
                upgradeBlackPriceText.text = upgradeBlackPrice.ToString();
                break;
            case COLOR_TYPE.WHITE:
                upgradeWhiteLVText.text = GameManager.Instance.UpgradeWhiteLV.ToString();
                upgradeWhitePriceText.text = upgradeWhitePrice.ToString();
                break;
        }
    }
    public void InfoTextUpdate()
    {
        if (towerController.CurTower == null) return;
        AttackTower attackTower = (AttackTower)towerController.CurTower;
        infoNameText.text = attackTower.Name.ToString();
        infoCurATKText.text = attackTower.CurATK.ToString();
        if ((TOWER_TYPE)Enum.Parse(typeof(TOWER_TYPE), attackTower.Name) == TOWER_TYPE.BLACK_PAWN ||
            (TOWER_TYPE)Enum.Parse(typeof(TOWER_TYPE), attackTower.Name) == TOWER_TYPE.WHITE_PAWN)
        { infoSkillProbabilityText.text = "NAN"; }
        else 
        { infoSkillProbabilityText.text = attackTower.SkillProbability.ToString(); }
        
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
    public void SetActiveClickMission()
    {
        clickMission.SetActive(true);
    }
    public void SetActiveClickUpgrage()
    {
        clickUpgrade.SetActive(true);
    }
    public void SetActiveClickInfo()
    {
        clickInfo.SetActive(true);
        InfoTextUpdate();
    }
    public void SetActiveClickMissionBack()
    {
        clickMission.SetActive(false);
    }
    public void SetActiveClickUpgrageBack()
    {
        clickUpgrade.SetActive(false);
    }
    public void SetActiveClickInfoBack()
    {
        clickInfo.SetActive(false);
    }
    public void ClickSpawn()
    {
        towerController.TowerPurchase();
    }
    public void ClickCombination()
    {
        towerController.TowerCombination();
    }
    public void ClickSale()
    {
        towerController.TowerSale();
    }
    public void ClickMissionStart(int step)
    {
        stageController.MissionStart(step);
        StartCoroutine(MissionCooltime(step));
    }
    public void ClickUpgrade(string color_typestr)
    {
        COLOR_TYPE cOLOR_TYPE = (COLOR_TYPE)Enum.Parse(typeof(COLOR_TYPE), color_typestr);
        switch (cOLOR_TYPE)
        {
            case COLOR_TYPE.BLACK:
                GameManager.Instance.UpgradeBlackLV += 1;
                GameManager.Instance.Gold -= upgradeBlackPrice;
                upgradeBlackPrice += 10;
                UpgradeTextUpdate(cOLOR_TYPE);
                break;
            case COLOR_TYPE.WHITE:
                GameManager.Instance.UpgradeWhiteLV += 1;
                GameManager.Instance.Gold -= upgradeWhitePrice;
                upgradeWhitePrice +=10;
                UpgradeTextUpdate(cOLOR_TYPE);
                break;
        }

    }
    #endregion Funcs
    IEnumerator MissionCooltime(int step)
    {
        switch (step)
        {
            case 1:
                buttonMissionOne.interactable = false;
                break;
            case 2:
                buttonMissionTwo.interactable = false;
                break;
            case 3:
                buttonMissionThree.interactable = false;
                break;
        }
        yield return new WaitForSeconds(20);
        switch (step)
        {
            case 1:
                buttonMissionOne.interactable = true;
                break;
            case 2:
                buttonMissionTwo.interactable = true;
                break;
            case 3:
                buttonMissionThree.interactable = true;
                break;
        }
    }
}
