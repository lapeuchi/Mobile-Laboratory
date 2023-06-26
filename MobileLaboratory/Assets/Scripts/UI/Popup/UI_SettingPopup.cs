using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SettingPopup : UI_Popup
{
    enum Sliders
    {
        SFXSlider,
        BGMSlider
    }

    enum Buttons
    {
        CloseButton,
        QuitButton,
    }

    protected override void Init()
    {
        base.Init();

        BindSlider(typeof(Sliders), true);
        BindButton(typeof(Buttons), true);
        
        GetButton((int)Buttons.CloseButton).onClick.AddListener(delegate { ClosePopupUI(); });
        GetButton((int)Buttons.QuitButton).onClick.AddListener(delegate { Application.Quit(); });

        Slider sfxSlider = GetSlider((int)Sliders.SFXSlider);
        Slider bgmSlider = GetSlider((int)Sliders.BGMSlider);
        sfxSlider.value = Managers.Data.userData.SFXVolume;
        bgmSlider.value = Managers.Data.userData.BGMVolume;

        EventTrigger vfxEvtTrigger = Util.GetOrAddComponent<EventTrigger>(sfxSlider.gameObject);
        EventTrigger.Entry vfxEvtEntry = new EventTrigger.Entry();
        vfxEvtEntry.eventID = EventTriggerType.PointerUp;
        vfxEvtEntry.callback.AddListener(delegate { Managers.Data.userData.SFXVolume = sfxSlider.value; Managers.Sound.GetAudioSorce(Define.Sound.SFX).volume = Managers.Data.userData.SFXVolume; });
        vfxEvtTrigger.triggers.Add(vfxEvtEntry);
        
        EventTrigger bgmEvtTrigger = Util.GetOrAddComponent<EventTrigger>(bgmSlider.gameObject);
        EventTrigger.Entry bgmEvtEntry = new EventTrigger.Entry();
        bgmEvtEntry.eventID = EventTriggerType.PointerUp;
        bgmEvtEntry.callback.AddListener(delegate { Managers.Data.userData.BGMVolume = bgmSlider.value; Managers.Sound.GetAudioSorce(Define.Sound.BGM).volume = Managers.Data.userData.BGMVolume; });
        bgmEvtTrigger.triggers.Add(bgmEvtEntry);

    }
}
