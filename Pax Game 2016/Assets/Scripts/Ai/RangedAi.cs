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
			turn = false;
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

    protected override void OnTargetInMin()
    {
        pathfinding.activatePathfinding();
        base.OnTargetInMin();
    }
		




    void turnToTarget()
	{
		if (target == null) 
		{
			turn = false;
			return;
		}
		

		Vector3 lookPos = target.position - transform.position;
		lookPos.y = 0f;
		Quaternion lookRoation = Quaternion.LookRotation (lookPos);
        
		transform.rotation =  Quaternion.Slerp(lookRoation, transform.rotation,Time.deltaTime * turnSmoothing);

    }

	protected override void OnCustomState ()
	{
		pathfinding.deactivatePathfinding ();
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
