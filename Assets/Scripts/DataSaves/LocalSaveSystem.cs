using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalSaveSystem : MonoBehaviour
{
    private string savePath;

    public static int slotIndex = 0;
    private SaveSlotData currentSlotData;

    private void Start()
    {        
        if (SceneManager.GetActiveScene().name != "MainMenu") currentSlotData = LoadSave();
        if(SceneManager.GetActiveScene().name == "CastleScene") LoadFieldTowerType();
    }
    private void Awake()
    {
        savePath = Application.persistentDataPath + "/saveprogress.json";
    }
    public void SaveGame(SaveSlotData data)
    {
        SaveDataWrapper dataWapper = LoadAllSaves();
        data.SetLastSaved(DateTime.Now);
        dataWapper.data[slotIndex] = data;
        string json = JsonUtility.ToJson(dataWapper);
        File.WriteAllText(savePath, json);
    }
    public void SaveGame()
    {
        SaveGame(currentSlotData);
    }
    
    public void NextLevel()
    {        
        currentSlotData.level++;      
    }
    public void SetHasGathered(bool hasGathered)
    {        
        currentSlotData.hasGathered = hasGathered;        
    }
    public void SetMaterialCount(List<MaterialCount> addedMaterialCounts)
    {
        currentSlotData.materialCount = addedMaterialCounts;
    }
    public void AddMaterialCount(List<MaterialCount> addedMaterialCounts)
    {                
        foreach(MaterialCount materialCount in addedMaterialCounts)
        {
            MaterialCount foundMaterial = Array.Find(currentSlotData.materialCount.ToArray(), savedMaterialCount =>
                savedMaterialCount.GetBuildMaterial() == materialCount.GetBuildMaterial()
                );        
            if(foundMaterial != null)
            {
                foundMaterial.AddCount(materialCount.GetCount());
                Debug.Log($"{materialCount.GetCount()} is added to {materialCount.GetBuildMaterial().ToString()} material and there is now {foundMaterial.GetCount()} of it");
            }
            else
            {
                currentSlotData.materialCount.Add(materialCount);
            }
        }
        Debug.Log(JsonUtility.ToJson(currentSlotData));
    }
    public void SetFieldTowerType()
    {
        TowerUpgradeControl[] towers = TowerUpgradeControl.towerScripts.ToArray();
        foreach (TowerUpgradeControl tower in towers)
        {
            Debug.Log(tower.GetTowerType());
        }
        if (towers.Length != 0) {
            currentSlotData.fieldTowerType = Array.ConvertAll(towers, tower => tower.GetTowerType()).ToList();
        }
        TowerUpgradeControl.towerScripts.Clear();

    }
    public void LoadFieldTowerType()
    {
        if (currentSlotData.fieldTowerType == null || currentSlotData.fieldTowerType.Count == 0) return;
        for(int i = 0; i < currentSlotData.fieldTowerType.Count(); i++)
        {
            //Debug.Log(TowerUpgradeControl.towerScripts.Count);
            TowerUpgradeControl towerUpgrade = TowerUpgradeControl.towerScripts[i];
            //Debug.Log(currentSlotData.fieldTowerType[i] + " " + towerUpgrade.GetTowerType());
            if(towerUpgrade.GetTowerType() != currentSlotData.fieldTowerType[i])
            {
                towerUpgrade.SetUpgrade(currentSlotData.fieldTowerType[i]);                
            }           
        }
        //TowerUpgradeControl.initDone = true;
    }
    public SaveSlotData LoadSave(int certainSlotIndex)
    {
        SaveDataWrapper saveSlots = LoadAllSaves();
        int index = certainSlotIndex;
        return saveSlots.data[index];
    }
    public SaveSlotData LoadSave()
    {
        return LoadSave(slotIndex);
    }

    private SaveDataWrapper LoadAllSaves()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);        
            return JsonUtility.FromJson<SaveDataWrapper>(json);
        }

        return new SaveDataWrapper();
    }

    public static void SetSlotIndex(int newSlotIndex)
    {
        slotIndex = newSlotIndex;
        PlayerPrefs.SetInt("slotIndex", slotIndex);
    }
    private static void GetSlotIndex()
    {
        slotIndex = PlayerPrefs.GetInt("slotIndex");
    }
    
}

[Serializable]
public class SaveDataWrapper
{
    public SaveSlotData[] data = new SaveSlotData[3]; // fills the array only with nulls
    public SaveDataWrapper()
    {
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = new SaveSlotData();
        }
    }
}
[Serializable]
public class SaveSlotData
{
    public int level;
    public bool hasGathered;
    public List<MaterialCount> materialCount;
    public List<TowerType> fieldTowerType;
    public int populationDeduction;
    public string lastSaved;
       

    public SaveSlotData(int level = 1, bool hasGathered = false, List<MaterialCount> materialCount = null, List<TowerType> towerType = null, int populationDeduction = 0, DateTime lastSaved = default(DateTime))
    {
        this.level = level;
        this.hasGathered = hasGathered;
        this.materialCount = materialCount == null ? new List<MaterialCount>() : materialCount;
        this.fieldTowerType = towerType;
        this.populationDeduction = populationDeduction;
        this.lastSaved = lastSaved.ToString("yyyy-MM-dd HH:mm:ss");
    }
    public void SetLastSaved(DateTime date)
    {
        this.lastSaved = date.ToString("yyyy-MM-dd HH:mm:ss");
    }
    public DateTime GetLastSaved()
    {
        return DateTime.Parse(this.lastSaved);
    }
}
