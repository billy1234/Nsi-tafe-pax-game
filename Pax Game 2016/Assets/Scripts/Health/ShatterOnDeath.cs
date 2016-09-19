using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Health))]
public class ShatterOnDeath : MonoBehaviour
{
    public GameObject brokenMesh;
    public GameObject mainMesh;
    public UnityEvent onDeath;
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
        if (rb != null)
        {
            foreach (Rigidbody r in brokenMesh.GetComponentsInChildren<Rigidbody>())
            {
                r.AddForceAtPosition(rb.velocity * forceExagerationFactor, rb.position);
                r.AddTorque(Random.onUnitSphere);
            }
        }
        onDeath.Invoke();
    }
}
