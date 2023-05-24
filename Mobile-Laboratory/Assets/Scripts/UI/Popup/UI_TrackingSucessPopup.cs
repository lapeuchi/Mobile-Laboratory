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
        ContentText
    }

    public void SetInfo(string name, int page, Action<PointerEventData> cancleEvtData)
    {
        GetText((int)Texts.TitleText).text = $"{name}실험";
        GetText((int)Texts.ContentText).text = $"{page}p {name}실험을\n시작하시겠습니까?";

        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(cancleEvtData);
    }

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

    }
}
