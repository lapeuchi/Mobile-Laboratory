using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class Content_DropBalloon : Content_Experiment
{
    Balloon balloon;
    
    GameObject plane;
    public GameObject cusion;
    float Excersice;

    private float[] heightArray = new float[3] { 0.5f, 1.0f, 2.0f };
    int _heightLevel;
    private float height;

    public int HeightLevel
    {
        get { return _heightLevel; }
        set
        { 
            _heightLevel = value;
            if (_heightLevel <= 0) _heightLevel = 0;
            else if (_heightLevel >= heightArray.Length) _heightLevel = heightArray.Length-1;
            height = heightArray[_heightLevel];
            
            ui.SetHeightText(height);
        }
    }

    float[] sizeArray = new float[3] {0.3f, 0.6f, 0.9f};
    float[] massArray = new float[3] { 1f, 1.5f, 2f };
    private float mass;
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
            
            ui.SetMassText(mass);
        }
    }
    
    UI_Balloon_Popup ui;
    protected override void Init()
    {
        MaxProgress = 2;
        ResultCode = Define.DataCodes.DropBalloon_Experiment;
        plane = GameObject.Find("Plane");
        balloon = GameObject.Find("Balloon").GetComponent<Balloon>();
        cusion = GameObject.Find("Cusion");
        ui = FindObjectOfType<UI_Balloon_Popup>().GetComponent<UI_Balloon_Popup>();

        base.Init();
    }

    void Start() 
    {
        balloon.p = 0;
        balloon.i = 0;
        MassLevel = 0;
        HeightLevel = 0;
        balloon.rigid.isKinematic = true;
    }

    protected override void SetProgress()
    {        
        switch (Progress)
        {
            case 1:
                ui.ActiveButtons(true);
                balloon.gameObject.SetActive(true);
                balloon.rigid.isKinematic = true;
                SetBalloonHeight(false);
                SetBalloonMass(false);
                balloon.isChecked = false;
                break;
            case 2:
                ui.ActiveButtons(false);
                balloon.gameObject.SetActive(true);
                SetBalloonHeight(false);
                SetBalloonMass(false);
                balloon.isChecked = false;
                balloon.rigid.isKinematic = false;
                break;
        }
        base.SetProgress();
    }

    protected override void ProgressUpdate()
    {
        base.ProgressUpdate();
        
        switch (Progress)
        {
           
            case 1:

                break;
        }
    }

    protected override void OnCompletedProgress()
    {
        switch (Progress)
        {
            case 2: 
                ui.ActiveResult(true, balloon.p, balloon.i);
                break;
        }
    }

    public void SetBalloonHeight(bool playAnim)
    {
        float destY = heightArray[HeightLevel]+plane.transform.position.y;
        if(playAnim)
        {
            balloon.transform.DOMoveY(destY, 0.3f);
        }
        else
        {
            balloon.transform.position =
            new Vector3 (
                plane.transform.position.x, 
                destY,
                plane.transform.position.z
            );
        }
        
    }

    public void SetBalloonMass(bool playAnim)
    {
        balloon.rigid.mass = massArray[MassLevel];
        float scale = sizeArray[MassLevel];
        if(playAnim)
        {
            balloon.transform.DOScale(scale, 0.2f);
        }
        else
        {       
            balloon.transform.localScale = new Vector3(scale, scale, scale);
        }
        
    } 

    public override void Clear()
    {
        base.Clear();
        ui.ClosePopupUI();
        Destroy(ui.gameObject);
    }
}

