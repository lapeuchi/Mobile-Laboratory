using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UI_PeriodicTablePopup : UI_Popup
{
    enum Images
    {
        MetalColorImage,
        NonMetalColorImage,
        MetalloidColorImage
    }

    Content_Elements content;

    string buttonName = "ElementButton_";

    Color metalColor = new Color(0.3f, 1, 0);
    Color nonMetalColor = new Color(1, 0.9f, 0);
    Color metalloidColor = new Color(1, 0.5f, 0);

    protected override void Init()
    {   
        BindImage(typeof(Images), true);

        GetImage((int)Images.MetalColorImage).color = metalColor;
        GetImage((int)Images.NonMetalColorImage).color = nonMetalColor;
        GetImage((int)Images.MetalloidColorImage).color = metalloidColor;

        content = GameObject.FindObjectOfType<Content_Elements>();

        for(int i = 0; i < Content_Elements.MaxElement; i++)
        {
            int tmp = i;
            try
            {
                Button elementBtn = Util.FindChild<Button>(gameObject, $"{buttonName}{tmp+1}", true, true);
                
                if (content.elementData.elements[i].metal_eng == "metal")
                {
                    elementBtn.GetComponent<Image>().color = metalColor;
                }
                else if (content.elementData.elements[i].metal_eng == "non-metal")
                {
                    elementBtn.GetComponent<Image>().color = nonMetalColor;
                }
                else if (content.elementData.elements[i].metal_eng == "metalloid")
                {
                    elementBtn.GetComponent<Image>().color = metalloidColor;
                }
                
                Util.FindChild<TMP_Text>(elementBtn.gameObject, "Code", false, false).text = content.elementData.elements[i].code;
                Util.FindChild<TMP_Text>(elementBtn.gameObject, "Num", false, false).text = content.elementData.elements[i].number;
                Util.FindChild<TMP_Text>(elementBtn.gameObject, "Name", false, false).text = content.elementData.elements[i].name;
                elementBtn.onClick.AddListener(()=> OnClickedElementButton(tmp));
            }
            catch
            {
                Debug.Log($"btn_{i}: error");
            }
            
            
        }

        base.Init();
    }

    void OnClickedElementButton(int n)
    {
        // Debug.Log($"Selected Element number: {n}");
        content.FocusElementIndex = n;
        Managers.UI.ShowPopupUI<UI_ElementsDescriptionPopup>().transform.SetParent(content.transform);
        
    }
    
    
}