using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        if(Managers.Data.userData.book == null)
        {
            Managers.UI.ShowPopupUI<BookSelect_Popup>();
        }
    }

    void Update()
    {
              
    }
}
