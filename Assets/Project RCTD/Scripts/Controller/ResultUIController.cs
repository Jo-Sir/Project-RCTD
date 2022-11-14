using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIController : MonoBehaviour
{
    IEnumerator ressToMainCo;
    #region UnityEngine
    private void Awake()
    {
        ressToMainCo = PressToMain();
        StartCoroutine(ressToMainCo);
    }
    #endregion

    #region IEnumerator
    IEnumerator PressToMain()
    {
        yield return new CustomInputTouchCo(false);
        GameManager.Instance.BackToMainMenu();
        StopCoroutine(ressToMainCo);
    }
#endregion
}
