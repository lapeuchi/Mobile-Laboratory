using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_MainScene : UI_Scene
{
    enum Buttons
    {
        BookSelectButton,
        SettingButton
    }

    enum Images
    {
        BookSelectImage,
    }

    Image bookSelectImage;


    private GameObject trackingModeUI;
    private GameObject contentModeUI;

    public void ChangeUIMode(Define.ModeState mode)
    {
        if(mode == Define.ModeState.Content)
        {
            contentModeUI.SetActive(true);
            trackingModeUI.SetActive(false);
        }
        else
        {
            trackingModeUI.SetActive(true);
            contentModeUI.SetActive(false);
        }
    } 

    protected override void Init()
    {
        base.Init();

        BindImage(typeof(Images), true);
        BindButton(typeof(Buttons), true);
        
        Button bookSelectButton = GetButton((int)Buttons.BookSelectButton);
        bookSelectImage = GetImage((int)Images.BookSelectImage);

        SetSelectImage();
        bookSelectButton.onClick.AddListener(delegate { Managers.UI.ShowPopupUI<UI_BookSelect_Popup>(); });
        GetButton((int)Buttons.SettingButton).onClick.AddListener(delegate { Managers.UI.ShowPopupUI<UI_SettingPopup>(); });
    
        trackingModeUI = Util.FindChild(gameObject, "TrackingMode", true, true);
        contentModeUI = Util.FindChild(gameObject, "ContentMode", true, true);
    }

    public void SetSelectImage()
    {
        if (Managers.Data.userData.Book != null)
        {
            bookSelectImage.sprite = Managers.Data.userData.Book.coverImage;
        }
    }
}
