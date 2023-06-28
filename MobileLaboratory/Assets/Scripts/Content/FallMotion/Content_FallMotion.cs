using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content_FallMotion : Content_Experiment
{
    Transform _afterImagesRoot;

    FallBall _freeDropBall;
    FallBall _verticalDropBall;
    
    protected override void Init()
    {
        maxProgress = 3;
        Managers.SetActiveToCamera(false);
        base.Init();
    }

    protected override void SetProgress()
    {
        base.SetProgress();

        switch (Progress)
        {
            case 1:
                if (_afterImagesRoot == null)
                    _afterImagesRoot = new GameObject { name = "AfterImagesRoot" }.transform;

                int childCount = _afterImagesRoot.childCount;

                if (childCount > 0)
                    for (int i = 0; i < childCount; i++)
                    {
                        GameObject go = _afterImagesRoot.GetChild(i).gameObject;
                        Managers.Resource.Destory(go);
                    }

                if (_freeDropBall != null)
                    Managers.Resource.Destory(_freeDropBall.gameObject);
                if(_verticalDropBall != null)
                    Managers.Resource.Destory(_verticalDropBall.gameObject);

                _freeDropBall = Managers.Resource.Instantiate("Contents/Content_FallMotion/Ball").GetOrAddComponent<FallBall>();
                _freeDropBall.transform.position = new Vector3(-8f, 4.25f, 6.5f);
                _freeDropBall.SetInfo(Define.Fall.FreeDrop, _afterImagesRoot);
                _freeDropBall.gameObject.GetComponent<Renderer>().material.color = Color.red;

                _verticalDropBall = Managers.Resource.Instantiate("Contents/Content_FallMotion/Ball").GetOrAddComponent<FallBall>();
                _verticalDropBall.transform.position = new Vector3(8f, 4.25f, 6.5f);
                _verticalDropBall.SetInfo(Define.Fall.VerticalDrop, _afterImagesRoot);
                _verticalDropBall.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 2:
                _freeDropBall.OnFire();
                _verticalDropBall.OnFire();
                break;
            case 3:
                IsComplete = true;
                break;
        }
    }

    bool IsArrive()
    {
        if (_verticalDropBall == null || _freeDropBall == null)
            return false;

        return _verticalDropBall.IsArrive || _freeDropBall.IsArrive;
    }

    protected override void ProgressUpdate()
    {
        base.ProgressUpdate();

        switch(Progress)
        {
            case 2:
                if(IsArrive())
                    Progress = 3;
                break;
            case 3:
                IsComplete = true;
                break;
        }
    }
}