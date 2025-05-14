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

    [Header("Damadge Color")]
    [SerializeField]private Color damadgeColor;
    [SerializeField] private float colorDuration;

    private int currentHitsToBreak;
    MaterialUse materialUse;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        currentHitsToBreak = hitsToBreak;
        spriteRenderer = GetComponent<SpriteRenderer>();
        materialUse = FindAnyObjectByType<BuildMaterialCollection>().GetMaterialUse(buildMaterial);
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

        if(materialUse != null)
        {
            materialUse.AddCount(materialGain);
        }
        StartCoroutine(DamadgeColorChange());
        

    }

    IEnumerator DamadgeColorChange()
    {
        spriteRenderer.color = damadgeColor;
        yield return new WaitForSecondsRealtime(colorDuration);
        spriteRenderer.color = Color.white;
    }

}
