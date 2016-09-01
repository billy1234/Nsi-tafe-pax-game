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

    // Movement
    private Vector3 moveDirection;
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
    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(hInput, 0, vInput);
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

        rigid.MovePosition(transform.position + moveDirection * Time.deltaTime);
    }
    private void HandleRotation()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.eulerAngles = new Vector3(0, yaw, 0);
        mainCamera.transform.eulerAngles = new Vector3(-pitch, yaw, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
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