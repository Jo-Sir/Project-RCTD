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
    protected float skillProbability = 5f;
    protected IEnumerator attackCo;
    #endregion Fields

    #region Properties
    public float CurATK { get => curATK + (Upgrade * 10f); }
    public float CurAS { get => curAS; }
    public float Upgrade
    {
        get
        {
            switch (COLOR_TYPE)
            {
                case COLOR_TYPE.BLACK:
                    upgrade = GameManager.Instance.UpgradeBlackLV;
                    break;
                case COLOR_TYPE.WHITE:
                    upgrade = GameManager.Instance.UpgradeWhiteLV;
                    break;
            }
            return upgrade;
        }
    }
    public float SkillProbability
    {
        get
        {
            if (skillProbability <= 100)
            {
                return skillProbability + Upgrade;
            }
            else
            {
                return 100f;
            }
        }
    }
    public bool IsAttackable => (bool)(isAttack == false && DetectTarget());
    #endregion Properties

    #region UnityEngine
    private new void Awake()
    {
        base.Awake();
        attackCo = AttackCo();
    }
    private void OnEnable()
    {
        isable = true;
        target = null;
        isAttack = false;
        StartCoroutine(attackCo);
    }
    private void OnDisable()
    {
        isable = false;
    }
    #endregion

    #region Funcs
    public void Attack()
    {
        bool skillProbabilityboll = ((SkillProbability + Upgrade) >= Random.Range(0f, 100f));
        animator.Play("TowerAttack");
        StartCoroutine(AttackCool(CurAS));
        if (skillProbabilityboll && skillCoolTimeOn) { UseSkill(); }
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
    protected IEnumerator AttackCo()
    {
        while (isable)
        { 
            yield return new CustomIsAttackableCo(this);
            if (IsAttackable == false){ continue;}
            Attack();
        }
        StopCoroutine(attackCo);
    }
    protected virtual IEnumerator AttackCool(float CurAS)
    {
        isAttack = true;
        yield return new WaitForSeconds(CurAS);
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
