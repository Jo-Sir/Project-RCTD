using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill
{
    #region SerializeField
    [SerializeField] protected float skillCoefficient;
    [SerializeField] protected float baseSkillAtk;
    #endregion

    #region Fields
    protected float totalAtk;
    #endregion

    public override void SKillSetting(float towerAtk)
    {
        totalAtk = (baseSkillAtk + (towerAtk * (skillCoefficient * 0.01f)));
        Debug.Log("totalAtk : " + totalAtk.ToString()); ;
    }

    protected override void Use()
    {
        skillActive= true;
        Collider[] targets = Physics.OverlapSphere(transform.position, range, layerMask);
        if (targets.Length > 0)
        {
            foreach (Collider target in targets)
            {
                target.GetComponent<IDamagable>().TakeHit(totalAtk);
            }
        }
    }
}
