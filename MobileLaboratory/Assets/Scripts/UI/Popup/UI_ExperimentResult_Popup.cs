using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Text.RegularExpressions;
using System;

public class UI_ExperimentResult_Popup : UI_Popup
{
    Content_Experiment experimentContent;
    RectTransform contentRect;

    GameObject titlePrefab;
    GameObject answerPrefab;

    float IntantiatePos;
    float contentExpand = 100;
    float curPoint;
    float titleExpand = 20;
    float answerExpand = 10;
    float keyExpend = 5;

    int curPage;

    enum Texts {TitleText}
    enum Buttons 
    {
        ResultButton,
        KeypointButton,
        CloseButton
    }
    
    DataForms_Result data;
    List<TMP_Text> dummyTexts = new List<TMP_Text>();

    protected override void Init()
    {
        BindText(typeof(Texts), true);
        BindButton(typeof(Buttons), true);
        GetText((int)Texts.TitleText).text = null;
        GetButton((int)Buttons.CloseButton).onClick.AddListener(OnClickedCloseButton);
        experimentContent = FindObjectOfType<Content_Experiment>().GetComponent<Content_Experiment>();
        data = new DataForms_Result(experimentContent.ResultCode);
        contentRect = GameObject.Find("ContentTransform").GetComponent<RectTransform>();
        
        titlePrefab = Managers.Resource.Load<GameObject>("Prefabs/UI/Item/TitleText");
        answerPrefab = Managers.Resource.Load<GameObject>("Prefabs/UI/Item/AnswerText");
        GetButton((int)Buttons.ResultButton).onClick.AddListener(OnClickedResultButton);
        GetButton((int)Buttons.KeypointButton).onClick.AddListener(OnClickedKeypointButton);
        base.Init();
    }

    void Start() 
    {
        OnClickedResultButton();
    }

    void ShowResult(Buttons btnType)
    {
        for(int i = 0; i < dummyTexts.Count; i++)
        {
            Destroy(dummyTexts[i]);
            dummyTexts.Remove(dummyTexts[i]);
            i--;
        }
        
        dummyTexts.Clear();
        curPoint = 0;
        
        int loopLock = btnType == Buttons.KeypointButton ?
            data.frm.keypoint.Count : data.frm.result.Count;

        for (int i = 0; i < loopLock; i++)
        {
            // title
            TMP_Text titleText = Instantiate(titlePrefab, contentRect.transform).GetComponent<TMP_Text>();
            if(i != 0)
            {
                curPoint -= titleExpand;
            }
            titleText.GetComponent<RectTransform>().localPosition = new Vector3(-50,curPoint, 0);
            titleText.color = new Color(0,0,0,0);
            curPoint -= titleExpand + titleText.GetComponent<RectTransform>().sizeDelta.y;
            dummyTexts.Add(titleText);
            if(btnType == Buttons.KeypointButton)
                titleText.text = data.frm.keypoint[i].title;
            else
                titleText.text = data.frm.result[i].title;
            titleText.DOFade(1.0f, 1f);
            titleText.transform.DOLocalMoveX(0, 1f);

            // answer
            string[] split;
            if(btnType == Buttons.KeypointButton)
                split = data.frm.keypoint[i].answer.Split(new string[] {"\n"}, StringSplitOptions.None);
            else
                split = data.frm.result[i].answer.Split(new string[] {"\n"}, StringSplitOptions.None);
          
            for(int j = 0; j < split.Length; j++)
            {
                TMP_Text answerText = Instantiate(answerPrefab, contentRect.transform).GetComponent<TMP_Text>();
                answerText.GetComponent<RectTransform>().localPosition = new Vector3(-100, curPoint, 0);
                titleText.color = new Color(0,0,0,0);
                curPoint -= answerExpand + answerText.GetComponent<RectTransform>().sizeDelta.y;
                answerText.text = split[j];
                dummyTexts.Add(answerText);
                answerText.DOFade(1.0f, 2.0f);
                answerText.transform.DOLocalMoveX(20f, 2.0f);
            }

            // keypoints p

            if(btnType == Buttons.KeypointButton)
            {
                TMP_Text keyText = Instantiate(answerPrefab, contentRect.transform).GetComponent<TMP_Text>();

                keyText.GetComponent<RectTransform>().localPosition = new Vector3(-50, curPoint, 0);
                keyText.color = new Color(0,0,0,0);
                curPoint -= keyExpend + keyText.GetComponent<RectTransform>().sizeDelta.y;
                dummyTexts.Add(keyText);
                keyText.text = data.frm.keypoint[i].p;
                keyText.DOFade(1.0f, 2.5f);
                keyText.transform.DOLocalMoveX(30, 2.5f);
            }

            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, -curPoint + 100);
            
        }
    }

    void OnClickedResultButton()
    {
        GetText((int)Texts.TitleText).DOText("실험 결과", 1.0f, false);
        ShowResult(Buttons.ResultButton);
        GetButton((int)Buttons.ResultButton).image.color = Color.yellow;
        GetButton((int)Buttons.KeypointButton).image.color = Color.white;
    }

    void OnClickedKeypointButton()
    {
        GetText((int)Texts.TitleText).DOText("실험 정리", 1.0f, false);
        ShowResult(Buttons.KeypointButton);
        GetButton((int)Buttons.KeypointButton).image.color = Color.yellow;
        GetButton((int)Buttons.ResultButton).image.color = Color.white;
    }
    void OnClickedCloseButton()
    {
        ClosePopupUI();
        experimentContent.Clear();
        
    }
}
