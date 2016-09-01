using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Collider))]
public class DamageOnCollide : WeaponBase
{
    public bool deactivateWhenHit;
    protected Collider myCol;

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
            
            onHit();
        }
		onColide ();
       
    }

    protected virtual void onHit()
    {
        if (deactivateWhenHit)
        {
            myCol.enabled = false;
        }
    }
	protected virtual void onColide()
	{


	}
}
