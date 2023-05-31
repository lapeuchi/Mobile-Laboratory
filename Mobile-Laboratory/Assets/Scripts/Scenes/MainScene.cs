using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public static MainScene instance;

    public UI_Tracking ui_Tracking;
    public UI_Content ui_Content;
    ImageTrackable imageTrackable;

    protected override void Init()
    {
        base.Init();
        instance = this;
        imageTrackable = GameObject.Find("ARCamera").GetComponent<ImageTrackable>();

        ui_Tracking = Managers.UI.ShowSceneUI<UI_Tracking>();
        
        if(Managers.Data.userData.Book == null)
        {
            Managers.UI.ShowPopupUI<BookSelect_Popup>();
        }
        
    }   
}