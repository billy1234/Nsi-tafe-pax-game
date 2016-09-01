using UnityEngine;
using System.Collections;

public class Blaster : Equiptable 
{
	public int damage;
	public GameObject bulletPrefab;
	public Transform barrel;
	public KeyCode shoot;
	public float projectileSpeed;
	public CoolDown cd;
	void Update () 
	{
		if (Input.GetKey (shoot))
		{
			if(cd.checkFire())
			{
				if (deductAmmo (ref resources.blasterAmmo))
				{
					cd.fire ();
					launchBullet ();
				}
			}

		}	
	}

	void launchBullet()
	{
		GameObject g = Instantiate (bulletPrefab,barrel.position,barrel.rotation)as GameObject;
		g.GetComponent<Rigidbody>().AddForce(barrel.forward * projectileSpeed,ForceMode.Force);
		g.GetComponent<DamageOnCollide> ().damage = damage;
	}
}
