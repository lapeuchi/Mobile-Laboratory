using maxstAR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        ContentsText
    }

    public void SetInfo(string name, int page, Action<PointerEventData> cancleEvtData)
    {
        GetText((int)Texts.TitleText).text = $"{name}";
        GetText((int)Texts.ContentsText).text = $"{page}p {name}을(를)\n하시겠습니까?";
        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(cancleEvtData);
        //GetButton((int)Buttons.ConfirmButton).gameObject.BindEvent(confirmEvtData);
    }

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

    }
}
