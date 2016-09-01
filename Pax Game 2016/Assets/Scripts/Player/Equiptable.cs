using UnityEngine;
using System.Collections;

public abstract class Equiptable : MonoBehaviour
{
	public PlayerRescources resources;


	protected void Awake()
	{
		resources = GetComponent<PlayerRescources>();
	}

	protected bool deductEnergy(float amount)
	{
		bool canUse = resources.energy.checkEnergy (amount);
		if (canUse) 
		{
			resources.energy.amount -= amount;
			return true;
		} 
		else
		{
			return false;
		}
	}
	/// <summary>
	/// Deducts amount from ammo if posible will return true or false depending on the avaliable ammo.
	/// </summary>
	/// <returns><c>true</c>, if ammo was deducted, <c>false</c> otherwise.</returns>
	/// <param name="amount">Amount.</param>
	/// <param name="ammoType">Ammo type.</param>
	protected bool deductAmmo(int amount, ref PlayerRescources.Ammo ammoType)
	{
		bool canUse = ammoType.checkAmmo(amount);
		if (canUse) 
		{
			ammoType.useAmmo (amount);
			return true;
		} 
		else
		{
			return false;
		}
	}
	/// <summary>
	/// trys to deduct 1 ammo if posible will return true or false depending on the avaliable ammo
	/// </summary>
	/// <returns><c>true</c>, if ammo was deducted, <c>false</c> otherwise.</returns>
	/// <param name="ammoType">Ammo type.</param>
	protected bool deductAmmo(ref PlayerRescources.Ammo ammoType)
	{
		return deductAmmo (1,ref ammoType);
	}

	protected virtual void equip()
	{
		gameObject.SetActive (true);
	}

	protected virtual void deEquip()
	{
		gameObject.SetActive (false);
	}
}
