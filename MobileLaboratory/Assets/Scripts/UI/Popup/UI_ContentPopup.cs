using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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

    Content_Base content;
    Content_Experiment experimentContent;
    Color highlightColor = Color.yellow;

    protected override void Init()
    {
        content = FindObjectOfType<Content_Base>();

        bool isExperimentContent = content is Content_Experiment;
        transform.Find("Experiment").gameObject.SetActive(isExperimentContent);
        BindButton(typeof(Buttons), isExperimentContent);
        BindText(typeof(Texts), isExperimentContent);
        BindObject(typeof(Objects), isExperimentContent);

        if (isExperimentContent)
        {
            experimentContent = content.GetComponent<Content_Experiment>();
            progressBtns = new Button[experimentContent.maxProgress];
            for(int i = 0; i < experimentContent.maxProgress; i++)
            {
                Button progressBtn = Managers.Resource.Instantiate("UI/Item/ProgressButton", GetObject((int)Objects.ProgressBtnGroup).transform).GetComponent<Button>();
                int tmp = i;
                progressBtn.onClick.AddListener(delegate { experimentContent.Progress = tmp; });
                progressBtns[i] = progressBtn;
            }
        }
        
        GetButton((int)Buttons.CloseButton).onClick.AddListener(OnClickedCloseButton);

        base.Init();
    }

    public void SetNextProgressButton(bool isActive)
    {
        if(isActive == false)
        {
            GetButton((int)Buttons.NextProgressButton).onClick.RemoveAllListeners();
        }
        else
        {
            GetButton((int)Buttons.NextProgressButton).onClick.AddListener(
                delegate
                {
                    experimentContent.Progress++;
                });
        }
        
        GetButton((int)Buttons.NextProgressButton).gameObject.SetActive(isActive);
        
    }

    public void HighlightProgressButton(int index)
    {
        progressBtns[index].image.color = highlightColor;
    }

    public void DoPlayStartTextTweening()
    {        

        GetText((int)Texts.StartText).transform.DOShakePosition(0.3f, Vector3.up * 20, 2).OnComplete
        (
            delegate
            {
                GetText((int)Texts.StartText).transform.DOScaleY(0, 0.2f);
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
