using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class ShatterOnDeath : MonoBehaviour
{
    public GameObject brokenMesh;
    public GameObject mainMesh;
    public Behaviour[] componentsToDeactivate;
    public Rigidbody rbToMakeKinematic;
    public Collider colToDisable;


	void Awake()
    {
        gameObject.GetComponent<Health>().OnDie.AddListener(shatterMesh);
	}
	

	void shatterMesh ()
    {
        mainMesh.SetActive(false);
        brokenMesh.SetActive(true);       
        if (rbToMakeKinematic != null)
        {
            rbToMakeKinematic.isKinematic = true;
        }
        if (colToDisable != null)
        {
            colToDisable.enabled = false;
        }
        for (int i = 0; i < componentsToDeactivate.Length; i++)
        {
            componentsToDeactivate[i].enabled = false;
        }
    }
}
