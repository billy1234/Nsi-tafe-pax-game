using UnityEngine;
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
    public LayerMask lifatbleObjects;

    public Transform cameraPos;
    private Rigidbody targetObject;
    private Color targetCol;
    private Renderer targetRend;

    [Header("ProjectileSettings")]
    public VelocityProjectile.velocityProjectileInfo projectileInfo;

    public Vector3 GetSingularityPosition()
    {
        return (cameraPos.position + cameraPos.forward * singularityDistance);
    }
    #endregion
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (targetObject == null)
                GetObject();
            else
                PushObjects();
        }
    }
    private void FixedUpdate()
    {
        if (targetObject != null)
        {
            if (deductEnergy(Time.deltaTime * energyPerSec))
            {
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
        if (Physics.Raycast(cameraPos.position, cameraPos.forward, out hit, 100f, lifatbleObjects.value))
        {
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (deductEnergy(Time.deltaTime * energyPerSec))
                {
                    DropObject();
                    targetObject = rb;
                    targetObject.gameObject.AddComponent<VelocityProjectile>().setInfo(projectileInfo);
                    targetRend = rb.gameObject.GetComponent<Renderer>();
                    targetCol = targetRend.material.color;
                    targetRend.material.color = Color.blue;
                }
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
            targetObject.AddForce(cameraPos.forward * forceStr, ForceMode.Impulse);
            deductEnergy(pushEnergy);
            DropObject();
        }
    }

    void DropObject()
    {
        if (targetRend != null)
        {
            targetRend.material.color = targetCol;
        }
        targetRend = null;
        targetCol = Color.white;
        targetObject = null;
    }

    void OnDisable()
    {
        DropObject();
    }
}