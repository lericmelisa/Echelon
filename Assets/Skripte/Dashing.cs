using System;
using System.Collections;
using System.Collections.Generic;
using Game.Movement;
using UnityEngine;

namespace Game.Managment
{
    public class Dashing : MonoBehaviour
    {
        [Header("References")] 
        public Transform orientation;

        public Transform playerCam;
        private Rigidbody rb;
        private PlayerMovement pm;


        [Header("Dashing")] 
        public float dashForce;
        public float dashUpwardForce;
        public float dashDuration;

        public float maxDashYSpeed;
        
        [Header("CameraEffects")]
        public PlayerCamera cam;
        public float dashFov;

        [Header("Dash Settings")] 
        public bool useCameraForward = true;

        public bool allowAllDirections = true;
        public bool disableGravity = false;
        public bool resetVel = true;

        [Header("Cooldown")] 
        private float dashCdTimer;

        public float dashCd;

        [Header("Input")]
        public KeyCode DashKey = KeyCode.LeftShift;

        private bool dashed;
        private void Start()
        {
            rb=GetComponent<Rigidbody>();
            pm = GetComponent<PlayerMovement>();
            dashed = false;
        }

        private void Update()
        {

            if (!EscapeMenu.isPaused)
            {
                if (Input.GetKeyDown(DashKey) && !pm.grounded && !dashed)
                {
                    Dash();
                    dashed = true;
                }
            }


            if (dashCdTimer > 0)
                dashCdTimer -= Time.deltaTime;
            if (pm.grounded)
                dashed = false;
        }

        private void Dash()
        {
            //implementing cool down time
            //dash is still active and we cant dash until time expires
            if (dashCdTimer > 0) return;
            else
            {
                dashCdTimer = dashCd;
            }

            pm.maxYSpeed = maxDashYSpeed;
           
             cam.DoFov(dashFov);

            Transform forwardT;
            if (useCameraForward)
                forwardT = playerCam;
            else
            {
                forwardT = orientation;
            }
            Vector3 direction = GetDirection(forwardT);
            

            pm.dashing = true;
            //calculatin dash force we want to apply
            Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;
            //deactivating gravitz while dashing
            if (disableGravity)
                rb.useGravity = false;
            
            delayedForceToApply=forceToApply;
        
            Invoke(nameof(DelayedDashForce),0.025f);
            
            //astopping dash after some time
            Invoke(nameof(ResetDash),dashDuration);

        }
        
        //sometimes force is added before movemenent script switches to dashing mode so we want to wait a little bit before adding dash force 
        //we'll do that with simple delay dash function

        private Vector3 delayedForceToApply;

        private void DelayedDashForce()
        {
            if (resetVel)
                rb.velocity = Vector3.zero;
            
            rb.AddForce(delayedForceToApply,ForceMode.Impulse);
            
        }
        private void ResetDash()
        {
            if (!disableGravity)
                rb.useGravity = true;

            pm.maxYSpeed = 0;
            pm.dashing = false;
            cam.DoFov(85f);

        }



        private Vector3 GetDirection(Transform forwardT)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3();

            if (allowAllDirections)
            {
                //dashing in eight different directions expl 7:41
                direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
            }
            else
            {
                direction = forwardT.forward;
            }

            if (verticalInput == 0 && horizontalInput == 0)
                direction = forwardT.forward;

            return direction.normalized;
        }
    }
}
