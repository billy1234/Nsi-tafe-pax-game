using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class SingularityBomb : MonoBehaviour 
{
	public LayerMask affectedRbLayer;
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
    public float imposionForce = 10f;
    public float implosionWait = 0.5f;
	public GameObject particleEffect;
	public float soundFinishWait = 5f;
	private bool active = false;
	public UnityEvent onExplode,onImplode,onActivate;
	private List<Rigidbody> affectedBodys;

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

		Play();
	}

	public void Play()
	{

		StartCoroutine(begin());
	}

	void aquireRbs()
	{
		affectedBodys = new List<Rigidbody>();
		Collider[] hitCols = Physics.OverlapSphere(transform.position,size,affectedRbLayer.value);
		foreach (Collider col in hitCols) 
		{
			Rigidbody r = col.GetComponent<Rigidbody> ();
			if (r != null) 
			{
				affectedBodys.Add (r);	
				r.useGravity = false;
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
		for(int i =0; i < affectedBodys.Count; i++)
		{
			affectedBodys [i].AddForce((affectedBodys[i].position - transform.position).normalized * explosionForce,ForceMode.Impulse);
			affectedBodys [i].useGravity = true;

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
