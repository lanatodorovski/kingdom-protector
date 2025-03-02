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
    private void FixedUpdate()
    {
        AnimatorStateInfo animatorState = textMaterialAnimator.GetCurrentAnimatorStateInfo(0);
        if (animatorState.IsName("Idle"))
        {
            txtMaterialAdd.text = "+0";
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
            int showMaterialCount = materialCount;
   
            showMaterialCount += int.Parse(txtMaterialAdd.text.Split("+")[1]);     
            //Debug.Log("aiduhawiod   " + showMaterialCount);
            txtMaterialAdd.text = "+" + showMaterialCount.ToString();
        }
        else
        {
            txtMaterialAdd.text = materialCount.ToString();
        }
        Debug.Log(txtMaterialAdd.text);       
        textMaterialAnimator.SetTrigger("Show");
    }
}
