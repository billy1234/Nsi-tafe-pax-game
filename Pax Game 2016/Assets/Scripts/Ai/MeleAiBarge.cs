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
		myRend.material.color = neutralColor;
		//print
		if (rb.isKinematic)
		{
			//print ("is this loss");
			myRend.material.color = neutralColor;

			state = aiState.PATROL;
		}
    }

	protected override void onPatrol ()
	{
		myRend.material.color = neutralColor;
		if (rb.isKinematic)
		{
			base.onPatrol ();
		}
	}

	protected override void onAttack ()
	{
		myRend.material.color = Color.red;
		if (rb.isKinematic)
		{
			
			base.onAttack ();
		}
	}

	protected override void OnTargetInMin()
	{
		rb.isKinematic = false;
		state = aiState.CUSTOM_STATE;
		pathfinding.turnOffPathfinding();
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
		
	protected override void onCustomState ()
	{
		myRend.material.color = Color.green;
		if ((target.position - transform.position).magnitude < minRange)
		{		

			if(UnityEngine.Random.Range(0,lungeChance) == lungeChance -1)
			{				
				meleAttack ();
			}
		} 
		else
		{
			pathfinding.turnOnPathfinding();
			state = aiState.ATTACK;
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
