using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private TowerController towerController;
    #endregion SerializeFields

    #region Fields
    #endregion Fields

    #region UnityEngines
    private void Start()
    {
        Debug.Log(towerController.transform.name);
    }
    #endregion UnityEngines
}
