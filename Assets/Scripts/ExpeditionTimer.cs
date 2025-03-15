using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ExpeditionTimer : MonoBehaviour
{
    [SerializeField] private float minuteTimeCounter = 3.0f;
    private float currentTime;

    [SerializeField] private TextMeshProUGUI timerText;
    // Start is called before the first frame update
    private void Awake()
    {
        currentTime = Mathf.Floor(minuteTimeCounter) * 60 + 100 * (minuteTimeCounter % 1);        
    }
    void Start()
    {
        Debug.Log(currentTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            UpdateCounter();
            UpdateUI();
        }
    }

    private void UpdateCounter()
    {
        currentTime -= Time.deltaTime;        
        if (currentTime <= 0)
        {
            EndExpedition();
        }
    }
    private void UpdateUI()
    {
        int outputMinutes = (int) currentTime / 60;
        int outputSeconds = (int) currentTime % 60;
        if (timerText != null) timerText.text = $"{outputMinutes:D2}:{outputSeconds:D2}"; // Formating in 00:00 form
    }

    private void EndExpedition()
    {
        if (timerText != null) timerText.text = "Time has ended";

        FindAnyObjectByType<MenuManager>().ToggleLevelCompletionUI();
        List<MaterialCount> materialCounts = FindAnyObjectByType<BuildMaterialCollection>().GetAllAsMaterialCount();
        SaveSlotData saveSlot = new SaveSlotData(0, true, materialCounts);
        FindAnyObjectByType<LocalSaveSystem>().SaveGame(saveSlot, 0);
    }

    private void OnValidate()
    {
        if (minuteTimeCounter % 1f > 0.6f)
        {
            minuteTimeCounter =  Mathf.Floor(minuteTimeCounter) + 0.6f;
        }
    }
}
