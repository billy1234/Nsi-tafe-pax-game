using UnityEngine;
using System.Collections;

public class EquipableSwitcher : MonoBehaviour
{
	public equiptableInfo telekenisis;
	public equiptableInfo blaster;

	[System.Serializable]
	public struct equiptableInfo
	{
		public bool locked;
		public KeyCode equiptKey;
		public Equiptable equiptable;
	}

	

	void Update ()
	{
		if(!telekenisis.locked && Input.GetKeyDown(telekenisis.equiptKey))
		{
			deEquipAll();
			telekenisis.equiptable.equip();
		}
		else if(!blaster.locked && Input.GetKeyDown(blaster.equiptKey))
		{
			deEquipAll();
			blaster.equiptable.equip();
		}
	}
	private void deEquipAll()
	{
		telekenisis.equiptable.deEquip();
		blaster.equiptable.deEquip();
	}
}
