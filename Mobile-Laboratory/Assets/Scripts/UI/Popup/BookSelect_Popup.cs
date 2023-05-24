using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Data;

public class BookSelect_Popup : UI_Popup
{
    [Header("Datas")]
    Data_Books bookData;
    Data_BookCode bookCode;

    [Header("UI Components")]
    RectTransform centerCheckerRectTr;
    TMP_Text selectedBookText;
    TMP_Dropdown gradeDropdown;
    List<GameObject> shelfes = new List<GameObject>();

    [Header("Resources Pathes")]
    string shelfPath = "Shelf";

    [Header("Resources Transforms")]
    Transform content;

    float originContentSize = 1150;
    float contentSize = 300f;
    int bookPerShelf = 4;
    string bookContentName = "Books";
    string labelContentName = "Labels";

    int curBook = 0;
    
    enum Texts
    {
        SelectedBookText
    }

    enum Buttons
    {
        
    }
    
    enum Dropdowns
    {
        GradeDropdown,
      //  SubjectDropdown,
      //  CompanyDropdown
    }

    protected override void Init()
    {
        BindButton(typeof(Buttons), true);
        BindText(typeof(Texts), true);
        BindDropdown(typeof(Dropdowns), true);

        bookData = new Data_Books();
        bookCode = new Data_BookCode();

        gradeDropdown = GetDropdown((int)Dropdowns.GradeDropdown);

        List<TMP_Dropdown.OptionData> gradeOptions = new List<TMP_Dropdown.OptionData>();
        for(int i = 0; i < bookCode.grades.Count; i++)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData($"{bookCode.grades[i].name}");
            gradeOptions.Add(option);
        }

        selectedBookText = GetText((int)Texts.SelectedBookText);

        gradeDropdown.ClearOptions();
        gradeDropdown.AddOptions(gradeOptions);

        content = Util.FindChild<Transform>(gameObject, "BookContent", true);

        gradeDropdown.onValueChanged.AddListener(delegate{SetShelf();});

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
        
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, originContentSize);
 
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
                bookImages[itemIndex].sprite = bookData.books[i].titleImage;
                bookLabelTexts[itemIndex].gameObject.SetActive(true);
                bookLabelTexts[itemIndex].text = $"{bookData.books[i].name}\n{bookData.books[i].publisher}";

                bookImages[itemIndex].gameObject.GetComponent<Button>().onClick.AddListener(delegate{ int r = i; BookFocus(r); });

                searchSuccessCnt++;
            }
        }

        if (itemIndex < bookPerShelf)
        {
            for(int i = itemIndex + 1; i < bookPerShelf; i++)
            {
                bookImages[i].gameObject.SetActive(false);
                bookLabelTexts[i].gameObject.SetActive(false);
            }
        }
    }

    void ClearShelf()
    {
        for (int i = 0; i < shelfes.Count; i++)
        {
            shelfes[i].SetActive(false);
        }
    }

    void BookFocus(int bookIndex)
    {
        curBook = bookIndex;
        selectedBookText.text = $"{bookData.books[curBook].name}";
    }
}