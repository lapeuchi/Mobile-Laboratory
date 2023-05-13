using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class TitleScene : BaseScene
{
    UI_TitleScene sceneUI;

    protected override void Init()
    {
        base.Init();
        sceneUI = Managers.UI.ShowSceneUI<UI_TitleScene>();

        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync("MainScene");
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            if (op.progress >= 0.9f)
            {
                break;
            }
            yield return null;
        }
        
        Debug.Log("Successfully loaded");
        
        yield return new WaitForSeconds(1f);
        sceneUI.FadeIn();
        
        yield return new WaitUntil(()=> sceneUI.isFadeFinished);
        op.allowSceneActivation = true;
    }
}
