using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content_Base : MonoBehaviour
{
   
    public MainScene mainScene;
    public UI_ContentPopup contentUI;

    void Awake()
    {
        mainScene = FindObjectOfType<MainScene>();
        mainScene.Mode = Define.ModeState.Content;
       
        Init();
    }
    
    protected virtual void Init()
    {
        contentUI = Managers.UI.ShowPopupUI<UI_ContentPopup>();
    }

    public virtual void Clear()
    {
        mainScene.Mode = Define.ModeState.Tracking;
        Destroy(gameObject);
    }
    
}
