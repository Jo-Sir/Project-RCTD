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
        GameManager.Instance.Gold = 400;
        UIManager.Instance.TextUpdate("curWave", curWave.ToString());
        ObjectPoolManager.Instance.Init();
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
            if (time < 10)
            { UIManager.Instance.TextUpdate("time", $"0{time:N2}"); }
            else
            { UIManager.Instance.TextUpdate("time", $"{time:N2}"); }
        }
        else
        {
            if (curWave != 0) ROUND_TYPE++;
            curWave++;
            time = 60f;
            WaveStart();
        }
        
    }
    public void WaveStart()
    {
        if (curWave == 16)
        { 
            Debug.Log("Å¬¸®¾î");
            return;
        }
        GameManager.Instance.Wave = curWave;
        StartCoroutine(SpawnCreep());
    }
    IEnumerator SpawnCreep()
    {
        for (int i = 0; i <= 40; i++)
        {             
            GameManager.Instance.ObjectGet(ROUND_TYPE, spawnPoint);
            yield return new WaitForSecondsRealtime(1f);
        }
    }
    #endregion
}
