using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class MainScene : BaseScene
{
    ImageTrackable imageTrackable;
    public UI_MainScene ui_MainScene;

    
    public Define.ModeState mode;
    public Define.ModeState Mode
    {
        get {return mode;}
        set 
        { 
            mode = value;
            if(mode == Define.ModeState.Content)
            {
                imageTrackable.enabled = false;
            }
            else
            {
                imageTrackable.enabled = true;
                imageTrackable._isSuccess = false;
            }
        }
    }

    protected override void Init()
    {
        base.Init();
        
        imageTrackable = GameObject.Find("ARCamera").GetComponent<ImageTrackable>();
        
        ui_MainScene = Managers.UI.ShowSceneUI<UI_MainScene>();
        Mode = Define.ModeState.Tracking;

        if(Managers.Data.userData.Book == null)
        {
            Managers.UI.ShowPopupUI<UI_BookSelect_Popup>();
        }
        
    }   

    public static void InstantiateContent(Data.TrackableImage imageData)
    {
        Managers.UI.ShowPopupUI<UI_TrackingSucessPopup>().SetInfo(imageData);
    } 

}
