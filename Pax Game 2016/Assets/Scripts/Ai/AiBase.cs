using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum aiState
{
    PATROL, ATTACK, TARGET_IN_MIN, CUSTOM_STATE
};


public abstract class AiBase : MonoBehaviour
{   

    


    protected aiState state = aiState.PATROL;


    public Transform target;

    [Tooltip("The max range this unit will atack from before getting closer")]
    public float maxRange =5f;

    [Tooltip("set range to 0 if you do not with this unit to kite back")]
    public float minRange =0f;

    private readonly float unitTickRate = 0.1f;
	protected AiPathFinding pathfinding;

    #region initalization
    protected void Start ()
    {
        StartCoroutine(aiTick());
        setUptrigger();
        pathfinding = gameObject.GetComponent<AiPathFinding>();
	}
    void setUptrigger()
    {
        SphereCollider myTrigger = gameObject.GetComponent<SphereCollider>();
        if (myTrigger != null && myTrigger.isTrigger)
        {
            myTrigger.radius = maxRange;
        }
        else
        {
            myTrigger =  gameObject.AddComponent<SphereCollider>();
            myTrigger.radius = maxRange;
            myTrigger.isTrigger = true;
        }
    }
    #endregion

    #region editor
    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (Selection.activeGameObject == gameObject)
        {
            //Gizmos.color = Color.green;

            //Gizmos.DrawWireSphere(transform.position, maxRange);


            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, minRange);
        }
    }
	#endif
    #endregion

    #region gameplayEvents

    /// <summary>
    /// as well as the colision events causing evets to trigger this function will be called every x seconds to 
    /// continue calling the patrol ect functions
    /// </summary>
    /// <returns></returns>
    IEnumerator aiTick()
    {
		yield return new WaitForSeconds(Random.Range(0f,unitTickRate)); //stop all the ai syncing

        while (enabled)
        {		

            if (target!= null && !target.gameObject.activeSelf)
            {
                OnTargetDisabled();
            }


            if (state == aiState.CUSTOM_STATE)
            {
                OnCustomState();
            }
            else if (target != null)            //if we have a target
            {
              
                if (targetInMin())  //target is both in range and in min range
                {
                    OnTargetInMin();
                }
                else                            //target is in range
                {
                    OnTargetInRange();
                }
            }
            else //if we have no target and are not in a custom ai state
            {
                OnPatrol();
            }
            yield return new WaitForSeconds(unitTickRate);
        }
    }

    protected bool targetInMin()
    {
        return (Mathf.Abs((transform.position - target.position).magnitude) < minRange);
    }


	protected virtual void OnCustomState()
	{
		//no custom state in base
	}

    protected virtual void OnTargetDisabled()
    {
        target = null;
    }
	protected virtual void OnPatrol()
    {
        //print("patroling");
        target = null;
        state = aiState.PATROL;
        if (pathfinding != null)
        {
            pathfinding.patrol();
        }
    }
	protected virtual void OnTargetInMin()
    {
        state = aiState.TARGET_IN_MIN;
        if (pathfinding != null)
        {
            pathfinding.kiteBack(target.position);
        }
    }
	protected virtual void OnTargetInRange()
    {
        if (pathfinding != null)
        {
            pathfinding.walkTowardTarget();
        }
    }


    

    /// <summary>
    /// called when a colider walks within the ais range return true if you wish to target the gameobject that triggerd ontrigger enter on this unit
    /// </summary>
    /// <param name="seenObject"></param>
    protected virtual bool targetNewUnit(GameObject seenObject)
    {
		return(seenObject.name == "Character");
        //return true;
    }


    /// <summary>
    /// the colider event then a target is aquired
    /// </summary>
    protected abstract void OnAquireTarget();

    /// <summary>
    /// the colider event when the target leaves range
    /// </summary>
    protected virtual void OnLoseTarget()
    {
        target = null;
    }

    #endregion

	#region physicsEvents
	void OnTriggerEnter(Collider col)
	{
		if (target == null && targetNewUnit(col.gameObject))
		{
			target = col.transform;
			OnAquireTarget ();        
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.transform == target)
		{            
			
			OnLoseTarget();
		}
	}
	#endregion


}
