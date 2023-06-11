using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class Content_Experiment : Content_Base
{
    public PlayableDirector director;

    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        director = GetComponent<PlayableDirector>();
        director.playOnAwake = false;
        director.extrapolationMode = DirectorWrapMode.Hold;
        director.timeUpdateMode = DirectorUpdateMode.GameTime;
        
    }

}