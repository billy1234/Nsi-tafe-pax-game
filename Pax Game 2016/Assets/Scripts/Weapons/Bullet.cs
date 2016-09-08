using UnityEngine;
using System.Collections;

public class Bullet : DamageOnCollide
{
	public GameObject spawnOnDeath;
	public Vector3 deathSpawnOffest;
	protected override void onColide ()
	{

		if (spawnOnDeath != null)
		{
			Instantiate (spawnOnDeath,transform.position + deathSpawnOffest,spawnOnDeath.transform.rotation);
		}
		gameObject.SetActive(false);

	}
}
