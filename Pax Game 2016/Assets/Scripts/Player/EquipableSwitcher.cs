using UnityEngine;
using System.Collections;

public class EquipableSwitcher : MonoBehaviour
{
	public equiptableInfo[] equiptables;
	public struct equiptableInfo
	{
		public bool unlocked;
		public Equiptable equiptable;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
