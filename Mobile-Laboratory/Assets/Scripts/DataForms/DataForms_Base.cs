using Defective.JSON;
using UnityEngine;
using System.Collections.Generic;

public interface IDataContent
{
    void LoadData();
}

public class Data_Books : IDataContent
{
    public List<Book> books = new List<Book>();
    
    public Data_Books()
    {
        LoadData();
    }

    public void LoadData()
    {
        JSONObject jsonObj = Managers.Data.LoadJsonObject(Define.DataCodes.BookData);
        List<JSONObject> booksObject = jsonObj.GetField("books").list;
        
        for(int i = 0; i < booksObject.Count; i++)
        {
            Book book = new Book
            (
                booksObject[i].GetField("code").stringValue,
                booksObject[i].GetField("name").stringValue,
                booksObject[i].GetField("grade").stringValue,
                booksObject[i].GetField("subject").stringValue,
                booksObject[i].GetField("publisher").stringValue,
                booksObject[i].GetField("revision").stringValue
            );
            books.Add(book);
        }
    }
}

public struct Book
{
    public string code;
    public string name;
    public string grade;
    public string subject;
    public string publisher;
    public string revision;

    public Book(string code, string name, string grade, string subject, string publisher, string revision)
    {
        this.code = code;
        this.name = name;
        this.grade = grade;
        this.subject = subject;
        this.publisher = publisher;
        this.revision = revision;
    }

    public class Data_BookCode : IDataContent
    {
        public Dictionary<string, string> grades = new Dictionary<string, string>();
        public Dictionary<string, string> publishers = new Dictionary<string, string>();
        public Dictionary<string, string> subjects = new Dictionary<string, string>();

        public Data_BookCode()
        {
            LoadData();
        }
        
        public void LoadData()
        {
            JSONObject jsonObject = Managers.Data.LoadJsonObject(Define.DataCodes.BookSelectOption);

            List<JSONObject> gradeObjects = jsonObject.GetField("grade").list;
            List<JSONObject> publishersObjects = jsonObject.GetField("publisher").list;
            List<JSONObject> subjectsObjects = jsonObject.GetField("subject").list;

            for (int i = 0; i < gradeObjects.Count; i++)
            {
                grades.Add
                (
                    gradeObjects[i].GetField("code").stringValue,
                    gradeObjects[i].GetField("name_kor").stringValue
                );
            }

            for (int i = 0; i < publishersObjects.Count; i++)
            {
                publishers.Add
                (
                    publishersObjects[i].GetField("code").stringValue,
                    publishersObjects[i].GetField("name_kor").stringValue
                );
            }

            for (int i = 0; i < subjectsObjects.Count; i++)
            {
                subjects.Add
                (
                    subjectsObjects[i].GetField("code").stringValue,
                    subjectsObjects[i].GetField("name_kor").stringValue
                );
            }
        }
        
        public void ChangeLanguage()
        {
            
        }

    }
}