using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialCountUI : MonoBehaviour
{
    private Image materialImage;
    private TextMeshProUGUI textView;

    public void SetMaterialUI(Sprite materialSprite, int count)
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.gameObject != gameObject)
            {
                materialImage = image;
            }
        }
        textView = GetComponentInChildren<TextMeshProUGUI>();
     
        materialImage.sprite = materialSprite;
        textView.text = count.ToString();
    }
}
