using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BookSelect_Popup : UI_Popup
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
    string shelfPath = "Shelf";

    [Header("Resources Transforms")]
    Transform content;

    float originContentSize = 1150;
    float contentSize = 300f;
    int bookPerShelf = 4;
    string bookContentName = "Books";
    string labelContentName = "Labels";

    int bookIndex = 0;
    
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
                bookLabelTexts[itemIndex].text = $"{bookData.books[i].name}\n{bookData.books[i].publisher}";

                Button bookBtn = bookImages[itemIndex].GetComponent<Button>();
                bookBtn.onClick.RemoveAllListeners();
                int tempIter = i; 
                RectTransform rectTransform = bookImages[itemIndex].GetComponent<RectTransform>();
                bookBtn.onClick.AddListener(()=> BookFocus(tempIter, rectTransform));

                // 선택된 책 포커스            
                if (Managers.Data.userData.BookIndex == i)
                {
                    BookFocus(tempIter, rectTransform);
                }
                
                searchSuccessCnt++;
            }
        }

        // 마지막 선반에서 정보가 없는 책 숨기기
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
        bookFocusImage.gameObject.SetActive(false);
        for (int i = 0; i < shelfes.Count; i++)
        {
            shelfes[i].SetActive(false);
        }
    }

    void BookFocus(int index, RectTransform rectTransform)
    {
        bookFocusImage.gameObject.SetActive(true);
        bookFocusImage.transform.SetAsLastSibling();
        bookIndex = index;
        selectedBookText.text = $"{bookData.books[index].name}";
        bookFocusImage.GetComponent<RectTransform>().position = rectTransform.position;
    }

    void OnClickedSelectButton()
    {
        Managers.Data.userData.SetBookSelectData(gradeDropdown.value, bookIndex, bookData.books[bookIndex]);
        MainScene.ui_Tracking.SetSelectImage();
        ClosePopupUI();
    }
}