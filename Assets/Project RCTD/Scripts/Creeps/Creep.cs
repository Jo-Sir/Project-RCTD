using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent (typeof(NavMeshAgent))]
public class Creep : MonoBehaviour, IDamagable
{
    #region SerializeFields
    [SerializeField] private ROUND_TYPE ROUND_TYPE;
    [SerializeField] private float baseHp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private PathsData pathsData;
    #endregion SerializeFields

    #region Fields
    [SerializeField] private float curHp;
    private CreepUIController creepUIController;
    private float hp;
    private Transform paths;
    private List<Transform> tragetTransform;
    private NavMeshAgent agent;
    private IEnumerator SetDeBuffTimeCo;
    private float colseDistance = 1f;
    private int listCount = 0;
    private Material material;
    private CapsuleCollider colledr;
    private bool isDie;
    #endregion Fields

    #region Properties
    public float BaseHp { get => baseHp; }
    public float CurHp 
    {
        set 
        {
            curHp = value;
            if (creepUIController.changeHpBar == null) return;
            creepUIController.changeHpBar.Invoke(curHp);
            if (curHp <= 0) 
            {
                if (!isDie)
                {
                    switch (ROUND_TYPE)
                    {
                        case ROUND_TYPE.MISSION_ONE:
                            GameManager.Instance.Gold += 200;
                            break;
                        case ROUND_TYPE.MISSION_TWO:
                            GameManager.Instance.Gold += 400;
                            break;
                        case ROUND_TYPE.MISSION_THREE:
                            GameManager.Instance.Gold += 800;
                            break;
                    }
                }
                Die(); 
            }
        }
        get 
        { 
            return curHp;
        }
    }
    public float MoveSpeed 
    {
        set
        {
            moveSpeed = value;
            agent.speed = moveSpeed;
        }
        get
        {
            return moveSpeed;
        }
    }
    public bool IsDie { get => isDie; }
    #endregion Properties

    #region UnityEngines
    private void Awake()
    {
        colledr = GetComponent<CapsuleCollider>();
        paths = pathsData.paths;
        agent = GetComponent<NavMeshAgent>();
        creepUIController = GetComponentInChildren<CreepUIController>();
        material = GetComponentInChildren<Renderer>().material;
        Transform[] patharr = paths.GetComponentsInChildren<Transform>();
        tragetTransform = new List<Transform>();
        foreach (Transform target in patharr)
        {
            if (paths.name == target.name) continue;
            tragetTransform.Add(target);
        }
    }
    private void Update()
    {
        DistanceCheck();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "GoalTile")
        {
            Die();
            switch (ROUND_TYPE)
            {
                case ROUND_TYPE.MISSION_ONE:
                    GameManager.Instance.Life -= 5;
                    break;
                case ROUND_TYPE.MISSION_TWO:
                    GameManager.Instance.Life -= 5;
                    break;
                case ROUND_TYPE.MISSION_THREE:
                    GameManager.Instance.Life -= 5;
                    break;
                default:
                    GameManager.Instance.Life -= 1;
                    break;
            }
        }
    }
    private void OnEnable()
    {
        colledr.enabled = true;
        transform.SetParent(null);
        colseDistance = 1f;
        listCount = 0;
        hp = baseHp;
        isDie = false;
        SetHP();
        agent.speed = moveSpeed;
        agent.isStopped = false;
        agent.destination = tragetTransform[listCount].position;
        material.SetFloat("_DissolveAmount", 0);
        creepUIController.gameObject.SetActive(true);
    }
    #endregion UnityEngines

    #region Funcs
    public void SetHP()
    {
        if (GameManager.Instance.Wave >= 5) hp *= 2f;
        if (GameManager.Instance.Wave >= 10) hp *= 2f;
        if (GameManager.Instance.Wave >= 15) hp *= 2f;
        CurHp = hp * GameManager.Instance.Wave;
    }
    public void TakeHit(float damage)
    {
        CurHp -= damage;
    }
    public void DistanceCheck()
    {
        if (listCount == 10) return;
        Vector3 offset = tragetTransform[listCount].position - transform.position;
        float sqrLen = offset.sqrMagnitude;
        if (sqrLen < colseDistance * colseDistance)
        {
            listCount++;
            agent.SetDestination(tragetTransform[listCount].position);
        }
    }
    public void Die()
    {
        if (isDie) { return; }
        StartCoroutine(SetDissolveAmount());
    }
    public void SetDeBuff(DEBUFF_TYPE deBuffname, float deBuffTime, float figure)
    {
        switch (deBuffname)
        {
            case DEBUFF_TYPE.SLOW:
                MoveSpeed -= figure;
                if (SetDeBuffTimeCo == null) 
                {
                    SetDeBuffTimeCo = SetDeBuffTime(deBuffname, deBuffTime, figure);
                }
                StartCoroutine(SetDeBuffTimeCo);
                break;
            default:
                return;
        }
    }
    #endregion Funcs

    #region IEnumerator
    IEnumerator SetDissolveAmount()
    {
        isDie = true;
        agent.isStopped = true;
        colledr.enabled = false;
        creepUIController.gameObject.SetActive(false);
        for (float i = 0; i <= 1; i += 0.007f)
        {
            material.SetFloat("_DissolveAmount", i);
            yield return null;
        }
        GameManager.Instance.ObjectReturn(ROUND_TYPE, this.gameObject);
    }

    IEnumerator SetDeBuffTime(DEBUFF_TYPE deBuffname, float deBuffTime, float figure)
    {
        yield return new WaitForSeconds(deBuffTime);
        switch (deBuffname)
        {
            case DEBUFF_TYPE.SLOW:
                MoveSpeed += figure;
                break;
            default:
                break;
        }
        SetDeBuffTimeCo = null;
    }    
    #endregion
}
