using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Projectiles : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    // [SerializeField] private 
    #endregion

    #region Fields
    private COLOR_TYPE COLOR_TYPE;
    private Transform target;
    private float atk;
    private float range;
    private LayerMask targetLayerMask;
    #endregion Fields

    #region UnityEngines
    void Update()
    {
        Launch();
    }
    private void OnEnable()
    {
        StartCoroutine(LifeTime());
    }
    #endregion UnityEngines

    #region Funcs
    private void Launch()
    {
        transform.LookAt(target);
        Vector3 targetVec = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        float distanceToTarget = (targetVec - transform.position).sqrMagnitude;
        if (distanceToTarget > Mathf.Pow(0.1f, 2))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetVec, speed* Time.deltaTime);
            if (distanceToTarget < Mathf.Pow(0.2f, 2))
            {
                Arrived(target);
            }
        }
    }
    public void ProjectilesSet(COLOR_TYPE COLOR_TYPE, float atk, float range, Transform target, LayerMask targetLayerMask)
    {
        this.COLOR_TYPE = COLOR_TYPE;
        this.atk = atk;
        this.range = range;
        this.target = target;
        this.targetLayerMask = targetLayerMask;
    }
    private void Arrived(Transform inputTarget)
    {
        switch (COLOR_TYPE)
        {
            case COLOR_TYPE.BLACK:
                Collider[] targets = Physics.OverlapSphere(transform.position, range, targetLayerMask);
                if (targets.Length > 0)
                {
                    foreach(var target in targets)
                    {
                        target.GetComponent<Creep>()?.TakeHit(atk);
                    }
                }
                break;
            case COLOR_TYPE.WHITE:
                inputTarget.GetComponent<Creep>()?.TakeHit(atk);
                break;
            default:
                break;
        }
        // ¹Ý³³
        GameManager.Instance.ObjectReturn(COLOR_TYPE, this.gameObject);
    }


    #endregion Funcs

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        GameManager.Instance.ObjectReturn(COLOR_TYPE, this.gameObject);
    }
}
