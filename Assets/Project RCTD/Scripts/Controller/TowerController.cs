using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class TowerController : MonoBehaviour
{
    #region Fields
    private RaycastHit hit;
    private Ray ray;
    private Tower curTower = null;
    private IBuildable curTile = null;
    #endregion Fields

    #region Property
    public Tower CurTower
    {
        get => curTower;
    }
    #endregion

    #region UnityEngines
    private void Awake()
    {
        StartCoroutine(InteractionCo());
    }
    #endregion UnityEngines

    #region Funcs
    /// <summary>
    /// 클릭한 곳의 타일이나 타워 상호작용
    /// </summary>
    private void Interaction()
    {
#if UNITY_EDITOR
        if (hit.transform != null) curTile.ParticleOnOff(false);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID
        if (IsPointerOverUIObject()) { return; }
        if (hit.transform != null) curTile.ParticleOnOff(false);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);  
        }
#endif
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 7))
        {
            if (hit.transform.GetComponent<IBuildable>() == null) { return; }
            curTile = hit.transform.GetComponent<IBuildable>();
            curTile.ParticleOnOff(true);
            if (curTile.BuildCheck(out curTower)) 
            {  UIManager.Instance.ClickTileUI(); }
            else
            {
                UIManager.Instance.ClickTowerUI();
                UIManager.Instance.TowerPurchaseButtonUpdate();
            }
            UIManager.Instance.TowerInfoUpdate();
        }
        else
        {
            if (curTile != null) {curTile.ParticleOnOff(false); }
            curTile = null;
            curTower = null;
            UIManager.Instance.ClickGroundUI();
        }
    }
    /// <summary>
    ///  타워생성
    /// </summary>
    public void TowerPurchase()
    {
        if (GameManager.Instance.Gold < 200) { return; }
        if (curTile.BuildCheck(out curTower) == false) { return; }
        Enum key = ((TOWER_TYPE)UnityEngine.Random.Range(0, 2));
        if (curTower != null) { return; }
        if (curTile == null) { return; }
        GameObject obj = GameManager.Instance.ObjectGet(key, curTile.GetTransForm());
        obj.transform.SetParent(curTile.GetTransForm());
        obj.transform.position += new Vector3(0, 0.5f, 0);
        GameManager.Instance.Gold -= 200;
        curTower = obj.GetComponent<Tower>();
        UIManager.Instance.ClickTowerUI();
        UIManager.Instance.TowerPurchaseButtonUpdate();
    }
    /// <summary>
    /// 타워 조합
    /// </summary>
    public void TowerCombination()
    {
        TOWER_TYPE TOWER_TYPE = (TOWER_TYPE)Enum.Parse(typeof(TOWER_TYPE), curTower.Name);
        Collider[] targets = Physics.OverlapSphere(transform.position, 30f, 1 << 8);// 레이어마스크 타워로검색
        Transform sameTraget = null;
        foreach (var curtarget in targets)
        {
            if (curTower.transform == curtarget.transform) continue;
            if (curTower.Name.Equals(curtarget.transform.GetComponent<Tower>().Name))
            {
                sameTraget = curtarget.GetComponent<Transform>();
                break;
            }
        }
        if (sameTraget == null) return;
        int[] towerRange = new int[2];
        if (0 >= (int)TOWER_TYPE || (int)TOWER_TYPE <= 1)
        {
            towerRange[0] = 2;
            towerRange[1] = 8;

        }
        else if (2 >= (int)TOWER_TYPE || (int)TOWER_TYPE <= 7)
        {
            towerRange[0] = 8;
            towerRange[1] = 12;
        }
        else
        {
            return;
        }
        // 소환 할 위치
        Transform parentTransform = curTower.transform.parent;
        // 같은 오브젝트 두개 반납
        GameManager.Instance.ObjectReturn(TOWER_TYPE, curTower.gameObject);
        GameManager.Instance.ObjectReturn(TOWER_TYPE, sameTraget.gameObject);
        Enum key = ((TOWER_TYPE)UnityEngine.Random.Range(towerRange[0], towerRange[1]));
        // Debug.Log("다음 단계 : " + key.ToString());
        // 한단계위의 오브젝트 선택한 자리에서 생성
        GameObject obj = GameManager.Instance.ObjectGet(key, parentTransform);
        obj.transform.SetParent(curTile.GetTransForm());
        obj.transform.position += new Vector3(0, 0.5f, 0);
        curTower = obj.GetComponent<Tower>();
        // choiceTarget 과 sameTraget 오브젝트 풀에 반환 뒤 TOWER_TYPE
    }
    /// <summary>
    /// 타워 판매
    /// </summary>
    public void TowerSale()
    {
        if (curTower == null) return;
        Enum key = (TOWER_TYPE)Enum.Parse(typeof(TOWER_TYPE), curTower.Name);
        curTile = curTower.GetComponentInParent<Tile>();
        GameManager.Instance.Gold += curTower.Price;
        GameManager.Instance.ObjectReturn(key, curTower.gameObject);
        UIManager.Instance.ClickTileUI();
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    #endregion Funcs

    #region IEnumerator
    private IEnumerator InteractionCo()
    {
        while (GameManager.Instance.GameOver == false)
        {
#if UNITY_EDITOR
            yield return new CustomInputTouchCo(true);
#elif UNITY_ANDROID
            yield return new CustomInputTouchCo(false);
#endif
            Interaction();
        }
    }
#endregion
}
