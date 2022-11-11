using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillKing : AttackSkill
{
    protected override void Use()
    {
        skillActive = true;
        Collider[] targets = Physics.OverlapSphere(transform.position, range, layerMask);
        if (targets.Length > 0)
        {
            foreach (Collider target in targets)
            {
                target.GetComponent<Creep>().SetDeBuff(DEBUFF_TYPE.SLOW, 3f, 1.5f);
                target.GetComponent<IDamagable>().TakeHit(totalAtk);
            }
        }
    }
}
