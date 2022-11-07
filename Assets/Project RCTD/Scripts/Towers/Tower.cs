using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IAttackable, IColorable
{
    #region SerializeFields
    [SerializeField] protected TOWER_TYPE TOWER_TYPE;
    [SerializeField] protected COLOR_TYPE COLOR_TYPE;
    [SerializeField] protected float baseATK;
    [SerializeField] protected float baseAS;
    [SerializeField] protected float atkAbleRange;
    [SerializeField] protected float projectilesRange;
    [SerializeField] protected Transform target;
    [SerializeField] protected LayerMask targetLayerMask;
    #endregion SerializeFields

    #region Fields
    protected float curATK;
    protected float curAS;
    protected float upgrade;
    private bool isAttack = false;
    private int price;
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
        SetColor();
        SetPrice();
    }
    private void Update()
    {
        if (DetectTarget() && isAttack == false)
        { Attack(); }
    }
    #endregion UnitiyEngine

    #region Funcs
    public abstract void Attack();
    public bool DetectTarget()
    {
        if (target == null)
        {
            Collider[] target = Physics.OverlapSphere(this.transform.position, atkAbleRange, targetLayerMask);
            if (target.Length > 0)
            {
                this.target = target[0].transform;
            }
            else
            {
                this.target = null;
            }
        }
        return this.target != null;
    }
    public void SetColor()
    {
        switch (COLOR_TYPE)
        {
            case COLOR_TYPE.BLACK:
                baseATK *= 0.5f;
                baseAS *= 0.5f;
                break;
            case COLOR_TYPE.WHITE:
                baseATK *= 1.5f;
                baseAS *= 1.5f;
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
                price = 100;
                break;
            case TOWER_TYPE.WHITE_PAWN:
                price = 100;
                break;
            case TOWER_TYPE.BLACK_KNIGHT:
                price = 200;
                break;
            case TOWER_TYPE.BLACK_ROOK:
                price = 200;
                break;
            case TOWER_TYPE.BLACK_BISHOP:
                price = 200;
                break;
            case TOWER_TYPE.WHITE_KNIGHT:
                price = 200;
                break;
            case TOWER_TYPE.WHITE_ROOK:
                price = 200;
                break;
            case TOWER_TYPE.WHITE_BISHOP:
                price = 200;
                break;
            case TOWER_TYPE.BLACK_QUEEN:
                price = 400;
                break;
            case TOWER_TYPE.BLACK_KING:
                price = 400;
                break;
            case TOWER_TYPE.WHITE_QUEEN:
                price = 400;
                break;
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
