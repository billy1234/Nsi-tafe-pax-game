using UnityEngine;
using System.Collections;
using System;

public class BasicAi : AiBase
{
    private Color neutralColor;
    Renderer myRend;
    private void Start()
    {
        base.Start();
        myRend = GetComponentInChildren<Renderer>();
        neutralColor = myRend.material.color;
    }
    protected override void OnAquireTarget()
    {
       // print("seen");
       myRend.material.color = Color.red;
        //throw new NotImplementedException();
    }

    protected override void OnLoseTarget()
    {
        myRend.material.color = neutralColor;
        //print("lost");
        //throw new NotImplementedException();
    }
}
