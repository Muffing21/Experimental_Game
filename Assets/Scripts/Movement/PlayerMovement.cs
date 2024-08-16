using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovementObserver
{
    public HotkeyBinding hotkeyBind;

    public float moveSpeed;

    public float jumpForce;
    public Transform currentHeight;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        hotkeyBind?.RegisterMovementObserver(this);
    }
    private void OnDisable()
    {
        hotkeyBind?.UnregisterMovementObserver(this);
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(currentHeight.position, groundCheckRadius, groundLayer);

    }

    public void OnMove(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    public void OnJump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
