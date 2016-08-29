using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class MeleAiBarge : AiBase
{
    private Color neutralColor;
	private Rigidbody rb;
    private Renderer myRend;
	public float lungeDistance = 0.1f;
	public float lungeCd = 2f;
	private bool canLunge = true;
	public int lungeChance = 5;
	public float standUpVelocity =1f;
    private void Start()
    {
        base.Start();
        myRend = GetComponentInChildren<Renderer>();
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
        neutralColor = myRend.material.color;
    }
    protected override void OnAquireTarget()
    {
		if (rb.isKinematic)
		{
			myRend.material.color = Color.red;
		}

    }

    protected override void OnLoseTarget()
	{
        if (Mathf.Abs((transform.position - target.position).magnitude) > maxRange)
        {
            target = null;
            myRend.material.color = neutralColor;
            rb.isKinematic = true;
            pathfinding.activatePathfinding();
            myRend.material.color = neutralColor;
            state = aiState.PATROL;
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
		if (rb.isKinematic)
		{
			
			base.OnTargetInRange ();
		}

	}

	protected override void OnTargetInMin()
	{
        rb.isKinematic = false;
		state = aiState.CUSTOM_STATE;
		pathfinding.deactivatePathfinding();

	}
		
	void meleAttack()
	{
		
		if (canLunge) 
		{
			rb.AddForce(( target.position - transform.position).normalized * lungeDistance, ForceMode.Force);
			StartCoroutine (cooldownLunge ());
		}
		//throw new NotImplementedException ();
	}
		
	protected override void OnCustomState ()
	{
		myRend.material.color = Color.green;
	    if(UnityEngine.Random.Range(0,lungeChance) == lungeChance -1)
	    {				
		    meleAttack ();
	    }
	}

	IEnumerator cooldownLunge()
	{
		canLunge = false;
		yield return new WaitForSeconds(lungeCd);
		canLunge = true;
	}

	void OnCollisionStay(Collision col)
	{
		if (rb.velocity.magnitude < standUpVelocity)
		{
			if (target == null) 
			{
				rb.isKinematic= true;
				return;
			}
			if ((target.position - transform.position).magnitude > maxRange) 
			{
				rb.isKinematic= true;
			}

		}

	}
}
