using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    
  public class PlayerController : MonoBehaviour
    {
        // Can change in inspector, controls player physics
        [Header("Player Speed")]
        [Space(10)]
        public float maxSpeed = 5f;
        
        [Header("Player Jump Force")]
        [Space(10)]
        public float jumpForce = 5f;

        [Header("Ground Checking")] [Space(10)]
        public float groundRadius;
        public LayerMask groundLayer;
        
        private Animator _animator;
        private Rigidbody2D _rb;
        private ParticleSystem _particles;

        private float horizontalInput;
        
        private bool canMove = true;
        private bool canJump = true;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _particles = GetComponent<ParticleSystem>();

            transform.position = GameObject.Find("StartPoint").gameObject.transform.position;
        }

        private void Update()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                canJump = true;
            }
            
            if(horizontalInput > 0.01f || horizontalInput == 0)
            {
                transform.localScale = new Vector3(1,1,1);
                /*var particlesShape = _particles.shape;
                particlesShape.scale = new Vector3(-1, 1, 1);
                particlesShape.position = new Vector3(-.21f, -0.48f, 0);
                particlesShape.rotation = new Vector3(0, 0, -9.48f);*/
            }
            else if (horizontalInput < 0.01f)
            {
                transform.localScale = new Vector3(-1,1,1);
               /* var particlesShape = _particles.shape;
                particlesShape.scale = new Vector3(1, 1, 1);
                particlesShape.position = new Vector3(.21f, -0.48f, 0);
                particlesShape.rotation = new Vector3(0, 0, 9.48f);*/
            }

            _animator.SetBool("isInverted", Switch.IsInverted);
            _animator.SetFloat("velocityX", Mathf.Abs(horizontalInput) / maxSpeed);
        }

        private void FixedUpdate()
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, groundRadius, groundLayer);
            bool isGrounded = col != null;
            
            _rb.velocity = new Vector2(horizontalInput * maxSpeed, _rb.velocity.y);

            if (canJump)
            {
                if (isGrounded && Switch.IsInverted == false)
                {
                    _rb.AddForce(Vector2.up * jumpForce);
                }
                else if (isGrounded && Switch.IsInverted)
                {
                    _rb.AddForce(Vector2.up * -jumpForce);
                }

                canJump = false;
            }
        }
    }
}



 
