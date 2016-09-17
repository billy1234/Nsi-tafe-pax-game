using UnityEngine;
using System.Collections;

public class AIAnimScript : MonoBehaviour
{
    public Animator animController;
	


    public void runAnimation()
    {
        animController.Play("Running");
    }

    public void attackAnimation()
    {
        animController.Play("Attack");
    }
}
