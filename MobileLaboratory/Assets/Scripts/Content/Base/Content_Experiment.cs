using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.Events;

public class Content_Experiment : Content_Base
{
    public PlayableDirector director;
    SignalReceiver receiver;

    UnityEvent action;

    protected override void Init()
    {
        base.Init();
        director = GetComponent<PlayableDirector>();
        receiver = GetComponent<SignalReceiver>();

        director.playOnAwake = false;

        director.extrapolationMode = DirectorWrapMode.Hold;
        director.timeUpdateMode = DirectorUpdateMode.GameTime;
        
        StopSignal(delegate{int i = 0;});
    }

    public void StopSignal(UnityAction evt)
    {
        director.Pause();
        action.AddListener(evt);
    }
}