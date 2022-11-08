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
        SetCOLOR_TYPE();
        SetPrice();
    }
    private void Update()
    {
        if (DetectTarget() && isAttack == false)
        { Attack(); }
    }
    #endregion UnitiyEngine

    #region Funcs
    public void Attack()
    {
        StartCoroutine(AttackCool(baseAS));
        LooksTarget(target, 10f);
        // 투사체 풀링에서 가져오기
        GameManager.Instance.ObjectGet(COLOR_TYPE, this.transform);
        Projectiles projectiles = GetComponentInChildren<Projectiles>();
        // 풀링해온 오브젝트에 정보전달
        projectiles.ProjectilesSet(COLOR_TYPE, curATK, projectilesRange, target, targetLayerMask);
        // 부모해제
        projectiles.transform.SetParent(null);
    }
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
    public void SetCOLOR_TYPE()
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
