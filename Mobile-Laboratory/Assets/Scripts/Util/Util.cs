using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public static class Util
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
    
        if (component == null)
            component = go.AddComponent<T>();
    
        return component;
    }

    public static T FindChild<T>(this GameObject go, string name = null, bool recursive = false, bool searchInactive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if(recursive)
        {
            foreach(T component in go.GetComponentsInChildren<T>(searchInactive))
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                
                T component = transform.GetComponent<T>();
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    if (component != null)
                        return component;
                }
            }
        }

        return null;
    }
    public static T[] FindChildren<T>(this GameObject go, string name = null, bool recursive = false, bool searchInactive = false) where T : UnityEngine.Object
    {
        List<T> list = new List<T>();
        if (go == null)
            return null;

        if(recursive)
        {
            foreach(T component in go.GetComponentsInChildren<T>(searchInactive))
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    if (component != null)
                        list.Add(component);
                }
            }
        }
        else
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                
                T component = transform.GetComponent<T>();
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    if (component != null)
                        list.Add(component);
                }
            }
        }

        return list.ToArray();
    }

    public static GameObject FindChild(this GameObject go, string name = null, bool recursive = false, bool searchInactive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive, searchInactive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static void BindEvent(this GameObject go, Action<PointerEventData> evtData, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler eventHandler = go.GetOrAddComponent<UI_EventHandler>();

        switch(type)
        {
            case Define.UIEvent.Click:
                eventHandler.OnClickHandler -= evtData;
                eventHandler.OnClickHandler += evtData;
                break;
        }
    }

    // 헥사값 컬러 반환( 코드 순서 : RGBA )
    public static Color HexColor(string hexCode)
    {
        Color color;
        if ( ColorUtility.TryParseHtmlString(hexCode, out color))
        {
            return color;
        }       
        Debug.LogError( "[Util::HexColor]invalid hex code - " + hexCode );
        return Color.white;
    }
}
