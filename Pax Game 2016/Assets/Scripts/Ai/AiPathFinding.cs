using UnityEngine;
using System.Collections;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AiBase))]
public class AiPathFinding : MonoBehaviour
{

    /// <summary>
    /// all ai share the same patrol nodes
    /// </summary>
    public static Transform[] patrolNodes;
    public Transform node;
    private NavMeshAgent myNavMeshAgent;
    private AiBase aiBase;
    public float fleeSpeed =1f;
    public void Awake()
    {
        aiBase = gameObject.GetComponent<AiBase>();
        myNavMeshAgent = GetComponent<NavMeshAgent>();
    }


    public void patrol()
    {
        if (node == null)
        {
            node = patrolNodes[Random.Range(0, patrolNodes.Length)];
        }
        else
        {
            myNavMeshAgent.SetDestination(node.position);
        }
    }

    public void walkTowardTarget()
    {
        myNavMeshAgent.SetDestination(aiBase.target.position);
    }

    public void kiteBack()
    {
        myNavMeshAgent.ResetPath();
        myNavMeshAgent.velocity = ((aiBase.target.position - transform.position).normalized * fleeSpeed);
    }
	
}
