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
        Progress_1End
    }

    enum Texts
    {    
        MassText,
        HeightText,
        IText,
        PText
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
        
        GetButton((int)Buttons.MassUpButton).onClick.AddListener(delegate{OnClickedMassButton(true);});
        GetButton((int)Buttons.MassDownButton).onClick.AddListener(delegate{OnClickedMassButton(false);});
        GetButton((int)Buttons.HeightUpButton).onClick.AddListener(delegate{OnClickedHeightButton(true);});
        GetButton((int)Buttons.HeightDownButton).onClick.AddListener(delegate{OnClickedHeightButton(false);});

        GetButton((int)Buttons.DropButton).onClick.AddListener(delegate { content.Progress = 2; });
        GetButton((int)Buttons.CusionButton).onClick.AddListener(delegate { content.cusion.SetActive(!content.cusion.activeSelf);});

        ActiveButtons(false);
        ActiveResult(false);

        base.Init();
    }

    void OnClickedMassButton(bool isUp)
    {
        if(isUp)
            ++content.MassLevel;
        else
            --content.MassLevel;        

        content.SetBalloonMass(true);
    }
    void OnClickedHeightButton(bool isUp)
    {
        if(isUp)
            ++content.HeightLevel;
        else
            --content.HeightLevel;

        content.SetBalloonHeight(true);
    }
    
    public void SetHeightText(float height)=> GetText((int)Texts.HeightText).text = $"{height}";
    public void SetMassText(float mass)=> GetText((int)Texts.MassText).text = $"{mass}";

    public void OnClickedCusionButton()
    {
        content.cusion.SetActive(false);    
    }

    public void ActiveButtons(bool isActive)
    {
        GetObject((int)Objects.Progress_1).SetActive(isActive);
    }

    public void ActiveResult(bool active, float p=0, float i=0) 
    {
        if (active) {
            GetObject((int)Objects.Progress_1End).SetActive(true);
            GetText((int)Texts.PText).text = $"운동량: {p}";
            GetText((int)Texts.IText).text = $"충격량: {i}";
        }
        else  {
            GetObject((int)Objects.Progress_1End).SetActive(false);
        }
    }
}
