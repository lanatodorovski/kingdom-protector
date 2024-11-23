using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildMaterial", menuName = "ScriptableObjects/BuildMaterialSO")]
public class BuildMaterialSO : ScriptableObject
{
    [SerializeField] private BuildMaterial material;
    [SerializeField] private Sprite sprite;

    public BuildMaterial GetBuildMaterial()
    {
        return this.material;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
}
[Serializable]
public enum BuildMaterial
{
    Wood,
    Stone
}