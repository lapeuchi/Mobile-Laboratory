using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_TitleScene : UI_Scene
{
    public bool isFadeFinished = false; 
    enum Images
    {
        FadeImage
    }

    protected override void Init()
    {
        base.Init();
        BindImage(typeof(Images), true);
    }


    public void FadeIn()
    {
        DOTweenAnimation img = GetImage((int)Images.FadeImage).GetComponent<DOTweenAnimation>();
        img.onComplete.AddListener(()=> isFadeFinished = true);
        img.DOPlay();        
    }
}
