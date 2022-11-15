using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomInputTouchCo : IEnumerator 
{
    private bool isNext;
    /// <summary>
    /// isnext = true
    /// !EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0)
    /// isnext = false
    /// Input.GetMouseButtonDown(0);
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
#if UNITY_EDITOR
            if (isNext)
            {
                return !(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0));
            }
            else
            {
                return !Input.GetMouseButtonDown(0);
            }

#elif PLATFORM_ANDROID
if (isNext)
            {
                return !(!EventSystem.current.IsPointerOverGameObject() && (Input.touchCount > 0 ));
            }
            else
            {
                return !(Input.touchCount > 0);
            }
#endif
        }
    }

    public bool MoveNext()
    {
#if UNITY_EDITOR
        if (isNext)
        {
            return !(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0));
        }
        else
        {
            return !Input.GetMouseButtonDown(0);
        }

#elif PLATFORM_ANDROID
if (isNext)
            {
                return !((Input.touchCount > 0));
            }
            else
            {
                return !(Input.touchCount > 0);
            }
#endif
    }
    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}

