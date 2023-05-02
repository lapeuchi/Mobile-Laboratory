using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BookSelect_Popup : UI_Popup
{
    RectTransform centerCheckerRectTr;
    TMP_Text selectedBookText;
    
    TMP_Dropdown gradeDropdown;

    List<Image> bookImages = new List<Image>();

    string bookPrefabPath = "BookImage";
    string bookLabelPrefabPath = "BookLabelText";
    string bookImgDirPath = "Sprites/TextbookImages";
    string shelfPath = "Shelf";

    Transform content;
    Transform bookParent;
    Transform labelParent;
    Transform textParent;

    float contentSize = 300f;

    Data_Books bookData;
    
    enum Texts
    {
        //SelectedBookText
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

        //selectedBookText = GetText((int)Texts.SelectedBookText);
        content = Util.FindChild<Transform>(gameObject, "BookContent", true);
        gradeDropdown = GetDropdown((int)Dropdowns.GradeDropdown);

        gradeDropdown.onValueChanged.AddListener(delegate{D();});

        bookData = new Data_Books();
        
        
        LoadBooks();
    }

    void LoadBooks()
    {
        for(int i = 0; i < bookData.books.Count; i++)
        {
            // if(i % 4 == 0)
            // {
            //     GameObject shelf = Managers.Resource.Instantiate(shelfPath, content);
            //     bookParent = Util.FindChild<Transform>(shelf, "Books");
            //     labelParent = Util.FindChild<Transform>(shelf, "Labels");
            //     content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, content.GetComponent<RectTransform>().sizeDelta.y + contentSize);
            // }
            
            GameObject bookObj = Managers.Resource.Instantiate(bookPrefabPath, bookParent);
            Image img = bookObj.GetComponent<Image>();
            img.sprite = Managers.Resource.Load<Sprite>($"{bookImgDirPath}/{bookData.books[i].code}");

            TMP_Text label = Managers.Resource.Instantiate(bookLabelPrefabPath, labelParent).GetComponent<TMP_Text>();
            label.text = $"{bookData.books[i].name}\n{bookData.books[i].publisher}";
            
            bookObj.SetActive(false);
        }
    }
    
    void D() 
    {

        return;
    }
}