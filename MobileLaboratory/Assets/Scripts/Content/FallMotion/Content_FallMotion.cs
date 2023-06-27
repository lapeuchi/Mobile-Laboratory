using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content_FallMotion : Content_Experiment
{
    GameObject _freeFallBall;
    GameObject _verticalDropBall;

    protected override void SetProgress()
    {
        base.SetProgress();

        switch (Progress)
        {
            case 0:
                _freeFallBall = Managers.Resource.Instantiate("Prefabs/");
                _verticalDropBall = Managers.Resource.Instantiate("");
                break;
            case 1:
                break;
        }
    }
}
