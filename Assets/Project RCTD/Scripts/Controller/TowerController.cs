using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1))
            {
                curTile = hit.transform.GetComponent<IBuildable>();
                if(curTile == null) return;
                if (curTile.BuildCheck())
                {
                    // ture 건설 가능
                    // UI 타워소환 버튼 으로 변경
                    UIManager.Instance.ClickTileUI();
                }
                else
                {
                    // false 건설 불가
                    curTower = curTile.GetTowerInfo(); // 이부분때문에 IBuildable 에 GetTowerInfo가 필요한가?
                    UIManager.Instance.ClickTowerUI();
                    // UI 타워 판매/ 타워 합성 버튼
                    // 타워 합성하면 오버랩 스피어로 같은 타워 모두 찾고 거리 계산 후 가까이있는것과 누른거 없애면서 다음타워랜덤 생성.
                    // 타워 정보 나오면 밑에거나오게하기
                }
                Debug.Log(curTile.BuildCheck());
            }
            else
            {
                curTile = null;
                curTower = null;
            }
        }
    }
    /// <summary>
    ///  타워생성
    /// </summary>
    public void TowerPurchase()
    {
        int[] towerRange = { 0, 1 };
        Enum key = ((TOWER_TYPE)UnityEngine.Random.Range(towerRange[0], towerRange[1]));
        GameManager.Instance.ObjectGet(key, curTile.GetTransForm());
    }
    /// <summary>
    /// 타워 조합
    /// </summary>
    public void TowerCombination()
    {
        TOWER_TYPE TOWER_TYPE = (TOWER_TYPE)Enum.Parse(typeof(TOWER_TYPE), curTower.Name);
        Collider[] targets = Physics.OverlapSphere(this.transform.position, 100f, 1);// 레이어마스크 타워로검색
        Transform sameTraget = null;
        foreach (var curtarget in targets)
        {
            if (curTower.Name.Equals(curtarget.transform.GetComponent<Tower>().Name))
            {
                sameTraget = curtarget.GetComponent<Transform>();
                break;
            }
            else { continue; }
        }
        if (sameTraget == null) return;
        int[] towerRange = new int[2];
        if (0 >= (int)TOWER_TYPE && (int)TOWER_TYPE <= 1)
        {
            towerRange[0] = 2;
            towerRange[1] = 7;

        }
        else if (2 >= (int)TOWER_TYPE && (int)TOWER_TYPE <= 7)
        {
            towerRange[0] = 8;
            towerRange[1] = 11;
        }
        else return;
        Transform parentTransform = curTower.transform.parent;
        // 같은 오브젝트 두개 반납
        GameManager.Instance.ObjectReturn(TOWER_TYPE, curTower.gameObject);
        GameManager.Instance.ObjectReturn(TOWER_TYPE, sameTraget.gameObject);
        Enum key = ((TOWER_TYPE)UnityEngine.Random.Range(towerRange[0], towerRange[1]));
        // 한단계위의 오브젝트 선택한 자리에서 생성
        GameManager.Instance.ObjectGet(key, parentTransform);
        // choiceTarget 과 sameTraget 오브젝트 풀에 반환 뒤 TOWER_TYPE
    }
    /// <summary>
    /// 타워 판매
    /// </summary>
    public void TowerSale()
    {
        if (curTower == null) return;
        Enum key = (TOWER_TYPE)Enum.Parse(typeof(TOWER_TYPE), curTower.Name);
        GameManager.Instance.Gold += curTower.Price;
        GameManager.Instance.ObjectReturn(key, curTower.gameObject);
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
    }
}
