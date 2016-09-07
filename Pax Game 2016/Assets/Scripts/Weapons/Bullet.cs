using UnityEngine;
using System.Collections;

public class Bullet : DamageOnCollide
{

	protected override void onColide ()
	{
		gameObject.SetActive(false);
	}
}
