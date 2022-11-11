using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region SerializeField
    [SerializeField, Range(0.01f, 0.1f)] private float mouseSpeed = 0.01f;
    #endregion

    #region Fields
    private float mouseY;
    private bool isScroll = false;
    #endregion

    #region Unity_Engine
    private void Awake()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        CameraMove();
    }


    #endregion

    #region Funcs

    float curMouseY;
    float preMouseY;

    private void CameraMove()
    {

        if (Input.GetMouseButtonDown(1))
        {
            isScroll = true;
            mouseY = Input.mousePosition.y;            
        }

        if(Input.GetMouseButtonUp(1))
        {
            isScroll = false;
            mouseY = 0f;
        }

        if(isScroll)
        {
            float curMouseY = (mouseY - Input.mousePosition.y) * mouseSpeed + transform.position.z;
            mouseY = Input.mousePosition.y;
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(curMouseY, -10.25f, -6.18f));
        }
    }
    #endregion
}
