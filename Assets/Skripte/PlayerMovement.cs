using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game.Movement
{
    public class PlayerMovement : MonoBehaviour
    {


        [Header("Movement")] private float moveSpeed;
        public float walkSpeed;
        public float sprintSpeed;
        public float slideSpeed;

        
        [Header("Dash")]
        private float desiredMoveSpeed;
        private float lastDesiredMoveSpeed;
        private MovementState lastState;
        private bool keepMomentum;
     
     

        public float speedIncreaseMultiplier;
        public float slopeIncreaseMultiplier;

      
        public float dashSpeed;
        public float dashSpeedChangeFactor;

        public float maxYSpeed;

  

        
        public float groundDrag;

        [Header("Jumping")] public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump;

        [Header("Crouching")] public float crouchSpeed;
        public float crouchYScale;
        private float startYScale;

        [Header("Keybinds")] public KeyCode jumpKey = KeyCode.Space;
        public KeyCode sprintKey = KeyCode.LeftShift;
        public KeyCode crouchKey = KeyCode.LeftControl;

        [Header("Ground Check")] public float playerHeight;
        public LayerMask whatIsGround;
        public bool grounded;

        [Header("Slope Handling")] public float maxSlopeAngle;
        private RaycastHit slopeHit;
        private bool exitingSlope;

        [Header("Camera Effects")]
        public PlayerCamera cam;
        public float grappleFov = 95f;

        public Transform orientation;

        float horizontalInput;
        float verticalInput;

        Vector3 moveDirection;

        Rigidbody rb;
        public MovementState state;
        public enum MovementState
        {
            freeze,
            walking,
            sprinting,
            crouching,
            sliding,
            air,
            dashing,
            grappling,
            rewinding,
            swinging
        }
        public bool freeze;

        public bool sliding;
        public bool dashing;
        public bool grappling;
        public bool rewinding;
        public bool swinging;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            readyToJump = true;
            startYScale = transform.localScale.y;
        }

        private void Update()
        {
            // ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            if(!EscapeMenu.isPaused)
                MyInput();
            SpeedControl();
            StateHandler();

            // handle drag
            //adding drag when its not dashing
            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
            
            
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MyInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            // when to jump
            if (Input.GetKey(jumpKey) && grounded && readyToJump)
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }

            // // start crouch
            // if (Input.GetKeyDown(crouchKey))
            // {
            //     transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            //     rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            // }

            // // stop crouch
            // if (Input.GetKeyUp(crouchKey))
            // {
            //     transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            // }
        }
        private float speedChangeFactor;
        private void StateHandler()
        {
            
            if (freeze)
            {
                state = MovementState.freeze;
                moveSpeed = 0;
                rb.velocity = Vector3.zero;
            }
            else if (rewinding)
            {
                state = MovementState.rewinding;
                moveSpeed = 0;
                rb.velocity = Vector3.zero;
            }
            //mode dashing

            else if (dashing)
            {
                state = MovementState.dashing;
                desiredMoveSpeed = dashSpeed;
                speedChangeFactor = dashSpeedChangeFactor;
            }
            // Mode - Sliding
            else if (sliding)
            {
                state = MovementState.sliding;

                if (OnSlope() && rb.velocity.y < 0.1f)
                    desiredMoveSpeed = slideSpeed;

                else
                    desiredMoveSpeed = sprintSpeed;
            }
            else if (grappling)
            {
                state = MovementState.grappling;
                desiredMoveSpeed = sprintSpeed;
            }
            else if (swinging)
            {
                state = MovementState.swinging;
                desiredMoveSpeed = sprintSpeed;
            }
            // // Mode - Crouching
            // else if (Input.GetKey(crouchKey))
            // {
            //     state = MovementState.crouching;
            //     desiredMoveSpeed = crouchSpeed;
            // }

            // // Mode - Sprinting
            // else if (grounded && Input.GetKey(sprintKey))
            // {
            //     state = MovementState.sprinting;
            //     desiredMoveSpeed = sprintSpeed;
            // }

            // Mode - Walking
            else if (grounded)
            {
                state = MovementState.walking;
                desiredMoveSpeed = sprintSpeed;
            }

            // Mode - Air
            else
            {
                state = MovementState.air;
            }
            if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
                {
                    StopAllCoroutines();
                    StartCoroutine(SmoothlyLerpMoveSpeed());
                    
                }
                else
                {
                    moveSpeed = desiredMoveSpeed;
                }

                bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
                // we want to keep momentum after dashing so
                if (lastState == MovementState.dashing || lastState == MovementState.grappling) 
                    keepMomentum = true;

                if (desiredMoveSpeedHasChanged)
                {
                    if (keepMomentum)
                    {
                        StopAllCoroutines();
                        StartCoroutine(SmoothlyLerpMoveSpeed());
                    }
                    else
                    {
                        StopAllCoroutines();
                        moveSpeed = desiredMoveSpeed;
                    }
                    
                    
                    
                    
                    
                    
                    
                }
                lastDesiredMoveSpeed = desiredMoveSpeed;
                lastState = state;
            
        }

        private IEnumerator SmoothlyLerpMoveSpeed()
        {
            // smoothly decreasing movementSpeed to desired value
            float time = 0;
            float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
            float startValue = moveSpeed;
            float boostFactor = speedChangeFactor;

            while (time < difference)
            {
                moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

                if (OnSlope())
                {
                    float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                    float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                    time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
                }
                else
                    time += Time.deltaTime * speedIncreaseMultiplier;

                yield return null;
            }

            moveSpeed = desiredMoveSpeed;
            speedChangeFactor = 1f;
            keepMomentum = false;
        }

        private void MovePlayer()
        {
            if (state == MovementState.dashing || state == MovementState.grappling || state == MovementState.swinging)
                return;
            
            
            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            
            

            // on slope
            if (OnSlope() && !exitingSlope)
            {
                cam.DoFov(90f);
                rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

                if (rb.velocity.y > 0)
                    rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }

            // on ground
            else if (grounded)
            {
                cam.DoFov(90f);
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }

            // in air
            else if (!grounded)
            {
                cam.DoFov(90f);
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
                rb.AddForce(Vector3.down * 20f * Time.deltaTime);
            }

            // turn gravity off while on slope
            rb.useGravity = !OnSlope();
        }

        private void SpeedControl()
        {
            if(grappling) return;
            // limiting speed on slope
            if (OnSlope() && !exitingSlope)
            {
                if (rb.velocity.magnitude > moveSpeed)
                    rb.velocity = rb.velocity.normalized * moveSpeed;
            }

            // limiting speed on ground or in air
            else if(grounded || !readyToJump)
            {
                Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                // limit velocity if needed
                if (flatVel.magnitude > moveSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized * moveSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
                }
            }
            
            
            //limiting y velocity so when using dashin and camera forward we dont jump to sky
            if (maxYSpeed != 0 && rb.velocity.y > maxYSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
            }
        }

        private void Jump()
        {
            exitingSlope = true;

            // reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;

            exitingSlope = false;
        }
        private bool enableMovementOnNextTouch;
        public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
        {
            grappling = true;

            velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
            Invoke(nameof(SetVelocity), 0.1f);
            //StartCoroutine(SetVelocity(targetPosition));

            Invoke(nameof(ResetRestrictions), 0f);
        }

        private Vector3 velocityToSet;
        private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;

        cam.DoFov(grappleFov);
    }
        public float maxGrapplingSpeed = 15;
        // private IEnumerator SetVelocity(Vector3 targetPosition)
        // {
        //     enableMovementOnNextTouch = true;
        //     float glideDuration = 0.1f;
        //     float elapsedTime = 0f;
        //     Vector3 initialVelocity = rb.velocity;

        //     while (elapsedTime != glideDuration)
        //     {
        //         elapsedTime += Time.deltaTime;

        //         float t = Mathf.Clamp01(elapsedTime / glideDuration);
        //         rb.velocity = Vector3.Lerp(initialVelocity, velocityToSet, t);
        //         rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxGrapplingSpeed); // Limit the magnitude of velocity

        //         yield return null;
        //     }

        //     rb.velocity = velocityToSet;
        //     rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxGrapplingSpeed); // Limit the magnitude of velocity
        //     cam.DoFov(grappleFov);
        // }
        public void ResetRestrictions()
        {
            grappling = false;
            cam.DoFov(85f);
        }
        // private void OnCollisionEnter(Collision collision)
        // {
        //     if (enableMovementOnNextTouch)
        //     {
        //         enableMovementOnNextTouch = false;
        //         ResetRestrictions();

        //         GetComponent<DualHooks>().CancelActiveGrapples();
        //     }
        // }
        public bool OnSlope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            return false;
        }

        public Vector3 GetSlopeMoveDirection(Vector3 direction)
        {
            return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
        }
       public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
        {
            float gravity = Physics.gravity.y;
            float displacementY = endPoint.y - startPoint.y;
            Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
            Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) 
                + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

            return velocityXZ + velocityY;
        }
    }
}

