using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentTest : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform tragetTransform;
    private float colseDistance = 1f;
    private RaycastHit hit;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        agent.SetDestination(tragetTransform.position);
    }
    void Update()
    {
        DistanceCheck();
    }
    public void DistanceCheck()
    {
        Vector3 offset = tragetTransform.position - transform.position;
        float sqrLen = offset.sqrMagnitude;
        if (sqrLen < colseDistance * colseDistance)
        {
            RePath();
        }
    }
    public void RePath()
    {
        if (Physics.Raycast(transform.position, -transform.right, out hit, Mathf.Infinity, 1 << 7 | 1 << 10))
        {
            tragetTransform.position = hit.transform.position;
            agent.SetDestination(tragetTransform.position);
        }
    }
}
