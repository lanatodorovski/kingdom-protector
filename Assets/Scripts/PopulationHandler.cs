using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PopulationHandler : MonoBehaviour
{
    [SerializeField] private int population;
    [SerializeField] private TextMeshProUGUI populationText;
    private static int populationDeduction = 0;

    LocalSaveSystem saveSystem;

    private void Awake()
    {
        saveSystem = FindAnyObjectByType<LocalSaveSystem>();
    }
    private void Start()
    {
        populationDeduction = saveSystem.LoadSave().populationDeduction;
        population -= populationDeduction;
        SetPopulationText();
    }
    public void RemovePopulation(int remove)
    {
        population -= remove;
        if(population <= 0)
        {
            FindAnyObjectByType<MenuManager>().ToggleCanvas("DeathMenuUI", false);
            population = 0;
        }
        SetPopulationText();        
    }

    public void AddPopulation(int add)
    {
        population += add;
        SetPopulationText();
    }

    public void SetPopulationText()
    {
        populationText.text = population.ToString();
    }

    public int GetPopulationCount()
    {
        return population;
    }

    public void AddPopulationDeduction(int deduct)
    {
        populationDeduction += deduct;   
        saveSystem.LoadSave().populationDeduction = populationDeduction;
        saveSystem.SaveGame();
    }


}
