using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// ��ǳ���� �̿��� ����� ��ݷ��� ���� �˾ƺ���
public class Content_DropBalloon : Content_Experiment
{
    GameObject balloon;
    Rigidbody balloonRigidbody;
    GameObject plane;

    float excersice;
    float height;
    float mass;

    protected override void Init()
    {
        base.Init();
        InstantiateActor("Contents/Content_DropBalloon/ExperimentSet", transform);
    }

    protected override void SetProgress(int progress)
    {
        switch (progress)
        {
            case 1:

                break;
        }

        base.SetProgress(progress);
    }
}