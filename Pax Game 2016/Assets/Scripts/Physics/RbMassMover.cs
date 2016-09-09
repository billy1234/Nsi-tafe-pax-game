using UnityEngine;
using System.Collections;

public class RbMassMover : MonoBehaviour
{

	public Transform newMassCenter;

	public void changeRbMass()
	{
		gameObject.GetComponent<Rigidbody> ().centerOfMass = newMassCenter.localPosition;
	}
}
