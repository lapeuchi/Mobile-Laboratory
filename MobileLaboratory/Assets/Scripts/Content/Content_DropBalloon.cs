using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Content_DropBalloon : Content_Experiment
{
    
    protected override void Init()
    {
        base.Init();
        InstantiateActor("Contents/Content_DropBalloon/ExperimentSet", transform);
    }
}