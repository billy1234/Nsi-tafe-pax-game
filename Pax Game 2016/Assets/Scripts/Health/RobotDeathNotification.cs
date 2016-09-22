using UnityEngine;
using System.Collections;

public class RobotDeathNotification : MonoBehaviour
{
    private Health robotHp;
    private RoundManager rManager;

	void Start ()
    {
        robotHp = GetComponent<Health>();
        rManager = FindObjectOfType<RoundManager>();
        robotHp.OnDie.AddListener(this.alertManager);
	}


    private void alertManager()
    {
        rManager.OnEnemyDeath();
    }

	
	
}
