using UnityEngine;
using System.Collections;

public class EquipableSwitcher : MonoBehaviour
{
	public equiptableInfo telekenisis;
	public equiptableInfo singularityBlaster;

	private WeaponIndicator weaponIndicator;

	[System.Serializable]
	public struct equiptableInfo
	{
		public bool locked;
		public KeyCode equiptKey;
		public Equiptable equiptable;
	}

	void Awake()
	{
		weaponIndicator = GetComponent<WeaponIndicator>();
		//equipt the starting weapon
		deEquipAll();
		telekenisis.equiptable.equip();
		if (weaponIndicator != null)
			weaponIndicator.displayWeapon (weaponType.TELEKENISIS);
	}

	void Update ()
	{
		if(!telekenisis.locked && Input.GetKeyDown(telekenisis.equiptKey))
		{
			deEquipAll();
			telekenisis.equiptable.equip();
			if (weaponIndicator != null)
				weaponIndicator.displayWeapon (weaponType.TELEKENISIS);
		}
		else if(!singularityBlaster.locked && Input.GetKeyDown(singularityBlaster.equiptKey))
		{
			deEquipAll();
			singularityBlaster.equiptable.equip();
			if (weaponIndicator != null)
				weaponIndicator.displayWeapon (weaponType.SINGULARITY);
		}
	}
	private void deEquipAll()
	{
		telekenisis.equiptable.deEquip();
		singularityBlaster.equiptable.deEquip();
	}


}
