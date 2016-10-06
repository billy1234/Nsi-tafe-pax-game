using UnityEngine;
using UnityEngine.Events;
using System.Collections;


public class Health : MonoBehaviour
{
    public UnityEvent OnDie, OnTakeDamage, OnHeal;
    public float normalizedHp()
    {
		if (_hp == 0)
		{
			return 0;
		}
        return (float)_hp / (float)MaxHp;
    }
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
    private bool alive = true;

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
		int originalHp = _hp;
        if (!alive)
            return;
		//modify hp
		_hp = newHp;

		if (_hp > MaxHp)
		{

			_hp = MaxHp;
		}
		if (_hp <= 0)
		{
			
			alive = false;
			_hp = 0;
		}

		if (!alive)
		{
			OnDie.Invoke();
		}

		//play hp events
		if (newHp > originalHp)
        {
            OnHeal.Invoke();
        }
		else if(newHp < originalHp)
        {
            OnTakeDamage.Invoke();
        }

       

       
    }

  
}
