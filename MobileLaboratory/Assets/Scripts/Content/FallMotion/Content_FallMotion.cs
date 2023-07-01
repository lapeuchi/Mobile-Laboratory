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
        ResultCode = Define.DataCodes.FallBall_Experiment;
        MaxProgress = 2;
        
        if (_afterImagesRoot == null)
            _afterImagesRoot = new GameObject { name = "AfterImagesRoot" }.transform;

        RefreshBalls();
        base.Init();

    }

    void RefreshBalls()
    {
        if (_freeDropBall != null)
            Managers.Resource.Destory(_freeDropBall.gameObject);
        if(_verticalDropBall != null)
            Managers.Resource.Destory(_verticalDropBall.gameObject);
            
        _freeDropBall = Managers.Resource.Instantiate("Contents/Content_FreeFallMotion/Ball").GetOrAddComponent<FallBall>();
        _freeDropBall.transform.position = new Vector3(-8f, 4.25f, 6.5f);
        _freeDropBall.SetInfo(Define.Fall.FreeDrop, _afterImagesRoot);
        _freeDropBall.gameObject.GetComponent<Renderer>().material.color = Color.red;
        
        _verticalDropBall = Managers.Resource.Instantiate("Contents/Content_FreeFallMotion/Ball").GetOrAddComponent<FallBall>();
        _verticalDropBall.transform.position = new Vector3(8f, 4.25f, 6.5f);
        _verticalDropBall.SetInfo(Define.Fall.VerticalDrop, _afterImagesRoot);
        _verticalDropBall.gameObject.GetComponent<Renderer>().material.color = Color.blue;

    }

    protected override void SetProgress()
    {
       base.SetProgress();

        switch (Progress)
        {
            case 1:
                RefreshBalls();

                int childCount = _afterImagesRoot.childCount;
                if (childCount > 0)
                for (int i = 0; i < childCount; i++)
                {
                    GameObject go = _afterImagesRoot.GetChild(i).gameObject;
                    Managers.Resource.Destory(go);
                }
                _freeDropBall.transform.position = new Vector3(-8f, 4.25f, 6.5f);
                _verticalDropBall.transform.position = new Vector3(8f, 4.25f, 6.5f);
                IsComplete = true;
                break;
            case 2:
                _freeDropBall.OnFire();
                _verticalDropBall.OnFire();
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
                    IsComplete = true;
                break;
            case 3:
                IsComplete = true;
                break;
        }
    }

    protected override void OnCompletedProgress()
    {
        switch(Progress)
        {
            case 1:
                contentUI.SetActiveNextProgressButton(true);
                break;
        }

    }

    public override void Clear()
    {
        Destroy(_verticalDropBall.gameObject);
        Destroy(_freeDropBall.gameObject);
        Destroy(_afterImagesRoot.gameObject);
        base.Clear();
    }
}