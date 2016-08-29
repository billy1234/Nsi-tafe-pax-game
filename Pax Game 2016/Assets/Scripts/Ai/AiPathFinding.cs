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
    private Transform node;
    private NavMeshAgent myNavMeshAgent;
    private AiBase aiBase;
    public float fleeSpeed =1f;
    public float nodeWidth = 3f;
    public void Awake()
    {
        aiBase = gameObject.GetComponent<AiBase>();
        myNavMeshAgent = GetComponent<NavMeshAgent>();
    }


    public void patrol()
    {
		//turnOnPathfinding ();
        if (node == null || (transform.position - node.position).magnitude < nodeWidth)
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
		//turnOnPathfinding ();
        myNavMeshAgent.SetDestination(aiBase.target.position);
    }

    public void kiteBack()
    {
		
		//turnOnPathfinding ();
        myNavMeshAgent.ResetPath();
        myNavMeshAgent.velocity = ((aiBase.target.position - transform.position).normalized * fleeSpeed);
    }
		

	public void turnOffPathfinding()
	{
		//myNavMeshAgent.Stop();
		myNavMeshAgent.enabled = false;
	}

	public void turnOnPathfinding()
	{		
		myNavMeshAgent.enabled = true;
		myNavMeshAgent.Resume ();
	}
	
}
