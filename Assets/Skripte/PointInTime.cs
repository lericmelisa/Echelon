using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PointInTime
{
    public Vector3 position;

    public Quaternion rotationO;
    public Quaternion rotationC;

    public PointInTime(Vector3 _position, Quaternion _rotationO, Quaternion _rotationC)
    {
        position = _position;
        rotationO = _rotationO;
        rotationC = _rotationC;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
