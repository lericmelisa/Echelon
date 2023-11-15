using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{

    [SerializeField] private string levelFile;
    [SerializeField] private string batteryFile;
    [SerializeField] private bool useEncryption;
    
    public GameData gameData;
    public BatteryData batteryData;
    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandler dataHandler;
    private FileDataHandler dataHandler2;
    public static DataPersistanceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the scene");
        }
        instance = this;
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, levelFile, useEncryption);
        this.dataHandler2 = new FileDataHandler(Application.persistentDataPath, batteryFile, useEncryption);
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    private void Start()
    {

    }

    public void NewGameData()
    {
        this.gameData = new GameData();
    } 
    public void NewBatteryData()
    {
        this.batteryData = new BatteryData();
    } 
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        this.batteryData = dataHandler2.LoadBatteryData();
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing the defaults");
            NewGameData();
        }

        if (this.batteryData == null)
        {
            Debug.Log("No data was found. Initializing the defaults");
            NewBatteryData();
        }
        
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData, batteryData);
        }
        Debug.Log("PRESAO: " + gameData.levelPassed);
        Debug.Log("Baterija: " + gameData.batteryPickedUp);
    }

    public void SaveBattery()
    {
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData, ref batteryData);
        }
        dataHandler2.SaveBatteryData(batteryData);
    }
    public void SaveGame()
    {
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData, ref batteryData);
        }
        dataHandler.Save(gameData);
        dataHandler2.SaveBatteryData(batteryData);

    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> _dataPersistanceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(_dataPersistanceObjects);
    }
}
