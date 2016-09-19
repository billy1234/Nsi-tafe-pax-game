using UnityEngine;
using System.Collections;

public abstract class WeaponBase : MonoBehaviour
{
    public int damage = 100;
    public bool playerImmune = true;

    protected virtual void damageUnit(Health unit, int damage)
    {

        if (playerImmune && unit.gameObject.GetComponent<CharacterMovement>() != null)
        {
            return;
        }

        unit.hp -= damage;

    }


}
