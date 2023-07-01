using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Events;

// 실험형 컨텐츠 (과정에 따라 흘러가는 실험의 부모클래스)
public abstract class Content_Experiment : Content_Base
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
            if (value > MaxProgress)
                _progress = MaxProgress;
            SetProgress();
        }
    }

    public int _maxProgress;
    public int MaxProgress
    {
        get { return _maxProgress; }
        set 
        {  
            _maxProgress = value;
            contentUI.CreateProgressButton(); 
        }
    }
    
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
            if (value)
            {
                if (Progress > 0 && Progress <= MaxProgress)
                {
                    OnCompletedProgress();
                    if (Progress == MaxProgress)
                        contentUI.SetActiveCompleteButton(true);
                    
                }
            }
            else
            {
                contentUI.SetActiveCompleteButton(false);
            }
        }
    }

    public Define.DataCodes ResultCode;
    
    protected override void Init()
    {
        base.Init();
        Progress = 0;
        IsComplete = false;
    }

    private void Update()
    {
        if(Progress >= 1)
        {
            ProgressUpdate();
        }
        else
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                contentUI.DoPlayStartTextTweening();
            }
        }
    }

    // 진행별 초기화 함	
    protected virtual void SetProgress()
    {
        IsComplete = false;
        if(Progress >= 1) contentUI.HighlightProgressButton(Progress-1);
    }

    protected virtual void ProgressUpdate() { }
    
    protected abstract void OnCompletedProgress();
}