using UnityEngine;
using UnityEngine.Events;
using System.Collections;


[RequireComponent(typeof(Collider))]
public class DamageOnCollide : WeaponBase
{
    public bool deactivateWhenHit;
    protected Collider myCol;
    public UnityEvent onDamage;

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
            
        }
		onCollide();
       
    }


    public void toggleColldier(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(toggleColliderCoroutine(duration));      
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
