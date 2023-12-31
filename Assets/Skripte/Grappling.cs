using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Movement
{
    public class Grappling : MonoBehaviour
    {
    [Header("References")]
        private PlayerMovement pm;
        public Transform cam;
        public Transform gunTip;
        public LayerMask whatIsGrappleable;
        public LineRenderer lr;

        [Header("Grappling")]
        public float maxGrappleDistance = 25f;
        public float grappleDelayTime = 0.0f;
        public float overshootYAxis = 2f;


        private Vector3 grapplePoint;

        [Header("Cooldown")]
        public float grapplingCd = 2.5f;
        private float grapplingCdTimer;

        [Header("Input")]
        public KeyCode grappleKey = KeyCode.Mouse1;

        private bool grappling;

        private void Start()
        {
            pm = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            // input
            if (Input.GetMouseButton(1)) StartGrapple();

            if (grapplingCdTimer > 0)
                grapplingCdTimer -= Time.deltaTime;
        }

        private void LateUpdate()
        {
            if (grappling)
                lr.SetPosition(0, gunTip.position);
        }

        public void StartGrapple()
        {
            if (!Input.GetMouseButton(1)) return;

            if(grappling) return;


            RaycastHit hit;
            if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
            {
                grapplePoint = hit.point;

                Invoke(nameof(ExcecuteGrapple), grappleDelayTime);
            }

            else
            {
                grapplePoint = cam.position + cam.forward * maxGrappleDistance;

                Invoke(nameof(StopGrapple), grappleDelayTime);
            }
            grappling = true;
            lr.enabled = true;
            lr.SetPosition(1, grapplePoint);
        }

        public void ExcecuteGrapple()
        {
            pm.freeze = false;

            Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

            float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
            float highestPointOfArc = grapplePointRelativeYPos + overshootYAxis;

            if (grapplePointRelativeYPos < 0) highestPointOfArc = overshootYAxis;

            pm.JumpToPosition(grapplePoint, highestPointOfArc);

            Invoke(nameof(StopGrapple), 1f);

        }

        public void StopGrapple()
        {
            pm.freeze = false;

            grappling = false;

            grapplingCdTimer = grapplingCd;

            lr.enabled = false;
        }

        public void OnObjectTouch()
        {
            if (pm.grappling) StopGrapple();
        }


        public bool IsGrappling()
        {
            return grappling;
        }

        public Vector3 GetGrapplePoint()
        {
            return grapplePoint;
        }
    }
}