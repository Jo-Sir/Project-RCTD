using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentTest : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform tragetTransform;
    [SerializeField] private Transform[] pass;
    private float colseDistance = 1f;
    private Quaternion left;
    private RaycastHit hit;
    private float y;
    bool isReSurch = false;
    int i = 0;
    private void Awake()
    {
        left = Quaternion.identity;
        agent = GetComponent<NavMeshAgent>();
        y = 90f;
    }
    void Start()
    {
        agent.SetDestination(pass[i].position);
        agent.SetDestination(tragetTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        SurchNav();
        Debug.DrawRay(transform.position, transform.forward * 15f, Color.red, 1f);
        Debug.Log(y);
        //RePassNav();
    }
   /* #region Pass
    public void RePassNav()
    {
        Vector3 offset = pass[i].position - transform.position;
        float sqrLen = offset.sqrMagnitude;
        if (sqrLen < colseDistance * colseDistance)
        {
            i++;
            agent.SetDestination(pass[i].position);
        }
    }
    #endregion Pass*/
    #region ray
    public void SurchNav()
    {
        Vector3 offset = tragetTransform.position - transform.position;
        float sqrLen = offset.sqrMagnitude;
        
        if (sqrLen < colseDistance * colseDistance)
        {
            left.eulerAngles = new Vector3(0, y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, left, Time.deltaTime * 30f);
            isReSurch = (bool)(transform.eulerAngles.y >= y);
            StartCoroutine(ReSurchPathCo());
        }
    }
    public void ReSurchPath()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, 1 << 7))
        {
            tragetTransform.position = hit.transform.position;
            agent.SetDestination(tragetTransform.position);
            y -= 90f;
            isReSurch = false;
        }
    }
    IEnumerator ReSurchPathCo()
    {
        yield return new WaitForSeconds(0.1f);
        if (isReSurch)
        {
            ReSurchPath();
        }
    }
    #endregion ray
}
