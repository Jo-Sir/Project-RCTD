using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDatas : MonoBehaviour
{
    [SerializeField] private List<ObjectData> datas;
    public List<ObjectData> Datas { get { return datas; } }
}
