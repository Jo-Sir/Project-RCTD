using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreepUIController : MonoBehaviour
{
    #region UnityAction
    public UnityAction<float> changeHpBar;
    #endregion

    #region SerializeField
    [SerializeField] private Image hpbar;
    [SerializeField] private Image damageBar;
    #endregion

    #region Fields
    private float creepMaxHp;
    private Creep parentcreep;
    IEnumerator updateRedHpCo;
    #endregion
    #region Unity_Engine
    private void Awake()
    {
        parentcreep = GetComponentInParent<Creep>();
        changeHpBar = (float curHp) => UpdateHp(curHp);
        hpbar.fillAmount = 1f;
        damageBar.fillAmount = 1f;
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(90f, 0, 0);
    }
    private void OnEnable()
    {
        creepMaxHp = parentcreep.CurHp;
        hpbar.fillAmount = 1f;
        damageBar.fillAmount = 1f;
    }
    #endregion

    #region Func
    private void UpdateHp(float curHp)
    {
        if (!gameObject.activeSelf) return;
        float cent = curHp / creepMaxHp;
        if (hpbar.fillAmount >= cent)
        {
            hpbar.fillAmount = cent;
            if(parentcreep.IsDie) return;
            StartCoroutine(UpdateRedHp(cent));
        }
    }
    #endregion Func

    #region IEnumerator
    private IEnumerator UpdateRedHp(float cent)
    {
        for (float i = damageBar.fillAmount; i > cent; i -= 0.007f)
        {

            damageBar.fillAmount = i;
            yield return null;
        }
        damageBar.fillAmount -= 0.008f;
    }
    #endregion IEnumerator
}
