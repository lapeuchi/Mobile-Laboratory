using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SettingPopup : UI_Popup
{
    enum Sliders
    {
        VfxVolumeSlider,
        BgmVolumeSlider
    }

    enum Buttons
    {
        CloseButton,
        QuitAppButton,

    }

    Slider vfxVolumeSlider;
    Slider bgmVolumeSlider;
    

    protected override void Init()
    {
        base.Init();

        BindSlider(typeof(Sliders), true);
        BindButton(typeof(Buttons), true);

        vfxVolumeSlider = GetSlider((int)Sliders.VfxVolumeSlider);
        bgmVolumeSlider = GetSlider((int)Sliders.BgmVolumeSlider);
        
        vfxVolumeSlider.onValueChanged.AddListener(delegate { Managers.Data.userData.SetBgmVolume(vfxVolumeSlider.value); });
        bgmVolumeSlider.onValueChanged.AddListener(delegate { Managers.Data.userData.SetBgmVolume(bgmVolumeSlider.value); });
        GetButton((int)Buttons.CloseButton).onClick.AddListener(delegate { ClosePopupUI(); });
       
    }

    
}
