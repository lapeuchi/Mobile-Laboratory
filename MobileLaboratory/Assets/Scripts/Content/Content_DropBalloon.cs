using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// ???????? ?????? ???????? ???????? ???? ????????
public class Content_DropBalloon : Content_Experiment
{
    GameObject balloon;
    Rigidbody balloonRigidbody;
    GameObject floor;

    float excersice;
    float height;
    float mass;

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
                InstantiateActor("Contents/Content_DropBalloon/ExperimentSet", transform);
                balloon = GameObject.Find("Balloon");
                balloonRigidbody = balloon.GetComponent<Rigidbody>();
                balloonRigidbody.isKinematic = true;
                floor = GameObject.Find("Plane");
                break;
            case 1:
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
                InstantiateActor("Contents/Content_DropBalloon/ExperimentSet", transform);
                balloon = GameObject.Find("Balloon");
                balloonRigidbody = balloon.GetComponent<Rigidbody>();
                balloonRigidbody.isKinematic = true;
                floor = GameObject.Find("Plane");
                break;
            case 1:
                balloonRigidbody.isKinematic = false;
                break;
        }
    }
}