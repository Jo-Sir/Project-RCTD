using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private Transform spawnPoint;
    #endregion

    #region Fields
    private int curWave = 0;
    private float time;
    private ROUND_TYPE ROUND_TYPE;
    #endregion

    #region UnityEngine
    private void Awake()
    {
        time = 10f;
        UIManager.Instance.TextUpdate("curWave", curWave.ToString());
        ROUND_TYPE = 0;
        WaveTimer();
    }
    private void Update()
    {
        WaveTimer();
    }
    #endregion
    #region Funcs
    public void WaveTimer()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            UIManager.Instance.TextUpdate("time", $"{time:N2}");
        }
        else
        { 
            curWave++;
            ROUND_TYPE++;
            time = 60f;
            UIManager.Instance.TextUpdate("curWave", curWave.ToString());
        }
    }

    IEnumerator SpawnCreep()
    {
        GameManager.Instance.ObjectGet(ROUND_TYPE, spawnPoint);
        yield return new WaitForSecondsRealtime(1f);
    }
    #endregion
}
