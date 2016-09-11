using UnityEngine;
using System.Collections;

public class Blaster : Equiptable 
{
    public int damage;
    public int bulletPrefabIndex;
    public Transform barrel;
    public float projectileSpeed;
    public CoolDown cd;
    private EntitySpawner spawner;
    void Start()
    {
        spawner = EntitySpawner.getSpawner();
    }

    protected void launchBullet()
    {
        GameObject g = spawner.Instantiate(bulletPrefabIndex, barrel.position, barrel.rotation) as GameObject;
        g.GetComponent<Rigidbody>().AddForce(barrel.forward * projectileSpeed, ForceMode.Force);
        g.GetComponent<DamageOnCollide>().damage = damage;
    }

    public void Fire()
    {
        if (cd.checkFire())
        {
            cd.fire();
            launchBullet();
        }
    }

}
