using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public static MainScene instance;

    public static UI_Tracking ui_Tracking;
    Camera actorCam;
    public Transform actorTransform;
    public GameObject actor;

    protected override void Init()
    {
        instance = this;

        base.Init();

        if(Managers.Data.userData.Book == null)
        {
            Managers.UI.ShowPopupUI<BookSelect_Popup>();
        }
        
        ui_Tracking = Managers.UI.ShowSceneUI<UI_Tracking>();

        actorTransform = GameObject.Find("ActorPos").transform;
        actorCam = GameObject.Find("ActorCam").GetComponent<Camera>();
    }
    

    public void InstantiateActor(string path, Transform parent)
    {
        actor = Managers.Resource.Instantiate(path, actorTransform.position, Quaternion.identity);
        actor.transform.LookAt(actorCam.transform);
        actor.transform.SetParent(parent);
    }
}
