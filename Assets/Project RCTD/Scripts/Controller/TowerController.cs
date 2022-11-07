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
    /// Ŭ���� ���� Ÿ���̳� Ÿ�� ��ȣ�ۿ�
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
                    // ture �Ǽ� ����
                    // UI Ÿ����ȯ ��ư ���� ����
                    UIManager.Instance.ClickTileUI();
                }
                else
                {
                    // false �Ǽ� �Ұ�
                    curTower = curTile.GetTowerInfo(); // �̺κж����� IBuildable �� GetTowerInfo�� �ʿ��Ѱ�?
                    UIManager.Instance.ClickTowerUI();
                    // UI Ÿ�� �Ǹ�/ Ÿ�� �ռ� ��ư
                    // Ÿ�� �ռ��ϸ� ������ ���Ǿ�� ���� Ÿ�� ��� ã�� �Ÿ� ��� �� �������ִ°Ͱ� ������ ���ָ鼭 ����Ÿ������ ����.
                    // Ÿ�� ���� ������ �ؿ��ų������ϱ�
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
    ///  Ÿ������
    /// </summary>
    public void TowerPurchase()
    {
        int[] towerRange = { 0, 1 };
        Enum key = ((TOWER_TYPE)UnityEngine.Random.Range(towerRange[0], towerRange[1]));
        GameManager.Instance.ObjectGet(key, curTile.GetTransForm());
    }
    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    public void TowerCombination()
    {
        TOWER_TYPE TOWER_TYPE = (TOWER_TYPE)Enum.Parse(typeof(TOWER_TYPE), curTower.Name);
        Collider[] targets = Physics.OverlapSphere(this.transform.position, 100f, 1);// ���̾��ũ Ÿ���ΰ˻�
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
        // ���� ������Ʈ �ΰ� �ݳ�
        GameManager.Instance.ObjectReturn(TOWER_TYPE, curTower.gameObject);
        GameManager.Instance.ObjectReturn(TOWER_TYPE, sameTraget.gameObject);
        Enum key = ((TOWER_TYPE)UnityEngine.Random.Range(towerRange[0], towerRange[1]));
        // �Ѵܰ����� ������Ʈ ������ �ڸ����� ����
        GameManager.Instance.ObjectGet(key, parentTransform);
        // choiceTarget �� sameTraget ������Ʈ Ǯ�� ��ȯ �� TOWER_TYPE
    }
    /// <summary>
    /// Ÿ�� �Ǹ�
    /// </summary>
    public void TowerSale()
    {
        if (curTower == null) return;
        Enum key = (TOWER_TYPE)Enum.Parse(typeof(TOWER_TYPE), curTower.Name);
        GameManager.Instance.Gold += curTower.Price;
        GameManager.Instance.ObjectReturn(key, curTower.gameObject);
    }
    /// <summary>
    /// Ÿ�� ����
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
