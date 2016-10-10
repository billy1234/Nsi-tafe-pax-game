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
    public Vector3 rbVelocity;


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
                r.AddForceAtPosition(rbVelocity * forceExagerationFactor, rb.position,ForceMode.Impulse);
                r.AddTorque(Random.onUnitSphere);               
            }
            rb.velocity = rbVelocity;
        }
		Collider[] colliders = GetComponents<Collider>();

		foreach (Collider c in colliders)
        {
            c.enabled = false;
        }
    }
}
