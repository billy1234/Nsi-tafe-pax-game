using UnityEngine;
using System.Collections;

public class Projectile : DamageOnCollide
{
    protected override void onHit()
    {
        gameObject.SetActive(false);
    }
}
