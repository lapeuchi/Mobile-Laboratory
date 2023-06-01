using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content : MonoBehaviour
{
    Camera actorCam;
    public Transform actorTransform;
    public GameObject actor;

    void Awake()
    {
        Init();
    }
    
    protected virtual void Init()
    {
        actorTransform = GameObject.Find("ActorPos").transform;
        actorCam = GameObject.Find("ActorCam").GetComponent<Camera>();
        MainScene.IsPlayingContent = true;
    }

    private void OnDestroy()
    {
        Clear();
    }
    
    protected virtual void Clear()
    {
        MainScene.IsPlayingContent = false;
    }

    public void InstantiateActor(string path, Transform parent)
    {
        actor = Managers.Resource.Instantiate(path, actorTransform.position, Quaternion.identity);
        actor.transform.LookAt(actorCam.transform);
        actor.transform.SetParent(parent);
    }
}
