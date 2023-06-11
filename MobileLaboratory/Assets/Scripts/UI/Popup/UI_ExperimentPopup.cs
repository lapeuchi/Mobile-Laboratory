using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UI_ExperimentPopup : UI_Popup
{
    enum Buttons
    {
        PlayButton,
        PrevButton,
        NextButton
    }
    enum Images
    {
        PlayButton,
        PrevButton,
        NextButton
    }
    
    Content_Experiment content;

    protected override void Init()
    {   
        BindButton(typeof(Buttons), true);
        
        base.Init();
        content = GameObject.FindObjectOfType<Content_Experiment>();
        
        GetButton((int)Buttons.PlayButton).onClick.AddListener(delegate { OnClickedPlay(); } );
        GetButton((int)Buttons.PrevButton).onClick.AddListener(delegate { OnClickedPrevButton(); } );
        GetButton((int)Buttons.NextButton).onClick.AddListener(delegate { OnClickedNextButton(); } );
    }

    void OnClickedPlay()
    {
        if(content.director.state == PlayState.Paused)
        {
            content.director.Resume();
            GetImage((int)Images.PlayButton).color = Color.yellow;
        }
        else
        {
            content.director.Stop();
            GetImage((int)Images.PlayButton).color = Color.cyan;
        }
    }

    void OnClickedPrevButton()
    {
        
    }

    void OnClickedNextButton()
    {

    }
}
