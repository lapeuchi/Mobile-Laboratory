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
        contentUI = Managers.UI.ShowPopupUI<UI_ContentPopup>();

        Init();
    }
    
    protected virtual void Init()
    {
       
    }

    public virtual void Clear()
    {
        mainScene.Mode = Define.ModeState.Tracking;
        contentUI.ClosePopupUI();
        Destroy(gameObject);
    }
    
}
