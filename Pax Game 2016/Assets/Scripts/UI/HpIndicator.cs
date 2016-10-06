using UnityEngine;
using System.Collections;

public class HpIndicator : MonoBehaviour
{
	private Health myHp;
	public GameObject[] hpIcons;

	void Awake()
	{
		myHp = GetComponent<Health>();
	}

	public void UpdateUi()
	{
		int iconCount =  (int)(myHp.normalizedHp() * (float)hpIcons.Length);
        if (myHp.normalizedHp() == 0)
        {
            iconCount = -1;
        }
		for (int i = 0; i < hpIcons.Length; i++) 
		{
			hpIcons [i].SetActive(i < iconCount);
		}
	}
}
