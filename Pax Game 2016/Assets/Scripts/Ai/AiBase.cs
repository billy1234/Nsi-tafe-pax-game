using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum aiState
{
    PATROL, ATTACK, KITE_BACK, CUSTOM_STATE
};


public abstract class AiBase : MonoBehaviour
{   

    

    //[HideInInspector]
    public aiState state = aiState.PATROL;
	//[HideInInspector]
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
        float targetDistance;
		yield return new WaitForSeconds(Random.Range(0f,unitTickRate)); //stop all the ai syncing
        while (gameObject.active)
        {
			yield return new WaitForSeconds(unitTickRate);
			if (state == aiState.CUSTOM_STATE)
			{
				print ("custom state");
				onCustomState();
			} 
			else
			{
				print ("state: " + state);
				if (target != null) //if i have a target
				{
					targetDistance = Mathf.Abs ((transform.position - target.position).magnitude);
					if (targetDistance < minRange)
					{
						OnTargetInMin ();
					} 
					else
					{
						onAttack ();
					}

				} 
				else
				{
					onPatrol ();
				} 
			}
        }
    }
	protected virtual void onCustomState()
	{
		//no custom state in base
	}
	protected virtual void onPatrol()
    {
        //print("patroling");
        target = null;
        state = aiState.PATROL;
        if (pathfinding != null)
        {
			print ("memes ey");
            pathfinding.patrol();
        }
    }
	protected virtual void OnTargetInMin()
    {
        state = aiState.KITE_BACK;
        if (pathfinding != null)
        {
            pathfinding.kiteBack();
        }
    }
	protected virtual void onAttack()
    {
        state = aiState.ATTACK;
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
		return(seenObject.name == "enemy");
        //return true;
    }


    /// <summary>
    /// the colider event then a target is aquired
    /// </summary>
    protected abstract void OnAquireTarget();

    /// <summary>
    /// the colider event when the target leaves range
    /// </summary>
    protected abstract void OnLoseTarget();

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
			target = null;
			OnLoseTarget();
		}
	}
	#endregion


}
