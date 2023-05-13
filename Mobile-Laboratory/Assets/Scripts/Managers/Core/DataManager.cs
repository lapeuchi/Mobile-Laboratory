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
    public string language;    
    public string subject;
    public string publisher;
    public string grade;
    public Data_Books.Book book;

    public UserData()
    {
        GetData();
    }

    private void GetData()
    {
        language = PlayerPrefs.GetString("language", "Kor");
        subject = PlayerPrefs.GetString("subject", "All");
        publisher = PlayerPrefs.GetString("grade", "All");
        grade = PlayerPrefs.GetString("publisher", "All");
    }

    private void SetData()
    {
        PlayerPrefs.SetString("language", language);
        PlayerPrefs.SetString("subject", subject);
        PlayerPrefs.SetString("publisher", publisher);
        PlayerPrefs.SetString("grade", grade);
    }
}