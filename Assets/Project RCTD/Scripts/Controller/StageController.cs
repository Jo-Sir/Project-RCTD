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
    private int spawnCount;
    #endregion

    #region UnityEngine
    private void Awake()
    {
        time = 10f;
        GameManager.Instance.Gold = 10000;
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
            if (((int)ROUND_TYPE+1) % 5 == 0) spawnCount = 1;
            else spawnCount = 40;
            WaveStart();
        }
        
    }
    public void WaveStart()
    {
        if (curWave == 16)
        { 
            // Debug.Log("Å¬¸®¾î");
            return;
        }
        GameManager.Instance.Wave = curWave;
        StartCoroutine(SpawnCreep());
    }
    IEnumerator SpawnCreep()
    {
        for (int i = 0; i < spawnCount; i++)
        {             
            GameManager.Instance.ObjectGet(ROUND_TYPE, spawnPoint);
            yield return new WaitForSeconds(1f);
        }
    }
    #endregion
}
