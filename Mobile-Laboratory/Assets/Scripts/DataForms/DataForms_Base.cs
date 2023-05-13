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
                booksObject[i].GetField("revision").stringValue,
                booksObject[i].GetField("titleImagePath").stringValue
            );
            books.Add(book);
        }
    }

    public class Book
    {
        public string code;
        public string name;
        public string grade;
        public string subject;
        public string publisher;
        public string revision;
        public Sprite titleImage;
        
        public Book(string code, string name, string grade, string subject, string publisher, string revision, string titleImagePath)
        {
            this.code = code;
            this.name = name;
            this.grade = grade;
            this.subject = subject;
            this.publisher = publisher;
            this.revision = revision;
            this.titleImage = Managers.Resource.Load<Sprite>(titleImagePath);
        }
    }
}

    public class Data_BookCode : IDataContent
    {
        public List<BookOptionForm> grades = new List<BookOptionForm>();
        public List<BookOptionForm> publishers = new List<BookOptionForm>();
        public List<BookOptionForm> subjects = new List<BookOptionForm>();

        public Data_BookCode()
        {
            LoadData();
        }
        
        public void LoadData()
        {
            JSONObject jsonObject = Managers.Data.LoadJsonObject(Define.DataCodes.BookCode);
            List<JSONObject> gradeObjects = jsonObject.GetField("grade").list;
            List<JSONObject> publishersObjects = jsonObject.GetField("publisher").list;
            List<JSONObject> subjectsObjects = jsonObject.GetField("subject").list;
            
            for (int i = 0; i < gradeObjects.Count; i++)
            {
                BookOptionForm frm = new BookOptionForm(
                    gradeObjects[i].GetField("code").stringValue,
                    gradeObjects[i].GetField("name").stringValue
                );
                
                grades.Add(frm);
            }

            for (int i = 0; i < publishersObjects.Count; i++)
            {
                BookOptionForm frm = new BookOptionForm(
                    publishersObjects[i].GetField("code").stringValue,
                    publishersObjects[i].GetField("name").stringValue
                );
                
                publishers.Add(frm);
            }

            for (int i = 0; i < subjectsObjects.Count; i++)
            {
                BookOptionForm frm = new BookOptionForm(
                    subjectsObjects[i].GetField("code").stringValue,
                    subjectsObjects[i].GetField("name").stringValue
                );
                
                subjects.Add(frm);
            }
        }

        public struct BookOptionForm
        {
            public string code;
            public string name;

            public BookOptionForm(string code, string name)
            {
                this.code = code;
                this.name = name;
            }
        }
    }