using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    ImageTrackable imageTrackable;

    private static bool isPlayingContent;
    public static bool IsPlayingContent 
    {
        get { return isPlayingContent; }
        set { isPlayingContent = value; }
    }

    protected override void Init()
    {
        base.Init();
        
        imageTrackable = GameObject.Find("ARCamera").GetComponent<ImageTrackable>();
        
        Managers.UI.ShowSceneUI<UI_Tracking>();
        
        if(Managers.Data.userData.Book == null)
        {
            Managers.UI.ShowPopupUI<UI_BookSelect_Popup>();
        }
        
    }   
}