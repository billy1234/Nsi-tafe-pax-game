using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Health))]
public class InstaDietest : MonoBehaviour {

	
	void Start ()
    {
        gameObject.GetComponent<Health>().hp = 0;
	}
	
	
}
