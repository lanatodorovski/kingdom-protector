using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalSaveSystem : MonoBehaviour
{
    private string savePath;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/saveprogress.json";
        Debug.Log(savePath);
    }
    public void SaveGame(SaveSlotData data, int slotIndex)
    {
        SaveDataWrapper dataWapper = LoadAllSaves();
        dataWapper.data[slotIndex] = data;
        //foreach (SaveSlotData slotData in saveSlots)
        //{
        //    Debug.Log(slotData);
        //}
        string json = JsonUtility.ToJson(dataWapper);
        Debug.Log(json);
        File.WriteAllText(savePath, json);
    }

    public SaveSlotData LoadSave(int slotIndex)
    {
        SaveDataWrapper saveSlots = LoadAllSaves();
        return saveSlots?.data[slotIndex];
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
}

[Serializable]
public class SaveDataWrapper
{
    public SaveSlotData[] data = new SaveSlotData[3];
}
[Serializable]
public class SaveSlotData
{
    public int level;
    public bool hasGathered;
    public List<MaterialCount> materialCount;

    public SaveSlotData(int level = 0, bool hasGathered = false, List<MaterialCount> materialCount = null)
    {
        this.level = level;
        this.hasGathered = hasGathered;
        this.materialCount = materialCount == null ? new List<MaterialCount>() : materialCount;
    }
}
