using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkillTornado : AttackSkill
{
    private Transform target = null;
    private void Update()
    {
        TraceTarget();
    }
    protected override void Use()
    {
        skillActive = true;
        StartCoroutine(TornadoAttack());
    }
    private void TraceTarget()
    {
        if (target == null) return;
        transform.LookAt(target);
        transform.rotation = Quaternion.Euler(-90f, 0, 0);
        Vector3 targetVec = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        float distanceToTarget = (targetVec - transform.position).sqrMagnitude;
        if (distanceToTarget > Mathf.Pow(0.1f, 2))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetVec, 3f * Time.deltaTime);
        }
    }

    IEnumerator TornadoAttack()
    {
        for (float i = 0; i < 3; i+=0.01f)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, range, layerMask);
            if (targets.Length > 0)
            {
                target = targets[0].transform;
                foreach (Collider target in targets)
                {
                    target.GetComponent<IDamagable>().TakeHit(totalAtk);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
        particleSystem.Stop();
    }
}
