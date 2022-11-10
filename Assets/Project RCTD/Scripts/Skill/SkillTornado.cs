using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTornado : AttackSkill
{
    protected override void Use()
    {
        skillActive = true;
        StartCoroutine(TornadoAttack());
    }

    IEnumerator TornadoAttack()
    {
        for (float i = 0; i < 3; i+=0.1f)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, range, layerMask);
            if (targets.Length > 0)
            {
                foreach (Collider target in targets)
                {
                    target.GetComponent<IDamagable>().TakeHit(totalAtk);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        particleSystem.Stop();
    }
}
