using UnityEngine;
using System.Collections;

public class SingularityPickup : MonoBehaviour
{
    private AmmoSpawner ammoSpawner;

    private void Awake()
    {
        ammoSpawner = GameObject.FindObjectOfType<AmmoSpawner>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<PlayerRescources>())
        {
            PlayerRescources pResources = col.GetComponent<PlayerRescources>();

            if(pResources.canPickup() == true)
            {
                ammoSpawner.AddToList(this);
                pResources.singularityAmmo.addAmmo(1);
            }
        }
    }

}
