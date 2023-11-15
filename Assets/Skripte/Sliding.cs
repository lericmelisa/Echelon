using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{
    public class Sliding : MonoBehaviour
    {
        [Header("References")] 
        public Transform orientation;

        public Transform playerObj;
        private Rigidbody rb;
        private PlayerMovement pm;

        [Header("Sliding")] 
        public float maxSlideTime;

        public float slideForce;
        private float slideTimer;
        
        //shrink dow player while sliding
        public float slideYScale;
        private float startYScale;

        [Header("Input")] 
        public KeyCode slideKey = KeyCode.L;

        private float horizontalInput;
        private float verticalInput;

        [Header("CameraEffects")]
        public PlayerCamera cam;
        public float slideFov;

        private void Start()
        {
            //getting rb and movement script component
            rb = GetComponent<Rigidbody>();
            pm = GetComponent<PlayerMovement>();


            startYScale = playerObj.localScale.y;
        }

        private void Update()
        {
            if (!EscapeMenu.isPaused)
            {
                verticalInput = Input.GetAxisRaw(("Vertical"));//w,s
                horizontalInput = Input.GetAxisRaw("Horizontal"); //a,d
            
                //to start sliding we need to be pressing at least on of our movement keys
                //getkeydown pressing it
                if(Input.GetKeyDown(slideKey) && (horizontalInput!=0 || verticalInput!=0) && pm.grounded)
                    StartSlide();
            
            
                //to stop sliding
                if(Input.GetKeyUp(slideKey) && pm.sliding)
                    StopSlide();
            }

                
                
        }

        private void StartSlide()
        {
            pm.sliding = true;
            
            
            //to shrink player down like put it into horizontal position
            //just change y scale
            playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
            //after shrinkin it we are floatiig in the air a little bit so we need to push it tot the ground a little bit
            rb.AddForce(Vector3.down*5f,ForceMode.Impulse);
            
            slideTimer = maxSlideTime;

        }

        private void FixedUpdate()
        {
            
            if(pm.sliding)
                SlidingMovement();
        }

        private void SlidingMovement()
        {
            //calculating input direction so we can slide in all directions depending on which jeys we are pressing
            Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            
            //sliding normal when we are not on object thats on some kind of angle
            //applying force in calculated force
            if (!pm.OnSlope() && rb.velocity.y > -0.1f)
            {
                 rb.AddForce(inputDirection.normalized*slideForce,ForceMode.Force);
                 cam.DoFov(slideFov);

                            //while sliding count down slide timer
                            slideTimer -= Time.deltaTime;
            }
            else
            {
                rb.AddForce(pm.GetSlopeMoveDirection(inputDirection)*slideForce,ForceMode.Force);
                cam.DoFov(slideFov);

            }
           
            
            if(slideTimer<=0)
                StopSlide();
        }
        
        
        private void StopSlide()
        {
            pm.sliding = false;
            cam.DoFov(85f);
            //and return player scale back to normal
            playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
        }

    }
    
}
