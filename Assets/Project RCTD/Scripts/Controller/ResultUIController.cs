using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIController : MonoBehaviour
{
    IEnumerator pressToMainCo;
    #region UnityEngine
    private void Awake()
    {
        pressToMainCo = PressToMain();
        StartCoroutine(pressToMainCo);
    }
    #endregion

    #region IEnumerator
    IEnumerator PressToMain()
    {
        yield return new CustomInputTouchCo(false);
        GameManager.Instance.BackToMainMenu();
        GameManager.Instance.Interstitial?.Show();
        StopCoroutine(pressToMainCo);
    }
#endregion
}
