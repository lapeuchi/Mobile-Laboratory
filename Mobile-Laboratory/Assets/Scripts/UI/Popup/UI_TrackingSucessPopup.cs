using maxstAR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TrackingSucessPopup : UI_Popup
{
    enum Buttons
    {
        CancleButton,
        ConfirmButton,
    }

    enum Texts
    {
        TitleText,
        ContentText
    }

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.CancleButton).onClick.AddListener(OnClickCancleButton);
        GetButton((int)Buttons.ConfirmButton).onClick.AddListener(OnClickConfirmButton);
    }

    void OnClickCancleButton()
    {
        ClosePopupUI();
    }

    void OnClickConfirmButton()
    {
        Debug.Log("Check");
    }
}
