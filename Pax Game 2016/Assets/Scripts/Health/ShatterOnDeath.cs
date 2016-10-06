using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Health))]
public class ShatterOnDeath : MonoBehaviour
{
    public GameObject brokenMesh;
    public GameObject mainMesh;
    private Health myHp;
    public float forceExagerationFactor = 50f;

    [HideInInspector]
    public Rigidbody rb; //if an rb happend to trigger death we can refrence it here to add force based on it
    


	void Awake()
    {
        gameObject.GetComponent<Health>().OnDie.AddListener(shatterMesh);
	}
	

	void shatterMesh ()
    {
        mainMesh.SetActive(false);
        brokenMesh.SetActive(true);
		brokenMesh.transform.parent = null;
		gameObject.SetActive (false);
        if (rb != null)
        {
            foreach (Rigidbody r in brokenMesh.GetComponentsInChildren<Rigidbody>())
            {
                r.AddForceAtPosition(rb.velocity * forceExagerationFactor, rb.position);
                r.AddTorque(Random.onUnitSphere);
            }
        }
		Collider[] colliders = GetComponents<Collider>();

		foreach (Collider c in colliders)
        {
            c.enabled = false;
        }
    }
}
