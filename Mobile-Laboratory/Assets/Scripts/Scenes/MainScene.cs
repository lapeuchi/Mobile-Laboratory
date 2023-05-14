using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public static UI_Tracking ui_Tracking;
    protected override void Init()
    {
        base.Init();

        if(Managers.Data.userData.Book == null)
        {
            Managers.UI.ShowPopupUI<BookSelect_Popup>();
        }
        
        ui_Tracking = Managers.UI.ShowSceneUI<UI_Tracking>();
    }
    
}
