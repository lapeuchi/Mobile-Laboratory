using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content_Base : MonoBehaviour
{
    Camera actorCam;
    public Transform actorTransform;
    public GameObject actor;
    public MainScene mainScene;
    public UI_ContentPopup contentUI;

    void Awake()
    {
        mainScene = FindObjectOfType<MainScene>();
        mainScene.Mode = Define.ModeState.Content;

        actorTransform = GameObject.Find("ActorPos").transform;
        actorCam = GameObject.Find("ActorCam").GetComponent<Camera>();
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

    public void InstantiateActor(string path, Transform parent)
    {
        actor = Managers.Resource.Instantiate(path, actorTransform.position, Quaternion.identity);
        actor.transform.LookAt(actorCam.transform);
        actor.transform.SetParent(parent);
    }
    
}
