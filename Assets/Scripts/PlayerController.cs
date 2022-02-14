using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float speed = 10f;

    [Header("Vertical Movement")]
    public float jumpHeight = 10f;


    [Header("Object References")]
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

    [Header("Ground Check")]
    public LayerMask layerMask;
    
    // Extra Private Variables
    private bool isFacingRight;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2d.velocity = new Vector2(-speed, rigidbody2d.velocity.y);
            isFacingRight = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody2d.velocity = new Vector2(speed, rigidbody2d.velocity.y);
            isFacingRight = true;
        }
        else
        {
            rigidbody2d.velocity = new Vector2 (0, rigidbody2d.velocity.y);
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Gay");
            rigidbody2d.velocity = Vector2.up * jumpHeight;
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = .2f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeight, layerMask);
        return raycastHit.collider != null;
    }
    
}
