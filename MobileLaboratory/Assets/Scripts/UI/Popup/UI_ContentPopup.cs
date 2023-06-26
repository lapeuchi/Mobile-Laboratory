using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UI_ContentPopup : UI_Popup
{
    enum Buttons
    {
        // normal items
        CloseButton,
        NextProgressButton
        // experiment items

    }
    Button[] progressBtns;
    enum Texts
    {
        // normal items

        // experiment items
        StartText
    }
    enum Objects
    {
        ProgressBtnGroup
    }
    enum Images
    {
        StartFadeImage
    }

    Content_Base content;
    Content_Experiment experimentContent;
    Color highlightColor = Color.yellow;

    Vector3 originProgressSize = new Vector3(1,1,1);
    Vector3 toProgressSize = new Vector3(1.2f,1.2f,1.2f);
    int lastIndex = -1;
    
    protected override void Init()
    {
        content = FindObjectOfType<Content_Base>();

        bool isExperimentContent = content is Content_Experiment;
        transform.Find("Experiment").gameObject.SetActive(isExperimentContent);
        BindButton(typeof(Buttons), isExperimentContent);
        BindText(typeof(Texts), isExperimentContent);
        BindObject(typeof(Objects), isExperimentContent);
        BindImage(typeof(Images), isExperimentContent);

        if (isExperimentContent)
        {
            experimentContent = content.GetComponent<Content_Experiment>();
            progressBtns = new Button[experimentContent.maxProgress];
            for(int i = 0; i < experimentContent.maxProgress; i++)
            {
                Button progressBtn = Managers.Resource.Instantiate("UI/Item/ProgressButton", GetObject((int)Objects.ProgressBtnGroup).transform).GetComponent<Button>();
                int tmp = i;
                progressBtn.onClick.AddListener(delegate 
                { 
                    experimentContent.Progress = tmp+1;
                    HighlightProgressButton(tmp);
                });
                progressBtn.GetComponentInChildren<TMP_Text>().text = $"{tmp+1}";
                progressBtns[i] = progressBtn; 
            }
            GetButton((int)Buttons.NextProgressButton).gameObject.SetActive(false);
        }
        
        GetButton((int)Buttons.CloseButton).onClick.AddListener(OnClickedCloseButton);

        base.Init();
    }

    // 다음 단계 이동 버튼 활성화
    public void ActiveNextProgressButton()
    {
        GetButton((int)Buttons.NextProgressButton).gameObject.SetActive(true);
        GetButton((int)Buttons.NextProgressButton).onClick.RemoveAllListeners();
        GetButton((int)Buttons.NextProgressButton).onClick.AddListener(
            delegate
            {
                ++experimentContent.Progress;
                GetButton((int)Buttons.NextProgressButton).gameObject.SetActive(false);
            });
        
        
    }

    public void HighlightProgressButton(int index)
    {
        if(lastIndex != -1) 
        {
            progressBtns[lastIndex].image.color = Color.white;
            progressBtns[lastIndex].image.transform.DOScale(originProgressSize, 0.2f);
        }
        
        progressBtns[index].image.color = highlightColor;
        progressBtns[index].image.transform.DOScale(toProgressSize, 0.2f);
        lastIndex = index;
    }

    public void DoPlayStartTextTweening()
    {   
        
        GetText((int)Texts.StartText).transform.DOShakePosition(0.3f, Vector3.up * 20, 2).OnComplete (
        delegate {
                GetText((int)Texts.StartText).transform.DOScaleY(0, 0.2f);
            }
        );
        GetImage((int)Images.StartFadeImage).DOFade(0, 0.5f).OnComplete (   
            delegate { 
                experimentContent.Progress = 1;
                Destroy(GetImage((int)Images.StartFadeImage));
            }
        );
    }

    void OnClickedCloseButton()
    {  
        if (content)
        {
            content.Clear();
        }        
        ClosePopupUI();
    }

}
