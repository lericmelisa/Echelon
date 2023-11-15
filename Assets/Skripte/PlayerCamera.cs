
using System;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

namespace Game.Movement
{
    public class PlayerCamera : MonoBehaviour
    {

       public float sensX;
    public float sensY;
    public float multiplier;

    public Transform orientation;
    public Transform camHolder;

    float xRotation;
    float yRotation;
    public bool lockCamera;

    [Header("Fov")]
    public bool useFluentFov;
    public PlayerMovement pm;
    public Rigidbody rb;
    public Camera cam;
    public float minMovementSpeed;
    public float maxMovementSpeed;
    public float minFov;
    public float maxFov;
    public bool stateChanged = false;
    public TimeManipulation tm;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       //camHolder.rotation = tm.druga;
       //orientation.rotation = tm.prva;
       //stateChanged = false;
       //float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
       //float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;
       //yRotation = tm.druga.y;
       //xRotation = tm.druga.x;
    }

    public float xRotationExpected;
    public float yRotationExpected;
    public void UnlockedCamera(Quaternion prva, Quaternion druga)
    {
        Debug.Log("Kad ucita " + camHolder.localRotation.x);


        camHolder.rotation = Quaternion.Euler(druga.eulerAngles);
        orientation.rotation = Quaternion.Euler(prva.eulerAngles);
        Debug.Log("Kad uglovi " + camHolder.localRotation.x);
        //else
        //{
        //    camHolder.rotation = Quaternion.Euler(druga.eulerAngles);
        //    orientation.rotation = Quaternion.Euler(prva.eulerAngles);
        //    //camHolder.rotation = Quaternion.Euler(abs(camHolder.rotation.x.euler), camHolder.rotation.y, camHolder.rotation.z);
        //    Debug.Log("Kad uglovi " + camHolder.localRotation.x);
        //}

        Vector3 eulerRotation = camHolder.rotation.eulerAngles;
        Debug.Log("Prije izmjene " + eulerRotation.x + "      " + eulerRotation.y);
        if (eulerRotation.x > 90)
        {
            eulerRotation.x -= 360;
            eulerRotation.y -= 360;
        }

        stateChanged = false;
        //float scaleFactor;
        //scaleFactor = yRotationExpected / druga.y;
        yRotation = eulerRotation.y;
        xRotation = Mathf.Clamp(eulerRotation.x, -90f, 90f); 
        Debug.Log("Poslije izmjene " + eulerRotation.x + "      " + eulerRotation.y);
        Debug.Log(xRotation + "      " + yRotation);



    }
    
    private void Update()
    {

        if (!EscapeMenu.isPaused)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

            yRotation += mouseX * multiplier;

            xRotation -= mouseY * multiplier;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // rotate cam and orientation
            camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            if (useFluentFov) HandleFov();
        }

            // get mouse input
       
    }

    private void HandleFov()
    {
        float moveSpeedDif = maxMovementSpeed - minMovementSpeed;
        float fovDif = maxFov - minFov;

        float rbFlatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z).magnitude;
        float currMoveSpeedOvershoot = rbFlatVel - minMovementSpeed;
        float currMoveSpeedProgress = currMoveSpeedOvershoot / moveSpeedDif;

        float fov = (currMoveSpeedProgress * fovDif) + minFov;

        float currFov = cam.fieldOfView;

        float lerpedFov = Mathf.Lerp(fov, currFov, Time.deltaTime * 200);

        cam.fieldOfView = lerpedFov;
    }

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
    }
}
