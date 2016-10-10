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
        g.GetComponent<Rigidbody>().AddForce(barrel.forward * projectileSpeed, ForceMode.Impulse);
        g.GetComponent<WeaponBase>().damage = damage;
    }

    public void Fire()
    {
        Fire(true);
    }

    public void Fire(bool setCd)
    {
        if (cd.checkFire())
        {
            if (setCd)
            {              
                cd.fire();
            }
           
            launchBullet();
        }
    }

    protected void FireIgnoreCd()
    {
        launchBullet();
    }

    public void FireDellay(float wait)
    {
        StartCoroutine(dellayFireCRoutine(wait,false));
    }

    public void FireDellayWithCd(float wait)
    {
        StartCoroutine(dellayFireCRoutine(wait,true));
    }


    IEnumerator dellayFireCRoutine(float wait,bool startCoolDown)
    {
        if (cd.checkFire())
        {
            if (startCoolDown)
            {                
                cd.fireImmediate();
            }
            yield return new WaitForSeconds(wait);
            FireIgnoreCd();
        }
    }
}
