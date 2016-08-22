using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum aiState
{
    PATROL, ATTACK, KITE_BACK
};

public class AiBase : MonoBehaviour {

   

    private readonly float unitTickRate = 1f;

   // [HideInInspector]
    public aiState state = aiState.PATROL;
    public Transform target;

    [Tooltip("The max range this unit will atack from before getting closer")]
    public float maxRange =5f;

    [Tooltip("set range to 0 if you do not with this unit to kite back")]
    public float minRange =0f;

    public voidEvent OnAiTick;

	void Start ()
    {
        StartCoroutine(aiTick());
	}

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, maxRange);


            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, minRange);
        }
    }
    #endif

    IEnumerator aiTick()
    {
        while (gameObject.active)
        {
            if (target != null)
            {
                float targetDistance = Mathf.Abs((transform.position - target.position).magnitude);
                if (targetDistance > maxRange)
                {
                    state = aiState.PATROL;
                }
                else if (targetDistance < minRange)
                {
                    state = aiState.KITE_BACK;
                }
                else
                {
                    state = aiState.ATTACK;
                }

            }
            else
            {
                state = aiState.PATROL;
            }
            if (OnAiTick != null)
            {
                OnAiTick();
            }

            yield return new WaitForSeconds(unitTickRate);
        }
    }
	


}
