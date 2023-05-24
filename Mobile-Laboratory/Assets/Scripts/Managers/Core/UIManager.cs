using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    Transform _root;
    public Transform Root
    {
        get
        {
            if(_root == null)
                _root = new GameObject { name = "@UI_Root" }.transform;
            
            return _root;
        }
    }
    
    public UI_Scene SceneUI { get; set; }
    Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    int order = 10;
    Vector2 resolution = new Vector2(2340, 1080);

    public void SetCanvas(GameObject go, bool sort = false)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler scaler = go.GetOrAddComponent<CanvasScaler>();
        scaler.referenceResolution = resolution;
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;

        if(canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            canvas.worldCamera = Camera.main;
        }

        go.transform.SetParent(_root);

        if(sort)
        {
            canvas.sortingOrder = order;
            order++;
            
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        if (go == null)
            return null;

        T popup = go.GetOrAddComponent<T>();
        popupStack.Push(popup);
        return popup;
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        if (go == null)
            return null;

        T sceneUI = go.GetOrAddComponent<T>();
        SceneUI = sceneUI;
        return sceneUI;
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");

        if (go == null)
            return null;

        T worldSpaceUI = go.GetOrAddComponent<T>();
        worldSpaceUI.transform.SetParent(_root);

        if (parent != null)
            worldSpaceUI.transform.SetParent(parent);

        return worldSpaceUI;
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;
        
        UI_Popup popup = popupStack.Pop();
        Managers.Resource.Destory(popup.gameObject);
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (popupStack.Count == 0)
            return;

        if(popupStack.Peek() == popup)
            ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        Managers.Resource.Destory(SceneUI.gameObject);
        SceneUI = null;
    }
}
