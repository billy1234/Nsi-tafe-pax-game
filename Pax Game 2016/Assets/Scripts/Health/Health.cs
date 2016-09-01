using UnityEngine;
using System.Collections;

public delegate void voidEvent();

public class Health : MonoBehaviour
{

    #region variables
    /// <summary>
    /// Max health this unit can hold
    /// </summary>
    public int MaxHp = 100;
    /// <summary>
    /// current health
    /// </summary>
    [HideInInspector]
    public int hp   {
                    get { return _hp; }
                    set { changeHp(value); }
                    }

    private int _hp;

    /// <summary>
    /// If you have a behavior you wish to trigger += it to these events
    /// idealy in awake, DO NOT call these events it will not cause death healing ect
    /// </summary>
    public voidEvent OnDie, OnTakeDamage, OnHeal;
    #endregion

    void Awake()
    {
        _hp = MaxHp;
    }


    private void changeHp(int newHp)
    {
        
        if (newHp > _hp)
        {
            runEvent(OnHeal);
        }
        else if(newHp < _hp)
        {
            runEvent(OnTakeDamage);
        }

        _hp = newHp;

        if (_hp > MaxHp)
        {

            _hp = MaxHp;
        }

        if (_hp <= 0)
        {
            runEvent(OnDie);
            _hp = 0;
        }
    }

    private void runEvent(voidEvent e)
    {
        if (e != null)
        {
            e();
        }
    }
}
