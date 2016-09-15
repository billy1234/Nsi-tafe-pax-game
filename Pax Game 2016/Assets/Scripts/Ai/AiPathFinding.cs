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
	private bool isKiting = false;
	private Vector3 area;
    public void Awake()
    {
        aiBase = gameObject.GetComponent<AiBase>();
        myNavMeshAgent = GetComponent<NavMeshAgent>();
    }


    public void patrol()
    {
        if (node == null || (transform.position - node.position).magnitude < nodeWidth)
            {
                node = patrolNodes[Random.Range(0, patrolNodes.Length)]; //get a new node
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

	public void kiteBack(Vector3 area,float duration)
    {   
		if (!isKiting) 
		{
			StopAllCoroutines ();
			this.area = area;
			StartCoroutine (moveback ( duration));
		}
    }

	IEnumerator moveback( float duration)
	{
		isKiting = true;
		myNavMeshAgent.ResetPath();
		yield return new WaitForSeconds (duration);
		isKiting = false;
	}

	void FixedUpdate()
	{
		if (isKiting) 
		{
			myNavMeshAgent.Move((transform.position - area).normalized * fleeSpeed * Time.fixedDeltaTime);
		}
	}
		

	public void deactivatePathfinding()
	{
		StopAllCoroutines ();
		myNavMeshAgent.enabled = false;	
	}

	public void activatePathfinding()
	{
            myNavMeshAgent.enabled = true;
            myNavMeshAgent.Resume();
	}

	void OnDisable()
	{
		StopAllCoroutines ();
	}
	
}
