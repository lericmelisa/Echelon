using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickUp : MonoBehaviour, IDataPersistance
{
    public DataPersistanceManager dpm;

    public static bool pokupljena;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(pokupljena == true)
            Destroy(gameObject);
    }

    public void LoadData(GameData data, BatteryData data2)
    {
        pokupljena = data.batteryPickedUp;
    }

    public void SaveData(ref GameData data, ref BatteryData data2)
    {
        
        data.batteryPickedUp = pokupljena;
        data2.batteryCounter ++;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            pokupljena = true;
            Destroy(gameObject);
            dpm.SaveGame();
        }
    }
}
