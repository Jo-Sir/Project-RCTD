using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewPathsData", menuName = "ScriptableObject/PathsData")]
public class PathsData : ScriptableObject
{
    public Transform paths;
}
