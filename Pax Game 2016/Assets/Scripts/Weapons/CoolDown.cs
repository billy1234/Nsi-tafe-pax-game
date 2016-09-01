using UnityEngine;
using System.Collections;

[System.Serializable]
public class CoolDown
{
	public float coolDown;
	private float lastShot = float.MinValue;

	public bool checkFire()
	{
		if (Time.time >= lastShot + coolDown) 
		{
			
			return true;
		} 
		else 
		{
			return false;
		}
	}
	public void fire()
	{

		if (checkFire())
		{
			lastShot = Time.time;
		}
		else 
		{
			Debug.LogWarning ("not cooled down use check fire");
		}

	}
		
}
