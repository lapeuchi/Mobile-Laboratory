using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;

public interface ILoader<T>
{
    List<T> MakeList();
}

public interface ILoader<TKey, TValue>
{
    Dictionary<TKey, TValue> MakeDict();

}

public class DataManager
{
    public UserData userData;
    public void Init()
    {
        userData = new UserData();
    }

    public JSONObject LoadJsonObject(Define.DataCodes code)
    {
        string jsonString = Managers.Resource.Load<TextAsset>($"Data/{code.ToString()}").ToString();
        JSONObject jsonObj = new JSONObject(jsonString);   
        return jsonObj;
    }
}

public class UserData
{   
    public int Grade { get; private set; }
    public int BookIndex { get; private set; }
    public Data_Books.Book Book { get; private set; }

    public float BgmVolume { get; private set; }
    public float VfxVolume { get; private set; }

    public UserData()
    {
        GetBookSelectData();
        GetOptionData();
    }
    
    public void GetBookSelectData()
    {
        Grade = PlayerPrefs.GetInt("grade", 0);
        BookIndex = PlayerPrefs.GetInt("bookIndex", -1);
        
        if (Book == null && BookIndex != -1)
        {
            Data_Books data_books = new Data_Books();
            Book = data_books.books[BookIndex];
        }

      //  Debug.Log(Grade);
      //  Debug.Log(BookIndex);
    }

    public void GetOptionData()
    {
        BgmVolume = PlayerPrefs.GetFloat("bgmVolume", 0.7f);
        VfxVolume = PlayerPrefs.GetFloat("vfxVolume", 1.0f);
    }

    // 교과서 선택 데이터
    public void SetBookSelectData(int grade, int bookIndex, Data_Books.Book book)
    {
        Grade = grade;
        BookIndex = bookIndex;
        this.Book = book;

        PlayerPrefs.SetInt("grade", grade);
        PlayerPrefs.SetInt("bookIndex", bookIndex);
        
    }
    
    public void SetOptionData()
    {
        PlayerPrefs.SetFloat("bgmVolume", BgmVolume);
        PlayerPrefs.SetFloat("vfxVolume", VfxVolume);

        
    }
}