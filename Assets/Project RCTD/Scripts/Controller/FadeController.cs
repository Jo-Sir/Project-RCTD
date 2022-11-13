using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    #region SerializeField
    [SerializeField] Image fadeImage;
    #endregion

    #region Fields
    private Animator animator;
    #endregion

    #region UnityEngine
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        animator = GetComponentInChildren<Animator>();
        fadeImage = GetComponentInChildren<Image>();
        fadeImage.enabled = false;
    }
    #endregion
    #region Funcs
    public void FadeIn()
    {
        animator.Play("FadeIn");
        
    }
    public void FadeOut()
    {
        animator.Play("FadeOut");
    }
    public void FadeImageSetActive(bool value)
    {
        fadeImage.enabled = value;
    }
    #endregion
}
