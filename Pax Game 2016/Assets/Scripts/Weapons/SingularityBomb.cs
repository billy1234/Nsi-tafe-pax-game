using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class SingularityBomb : MonoBehaviour 
{
	public LayerMask affectedRbLayer;
	public LayerMask explosionDamageLayer;
	public float size = 1f; //of the explosion
	public float minVortexSize =1f;
	public float maxVortexSize = 5f;
	protected float vortexRadius =3f;
	public float vortexPulseSpeed = 1f;
	public float spinSpeed = 1f;
	public float vortextPushPullStrength =1f;
	public float maxDebrisVelocity = 10f;
	public float duration = 10f;
	public float explosionForce = 10f;
    public float explosionYForce = 1f;
    public float imposionForce = 10f;
    public float implosionWait = 0.5f;
	public float explosionDamageRadius = 3f;
	public int explosionDamage = 100;
	public GameObject particleEffect;
	public float soundFinishWait = 5f;
	private bool active = false;
	public UnityEvent onExplode,onImplode,onActivate;
	private List<Rigidbody> affectedBodys;
	//private List<AiPathFinding> affectedEnemys;

	Vector3 debrisPos;
	Vector3 circlePoint;
	Vector3 moveToCircleVel;
	Vector3 spinVel;
	Vector3 forceDir;

	private IEnumerator begin()
	{
		aquireRbs();
		active = true;
		onActivate.Invoke ();
		yield return new WaitForSeconds (duration);
		active = false;
		StartCoroutine(explode ());		

	}
	void OnEnable () 
	{	
		affectedBodys = new List<Rigidbody>();
		Play();
	}

	public void Play()
	{

		StartCoroutine(begin());
	}

	void aquireRbs()
	{
		
		Collider[] hitCols = Physics.OverlapSphere(transform.position,size,affectedRbLayer.value);
		foreach (Collider col in hitCols) 
		{
			Rigidbody r = col.GetComponent<Rigidbody> ();
			if (r != null && !r.isKinematic) 
			{
				affectedBodys.Add (r);	
				r.useGravity = false;
			}
			AiBase ai = col.GetComponent<AiBase> ();
			if (ai != null)
			{
				if ((transform.position - ai.transform.position).magnitude < size)
				{					

					r = ai.setStun (true);
					affectedBodys.Add (r);
					r.useGravity = false;
				}
			}
		}
	}
	

	void FixedUpdate () 
	{
		if (active)
		{
			swirlVortex (transform.position);

		}
	}

	IEnumerator explode()
	{
        onImplode.Invoke();
        for (int i = 0; i < affectedBodys.Count; i++)
        {
            affectedBodys[i].velocity = Vector3.zero;
            affectedBodys[i].AddForce((transform.position - affectedBodys[i].position).normalized * imposionForce, ForceMode.Force);
        }
        yield return new WaitForSeconds(implosionWait);
		damageEnemys ();
		yield return new WaitForSeconds (Time.fixedDeltaTime);
		aquireRbs ();

		Health h;
		for(int i =0; i < affectedBodys.Count; i++)
		{
            affectedBodys[i].velocity = Vector3.zero;
            Vector3 forceDir = ( Random.onUnitSphere).normalized * explosionForce;
            forceDir.y = explosionYForce;
            affectedBodys[i].AddForce(forceDir, ForceMode.Impulse);
            affectedBodys[i].velocity = forceDir;
			affectedBodys[i].useGravity = true;
			h = affectedBodys [i].GetComponent<Health> ();
			if(h != null)
			{
				h.hp = 0;
			}


		}
		affectedBodys.Clear ();
		onExplode.Invoke ();
		particleEffect.SetActive (false);
		yield return new WaitForSeconds(soundFinishWait);
		gameObject.SetActive (false);
    }

	void swirlVortex(Vector3 position)
	{
		vortexRadius = minVortexSize + Mathf.Sin (Time.time * vortexPulseSpeed) * (maxVortexSize - minVortexSize);
		for(int i =0; i < affectedBodys.Count; i++)
		{
			debrisPos = affectedBodys [i].position;
			circlePoint = calculateCirclePoint (debrisPos,position);
			moveToCircleVel = ( circlePoint - debrisPos).normalized  * vortextPushPullStrength;
			spinVel = pushAlongCircle(circlePoint,debrisPos,affectedBodys[i].transform.forward);
			forceDir =  moveToCircleVel + spinVel; // + spinVel;

			affectedBodys [i].AddForce(forceDir,ForceMode.Acceleration);
			if (affectedBodys [i].velocity.magnitude > maxDebrisVelocity) 
			{
				affectedBodys [i].velocity = affectedBodys [i].velocity * 0.8f;
			}

		}

	}

	private void damageEnemys()
	{
		Collider[] nearbodys = Physics.OverlapSphere(transform.position,explosionDamageRadius,explosionDamageLayer);
		Health enemyHp;
		for (int i = 0; i < nearbodys.Length; i++) 
		{
			enemyHp = nearbodys[i].GetComponent<Health>();
			if (enemyHp != null) 
			{
				enemyHp.hp -= explosionDamage;
			}
		}
	}


	private Vector3 calculateCirclePoint(Vector3 debrisPos,Vector3 center)
	{
		return (center + (debrisPos - center).normalized * vortexRadius) ;
	}
	private Vector3 pushAlongCircle(Vector3 circlePoint, Vector3 debrisPosition,Vector3 debrisForward)
	{
		Vector3 dirToCircle = (circlePoint - debrisPosition).normalized;
		return  (Quaternion.Euler(transform.up) * dirToCircle) * spinSpeed;
	}

}
