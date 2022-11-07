using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IBuildable
{
    #region Fields
    private Tower curTower;
    private bool isBuildable = true;
    #endregion Fields

    #region Properties
    public Tower CurTower { get => curTower; }
    public bool IsBuildable { set => isBuildable = value; get => isBuildable; }
    #endregion Properties

    #region Funcs
    public void Build()
    {
        if (IsBuildable) return;
        // ������Ʈ Ǯ������ �� �������� ��ȯ
    }

    public bool BuildCheck()
    {
        if (GetComponentInChildren<Tower>() == null) return isBuildable;
        IsBuildable = false;
        curTower = GetComponentInChildren<Tower>();
        return IsBuildable;
    }
    public Tower GetTowerInfo()
    {
        if (curTower == null) return null;
        return CurTower;
    }

    public Transform GetTransForm()
    {
        return transform;
    }
    #endregion Funcs
}
