using UnityEngine;
using System.Collections;

public class AIAnimScript : MonoBehaviour
{
    public Animator animController;
	
    void Start()
    {
        //must be called so the Animator will actually play anything work when you change states
        animController.Play("Idle");
    }


    public void runAnimation()
    {
        animController.Play("Running");
    }

    public void attackAnimation()
    {
        animController.Play("Attack");
    }

    public void ideAnimaton()
    {
        animController.Play("Idle");
    }
}
