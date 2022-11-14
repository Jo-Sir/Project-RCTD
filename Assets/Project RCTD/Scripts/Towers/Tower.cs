using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IHasColorTYPE
{
    #region SerializeFields
    [SerializeField] protected TOWER_TYPE TOWER_TYPE;
    [SerializeField] protected COLOR_TYPE COLOR_TYPE;
    [SerializeField] protected SKILL_TYPE SKILL_TYPE;
    [SerializeField] protected float atkAbleRange;
    [SerializeField] protected float projectilesRange;
    [SerializeField] protected float coolTime;
    #endregion SerializeFields

    #region Fields
    protected int price;
    protected bool skillCoolTimeOn = true;
    protected bool isable;
    protected Animator animator;
    #endregion Fields

    #region Properties
    public string Name { get => TOWER_TYPE.ToString(); }
    public int Price { get => price; }
    #endregion Properties

    #region UnitiyEngine
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        SetCOLOR_TYPE();
        SetPrice();
    }
    #endregion UnitiyEngine

    #region Funcs

    protected virtual void SetPrice()
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
    public abstract void SetCOLOR_TYPE();
    #endregion Funcs


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atkAbleRange);
    }
}
