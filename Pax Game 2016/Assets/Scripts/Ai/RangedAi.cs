using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class RangedAi : AiBase
{

	private Color neutralColor;
	private Renderer myRend;
    public float turnSmoothing;
    public float turnStateUpdateSpeed =0.05f;
    public UnityEvent onFire;
    private bool turn = true;

	public UnityEvent OnWalk,OnRun,OnTurnLeft,OnTurnRight;
	private void Start()
	{
		base.Start();
		myRend = GetComponentInChildren<Renderer>();
		neutralColor = myRend.material.color;
		if (OnWalk != null)
		{
			OnWalk.Invoke ();
		}
	}
	protected override void OnAquireTarget()
	{
			myRend.material.color = Color.red;

	}

	protected override void OnLoseTarget()
	{
		if (target == null || Mathf.Abs((transform.position - target.position).magnitude) > maxRange)
		{
			target = null;
			myRend.material.color = neutralColor;
			pathfinding.activatePathfinding();
			myRend.material.color = neutralColor;
			state = aiState.PATROL;
			if (OnWalk != null)
			{
				OnWalk.Invoke ();
			}
		}

	}

	protected override void OnPatrol ()
	{
		myRend.material.color = neutralColor;

		base.OnPatrol ();
	}

	protected override void OnTargetInRange ()
	{
		myRend.material.color = Color.red;
        pathfinding.deactivatePathfinding();
        turn = true;
        state = aiState.CUSTOM_STATE;

	}


	void rangedAttack()
	{
     

	}



    /// <summary>
    /// verry hacky turn logic
    /// </summary>
    void turnToTarget()
    {
        Quaternion myRotation = transform.rotation;
        //only changing transform here as i can not instanciate a new transform class and then use the lookat function
        transform.LookAt(target,Vector3.up);
        transform.rotation =  Quaternion.Slerp(myRotation, transform.rotation,Time.deltaTime * turnSmoothing);

    }

	protected override void OnCustomState ()
	{
        /*
		if (target == null)
		{
			OnLoseTarget();
		}
		myRend.material.color = Color.green;
		if(UnityEngine.Random.Range(0,lungeChance) == lungeChance -1)
		{				
			rangedAttack ();
		}
       */
        pathfinding.enabled = false;
        turnToTarget();
        onFire.Invoke();
	}

    void Update()
    {
        if (turn)
        {
            turnToTarget();
        }
    }


}
