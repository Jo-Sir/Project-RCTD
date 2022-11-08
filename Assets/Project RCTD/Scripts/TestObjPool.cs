using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjPool : MonoBehaviour
{
    private void Awake()
    {
        ObjectPoolManager.Instance.Init();
    }
}
