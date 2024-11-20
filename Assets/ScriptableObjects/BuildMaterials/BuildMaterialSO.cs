using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildMaterial", menuName = "ScriptableObjects/BuildMaterialSO")]
public class BuildMaterialSO : ScriptableObject
{
    [SerializeField] private BuildMaterial material;
    [SerializeField] private int count;
    [SerializeField] private Sprite sprite;

    public BuildMaterial GetBuildMaterial()
    {
        return this.material;
    }
    public int GetCount()
    {
        return count;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
    public void CostTakeAway(int cost)
    {
        count -= cost;
    }
}
[Serializable]
public enum BuildMaterial
{
    Wood,
    Stone
}