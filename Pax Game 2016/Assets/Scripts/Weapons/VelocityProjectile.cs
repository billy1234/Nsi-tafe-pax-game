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

	private readonly float minVelocity = 0.2f;

    Rigidbody myRb;
    public float maxDamagevelocity = 5;
    public bool destroyOnDamage = true;
    private bool isTrackingVelocity = false;
    private Vector3 velLastFrame;
    void Awake()
    {
        myRb = GetComponent<Rigidbody>();
    }
    protected override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);
        isTrackingVelocity = false;
    }

    protected override void damageUnit(Health unit, int damage)
    {
		if (myRb.velocity.magnitude < minVelocity)
			return;

        damage = Mathf.Clamp( Mathf.RoundToInt(myRb.velocity.magnitude / maxDamagevelocity  * myRb.mass) * damage, 0,damage);
		if (myRb.velocity.magnitude == 0)
		{
			damage = 0;
		}

        base.damageUnit(unit, damage);
        ShatterOnDeath s =  unit.gameObject.GetComponent<ShatterOnDeath>();
        if(s != null)
        {
            s.rb = myRb;
            s.rbVelocity = velLastFrame;
        }
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

    public void trackVelocity()
    {
        isTrackingVelocity = true;
    }

    private void FixedUpdate()
    {
        if (!isTrackingVelocity)
            return;
        velLastFrame = myRb.velocity;
        
    }
}
