using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ForcePush : Equiptable
{
    #region Variables
    [Header("Force")]
    public float forceStr = 150f;
    public float energyPerSec = 0.05f;
    public float pushEnergy = 0.2f;
    [Header("Object Movement")]
    [Range(0f, 1f)]
    public float dampFactor = 0.9f;
    public float maxObjectVelocity = 1f;
    [Header("Singularity")]
    public float singularityDistance = 5f;
    public float singularityRadius = 1f;
	public float sphereCastWidth =1f;
    public LayerMask lifatbleObjects;

    public Transform cameraPos;
	[HideInInspector]
	public Rigidbody targetRb;
    private Color targetCol;
    private Renderer targetRend;
	public UnityEvent onCast,onAquired,onHold,onDrop,onLaunch;

    public Vector3 GetSingularityPosition()
    {
        return (cameraPos.position + cameraPos.forward * singularityDistance);
    }
    #endregion
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {			
			if (targetRb == null) {
				onCast.Invoke ();
				GetObject ();
			} 
			else 
			{
				PushObjects ();
			}
        }
    }
    private void FixedUpdate()
    {
        if (targetRb != null)
        {
            if (deductEnergy(Time.deltaTime * energyPerSec))
            {
				onHold.Invoke();
                MoveObject();
            }
            else
            {
                DropObject();
            }
        }
    }
    private void GetObject()
    {
        RaycastHit hit;
		if (Physics.SphereCast(cameraPos.position,sphereCastWidth, cameraPos.forward, out hit, 100f, lifatbleObjects.value))
        {
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (deductEnergy(Time.deltaTime * energyPerSec))
                {
					DropObject();
					onAquired.Invoke ();                   
                    targetRb = rb;
					targetRb.useGravity = false;
                    targetRend = rb.gameObject.GetComponent<Renderer>();
                    targetCol = targetRend.material.color;
                    targetRend.material.color = Color.blue;
                }
            }
        }
    }
    private void MoveObject()
    {
        Vector3 singularityPos = GetSingularityPosition();
        Vector3 offset = singularityPos - targetRb.position;
        if (targetRb.velocity.magnitude > maxObjectVelocity)
        {
            targetRb.velocity = targetRb.velocity * dampFactor;
        }
        //targetRb.MovePosition(targetRb.position + offset / 5f);
        targetRb.AddForce(offset,ForceMode.VelocityChange);
        
       

    }
    private void PushObjects()
    {
        if (targetRb != null)
        {
			onLaunch.Invoke ();
			targetRb.velocity = Vector3.zero;
            targetRb.AddForce(cameraPos.forward * forceStr, ForceMode.Impulse);
            deductEnergy(pushEnergy);
            DropObject();
        }
    }

    void DropObject()
    {
		onDrop.Invoke();
        if (targetRend != null)
        {
            targetRend.material.color = targetCol;
        }      
        targetCol = Color.white;
		if (targetRb != null)
		{
			targetRb.useGravity = true;
			targetRb = null;
		}
		targetRend = null;
    }

    void OnDisable()
    {
        DropObject();
    }

}