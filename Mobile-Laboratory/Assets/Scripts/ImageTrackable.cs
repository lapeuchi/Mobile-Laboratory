using maxstAR;
using System.Collections.Generic;
using UnityEngine;
using Data;
using UnityEngine.EventSystems;

public class ImageTrackable : ARBehaviour
{
    List<ImageTrackableBehaviour> _trackableList = new List<ImageTrackableBehaviour>();
    CameraBackgroundBehaviour cameraBackgroundBehaviour = null;

    void Awake()
    {
        Init();

        AndroidRuntimePermissions.Permission[] result = AndroidRuntimePermissions.RequestPermissions("android.permission.WRITE_EXTERNAL_STORAGE", "android.permission.CAMERA");
        if (result[0] == AndroidRuntimePermissions.Permission.Granted && result[1] == AndroidRuntimePermissions.Permission.Granted)
            Debug.Log("We have all the permissions!");
        else
            Debug.Log("Some permission(s) are not granted...");

        cameraBackgroundBehaviour = FindObjectOfType<CameraBackgroundBehaviour>();
        if (cameraBackgroundBehaviour == null)
        {
            Debug.LogError("Can't find CameraBackgroundBehaviour.");
            return;
        }
    }

    void Start()
    {
        TrackerManager.GetInstance().SetCloudRecognitionSecretIdAndSecretKey("32b41d66c3924477955...", "c40d6fbca31e4d03baa6...");
        TrackerManager.GetInstance().StartTracker(TrackerManager.TRACKER_TYPE_CLOUD_RECOGNIZER);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        _trackableList.Clear();
        ImageTrackableBehaviour[] imageTrackables = FindObjectsOfType<ImageTrackableBehaviour>();
        foreach (var trackable in imageTrackables)
        {
            
            _trackableList.Add(trackable);
            Debug.Log("Trackable add: " + trackable.TrackableName);
        }

        TrackerManager.GetInstance().StartTracker(TrackerManager.TRACKER_TYPE_IMAGE);
        AddTrackerData();

        StartCamera();

        // For see through smart glass setting
        if (ConfigurationScriptableObject.GetInstance().WearableType == WearableCalibration.WearableType.OpticalSeeThrough)
        {
            WearableManager.GetInstance().GetDeviceController().SetStereoMode(true);

            CameraBackgroundBehaviour cameraBackground = FindObjectOfType<CameraBackgroundBehaviour>();
            cameraBackground.gameObject.SetActive(false);

            WearableManager.GetInstance().GetCalibration().CreateWearableEye(Camera.main.transform);
        }
    }

    private void AddTrackerData()
    {
        foreach (var trackable in _trackableList)
        {
            if (trackable.TrackerDataFileName.Length == 0)
            {
                continue;
            }

            if (trackable.StorageType == StorageType.AbsolutePath)
            {
                TrackerManager.GetInstance().AddTrackerData(trackable.TrackerDataFileName);
                TrackerManager.GetInstance().LoadTrackerData();
            }
            else if (trackable.StorageType == StorageType.StreamingAssets)
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    StartCoroutine(MaxstARUtil.ExtractAssets(trackable.TrackerDataFileName, (filePah) =>
                    {
                        TrackerManager.GetInstance().AddTrackerData(filePah, false);
                        TrackerManager.GetInstance().LoadTrackerData();
                    }));
                }
                else
                {
                    TrackerManager.GetInstance().AddTrackerData(Application.streamingAssetsPath + "/" + trackable.TrackerDataFileName);
                    TrackerManager.GetInstance().LoadTrackerData();
                }
            }
        }
    }

    private void DisableAllTrackables()
    {
        foreach (var trackable in _trackableList)
        {
            trackable.OnTrackFail();
        }
    }

    bool _isSuccess;

    void Update()
    {
        DisableAllTrackables();

        TrackingState state = TrackerManager.GetInstance().UpdateTrackingState();

        if (state == null)
        {
            return;
        }

        cameraBackgroundBehaviour.UpdateCameraBackgroundImage(state);

        if (_isSuccess)
            return;

        TrackingResult trackingResult = state.GetTrackingResult();

        for (int i = 0; i < trackingResult.GetCount(); i++)
        {
            //인식 성공한 개체

            if (_isSuccess)
                break;

            Trackable trackable = trackingResult.GetTrackable(i);
            TrackableImage imageData = Managers.Data.TrackableImages[trackable.GetId()];
            _trackableList[i].OnTrackSuccess(trackable.GetId(), trackable.GetName(), trackable.GetPose());
            OnSuccessByImageTracking();

            if(_isSuccess)
            {
                Managers.UI.ShowPopupUI<UI_TrackingSucessPopup>().SetInfo(imageData.name, imageData.page, OnCancleByImageTracking);
            }
        }
    }

    public bool OnSuccessByImageTracking()
    {
        if (_isSuccess)
            return true;

        _isSuccess = true;
        return true;
    }

    public void OnCancleByImageTracking(PointerEventData evtData)
    {
        if(_isSuccess)
        {
            _isSuccess = false;
            Managers.UI.ClosePopupUI();
        }
    }

    public void SetNormalMode()
    {
        TrackerManager.GetInstance().SetTrackingOption(TrackerManager.TrackingOption.NORMAL_TRACKING);
    }

    public void SetExtendedMode()
    {
        TrackerManager.GetInstance().SetTrackingOption(TrackerManager.TrackingOption.EXTEND_TRACKING);
    }

    public void SetMultiMode()
    {
        TrackerManager.GetInstance().SetTrackingOption(TrackerManager.TrackingOption.MULTI_TRACKING);
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            TrackerManager.GetInstance().StopTracker();
            StopCamera();
        }
        else
        {
            StartCamera();
            TrackerManager.GetInstance().StartTracker(TrackerManager.TRACKER_TYPE_IMAGE);
        }
    }

    void OnDestroy()
    {
        _trackableList.Clear();
        TrackerManager.GetInstance().SetTrackingOption(TrackerManager.TrackingOption.NORMAL_TRACKING);
        TrackerManager.GetInstance().StopTracker();
        TrackerManager.GetInstance().DestroyTracker();
        StopCamera();
    }
}
