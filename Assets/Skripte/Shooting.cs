using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movement
{

    public class Shooting : MonoBehaviour
    {
        //bullet 
        public GameObject projectilePrefab;
        public Transform shootPoint;
        public KeyCode shootKey = KeyCode.Mouse1;
        public float shootForce;
        public float shootUpwardForce;
        private bool readyToShoot = true;
        private GameObject projectile;
        public Camera cam;
       public AudioSource audioSource;
       public Animator animator;
       public LayerMask whatIsTarget;
        private void Start()
        {
        }

        private void Update()
        {
            if (!EscapeMenu.isPaused)
            {
                if (Input.GetKeyDown(shootKey) && readyToShoot)
                {
                    animator.SetTrigger("TrShoot");
                    ShootProjectile();
                    audioSource.Play();
                }
            }

        }

        private void ShootProjectile()
        {
            readyToShoot = false;
            
            //projectileRb.AddForce(throwDirection * shootForce, ForceMode.Impulse);
            //projectileRb.AddForce(Vector3.up * shootUpwardForce, ForceMode.Impulse);
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
            RaycastHit hit;
            //check if ray hits something shootPoint.position, shootPoint.forward
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit,75f,whatIsTarget))
            {
                targetPoint = hit.point;
                Debug.Log(hit.transform.name);
            }
            else
                targetPoint = ray.GetPoint(150f); //Just a point far away from the player

            //Calculate direction from attackPoint to targetPoint
            //Vector3 directionWithoutSpread = ;
            Vector3 shotDirection = targetPoint - shootPoint.position;
            
            //Calculate new direction with spread
            //Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

            //Instantiate bullet/projectile
            projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

            //GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
            //Rotate bullet to shoot direction
            //Debug.DrawRay(shootPoint.position, shotDirection, Color.red);

            projectile.transform.forward = shotDirection.normalized;
            // Add forces to bullet
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.AddForce(shotDirection.normalized * shootForce, ForceMode.Impulse);
            projectileRb.AddForce(cam.transform.up * shootUpwardForce, ForceMode.Impulse);

            Invoke(nameof(ResetShot), 0.2f);
        }

        private void ResetShot()
        {
            readyToShoot = true;
        }
    }
}
