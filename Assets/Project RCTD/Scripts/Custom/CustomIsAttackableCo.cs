using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomIsAttackableCo : IEnumerator
{
    private AttackTower obj;
    private bool isNext;
    /// <summary>
    /// isnext = true
    /// !EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0)
    /// isnext = false
    /// Input.GetMouseButtonDown(0);
    /// </summary>
    /// <param isnext="value"></param>
    public CustomIsAttackableCo(AttackTower value)
    {
        obj = value;
    }

    public object Current
    {
        get
        {
            return !obj.IsAttackable;
        }
    }

    public bool MoveNext()
    {
        return !obj.IsAttackable;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}
