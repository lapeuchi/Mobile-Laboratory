using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public static MainScene instance;

    public UI_Tracking ui_Tracking;
    public UI_Content ui_Content;
    ImageTrackable imageTrackable;

    
    Define.ModeState mode;
    public Define.ModeState Mode 
    {
        get {return mode;}
        set 
        {
            switch (value)
            {
                case Define.ModeState.Tracking:
                imageTrackable.enabled = true;
                break;
                case Define.ModeState.Content:
                imageTrackable.enabled = false;
                break;
            }

            mode = value;
        }
    }

    protected override void Init()
    {
        base.Init();
        instance = this;
        imageTrackable = GameObject.Find("ARCamera").GetComponent<ImageTrackable>();


        if(Managers.Data.userData.Book == null)
        {
            Managers.UI.ShowPopupUI<BookSelect_Popup>();
        }
        else
        {
            ui_Tracking = Managers.UI.ShowSceneUI<UI_Tracking>();
        }   
    }   
}