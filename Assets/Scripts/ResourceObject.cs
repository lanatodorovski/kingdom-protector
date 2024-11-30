using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    [SerializeField] int hitsToBreak = 4;
    [SerializeField] BuildMaterial buildMaterial;

    [Header("Generate Per Hit")]
    [SerializeField] int minMaterialGain = 0;
    [SerializeField] int maxMaterialGain = 2;

    private int currentHitsToBreak;
    MaterialUse materialUse;
    private void Awake()
    {
        materialUse = FindAnyObjectByType<BuildMaterialCollection>().GetMaterialUse(buildMaterial);
        currentHitsToBreak = hitsToBreak;
    }

    [ContextMenu("Hit And Gain")]
    public void HitAndGain()
    {
        currentHitsToBreak--;
        if(currentHitsToBreak <= 0)
        {
            Destroy(gameObject);
        }

        int materialGain = Random.Range(minMaterialGain, maxMaterialGain + 1);
        Debug.Log($"You gained {materialGain} {buildMaterial}");

        materialUse.AddCount(materialGain);        
    }


}
