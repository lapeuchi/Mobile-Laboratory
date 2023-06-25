using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content_Base : MonoBehaviour
{
    Camera actorCam;
    public Transform actorTransform;
    public GameObject actor;
    public MainScene mainScene;


    void Awake()
    {
        Managers.UI.ShowPopupUI<UI_ContentPopup>();
        Init();
    }
    
    protected virtual void Init()
    {
        mainScene = GameObject.FindObjectOfType<MainScene>();
        mainScene.Mode = Define.ModeState.Content;

        actorTransform = GameObject.Find("ActorPos").transform;
        actorCam = GameObject.Find("ActorCam").GetComponent<Camera>();
        
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
