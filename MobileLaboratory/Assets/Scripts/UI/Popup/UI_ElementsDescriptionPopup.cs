using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_ElementsDescriptionPopup : UI_Popup
{
    
    enum Texts
    {
        NoText,
        CodeText,
        NameText,
        GridPosText,
        MetalText,
        //DescriptionText
    }
    enum Buttons
    {
        CloseButton,
        PrevButton,
        NextButton,
        
    }

    Content_Elements content;

    protected override void Init()
    {
        BindText(typeof(Texts), true);
        BindButton(typeof(Buttons), true);

        GetButton((int)Buttons.CloseButton).onClick.AddListener(delegate { ClosePopupUI(); });
        
        content = GameObject.FindObjectOfType<Content_Elements>();
        
        GetButton((int)Buttons.PrevButton).onClick.AddListener(delegate { 
            content.FocusElementIndex--; 
            Load(); 
            return; 
            });
        GetButton((int)Buttons.NextButton).onClick.AddListener(delegate {
            content.FocusElementIndex++;
            Load();
            return; 
            });    
        
        base.Init();

        Load();

    }

    void Load()
    {
        Data_Element.ElementForm data = content.elementData.elements[content.FocusElementIndex];
        GetText((int)Texts.NoText).text = data.number;
        GetText((int)Texts.CodeText).text = data.code;
        GetText((int)Texts.NameText).text = data.name;
        GetText((int)Texts.GridPosText).text = $"{data.group}족 {data.period}주기";
        GetText((int)Texts.MetalText).text = data.metal;
        //GetText((int)Texts.DescriptionText).text = data.description;
        
    }
}
