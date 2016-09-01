﻿using UnityEngine;
using System.Collections;
public class PlayerRescources : MonoBehaviour
{

	public Energy energy;
	public Ammo blasterAmmo;

	void Start()
	{
	
		energy.amount = 0;
	}

	void Update()
	{
		energy.incrementTime (Time.deltaTime);
		if (Input.anyKey)
		{
			if (energy.checkEnergy (0.1f)) {
				energy.amount -= 0.1f;
			} 
			else 
			{
				print ("ey");
			}
		}
	}


	[System.Serializable]
	public class Energy
	{
		/// <summary>
		/// 0 means empty 1 means full
		/// </summary>
		public float amount {	get{return _amount;}
								set{changeEnergy (value);}}
	
		private float _amount =1f;

		[Tooltip("In Seconds")]
		public float energyPerSecond = 0.2f;
		[Tooltip("In Seconds")]
		public float timeTillRecharge =1f;
		[Tooltip("In Seconds")]
		public float useRechargeDellay = 1f;

		public bool checkEnergy(float value)
		{
			return(_amount - value >= 0);
		}

		private void changeEnergy(float value)
		{
			if (value < _amount) 
			{
				timeTillRecharge = useRechargeDellay;
			}
			_amount = value;
			if (_amount < 0)
			{
				_amount = 0;
			}
			else if (_amount > 1)
			{
				_amount = 1;
			}
		}
		public void incrementTime(float time)
		{

			if (timeTillRecharge > 0) {
				timeTillRecharge -= time;
				if (timeTillRecharge < 0)
				{
					timeTillRecharge = 0f;
				}
			} 
			else if(_amount < 1f)
			{
				_amount += time * energyPerSecond;

			}
		}
	}

	public class Ammo
	{
		public int maxAmmo;

		private int currentAmmo;

		private void changeAmmo(int value)
		{
			
			currentAmmo = value;
			if (currentAmmo < 0)
			{
				currentAmmo = 0;
			}
			else if (currentAmmo > maxAmmo)
			{
				currentAmmo = maxAmmo;
			}
		}

		public bool checkAmmo()
		{
			return checkAmmo (1);
		}
		public bool checkAmmo(int amount)
		{
			return(currentAmmo - amount >= 0);

		}

		public void useAmmo()
		{
			useAmmo (1);
		}
		public void useAmmo(int amount)
		{
			if (checkAmmo (amount)) {
				currentAmmo = currentAmmo - amount;
			} 
			else
			{
				print ("no ammo");
			}

		}
	}
}

