using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomInputTouchCo : IEnumerator 
{
    private bool isNext;
    /// <summary>
    /// isnext == true
    /// UI가 아닌곳에 입력이됐나 확인
    /// isnext == false
    /// 입력 확인
    /// </summary>
    /// <param isNext="value"></param>
    public CustomInputTouchCo(bool value)
    {
        isNext = value;
    }

    public object Current
    {
        get
        {
            bool isIf;
            bool isElseIf;
#if UNITY_EDITOR
            isIf     = !(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0));
            isElseIf = !Input.GetMouseButtonDown(0);
#elif UNITY_ANDROID
            isIf     = !(!EventSystem.current.IsPointerOverGameObject() && (Input.touchCount > 0 ));
            isElseIf = !(Input.touchCount > 0);
#endif
            if (isNext)
            { return isIf; }
            else
            { return isElseIf; }
         
        }
    }

    public bool MoveNext()
    {
        bool isIf;
        bool isElseIf;

#if UNITY_EDITOR
        isIf = !(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0));
        isElseIf = !Input.GetMouseButtonDown(0);
#elif UNITY_ANDROID
            isIf     = !(!EventSystem.current.IsPointerOverGameObject() && (Input.touchCount > 0 ));
            isElseIf = !(Input.touchCount > 0);
#endif

        if (isNext)
        { return isIf; }
        else
        { return isElseIf; }
    }
    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}

