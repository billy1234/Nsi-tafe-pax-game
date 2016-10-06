using UnityEngine;
using UnityEngine.Events;
using System.Collections;


[RequireComponent(typeof(Collider))]
public class DamageOnCollide : WeaponBase
{
    public bool deactivateWhenHit;
    protected Collider myCol;
    public UnityEvent onDamage;
	public float knockBackForce =0f;

    void Start()
    {
        myCol = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision col)
    {
		
        Health otherUnit = col.gameObject.GetComponent<Health>();
        if (otherUnit != null)
        {
            damageUnit(otherUnit, damage);
            onDamage.Invoke();
            onHit();
			applyKnockBack (col.gameObject);
        }
		onCollide();
       
    }

	void OnTriggerEnter(Collider col)
	{
		if (!myCol.isTrigger)
		{
			return;
		}
		
		Health otherUnit = col.gameObject.GetComponent<Health>();
		if (otherUnit != null)
		{
			damageUnit(otherUnit, damage);
			onDamage.Invoke();
			onHit();
			applyKnockBack (col.gameObject);
		}
		onCollide();

	}


    public void toggleColldier(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(toggleColliderCoroutine(duration));      
    }
	void applyKnockBack(GameObject other)
	{
		
		if (knockBackForce <= 0)
		{
			return;
		}
		Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

		if (rb == null)
		{
			return;
		}
		Vector3 forceDir = (other.gameObject.transform.position - transform.position);
		forceDir.y = 0;
		rb.AddForce (forceDir.normalized * knockBackForce,ForceMode.Impulse);
	}
    private IEnumerator toggleColliderCoroutine(float duration)
    {
        myCol.enabled = true;
        yield return new WaitForSeconds(duration);
        if (myCol.enabled)
        {
            myCol.enabled = false;
        }
    }

    protected virtual void onHit()
    {
        if (deactivateWhenHit)
        {
            myCol.enabled = false;
        }
    }
	protected virtual void onCollide()
	{


	}
}
