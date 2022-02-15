using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
  public class PlayerController : CustomPhysics
    {
        // Can change in inspector, controls player physics
        [Header("Player Speed")]
        [Space(10)]
        public float maxSpeed = 5f;
        
        [Header("Player Jump Force")]
        [Space(10)]
        public float jumpForce = 5f;
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        
        public bool canMove = true;
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }
        
        // Overriding the physics script's ComputeVelocity method to allow for the velocity of the player to be changed.
        protected override void ComputeVelocity()
        {
            if (canMove)
            {
                // Reset the velocity.
                Vector2 move = Vector2.zero;
                // Ask for horizontal input.
                move.x = Input.GetAxis("Horizontal");
                // Input for jumping as well as adding a jump force.
                if (Input.GetButtonDown("Jump") && Grounded)
                {
                    Velocity.y = jumpForce;
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    if (Velocity.y > 0)
                    {
                        Velocity.y = Velocity.y * 0.5f;
                    }
                }
                // Flip Character
                if (move.x > 0.01f || move.x == 0)
                {
                    _spriteRenderer.flipX = false;
                }
                else if (move.x < 0.01f)
                {
                    _spriteRenderer.flipX = true;
                }
                
                _animator.SetBool("isInverted", Switch.IsInverted);
                _animator.SetFloat("velocityX", Mathf.Abs(Velocity.x) / maxSpeed);
                /*
                animator.SetBool("grounded", grounded);

                animator.SetFloat("velocityY", velocity.y);
                */
                
                // Modify the targetVelocity variable to change the x velocity in the physics script.
                TargetVelocity = move * maxSpeed;
            }
        }
    }
}



 
