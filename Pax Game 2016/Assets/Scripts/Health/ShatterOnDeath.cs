using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Health))]
public class ShatterOnDeath : MonoBehaviour
{
    public GameObject brokenMesh;
    public GameObject mainMesh;
    public UnityEvent onDeath;


	void Awake()
    {
        gameObject.GetComponent<Health>().OnDie.AddListener(shatterMesh);
	}
	

	void shatterMesh ()
    {
        mainMesh.SetActive(false);
        brokenMesh.SetActive(true);
        onDeath.Invoke();
    }
}
