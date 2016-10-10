using UnityEngine;
using System.Collections;

public class LookAtParticle : MonoBehaviour
{
	public ForcePush fp;
	private Transform lookAtTrans;


	public void changeRb()
	{
		if (fp.targetRb == null) 
		{
			transform.rotation = Quaternion.identity;
			return;
		}
		lookAtTrans = fp.targetRb.transform;
	}

	void Update()
	{
		if (lookAtTrans != null) 
		{
			transform.LookAt (lookAtTrans);
           
		}
	}

}
