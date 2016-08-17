using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class DisableOnDie : MonoBehaviour
{
    private Health h;

    void Awake()
    {

        h = gameObject.GetComponent<Health>();     
        h.OnDie += Disable;

    }


    void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
