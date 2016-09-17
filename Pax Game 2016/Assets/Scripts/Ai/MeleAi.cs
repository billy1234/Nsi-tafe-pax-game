using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MeleAi : AiBase
{
    private Color neutralColor;
    private Renderer myRend;
    public CoolDown meleAttackCd;
    public int attackChance = 5;

    private void Start()
    {
        base.Start();
        myRend = GetComponentInChildren<Renderer>();
        neutralColor = myRend.material.color;
        OnWalk.Invoke();
    }
    protected override void OnAquireTarget()
    {
        turn = false;
        myRend.material.color = Color.red;

    }


    protected override void OnPatrol()
    {
        if (state != aiState.CUSTOM_STATE)
        {
            myRend.material.color = neutralColor;

            base.OnPatrol();
        }
    }

    protected override void OnTargetInRange()
    {
        pathfinding.activatePathfinding();
        myRend.material.color = Color.red;
        turn = false;
        base.OnTargetInRange();

    }

    protected override void OnTargetInMin()
    {
        pathfinding.deactivatePathfinding();
        state = aiState.CUSTOM_STATE;
        meleAttack();

    }

    void meleAttack()
    {
        turn = true;
        if (meleAttackCd.checkFire())
        {
            meleAttackCd.fire();
            OnAttack.Invoke();
        }
    }

    protected override void OnCustomState()
    {
        if (target == null)
        {
            OnLoseTarget();
        }
        myRend.material.color = Color.green;
        if (UnityEngine.Random.Range(0, attackChance) == attackChance - 1)
        {
            meleAttack();
        }
    }

  


 }
