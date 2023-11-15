using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistance
{
    void LoadData(GameData data, BatteryData data2);
    
    void SaveData(ref GameData data, ref BatteryData data2);
}
