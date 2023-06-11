using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ContentPopup : UI_Popup
{
    enum Buttons
    {
        CloseButton,
    }
    
    protected override void Init()
    {
        BindButton(typeof(Buttons), true);
        GetButton((int)Buttons.CloseButton).onClick.AddListener(OnClickedCloseButton);
        
        base.Init();
    }

    void OnClickedCloseButton()
    {
        Content_Base content = GameObject.FindObjectOfType<Content_Base>();
        if (content)
        {
            content.Clear();
        }        
        ClosePopupUI();
    }


}
