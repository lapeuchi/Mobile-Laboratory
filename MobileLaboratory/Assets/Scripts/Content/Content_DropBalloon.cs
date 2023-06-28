using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Content_DropBalloon : Content_Experiment
{
    Rigidbody balloonRigidbody;

    GameObject plane;

    float Excersice;

    public float[] heightArray = new float[3] { 1.0f, 2.0f, 3.0f };
    int _heightLevel;
    public float height;
    public int HeightLevel
    {
        get { return _heightLevel; }
        set
        { 
            _heightLevel = value;
            if (_heightLevel <= 0) _heightLevel = 0;
            else if (_heightLevel >= heightArray.Length) _heightLevel = heightArray.Length-1;
            height = heightArray[_heightLevel];
            SetBalloon();
        }
    }

    float[] massArray = new float[3] { 0.1f, 0.5f, 1.0f };
    public float mass;
    int _massLevel;
    public int MassLevel
    {
        get { return _massLevel; }
        set 
        { 
            _massLevel = value; 
            if (_massLevel <= 0) _massLevel = 0;
            else if (_massLevel >= massArray.Length) _massLevel = heightArray.Length-1;
            mass = massArray[_massLevel];
            SetBalloon();
        }
    }
    
    UI_Balloon_Popup ui;
    protected override void Init()
    {
        maxProgress = 2;
        base.Init();
    }

    protected override void SetProgress()
    {
        base.SetProgress();
        
        switch (Progress)
        {
            case 0:
                ui = FindObjectOfType<UI_Balloon_Popup>().GetComponent<UI_Balloon_Popup>();
                ui.ActiveButtons(false);
                Transform contentSet = transform.Find("Content");
                contentSet.SetParent(transform);
                contentSet.position = GameObject.Find("Lab").transform.position + new Vector3(0, 2, -0.2f);
        
                plane = GameObject.Find("Plane");
                balloonRigidbody = GameObject.Find("Balloon").GetComponent<Rigidbody>();
                balloonRigidbody.isKinematic = true;
                SetBalloon();
                break;
            case 1:
                ui.ActiveButtons(true);
                balloonRigidbody.isKinematic = true;
                SetBalloon();
                break;
            case 2:
                ui.ActiveButtons(false);
                balloonRigidbody.isKinematic = false;
                break;
        }
    }

    protected override void ProgressUpdate()
    {
        base.ProgressUpdate();
        
        switch (Progress)
        {
            case 0:
                
                break;
            case 1:

                break;
        }
    }

    public void SetBalloon()
    {
        balloonRigidbody.transform.position = new Vector3(plane.transform.position.x, heightArray[HeightLevel]+plane.transform.position.y, plane.transform.position.z);
        balloonRigidbody.mass = massArray[MassLevel];
    }
}