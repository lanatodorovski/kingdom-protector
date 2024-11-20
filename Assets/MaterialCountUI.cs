using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialCountUI : MonoBehaviour
{
    [SerializeField] private BuildMaterialSO buildMaterialSO;

    private Image materialImage;
    private TextMeshProUGUI textView;

    private void Awake()
    {
        textView = GetComponentInChildren<TextMeshProUGUI>();
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.gameObject != gameObject)
            {
                materialImage = image;
                return;
            }
        }
    }
    private void Start()
    {
        if(buildMaterialSO != null)
        {
            SetBuildMaterial(buildMaterialSO);
        }
    }

    public void SetBuildMaterial(BuildMaterialSO material)
    {
        SetBuildMaterial(material.GetSprite(), material.GetCount());
    }
    public void SetBuildMaterial(Sprite materialSprite, int count = 0)
    {
        materialImage.sprite = materialSprite;
        Debug.Log(count);
        SetCountUI(count);
    }

    public void SetCountUI(int count)
    {
        textView.text = count.ToString();
    }
}
