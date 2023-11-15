using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Movement
{
public class TimeManipulation : MonoBehaviour
{
    public Transform PlayerObj;
    List<PointInTime> pointsInTime;
    public bool isRewinding;
    public bool isRecording;
    public Rigidbody rb;
    public PlayerMovement pm;
    public PlayerCamera pc;
    public Transform orientation;
    public Transform CamHolder;
    public GameObject bw;
  public AudioSource audioSource;
    // Start is called before the first frame update

    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!EscapeMenu.isPaused)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1) && pointsInTime.Count == 0)
            {
                StartRecording();
            }
            else if(Input.GetKeyDown(KeyCode.Mouse1) && pointsInTime.Count > 0)
            {
                StopRecording();
                StartRewinding();

            }
        }


        if (isRewinding)
        {
            JumpBackToPoint();
         
        }
          
        if (isRecording)
        {
            Record();
        }
    }

    public Quaternion prva;
    public Quaternion druga;
    void FixedUpdate()
    {

    }
    private void StartRecording()
    {
        //firstPoint.position = PlayerObj.position;
        prva = orientation.localRotation;
        druga = CamHolder.localRotation;
        Debug.Log(prva + "  " + druga);
        //prva = orientation.rotation;
        //druga = CamHolder.rotation;
        //firstPoint.rotationO = orientation.rotation;
       // firstPoint.rotationC = CamHolder.rotation;
        isRecording=true;
        bw.SetActive(true);
    }
    private void StopRecording()
    {
        isRecording=false;
        Debug.Log(prva + "  " + druga);
    }
    public PointInTime firstPoint;
    private void StartRewinding()
    {
        isRewinding = true;
        rb.isKinematic = true;
        pm.freeze = true;
        pc.enabled = false;
        rewindTimer = 0f;
        audioSource.Play();
    }

    private void UnlockCamera()
    {
        //CamHolder.rotation = druga;
        //orientation.rotation = prva;
        pc.enabled = true;

    }
    private void StopRewinding()
    {
        Debug.Log(prva + "  " + druga);
        rb.isKinematic = false;
        isRewinding = false;
        pm.freeze = false;
        pc.stateChanged = true;
        Invoke(nameof(UnlockCamera), 0.1f);
        rewindTimer = 0f;
        pointsInTime.Clear();
        pc.UnlockedCamera(prva, druga);
        bw.SetActive(false);
        audioSource.Stop();



    }

    private float recordDuration;
    private float rewindTimer; // Timer variable to track elapsed time
    private float rewindDuration = 1f; // Desired duration of the rewind process in seconds
    private void JumpBackToPoint(){
        
        if (pointsInTime.Count > 0)
        {
            rewindTimer += Time.deltaTime;
            float t;
            float timer;
            if (recordDuration < rewindDuration)
            {
                t = rewindTimer / recordDuration;
                timer = recordDuration;
            }
            else
            {
                 // Calculate interpolation factor
                t = rewindTimer / rewindDuration;
                timer = rewindDuration;
            }

            // Interpolate player position and rotation
            int currentIndex = Mathf.FloorToInt(t * (pointsInTime.Count - 1));
            int nextIndex = currentIndex + 1;
            t = (t * (pointsInTime.Count - 1)) - currentIndex;

            if (nextIndex < pointsInTime.Count)
            {
                PlayerObj.position = Vector3.Lerp(pointsInTime[currentIndex].position, pointsInTime[nextIndex].position, t);
                orientation.rotation = Quaternion.Slerp(pointsInTime[currentIndex].rotationO, pointsInTime[nextIndex].rotationO, t);
                CamHolder.rotation = Quaternion.Slerp(pointsInTime[currentIndex].rotationC, pointsInTime[nextIndex].rotationC, t);
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
            if (rewindTimer >= timer)
            {
                StopRewinding(); // Call the StopRewinding method when the rewind process is complete
            }


        }
        else
        {
            
            StopRewinding();
        }
    }
    private void Record()
    {
        recordDuration += Time.deltaTime;
        pointsInTime.Insert(0,new PointInTime(PlayerObj.position, orientation.rotation, CamHolder.rotation));
    }
}
}