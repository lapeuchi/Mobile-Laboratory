using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_ElementsDescriptionPopup : UI_Popup
{
    MeshRenderer cubeMeshRenderer;
    string materialPath = "Materials/Contents/Content_Elements/CubeMat_";
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

        GetButton((int)Buttons.CloseButton).onClick.AddListener(delegate { Destroy(Content_Elements.instance.actor); ClosePopupUI(); });
       
        Content_Elements.instance.InstantiateActor($"Contents/Content_Elements/ElementCube", transform);
        cubeMeshRenderer = Content_Elements.instance.actor.GetComponent<MeshRenderer>();
        
        base.Init();

        Load();
    }

    void Load()
    {
        Data_Element.ElementForm data = Content_Elements.instance.elementData.elements[Content_Elements.instance.focusElementIndex];
        GetText((int)Texts.NoText).text = data.number;
        GetText((int)Texts.CodeText).text = data.code;
        GetText((int)Texts.NameText).text = data.name;
        GetText((int)Texts.GridPosText).text = $"{data.group}족 {data.period}주기";
        GetText((int)Texts.MetalText).text = data.metal;
        GetText((int)Texts.DescriptionText).text = data.description;

        cubeMeshRenderer.material = Managers.Resource.Load<Material>($"{materialPath}{Content_Elements.instance.focusElementIndex+1}");
    }


}
