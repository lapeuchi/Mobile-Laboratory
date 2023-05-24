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
        DescriptionText
    }
    enum Buttons
    {
        CloseButton
    }
    
    protected override void Init()
    {
        BindText(typeof(Texts), true);
        BindButton(typeof(Buttons), true);

        Data_Element.ElementForm data = Content_Elements.instance.elementData.elements[Content_Elements.instance.focusElementIndex];
        GetText((int)Texts.NoText).text = data.number;
        GetText((int)Texts.CodeText).text = data.code;
        GetText((int)Texts.NameText).text = data.name;
        GetText((int)Texts.GridPosText).text = $"{data.group}족 {data.period}주기";
        GetText((int)Texts.MetalText).text = data.metal;
        GetText((int)Texts.DescriptionText).text = data.description;

        GetButton((int)Buttons.CloseButton).onClick.AddListener(delegate { Destroy(MainScene.instance.actor); ClosePopupUI(); });
       
        MainScene.instance.InstantiateActor($"Content_Elements/Cubes/Cube_{Content_Elements.instance.focusElementIndex+1}", Content_Elements.instance.transform);
        MainScene.instance.actor.AddComponent<DragToRotate>();
       
        base.Init();
    
    }


}
