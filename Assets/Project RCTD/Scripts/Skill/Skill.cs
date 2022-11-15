using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    #region SerializeField
    [SerializeField] protected SKILL_TYPE SKILL_TYPE;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected float range;
    #endregion

    #region Fields
    protected bool skillActive = false;
    protected new ParticleSystem particleSystem;
    #endregion

    #region UnityEngine
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        GameManager.Instance.returnAllObj += () => GameManager.Instance.ObjectReturn(SKILL_TYPE, gameObject);
    }
    private void OnEnable()
    {
        particleSystem.Play();
    }
    private void OnParticleTrigger()
    {
        if (skillActive == false) { Use(); }
    }
    private void OnParticleSystemStopped()
    {
        skillActive = false;
        GameManager.Instance.ObjectReturn(SKILL_TYPE, this.gameObject);
    }
    #endregion

    #region Func
    protected abstract void Use();
    public abstract void SKillSetting(float towerAtk);
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, range);
    }
}
