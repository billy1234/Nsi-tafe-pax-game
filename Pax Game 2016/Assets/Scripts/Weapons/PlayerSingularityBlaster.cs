using UnityEngine;
using System.Collections;

public class PlayerSingularityBlaster : Blaster {
     
    public KeyCode shoot;

    void Update()
    {
        if (Input.GetKey(shoot))
        {
            if (cd.checkFire())
            {
				if (deductAmmo(ref resources.singularityAmmo))
                {
                    cd.fire();
                    launchBullet();
                }
            }

        }
    }
}
