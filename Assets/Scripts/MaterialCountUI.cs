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

    private Animator textMaterialAnimator;
    AnimatorStateInfo animatorState;
    public bool playingAnimation = false;
    private void Awake()
    {
        textView = GetComponentInChildren<TextMeshProUGUI>();
        if(txtMaterialAdd != null) textMaterialAnimator = txtMaterialAdd.GetComponent<Animator>();
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
    private void Update()
    {       
        if (textMaterialAnimator != null)
        {
            AnimatorStateInfo animatorState = textMaterialAnimator.GetCurrentAnimatorStateInfo(0);            
            if (animatorState.IsName("Idle"))
            {
                playingAnimation = false;                
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

    public void SetCountUI(int count, string additionalText = "")
    {
        textView.text = additionalText + count.ToString();
    }

    public void ShowAddedMaterial(int materialCount)
    {
        if (!animatorState.IsName("MaterialGain") && !playingAnimation)
        {                        
            playingAnimation = true;
            txtMaterialAdd.text = "+0";
            textMaterialAnimator.SetTrigger("Show");            
        }
        if (materialCount >= 0)
        {            
            int showMaterialCount = materialCount;
            showMaterialCount += int.Parse(txtMaterialAdd.text.Split("+")[1]);                
            txtMaterialAdd.text = "+" + showMaterialCount.ToString();
        }
        else
        {
            txtMaterialAdd.text = materialCount.ToString();
        }        
    }
}
