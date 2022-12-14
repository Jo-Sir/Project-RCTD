using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    #region Fields
    private List<ObjectData> objectDatas;
    private Dictionary<string, Stack<GameObject>> poolDict;
    private Dictionary<string, ObjectData> dataDict;
    #endregion Fields

    #region UnityEngines
    private new void Awake()
    {
        base.Awake();
        if (objectDatas == null)
        {
            ObjectDatas datas = Resources.Load<ObjectDatas>("Prefabs/Data/ObjectDatas").GetComponent<ObjectDatas>();
            objectDatas = datas.Datas;
        }
    }
    #endregion UnityEngines

    #region Funcs
    /// <summary>
    /// 오브젝트 풀 매니저 준비
    /// </summary>
    public void Init()
    {
        // objectDatas에 데이터가 없으면 리턴
        if (objectDatas.Count == 0) { return; }
        // 있으면 딕셔너리 초기화
        poolDict = new Dictionary<string, Stack<GameObject>>();
        dataDict = new Dictionary<string, ObjectData>();
        // 데이터를 var형식 변수에 넣고 풀 만들어주기
        foreach (var data in objectDatas)
        { CreatePool(data); }
    }
    /// <summary>
    /// 오브젝트 풀 컨테이너 만들기
    /// </summary>
    /// <param name="data"></param>
    private void CreatePool(ObjectData data)
    {
        // poolDict 딕셔너리에 같은 키가 있으면 리턴
        if (poolDict.ContainsKey(data.key)) { return; }
        GameObject poolobj = null;
        // 있으면 그 키값 형태의 컨테이너 만들기
        if (!GameObject.Find(data.key.ToString()))
        {
            poolobj = new GameObject(data.key.ToString());
            poolobj.transform.SetParent(transform);
        }
        // 만들어진 컨테이너 채우기
        CreateObject(data);
    }
    /// <summary>
    /// 만든 풀에 오브젝트 채워넣기
    /// </summary>
    /// <param name="data">채워넣을 오브젝트 정보</param>
    private void CreateObject(ObjectData data)
    {
        Stack<GameObject> pool = new Stack<GameObject>(data.initCount);
        // 넣을 컨테이너 정보 가져오기
        GameObject poolobj = GameObject.Find(data.key.ToString());
        // 만든 컨테이너에 오브젝트 만들어서 넣기 넣기
        for (int i = 0; i < data.initCount; i++)
        {
            GameObject obj = Instantiate(data.prefab);
            obj.transform.SetParent(poolobj.transform);
            obj.SetActive(false);
            pool.Push(obj);
        }
        // 키값이 없으면
        if (!poolDict.TryGetValue(data.key, out var poolkey))
        {
            // 풀딕셔너리와 데이터 딕셔너리에 키와 값 추가
            poolDict.Add(data.key, pool);
            dataDict.Add(data.key, data);
        }
        // 키값이 있으면 값만 추가
        poolDict[data.key] = pool;
        dataDict[data.key] = data;
    }
    /// <summary>
    /// 오브젝트 가져오기
    /// </summary>
    /// <param name="key">키값</param>
    /// <param name="parentTransform">가저오는 오브젝트의 Transform</param>
    /// <returns></returns>
    public GameObject GetObject(string key, Transform parentTransform)
    {
        // 풀 딕셔너리에 키가 존재하지 않는 경우 null 리턴
        if (!poolDict.TryGetValue(key, out var pool)) { return null; }
        GameObject obj = null;
        // 딕셔너리 풀에서 꺼내기
        if (pool.Count > 0)
        { obj = pool.Pop(); }
        // 풀에 데이터가 없으면 데이터 데이터 딕셔너리에서 키값으로 정보를 꺼내서 다시 생성
        else
        {
            if (dataDict.TryGetValue(key, out var data))
            {
                CreateObject(data);
                // 만들고 풀 다시 확인
                poolDict.TryGetValue(key, out var newpool);
                obj = newpool.Pop();
            }
        }
        // 꺼내온 오브젝트 부모를 바꾸고 활성화
        obj.transform.SetParent(null);
        obj.transform.position = parentTransform.position;
        obj.gameObject.SetActive(true);
        return obj;
    }
    /// <summary>
    /// 오브젝트 반납
    /// </summary>
    /// <param name="key">키값</param>
    /// <param name="obj">반납할려는 오브젝트</param>
    public void ReturnObject(string key, GameObject obj)
    {
        // 풀 딕셔너리에 키 정보가 없으면 부숨
        if (!poolDict.TryGetValue(key, out var pool))
        {
            Destroy(obj);
            return;
        }
        // 있으면 자기가 들어가야할 컨테이너에 다시 넣음
        GameObject poolobj = GameObject.Find(key.ToString());
        obj.transform.SetParent(poolobj.transform);
        obj.SetActive(false);
        pool.Push(obj);
    }
    #endregion Funcs
}
