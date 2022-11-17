using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    #region Fields
    private float mouseY;
    private bool isScroll = false;
    private float beganTouch;
    private float curTouch;
    #endregion

    #region Unity_Engine
    void Update()
    {
        CameraMove();
    }
    #endregion

    #region Funcs

    private void CameraMove()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {
            isScroll = true;
            mouseY = Input.mousePosition.y;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isScroll = false;
            mouseY = 0f;
        }
        if (isScroll)
        {
            float curMouseY = (mouseY - Input.mousePosition.y) * 0.01f + transform.position.z;
            mouseY = Input.mousePosition.y;
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(curMouseY, -10.25f, -6.18f));
        }
#elif UNITY_ANDROID
        #region UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                beganTouch = Input.GetTouch(0).position.y;
            }
            if(Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                beganTouch = Input.GetTouch(0).position.y;
            }
            // 스크롤중일때
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                curTouch = (beganTouch - Input.GetTouch(0).position.y) * 0.001f + transform.position.z;
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(curTouch, -10.25f, -6.18f));
            }
        }
        #endregion
#endif
    }
    #endregion
}
