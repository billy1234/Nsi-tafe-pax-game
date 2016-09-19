using UnityEngine;
using UnityEngine.Events;
using System.Collections;


public class Health : MonoBehaviour
{
    public UnityEvent OnDie, OnTakeDamage, OnHeal;
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
    #endregion

    void Awake()
    {
        _hp = MaxHp;
    }


    private void changeHp(int newHp)
    {
        
        if (newHp > _hp)
        {
            OnHeal.Invoke();
        }
        else if(newHp < _hp)
        {
            OnTakeDamage.Invoke();
        }

        _hp = newHp;

        if (_hp > MaxHp)
        {

            _hp = MaxHp;
        }

        if (_hp <= 0)
        {
            OnDie.Invoke();
            _hp = 0;
        }
    }

  
}
