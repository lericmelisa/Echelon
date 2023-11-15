using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float finalVrijeme;
    public bool levelPassed;
    public bool batteryPickedUp;

    public GameData()
    {
        finalVrijeme = 50;
        levelPassed = false;
        batteryPickedUp = false;
    }
}
