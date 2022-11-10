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
    private void Awake()
    {
        if (objectDatas == null)
        {
            ObjectDatas datas = GameObject.Find("ObjectDatas").GetComponent<ObjectDatas>();
            objectDatas = datas.Datas;
        }
        // Init();
    }
    #endregion UnityEngines

    #region Funcs
    public void Init()
    {
        // �ν�����â���� �Էµȵ����Ͱ� ������ ����
        if (objectDatas.Count == 0) { return; }
        // ������ ��ųʸ� �ʱ�ȭ
        poolDict = new Dictionary<string, Stack<GameObject>>();
        dataDict = new Dictionary<string, ObjectData>();
        // �����͸� var���� ������ �ְ� Ǯ ������ֱ�
        foreach (var data in objectDatas)
        {
            CreatePool(data);
        }
    }
    private void CreatePool(ObjectData data)
    {
        // poolDict ��ųʸ��� ���� Ű�� ������ ����
        if (poolDict.ContainsKey(data.key)) { return; }
        GameObject poolobj = null;
        // ������ �� Ű�� ������ �����̳� �����
        if (!GameObject.Find(data.key.ToString()))
        {
            poolobj = new GameObject(data.key.ToString());
            poolobj.transform.SetParent(transform);
        }
        // ������� �����̳� ä���
        CreateObject(data);
    }
    private void CreateObject(ObjectData data)
    {
        Stack<GameObject> pool = new Stack<GameObject>(data.initCount);
        // ���� �����̳� ���� ��������
        GameObject poolobj = GameObject.Find(data.key.ToString()); //�̰͵� ���ε� ���ϰ�
        // ���� �����̳ʿ� ������Ʈ ���� �ֱ� �ֱ�
        for (int i = 0; i < data.initCount; i++)
        {
            GameObject obj = Instantiate(data.prefab);
            obj.transform.SetParent(poolobj.transform);
            obj.SetActive(false);
            pool.Push(obj);
        }
        // Ű���� ������
        if (!poolDict.TryGetValue(data.key, out var poolkey))
        {
            // Ǯ��ųʸ��� ������ ��ųʸ��� Ű�� �� �߰�
            poolDict.Add(data.key, pool);
            dataDict.Add(data.key, data);
        }
        // Ű���� ������ ���� �߰�
        poolDict[data.key] = pool;
        dataDict[data.key] = data;
    }
    public GameObject GetObject(string key, Transform parentTransform)
    {
        // Ǯ ��ųʸ��� Ű�� �������� �ʴ� ��� null ����
        if (!poolDict.TryGetValue(key, out var pool)) { return null; }
        GameObject obj = null;
        // ��ųʸ� Ǯ���� ������
        if (pool.Count > 0)
        {
            obj = pool.Pop();
        }
        // Ǯ�� �����Ͱ� ������ ������ ������ ��ųʸ����� Ű������ ������ ������ �ٽ� ����
        else
        {
            if (dataDict.TryGetValue(key, out var data))
            {
                CreateObject(data);
                // ����� Ǯ �ٽ� Ȯ��
                poolDict.TryGetValue(key, out var newpool);
                obj = newpool.Pop();
            }
        }
        // ������ ������Ʈ �θ� �ٲٰ� Ȱ��ȭ
        obj.transform.SetParent(null);
        obj.transform.position = parentTransform.position;
        obj.gameObject.SetActive(true);
        // obj.transform.position = transform.position;
        return obj;
    }
    public void ReturnObject(string key, GameObject obj)
    {
        // Ǯ ��ųʸ��� Ű ������ ������ �μ�
        if (!poolDict.TryGetValue(key, out var pool))
        {
            Destroy(obj);
            return;
        }
        // ������ �ڱⰡ ������ �����̳ʿ� �ٽ� ����
        GameObject poolobj = GameObject.Find(key.ToString());//�̰� ���ε� ã���ʰ�
        obj.transform.SetParent(poolobj.transform);
        obj.SetActive(false);
        pool.Push(obj);
    }
    #endregion Funcs
}
