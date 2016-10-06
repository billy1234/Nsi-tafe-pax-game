using UnityEngine;
using System.Collections;

public enum weaponType
{
	SINGULARITY, TELEKENISIS
};

public class WeaponIndicator : MonoBehaviour {
	[System.Serializable]
	public struct weaponUi
	{
		public GameObject icon;
		public Color color;
	}
	public weaponUi singularityUi;
	public weaponUi telekenisisUi;

	public Material armMaterial;


	public void displayWeapon(weaponType type)
	{
		disableAllIcons ();
		switch (type)
		{
		case weaponType.SINGULARITY:
			updateUi(singularityUi);
			break;
		case weaponType.TELEKENISIS:
			updateUi(telekenisisUi);
			break;
		}
	}



	private void updateUi(weaponUi w)
	{
		disableAllIcons ();
		armMaterial.SetColor("_EmissionColor",w.color);
		w.icon.SetActive (true);
	}
	private void disableAllIcons()
	{
		singularityUi.icon.SetActive (false);
		telekenisisUi.icon.SetActive (false);
	}

}
