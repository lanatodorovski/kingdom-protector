using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialCountUI : MonoBehaviour
{
    private Image materialImage;
    private TextMeshProUGUI textView;
    public TextMeshProUGUI txtMaterialAdd;

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
  
    public void SetMaterial(MaterialUse material)
    {
        SetMaterial(material.GetBuildMaterialSO().GetSprite(), material.GetCount());
    }
    public void SetMaterial(Sprite materialSprite, int count = 0)
    {
        materialImage.sprite = materialSprite;
        //Debug.Log(count);
        SetCountUI(count);
    }

    public void SetCountUI(int count)
    {
        textView.text = count.ToString();
    }

    public void ShowAddedMaterial(int materialCount)
    {
        if (materialCount >= 0)
        {
            txtMaterialAdd.text = "+" + materialCount.ToString();
        }
        else
        {
            txtMaterialAdd.text = materialCount.ToString();
        }
        Debug.Log(txtMaterialAdd.text);
        txtMaterialAdd.GetComponent<Animator>().SetTrigger("Show");

    }
}
