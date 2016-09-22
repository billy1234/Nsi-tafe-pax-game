using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    public float movementSpeed = 7.5f;
    [Header("Jumping")]
    public float jumpForce = 8f;
    [Header("Camera")]
    public float mouseSensitivity = 2.5f;
	public float maxVelocity = 3f;
	public float maxCameraRotation = 90, minCameraRotation =  -90;
    // Movement
    private Vector3 moveDirection;
    private Vector3 rbVel;
    private Rigidbody rigid;
    // Jumping
    private bool isGrounded = true;
    private bool isJumping = false;
    // Camera
    private Transform mainCamera;
    private float yaw;
    private float pitch;
    #endregion
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        mainCamera = GameObject.FindObjectOfType<Camera>().transform;
        LockCursor(true);
    }
    private void FixedUpdate()
    {
        HandleMovement();
       
    }
	private void LateUpdate()
	{
		HandleRotation();
	}
    private void HandleMovement()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(hInput, 0f, vInput);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= movementSpeed;

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigid.AddForce(transform.up * jumpForce, ForceMode.Impulse);
				
                isGrounded = false;
                isJumping = true;
            }
        }

		rigid.AddForce(moveDirection * Time.fixedDeltaTime * movementSpeed,ForceMode.VelocityChange);
        if (rigid.velocity.magnitude > maxVelocity)
        {
            rbVel = rigid.velocity.normalized * maxVelocity;
            rbVel.y = rigid.velocity.y;
            rigid.velocity = rbVel;
        }
    }
    private void HandleRotation()
    {
		if(Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)
			return;

		yaw 	+= Input.GetAxis("Mouse X") * mouseSensitivity;
		pitch 	+= -Input.GetAxis("Mouse Y") * mouseSensitivity;
		pitch 	= Mathf.Clamp (pitch ,minCameraRotation,maxCameraRotation)				;

		transform.rotation = Quaternion.Euler (0f,yaw,0f);
		mainCamera.rotation = Quaternion.Euler (pitch,yaw,0f);

        if (Input.GetKey(KeyCode.Escape))
        {
            LockCursor(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            LockCursor(true);
        }
    }
    private void LockCursor(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (!isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 2f))
            {
                isGrounded = true;
                isJumping = false;
            }
            else
            {
                isGrounded = false;
            }
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 2f))
            {
                isGrounded = true;
                isJumping = false;
            }
            else
            {
                isGrounded = false;
            }
        }
    }
}