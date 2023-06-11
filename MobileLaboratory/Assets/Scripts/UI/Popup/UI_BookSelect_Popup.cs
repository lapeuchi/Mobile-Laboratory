using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Data;

public class UI_BookSelect_Popup : UI_Popup
{
    [Header("Datas")]
    Data_Books bookData;
    Data_BookCode bookCode;

    [Header("UI Components")]
    TMP_Text selectedBookText;
    TMP_Dropdown gradeDropdown;
    List<GameObject> shelfes = new List<GameObject>();
    Image bookFocusImage;
    TMP_Text SelectedBookText;
    Button selectButton;

    [Header("Resources Pathes")]
    string shelfPath = "BookShelf/Shelf";

    [Header("Object Name")]
    string bookContentName = "Books";
    string labelContentName = "Labels";

    [Header("Resources Transforms")]
    Transform content;

    float originContentSize = 800;
    float contentSize = 350f;
    int bookPerShelf = 4;

    int curBookIndex = 0;
    
    UI_MainScene ui_Tracking;

    enum Texts
    {
        SelectedBookText
    }

    enum Buttons
    {
        SelectButton
    }
    
    enum Dropdowns
    {
        GradeDropdown,
    }
    
    enum Images
    {
        BookFocusImage
    }

    protected override void Init()
    {
        base.Init();
        BindButton(typeof(Buttons), true);
        BindText(typeof(Texts), true);
        BindDropdown(typeof(Dropdowns), true);
        BindImage(typeof(Images), true);

        bookData = new Data_Books();
        bookCode = new Data_BookCode();

        gradeDropdown = GetDropdown((int)Dropdowns.GradeDropdown);

        bookFocusImage = GetImage((int)Images.BookFocusImage);
        bookFocusImage.gameObject.SetActive(false);
        
        selectedBookText = GetText((int)Texts.SelectedBookText);
        selectButton = GetButton((int)Buttons.SelectButton);

        content = Util.FindChild<Transform>(gameObject, "BookContent", true);
        selectedBookText = GetText((int)Texts.SelectedBookText);

        List<TMP_Dropdown.OptionData> gradeOptions = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i < bookCode.grades.Count; i++)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData($"{bookCode.grades[i].name}");
            gradeOptions.Add(option);
        }

        gradeDropdown.ClearOptions();
        gradeDropdown.AddOptions(gradeOptions);
        gradeDropdown.value = Managers.Data.userData.Grade;
        gradeDropdown.onValueChanged.AddListener(delegate{SetShelf();});

        selectButton.onClick.AddListener(()=>OnClickedSelectButton());

        curBookIndex = Managers.Data.userData.BookIndex;
        if (curBookIndex == -1)
        {
            selectedBookText.text = "교과서를 선택해 주세요.";
        }
        else
        {
            selectedBookText.text = $"{bookData.books[curBookIndex].name}";
        }   

        ui_Tracking = GameObject.FindObjectOfType<UI_MainScene>();
        
        LoadBooks();
    }

    void LoadBooks()
    {
        for(int i = 0; i < bookData.books.Count; i++)
        {
            if(i % bookPerShelf == 0)
            {
                GameObject shelf = Managers.Resource.Instantiate(shelfPath, content);
                shelfes.Add(shelf);
                shelf.SetActive(false);
            }
        }

        SetShelf();
    }

    void SetShelf()
    {
        ClearShelf();
        
        int shelfIndex = 0;
        int searchSuccessCnt = 0;
        Image[] bookImages = null;  
        TMP_Text[] bookLabelTexts = null;
        int itemIndex = 0;  
        
        RectTransform contentRectTransform = content.GetComponent<RectTransform>();
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, originContentSize);
        contentRectTransform.anchoredPosition = Vector2.zero;

        bookFocusImage.gameObject.SetActive(false);
        for (int i = 0; i < bookData.books.Count; i++)
        {
            bool gradeValueCheck = gradeDropdown.value == 0 || bookData.books[i].grade == bookCode.grades[gradeDropdown.value].code;
            // 검색 조건에 맞을 때
            if(gradeValueCheck)
            {   
                itemIndex = searchSuccessCnt % bookPerShelf;
                if(itemIndex == 0) 
                {
                    shelfes[shelfIndex].SetActive(true);

                    bookImages = Util.FindChild(shelfes[shelfIndex].gameObject, bookContentName, false, true).GetComponentsInChildren<Image>(true);                
                    bookLabelTexts = Util.FindChild(shelfes[shelfIndex].gameObject, labelContentName, false, true).GetComponentsInChildren<TMP_Text>(true);
                    
                    content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, content.GetComponent<RectTransform>().sizeDelta.y + contentSize);
                    shelfIndex++;
                }
                
                bookImages[itemIndex].gameObject.SetActive(true);
                bookImages[itemIndex].sprite = bookData.books[i].coverImage;
                bookLabelTexts[itemIndex].gameObject.SetActive(true);
                bookLabelTexts[itemIndex].text = SetLabel(i);
                
                Button bookBtn = bookImages[itemIndex].GetComponent<Button>();
                bookBtn.onClick.RemoveAllListeners();

                int searchSuccessCnt_Temp = searchSuccessCnt;
                int i_Temp = i;
                RectTransform rectTransform = bookImages[itemIndex].GetComponent<RectTransform>();
                bookBtn.onClick.AddListener(()=> StartCoroutine(BookFocus(i_Temp, searchSuccessCnt_Temp, rectTransform)));

                // 선택된 책 포커스
                if (curBookIndex == i)
                {
                    StartCoroutine(BookFocus(i_Temp, searchSuccessCnt_Temp, rectTransform));
                }
                
                searchSuccessCnt++;
            }
        }
        
        // 마지막 선반에서 정보가 없는 책 숨기기
        if (itemIndex < bookPerShelf)
        {
            for (int i = itemIndex + 1; i < bookPerShelf; i++)
            {
                bookImages[i].gameObject.SetActive(false);
                bookLabelTexts[i].gameObject.SetActive(false);
            }
        }
    }
    
    string SetLabel(int index)
    {
        return $"{bookData.books[index].name}\n{bookData.books[index].publisher}";
    }

    void ClearShelf()
    {
        bookFocusImage.gameObject.SetActive(false);
        for (int i = 0; i < shelfes.Count; i++)
        {
            shelfes[i].SetActive(false);
        }
    }
    
    IEnumerator BookFocus(int bookIndex, int focusIndex, RectTransform rectTransform)
    {
        bookFocusImage.gameObject.SetActive(true);
        bookFocusImage.transform.SetAsLastSibling();
        yield return null;
        curBookIndex = bookIndex;
        yield return null;        
        selectedBookText.text = $"{bookData.books[curBookIndex].name}";
        bookFocusImage.rectTransform.position = rectTransform.position;
    }

    void OnClickedSelectButton()
    {
        //Debug.Log("book Number: "+curBookIndex);
        Managers.Data.userData.SetBookSelectData(gradeDropdown.value, curBookIndex, bookData.books[curBookIndex]);
        ui_Tracking.SetSelectImage();
        ClosePopupUI();
    }
}