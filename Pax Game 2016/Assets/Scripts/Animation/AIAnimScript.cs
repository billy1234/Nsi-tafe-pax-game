﻿using UnityEngine;
using System.Collections;

public class AIAnimScript : MonoBehaviour
{
    public Animator animController;
	
	void Awake ()
    {
		//gameObject.GetComponent<MeleAiBarge> ().OnWalk.AddListener(runAnimation);
    }

    public void runAnimation()
    {
        animController.Play("Running");
    }
}
