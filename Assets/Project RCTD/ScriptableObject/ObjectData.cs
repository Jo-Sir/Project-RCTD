using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
[CreateAssetMenu(fileName = "NewObjectPoolData", menuName = "ScriptableObject/ObjectPoolData")]
public class ObjectData : ScriptableObject
{
    public string key;
    public GameObject prefab;
    public int initCount;
}
