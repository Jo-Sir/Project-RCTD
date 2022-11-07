using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    #region Fields
    private COLOR_TYPE COLOR_TYPE;
    private Transform target;
    private float atk;
    private float speed;
    private float range;
    private LayerMask targetLayerMask;
    #endregion Fields

    #region UnityEngines
    void Update()
    {
        Launch();
    }
    private void OnTriggerEnter(Collider other)
    {
        Arrived(other);
    }
    #endregion UnityEngines

    #region Funcs
    private void Launch()
    {
        transform.LookAt(target);
        Vector3 newPostion = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.position = newPostion;
    }
    public void ProjectilesSet(COLOR_TYPE COLOR_TYPE, float atk, float range, Transform target, LayerMask targetLayerMask)
    {
        this.COLOR_TYPE = COLOR_TYPE;
        this.atk = atk;
        this.range = range;
        this.target = target;
        this.targetLayerMask = targetLayerMask;
    }
    private void Arrived(Collider other)
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
                other.GetComponent<Creep>()?.TakeHit(atk);
                break;
            default:
                break;
        }
        // ¹Ý³³
        GameManager.Instance.ObjectReturn(COLOR_TYPE, this.gameObject);
    }
    #endregion Funcs
}
