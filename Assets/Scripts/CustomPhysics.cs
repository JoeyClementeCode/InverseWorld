using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class CustomPhysics : MonoBehaviour
    {
        // Can change in inspector
        [Header("Gravity")] 
        public float gravityModifier = 1f;

        [Header("Minimum Ground Check")] [Space(10)]
        public float minGroundCheck = 0.65f;

        // Bool for Ground Check
        protected bool Grounded;

        // Velocity Vector2's
        protected Vector2 Velocity;
        private Vector2 _groundNormal;
        protected Vector2 TargetVelocity;

        // Component Instances
        private Rigidbody2D _rb;
        private ContactFilter2D _contactFilter;

        // Array and List of Raycast Hits
        private readonly RaycastHit2D[] _hits = new RaycastHit2D[16];

        private readonly List<RaycastHit2D> _hitList = new List<RaycastHit2D>(16);

        // Constants for hit calculations
        private const float MinMoveDistance = 0.001f;

        private const float CollisionBuffer = 0.01f;

        // Get Rigidbody Component
        void OnEnable()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            // Reset contact filter to derive from physics object.
            _contactFilter.useTriggers = false;
            _contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
            _contactFilter.useLayerMask = true;
        }

        // Computing Velocity from various sources
        void Update()
        {
            TargetVelocity = Vector2.zero;
            ComputeVelocity();
        }

        // Virtual method that allows computation of velocity across all objects inheriting this CustomPhysics class. I.e, the player (most likely)
        protected virtual void ComputeVelocity()
        {
        }

        void FixedUpdate()
        {
            // Modify Rigidbody Velocity through gravity.
            Velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
            Velocity.x = TargetVelocity.x;
            Grounded = false;
            Vector2 changeInPosition = Velocity * Time.deltaTime;
            
            // X Velocity
            Vector2 changeAlongX = new Vector2(_groundNormal.y, -_groundNormal.x);
            
            Vector2 move = changeAlongX * changeInPosition.x;
            Movement(move, false);
            
            // Y Velocity
            move = Vector2.up * changeInPosition.y;
            Movement(move, true);
        }

        // Changes velocity based off modified velocity and rigid body ground checks. Parameters allow input from velocity and a bool regarding whether the movement is vertical.
        public void Movement(Vector2 move, bool yMovement)
        {
            float distance = move.magnitude;
            if (distance > MinMoveDistance)
            {
                // Count raycast hits and put them in a List named hitList.
                int count = _rb.Cast(move, _contactFilter, _hits, distance + CollisionBuffer);
                _hitList.Clear();
                // Adding Raycast hits from array to List.
                for (int i = 0; i < count; i++)
                {
                    _hitList.Add(_hits[i]);
                }

                // Checks all hit's normals.
                for (int i = 0; i < _hitList.Count; i++)
                {
                    Vector2 currentNormal = _hitList[i].normal;
                    if (currentNormal.y > minGroundCheck)
                    {
                        Grounded = true;
                        if (yMovement)
                        {
                            _groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    /*
                    Modify the distance based on the dot product of the velocity and the nearest normal. This will result in more realistic physics whenever a midair collision is made.
                    Usually mostly applicable to slanted surfaces on roofs of environments or objects.
                    */
                    float projection = Vector2.Dot(Velocity, currentNormal);
                    if (projection < 0)
                    {
                        Velocity = Velocity - projection * currentNormal;
                    }

                    float modifiedDistance = _hitList[i].distance - CollisionBuffer;
                    if (modifiedDistance < distance)
                    {
                        distance = modifiedDistance;
                    }
                }
            }

            // Caluclate the final position and add it to the rigidbody and output to the object.
            _rb.position = _rb.position + move.normalized * distance;
        }
    }
}
