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
        GetText((int)Texts.TitleText).text = $"{name}����";
        GetText((int)Texts.ContentText).text = $"{page}p {name}������\n�����Ͻðڽ��ϱ�?";

        GetButton((int)Buttons.CancleButton).gameObject.BindEvent(cancleEvtData);
    }

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

    }
}
