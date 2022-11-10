using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackTower : Tower, IAttackable
{
    #region SerializeFields
    [SerializeField] protected float baseATK;
    [SerializeField] protected float baseAS;
    [SerializeField] protected Transform target;
    [SerializeField] protected LayerMask targetLayerMask;
    #endregion SerializeFields

    #region Fields
    protected float curATK;
    protected float curAS;
    protected float upgrade = 0;
    protected bool isAttack = false;
    protected float skillProbability = 10f;
    #endregion Fields

    #region Properties
    public float CurATK { get => baseATK + Upgrade; }
    public float CurAS { get => baseAS; }
    public float Upgrade { set => upgrade = value; get => upgrade * baseATK; }
    public float SkillProbability { set => skillProbability = skillProbability + value; get => skillProbability; }
    #endregion Properties

    #region UnityEngine
    private void Update()
    {
        if (isAttack == false && DetectTarget())
        { Attack(); }
    }
    #endregion

    #region Funcs
    public void Attack()
    {
        animator.Play("TowerAttack");
        StartCoroutine(AttackCool(baseAS));
        if (skillCoolTimeOn && (SkillProbability >= Random.Range(0f, 100f))) { UseSkill(); }
        GameObject obj = GameManager.Instance.ObjectGet(COLOR_TYPE, this.transform);
        obj.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        Projectiles projectiles = obj.GetComponentInChildren<Projectiles>();
        projectiles.transform.SetParent(null);
        projectiles.ProjectilesSet(COLOR_TYPE, CurATK, projectilesRange, target, targetLayerMask);
    }
    public bool DetectTarget()
    {
        Collider[] targetColliders = Physics.OverlapSphere(this.transform.position, atkAbleRange, targetLayerMask);
        float curDist = 100f;
        if (targetColliders.Length > 0)
        {
            foreach (Collider target in targetColliders)
            {
                Vector3 targetVec = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
                float distanceToTarget = (targetVec - transform.position).sqrMagnitude;
                if (curDist >= (distanceToTarget))
                {
                    curDist = distanceToTarget;
                    this.target = target.transform;
                }
            }
        }
        else
        {
            this.target = null;
        }
        return this.target != null;
    }
    protected abstract void UseSkill();
    public override void SetCOLOR_TYPE()
    {
        switch (COLOR_TYPE)
        {
            case COLOR_TYPE.BLACK:
                curATK = baseATK * 0.5f;
                curAS = baseAS * 0.5f;
                break;
            case COLOR_TYPE.WHITE:
                curATK = baseATK * 1.5f;
                curAS = baseAS * 1.5f;
                projectilesRange = 0f;
                break;
            default:
                break;
        }
    }
    #endregion
    
    #region IEnumerators
    protected virtual IEnumerator AttackCool(float baseAS)
    {
        isAttack = true;
        yield return new WaitForSeconds(baseAS);
        isAttack = false;
    }
    protected virtual IEnumerator SkillCollTime()
    {
        skillCoolTimeOn = false;
        yield return new WaitForSeconds(coolTime);
        skillCoolTimeOn = true;
    }
    #endregion IEnumerators
}
