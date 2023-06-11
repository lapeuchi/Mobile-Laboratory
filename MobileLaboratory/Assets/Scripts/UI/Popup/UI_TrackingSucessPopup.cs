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

    public void SetInfo(Data.TrackableImage imageData)
    {
        GetText((int)Texts.TitleText).text = $"{imageData.name}";
        GetText((int)Texts.ContentsText).text = $"{imageData.page}p {imageData.name}";//을(를)\n하시겠습니까?";
       
        GetButton((int)Buttons.CancleButton).onClick.AddListener(delegate { OnClickCancelButton(); });
        GetButton((int)Buttons.ConfirmButton).onClick.AddListener(delegate { OnClickConfirmButton(imageData.contentPath); } );
    }

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
    }

    void OnClickConfirmButton(string path)
    {
        Managers.UI.ClosePopupUI(this);
        Managers.Resource.Instantiate(path);
    }

    void OnClickCancelButton()
    {
        GameObject.FindObjectOfType<ImageTrackable>().OnCancleByImageTracking();
        ClosePopupUI();
    }
}
