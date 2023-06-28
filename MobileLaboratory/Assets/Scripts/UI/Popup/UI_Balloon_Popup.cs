using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Balloon_Popup : UI_Popup
{
    Content_DropBalloon content;

    enum Objects
    {
        Progress_1,
    }

    enum Texts
    {    
        MassText,
        HeightText,
    }   

    enum Buttons
    {
        MassUpButton,
        MassDownButton,
        HeightUpButton,
        HeightDownButton,
        DropButton,
        CusionButton
    }

    protected override void Init()
    {
        BindButton(typeof(Buttons), true);
        BindText(typeof(Texts), true);
        BindObject(typeof(Objects), true);
        content = FindObjectOfType<Content_DropBalloon>();
        
        base.Init();

        GetButton((int)Buttons.MassUpButton).onClick.AddListener(delegate{OnClickedMassButton(true);});
        GetButton((int)Buttons.MassDownButton).onClick.AddListener(delegate{OnClickedMassButton(false);});
        GetButton((int)Buttons.HeightUpButton).onClick.AddListener(delegate{OnClickedHeightButton(true);});
        GetButton((int)Buttons.HeightDownButton).onClick.AddListener(delegate{OnClickedHeightButton(false);});

        GetButton((int)Buttons.DropButton).onClick.AddListener(delegate { content.Progress = 2; });
        GetText((int)Texts.MassText).text = $"{content.mass}";
        GetText((int)Texts.HeightText).text = $"{content.height}";
    }

    void OnClickedMassButton(bool isUp)
    {
        if(isUp)
        {
            ++content.MassLevel;
            
        }
        else
        {
            --content.MassLevel;
        }
        
        GetText((int)Texts.MassText).text = $"{content.mass}";
        
    }
    void OnClickedHeightButton(bool isUp)
    {
        if(isUp)
        {
            ++content.HeightLevel;
            
        }
        else
        {
            --content.HeightLevel;
        }
        GetText((int)Texts.HeightText).text = $"{content.height}";
    }
    
    public void ActiveButtons(bool isActive)
    {
        GetObject((int)Objects.Progress_1).SetActive(isActive);
    }
}
