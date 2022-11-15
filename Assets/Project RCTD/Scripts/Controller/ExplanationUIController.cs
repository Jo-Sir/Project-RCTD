using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationUIController : MonoBehaviour
{
    public void CloseExplanationUI()
    { 
        gameObject.SetActive(false);
    }
}
