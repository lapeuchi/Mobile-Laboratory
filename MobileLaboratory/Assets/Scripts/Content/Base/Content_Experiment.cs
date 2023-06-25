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
        private set
        {
            _progress = value;
            SetProgress(value);
        }
    }
    public int maxProgress;
    public bool isPaused;
    
    protected override void Init()
    {
        Progress = 0;

        base.Init();
    }

    protected virtual void SetProgress(int progress)
    {
    
    }
}