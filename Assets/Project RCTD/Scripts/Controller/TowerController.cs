using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TowerController : MonoBehaviour
{
    #region Fields
    private RaycastHit hit;
    private Tower curTower = null;
    private IBuildable curTile = null;
    #endregion Fields

    #region UnityEngines
    private void Awake()
    {
        transform.position = Camera.main.transform.position;
    }
    private void Update()
    {
        Interaction();
    }
    #endregion UnityEngines

    #region Funcs
    /// <summary>
    /// 클릭한 곳의 타일이나 타워 상호작용
    /// </summary>
    private void Interaction()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (hit.transform != null) curTile.ParticleOnOff(false); ;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 7))
            {
                if (hit.transform.GetComponent<IBuildable>() == null) return;
                curTile = hit.transform.GetComponent<IBuildable>();
                curTile.ParticleOnOff(true);
                if (curTile.BuildCheck(out curTower))
                {
                    UIManager.Instance.ClickTileUI();
                }
                else
                {
                    UIManager.Instance.ClickTowerUI();
                }
            }
            else
            {
                if (curTile != null) curTile.ParticleOnOff(false);
                curTile = null;
                curTower = null;
                UIManager.Instance.ClickGroundUI();
            }
        }
    }
    /// <summary>
    ///  타워생성
    /// </summary>
    public void TowerPurchase()
    {
        if (GameManager.Instance.Gold < 200) return;
        if (curTile.BuildCheck(out curTower) == false) return;
        int[] towerRange = { 0, 2 };
        Enum key = ((TOWER_TYPE)UnityEngine.Random.Range(towerRange[0], towerRange[1]));
        if (curTower != null) return;
        if (curTile == null) return;
        GameObject obj = GameManager.Instance.ObjectGet(key, curTile.GetTransForm());
        obj.transform.SetParent(curTile.GetTransForm());
        obj.transform.position += new Vector3(0, 0.5f, 0);
        GameManager.Instance.Gold -= 200;
        curTower = obj.GetComponent<Tower>();
        UIManager.Instance.ClickTowerUI();
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
        // Debug.Log((int)TOWER_TYPE);
        if (0 >= (int)TOWER_TYPE || (int)TOWER_TYPE <= 1)
        {
            // Debug.Log("2단계");
            towerRange[0] = 2;
            towerRange[1] = 8;

        }
        else if (2 >= (int)TOWER_TYPE || (int)TOWER_TYPE <= 7)
        {
            // Debug.Log("3단계");
            towerRange[0] = 8;
            towerRange[1] = 12;
        }
        else
        {
            return;
        }
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
    /// <summary>
    /// 타워 정보
    /// </summary>
    public Tower TowerInfo()
    {
        return curTower;
    }
    #endregion Funcs
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, hit.point);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 30f);
    }
}
