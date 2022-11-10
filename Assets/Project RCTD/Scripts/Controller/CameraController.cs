using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float horizontal;
    #endregion

    #region Fields
    private float mouseY;
    #endregion
    #region Unity_Engine
    private void Awake()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        //CameraMove();
    }


    #endregion

    #region Funcs
    private void CameraMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseY = Input.GetAxis("Mouse Y");
            transform.position = new Vector3(0, transform.position.y, mouseY);
        }
    }
    #endregion
}
