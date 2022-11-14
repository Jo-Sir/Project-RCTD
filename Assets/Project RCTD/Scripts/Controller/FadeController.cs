using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    #region Fields
    private Animator animator;
    private Image fadeImage;
    #endregion

    #region UnityEngine
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        animator = GetComponentInChildren<Animator>();
        fadeImage = GetComponentInChildren<Image>();
        StartCoroutine(StartCo());
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
        Time.timeScale = 1.0f;
    }
    public void FadeImageSetActive(bool value = false)
    {
        fadeImage.enabled = value;
    }
    #endregion

    IEnumerator StartCo()
    {
        FadeImageSetActive(true);
        FadeIn();
        yield return new WaitForSeconds(0.9f);
        FadeImageSetActive(false);
    }
}
