using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UI_PeriodicTableScene : UI_Scene
{
    enum Images
    {
        
    }

    int totalElements = 118;
    string buttonName = "ElementButton_";
    protected override void Init()
    {   
        for(int i = 0; i < totalElements; i++)
        {
            int tmp = i;
            Button elementBtn = Util.FindChild<Button>(gameObject, $"{buttonName}{tmp+1}", true, true);
            try
            {   
                elementBtn.onClick.AddListener(()=> OnClickedElementButton(tmp));
            }   
            catch
            {

            }
        }

        base.Init();
    }

    void OnClickedElementButton(int n)
    {
        Content_Elements.instance.focusElementIndex = n;
        Managers.UI.ShowPopupUI<UI_ElementsDescriptionPopup>();
    }
}