using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmoSpawner : MonoBehaviour
{
    public int ammoPrefab;
    //public CoolDown refresh;
    public List<SingularityPickup> inactivePickups;

    void Awake()
    {
        foreach(SingularityPickup pickup in GameObject.FindObjectsOfType<SingularityPickup>())
        {
            inactivePickups.Add(pickup);
            pickup.gameObject.SetActive(false);
        }
    }

    public void RespawnAmmo()
    {
        SingularityPickup activePickup = inactivePickups[Random.Range(0, inactivePickups.Count - 1)];
        activePickup.gameObject.SetActive(true);
        inactivePickups.Remove(activePickup);
    }

    public void AddToList(SingularityPickup pickup)
    {
        inactivePickups.Add(pickup);
        pickup.gameObject.SetActive(false);
    }
}