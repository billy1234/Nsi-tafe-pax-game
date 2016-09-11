using UnityEngine;
using System.Collections;

public class PlayerBlaster : Blaster {
     
    public KeyCode shoot;

    void Update()
    {
        if (Input.GetKey(shoot))
        {
            if (cd.checkFire())
            {
                if (deductAmmo(ref resources.blasterAmmo))
                {
                    cd.fire();
                    launchBullet();
                }
            }

        }
    }
}
