using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class VelocityProjectile : DamageOnCollide
{
    [System.Serializable]
    public struct velocityProjectileInfo
    {
        public float maxDamagevelocity;
        public int damage;
        public bool destroyOnDamage;
    }


    Rigidbody myRb;
    public float maxDamagevelocity = 5;
    public bool destroyOnDamage = true;
	public bool playerImmune = true;
    void Awake()
    {
        myRb = GetComponent<Rigidbody>();
    }
    protected override void damageUnit(Health unit, int damage)
    {
		
		if (playerImmune && unit.gameObject.GetComponent<CharacterMovement> () != null)
		{
			return;
		}

        damage = Mathf.Clamp( Mathf.RoundToInt(myRb.velocity.magnitude / maxDamagevelocity  * myRb.mass) * damage, 0,damage);
		if (myRb.velocity.magnitude == 0)
		{
			damage = 0;
		}

        base.damageUnit(unit, damage);
        
    }

    protected override void onHit()
    {
        if (destroyOnDamage)
        {
            base.onHit();
        }
    }

    public void setInfo(velocityProjectileInfo info)
    {
        maxDamagevelocity = info.maxDamagevelocity;
        damage = info.damage;
        destroyOnDamage = info.destroyOnDamage;
    }
}
