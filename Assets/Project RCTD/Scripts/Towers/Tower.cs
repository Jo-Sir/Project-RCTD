using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IAttackable, IhasColorTYPE
{
    #region SerializeFields
    [SerializeField] protected TOWER_TYPE TOWER_TYPE;
    [SerializeField] protected COLOR_TYPE COLOR_TYPE;
    [SerializeField] protected float baseATK;
    [SerializeField] protected float baseAS;
    [SerializeField] protected float atkAbleRange;
    [SerializeField] protected Transform target;
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] protected float projectilesRange;
    #endregion SerializeFields

    #region Fields
    protected float curATK;
    protected float curAS;
    protected float upgrade = 0;
    private int price;
    private bool isAttack = false;
    private Animator animator;
    #endregion Fields

    #region Properties
    public string Name { get => TOWER_TYPE.ToString(); }
    public float CurATK { get => baseATK + Upgrade; }
    public float CurAS { get => baseAS; }
    public float Upgrade { set => upgrade = value; get => upgrade * baseATK; }
    public int Price { get => price; }
    #endregion Properties

    #region UnitiyEngine
    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetCOLOR_TYPE();
        SetPrice();
    }
    private void FixedUpdate()
    {
        if (isAttack == false && DetectTarget())
        { Attack(); }
    }
    #endregion UnitiyEngine

    #region Funcs
    public void Attack()
    {
        animator.Play("TowerAttack");
        StartCoroutine(AttackCool(baseAS));
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
    public void SetCOLOR_TYPE()
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
    private void SetPrice()
    {
        switch (TOWER_TYPE)
        {
            case TOWER_TYPE.BLACK_PAWN:
            case TOWER_TYPE.WHITE_PAWN:
                price = 100;
                break;
            case TOWER_TYPE.BLACK_KNIGHT:
            case TOWER_TYPE.WHITE_KNIGHT:
            case TOWER_TYPE.BLACK_ROOK:
            case TOWER_TYPE.WHITE_ROOK:
            case TOWER_TYPE.BLACK_BISHOP:
            case TOWER_TYPE.WHITE_BISHOP:
                price = 200;
                break;
            case TOWER_TYPE.BLACK_QUEEN:
            case TOWER_TYPE.WHITE_QUEEN:
            case TOWER_TYPE.BLACK_KING:
            case TOWER_TYPE.WHITE_KING:
                price = 400;
                break;
            default:
                break;
        }
    }
    protected void LooksTarget(Transform targetEnInfo, float rotationSpeed)
    {
        if (targetEnInfo != null)
        {
            Vector3 dir = targetEnInfo.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
        }
    }
    #endregion Funcs

    #region IEnumerators
    protected virtual IEnumerator AttackCool(float baseAS)
    {
        isAttack = true;
        yield return new WaitForSeconds(baseAS);
        isAttack = false;
    }
    #endregion IEnumerators
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atkAbleRange);
    }
}
