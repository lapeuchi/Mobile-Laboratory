#define Debug
//#define Release

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
        get { return mode; }
        set 
        { 
            mode = value;
            if(mode == Define.ModeState.Content)
            {
                imageTrackable.enabled = false;
                Camera.main.gameObject.SetActive(false);
                ui_MainScene.gameObject.SetActive(false);
            }
            else
            {
                imageTrackable.enabled = true;
                imageTrackable._isSuccess = false;
                Camera.main.gameObject.SetActive(true);
                ui_MainScene.gameObject.SetActive(true);
            }
        }
    }

#if DEBUG
    [SerializeField] string TestContent;
#endif

    protected override void Init()
    {
        ui_MainScene = Managers.UI.ShowSceneUI<UI_MainScene>();
        imageTrackable = GameObject.Find("ARCamera").GetComponent<ImageTrackable>();
        
        Mode = Define.ModeState.Tracking;

        if(Managers.Data.userData.Book == null)
        {
            Managers.UI.ShowPopupUI<UI_BookSelect_Popup>();
        }   
        base.Init();
    }
    
#if Debug
    void Start()
    {
        InstantiateContent();  
    }
#endif
    
    public void InstantiateContent()
    {
        if(TestContent != null)
        {
            Managers.Resource.Instantiate($"Contents/{TestContent}/{TestContent}");
        }
    }

    public static void InstantiateContent(Data.TrackableImage imageData)
    {
        Managers.UI.ShowPopupUI<UI_TrackingSucessPopup>().SetInfo(imageData);
    } 

}
