using UnityEngine;
using System.Collections;

public abstract class WeaponBase : MonoBehaviour
{
    public int damage = 100;
    protected virtual void damageUnit(Health unit, int damage)
    {
        unit.hp -= damage;

    }


}
