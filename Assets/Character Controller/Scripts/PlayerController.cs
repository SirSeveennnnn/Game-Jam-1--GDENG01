using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    [SerializeField] private float jumpForce;


    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Components")]
    [SerializeField] private Transform orientation;

    //variables
    private float xInput;
    private float yInput;

    private Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    private void Update()
    {
        HandleInput();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Move();
        ControlSpeed();
    }

    void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        Debug.Log(xInput);
        Debug.Log(yInput);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

    }

    void Move()
    {
        moveDirection = (orientation.forward * yInput + orientation.right * xInput).normalized;

        if (isGrounded)
        {
            rb.AddForce(moveDirection * moveSpeed * 10, ForceMode.Force);

        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection * moveSpeed * 10 * airMultiplier, ForceMode.Force);

        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void ControlSpeed()
    {
        Vector3 initialVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (initialVelocity.magnitude > moveSpeed)
        {
            Vector3 newVelocity = initialVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
