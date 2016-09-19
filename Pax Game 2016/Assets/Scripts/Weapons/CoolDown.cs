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


    /// <summary>
    /// Will set the cooldown to now if the object is off cooldown
    /// </summary>
	public void fire()
	{

		if (checkFire())
		{
			lastShot = Time.time;
		}
		else 
		{
			//Debug.LogWarning ("not cooled down use check fire");
		}

	}


    /// <summary>
    /// Will set the cooldown to now even if the object is not off cooldown
    /// </summary>
    public void fireImmediate()
    {
        lastShot = Time.time;        
    }

}
