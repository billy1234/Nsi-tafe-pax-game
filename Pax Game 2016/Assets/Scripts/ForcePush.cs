using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForcePush : MonoBehaviour
{
    #region Variables
    [Header("Force")]
    public float forceStr = 150f;
    [Header("Object Movement")]
    [Range(0f, 1f)]
    public float dampFactor = 0.9f;
    public float maxObjectVelocity = 1f;
    [Header("Singularity")]
    public float singularityDistance = 5f;
    public float singularityRadius = 1f;
    public LayerMask lifatbleObjects;

    private Collider[] hitColliders;
    private Rigidbody targetObject;
    // private List<Rigidbody> targetObjects = new List<Rigidbody>();
    private Color targetCol;
    private Renderer targetRend;

    [Header("ProjectileSettings")]
    public VelocityProjectile.velocityProjectileInfo projectileInfo;


    public Vector3 GetSingularityPosition()
    {
        return (transform.position + transform.forward * singularityDistance);
    }
    #endregion
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetObject();
        }
        if (Input.GetMouseButtonDown(1))
        {
            PushObjects();
        }
    }
    private void FixedUpdate()
    {
        if (targetObject != null)
            MoveObject();
    }

    private void GetObject()
    {
        hitColliders = Physics.OverlapSphere(GetSingularityPosition(), singularityRadius, lifatbleObjects.value);
        foreach (Collider col in hitColliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (targetObject != null)
                {
                    targetRend.material.color = targetCol;
                }

                targetObject = rb;
                targetObject.gameObject.AddComponent<VelocityProjectile>().setInfo(projectileInfo);
                // trun the object blue
                targetRend = rb.gameObject.GetComponent<Renderer>();
                targetCol = targetRend.material.color;
                targetRend.material.color = Color.blue;
                //
                break;
            }
        }
    }

    private void MoveObject()
    {
        Vector3 lastFrame = GetSingularityPosition();

        Vector3 offset = lastFrame - targetObject.position;

        if (targetObject.velocity.magnitude > maxObjectVelocity)
        {
            targetObject.velocity = targetObject.velocity * dampFactor;
        }

        targetObject.AddForce(offset, ForceMode.Impulse);

    }
    private void PushObjects()
    {
        if (targetObject != null)
        {
            targetObject.AddForce(transform.forward * forceStr, ForceMode.Impulse);            
            targetRend.material.color = targetCol;
            targetObject = null;
        }
    }
 
}