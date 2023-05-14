using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Tracking : UI_Scene
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

    Button bookSelectButton;
    Image bookSelectImage;

    protected override void Init()
    {
        base.Init();

        BindImage(typeof(Images), true);
        BindButton(typeof(Buttons), true);
        
        bookSelectButton = GetButton((int)Buttons.BookSelectButton);
        bookSelectImage = GetImage((int)Images.BookSelectImage);

        SetSelectImage();
        bookSelectButton.onClick.AddListener(delegate { Managers.UI.ShowPopupUI<BookSelect_Popup>(); });
        GetButton((int)Buttons.SettingButton).onClick.AddListener(delegate { Managers.UI.ShowPopupUI<UI_SettingPopup>(); });
    }

    public void SetSelectImage()
    {
        if (Managers.Data.userData.Book != null)
        {
            bookSelectImage.sprite = Managers.Data.userData.Book.coverImage;
        }
    }
}
