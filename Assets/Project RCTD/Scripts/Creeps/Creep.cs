using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent (typeof(NavMeshAgent))]
public class Creep : MonoBehaviour, IDamagable
{
    #region SerializeFields
    [SerializeField] private ROUND_TYPE ROUND_TYPE;
    [SerializeField] private float hp;
    [SerializeField] private float moveSpeed;
    #endregion SerializeFields

    #region Fields
    private NavMeshAgent agent;
    private Transform destination;
    #endregion Fields

    #region Properties
    public float Hp 
    {
        set => hp = value;
        get 
        { 
            if (hp <= 0)
            { 
                // 오브젝트 풀에 반납
            }
            return hp;
        }
    }
    #endregion Properties

    #region UnityEngines
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        agent.SetDestination(destination.position);
    }
    #endregion UnityEngines

    #region Funcs
    public void TakeHit(float damage)
    {
        Hp -= damage;
    }
    #endregion Funcs
}
