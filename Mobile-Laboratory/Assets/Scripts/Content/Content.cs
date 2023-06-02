using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content : MonoBehaviour
{
    Camera actorCam;
    public Transform actorTransform;
    public GameObject actor;
    public MainScene mainScene;

    public bool isPaused;

    void Awake()
    {
        Init();
    }
    
    protected virtual void Init()
    {
        mainScene = GameObject.FindObjectOfType<MainScene>();
        mainScene.Mode = Define.ModeState.Content;

        actorTransform = GameObject.Find("ActorPos").transform;
        actorCam = GameObject.Find("ActorCam").GetComponent<Camera>();
        
        
        isPaused = false;
    }

    protected virtual void Clear()
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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) SetPause(!isPaused);
    }

    public void SetPause(bool active)
    {
        if (active)
        {
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
        }
        
    }
}
