using System;
using System.Collections;
using System.Collections.Generic;
using Game.Movement;
using Unity.Mathematics;
using UnityEngine;

public class Throwing : MonoBehaviour
{
  public AudioSource audioSource;

   //public Transform cam;

   //public Transform attackPoint;

   //public GameObject objectToThrow;

   //public float throwCooldown;

   //public KeyCode throwKey = KeyCode.Mouse1;
   //public float throwForce;
   //public float throwUpwardForce;

   //private bool readyToThrow;
   //// Start is called before the first frame update
   //void Start()
   //{
   //    readyToThrow = true;
   //}

   //// Update is called once per frame
   //void Update()
   //{
   //    if (Input.GetKeyDown(throwKey) && readyToThrow)
   //    {
   //        Throw();
   //    }
   //}

   //private void Throw()
   //{
   //    readyToThrow = false;
   //    GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

   //    Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

   //    Vector3 forceDirection = cam.transform.forward;
   //    RaycastHit hit;
   //    if (Physics.Raycast(cam.position, cam.forward, out hit, 1000f))
   //    {
   //        forceDirection = (hit.point - attackPoint.position).normalized;
   //    }
   //    Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
   //    
   //    projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
   //    
   //    Invoke(nameof(ResetThrow), throwCooldown);
   //}

   //private void ResetThrow()
   //{
   //    readyToThrow = true;
   //}
   
   public GameObject projectilePrefab;
   public Transform throwPoint;
   public KeyCode throwKey = KeyCode.Mouse1;
   public float throwForce;
   public float throwUpwardForce;
   private bool readyToThrow = true;
   private int throwC = 0;
   private GameObject projectile;
   public GameObject teleportationEffectPrefab;
   public bool isRecording;
   public List<Vector3> PointsInTime;
   public Rigidbody rb;
   public PlayerMovement pm;
   public bool isTeleporting;
   private float tpTimer; // Timer variable to track elapsed time
   private float tpDuration = 0.1f;
   public PlayerCamera cam;
   public float dashFov;
   private int throwCounter = 0;
   public Animator animator;
   
   private void Start()
   {
   }

   private void Update()
   {

      if (!EscapeMenu.isPaused)
      {
         if (Input.GetKeyDown(throwKey) && readyToThrow && throwC == 0 && throwCounter<3)
         {
            StartRecording();
            animator.SetTrigger("TrShoot");
            ThrowProjectile();
            audioSource.Play();
         }
         else if (Input.GetKeyDown(throwKey) && throwC == 1)
         {
            StopRecording();
            StartTeleportation();
         
            //TeleportPlayer(projectile.transform.position);
            Destroy(projectile);
         }
      }


      if (pm.grounded == true)
         throwCounter = 0;
     if(isRecording)
        Record();
     if(isTeleporting)
        JumpBackToPoint();
   }

   private void ThrowProjectile()
   {
      readyToThrow = false;
      throwC = 1;
      throwCounter++;

       projectile = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);
      Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

      Vector3 throwDirection = throwPoint.forward;
      projectileRb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
      projectileRb.AddForce(Vector3.up * throwUpwardForce, ForceMode.Impulse);
      
      CustomBullet projectileCollision = projectile.GetComponent<CustomBullet>();
      projectileCollision.Initialize(this, projectileRb);
   }
    
   private void StartRecording()
   {
      isRecording=true;

    }
   public void StopRecording()
   {
      isRecording=false;
   }
   public void StartTeleportation()
   {
      isTeleporting = true;
      rb.isKinematic = true;
      pm.freeze = true;
      tpTimer = 0f;


   }
   private void StopTeleportation()
   {
      rb.isKinematic = false;
      isTeleporting = false;
      pm.freeze = false;
      PointsInTime.Clear();
      tpTimer = 0f;
      throwC = 0;
      readyToThrow = true;
      cam.DoFov(85f);


   }
   
   private void JumpBackToPoint()
   {
      throwC = 0;
      readyToThrow = true;

      if (PointsInTime.Count > 0)
      {
         tpTimer += Time.deltaTime;

         float t = tpTimer / tpDuration; // Calculate interpolation factor

         // Interpolate player position and rotation
         int currentIndex = Mathf.FloorToInt(t * (PointsInTime.Count - 1));
         int nextIndex = currentIndex + 1;
         t = (t * (PointsInTime.Count - 1)) - currentIndex;
         cam.DoFov(dashFov);
         if (nextIndex < PointsInTime.Count)
         {
            rb.transform.position = Vector3.Lerp(PointsInTime[currentIndex], PointsInTime[nextIndex], t);
         }
         // else
         // {
         //     if (currentIndex >= 0 && currentIndex <= pointsInTime.Count)
         //     {
         //         PlayerObj.position = pointsInTime[pointsInTime.Count - 1].position; // Set the player position to the first recorded position
         //         orientation.rotation = pointsInTime[pointsInTime.Count - 1].rotationO; // Set the player's orientation to the first recorded rotation
         //         CamHolder.rotation = pointsInTime[pointsInTime.Count - 1].rotationC; // Set the camera holder rotation to the first recorded rotation
         //     }
         // }
         if (tpTimer >= tpDuration)
         {
            StopTeleportation(); // Call the StopRewinding method when the rewind process is complete
         }


      }
      else
      {
            
         StopTeleportation();
      }
   }
   //public void TeleportPlayer(Vector3 teleportPosition)
   //{
   //   transform.position = teleportPosition;
//
   //   // Perform any additional teleportation logic or effects here
   //   GameObject teleportationEffect = Instantiate(teleportationEffectPrefab, transform.position, Quaternion.identity);
   //   readyToThrow = true;
   //   throwC = 0;
   //   Destroy(teleportationEffect, 2f);
   //}
  private void Record()
  {
     PointsInTime.Add(projectile.transform.position);
  }
}

   

