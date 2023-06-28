using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Events;

// 실험형 컨텐츠 (과정에 따라 흘러가는 실험의 부모클래스)
public class Content_Experiment : Content_Base
{
    int _progress;
    
    public int Progress
    {
        get
        {
            return _progress;
        }
        set
        {
            _progress = value;
            if (value > maxProgress)
                _progress = maxProgress;
            SetProgress();
        }
    }

    public int maxProgress;
    public bool isPaused;
    bool _isComplete;
    
    public bool IsComplete
    {
        get
        {
            return _isComplete;
        }
        
        set
        {
            _isComplete = value;
            if (IsComplete)
            {
                if (Progress > 0 && Progress < maxProgress)
                    contentUI.ActiveNextProgressButton();
            }
            
        }
    }
    
    protected override void Init()
    {
        base.Init();
    }
    void Start() 
    {
        Progress = 0;
        IsComplete = false;
    }

    private void Update()
    {
        ProgressUpdate();
    }

    // 진행별 초기화 함	
    protected virtual void SetProgress()
    {
        IsComplete = false;
        switch (Progress)
        {
            case 0:
                //Init
                break;
                
            case 1:
                
                break;
        }

        if(Progress > 0) contentUI.HighlightProgressButton(Progress-1);
    }

    protected virtual void ProgressUpdate()
    {
        switch (Progress)
        {
            case 0:
                if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    contentUI.DoPlayStartTextTweening();
                    
                }
                break;
            case 1:

                break;
        }
    }
}