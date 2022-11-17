using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IBuildable
{
    #region SerializeField
    [SerializeField] private GameObject particle;
    #endregion
    #region Fields
    private Tower curTower;
    #endregion Fields

    #region Properties
    public Tower CurTower { get => curTower; }
    #endregion Properties

    #region Funcs

    public bool BuildCheck(out Tower tower)
    {
        curTower = GetComponentInChildren<Tower>();
        tower = curTower;
        return (bool)(curTower == null);
    }

    public Transform GetTransForm()
    {
        return transform;
    }
    public void ParticleOnOff(bool on)
    {
        if (on)
        {
            particle.SetActive(true);
        }
        else
        { 
            particle.SetActive(false);
        }
    }
    #endregion Funcs
}
