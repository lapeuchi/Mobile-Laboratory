using Defective.JSON;
using UnityEngine;
using System.Collections.Generic;
using Data;

public class Data_Element : IDataContent
{
    public List<ElementForm> elements = new List<ElementForm>();
    
    public Data_Element()
    {
        LoadData();
    }

    public void LoadData()
    {
        JSONObject jsonObj = Managers.Data.LoadJsonObject(Define.DataCodes.Elements);
        List<JSONObject> elementObjects = jsonObj.GetField("elements").list;
        
        for(int i = 0; i < elementObjects.Count; i++)
        {
            ElementForm element = new ElementForm
            (
                elementObjects[i].GetField("code").stringValue,
                elementObjects[i].GetField("number").stringValue,
                elementObjects[i].GetField("name").stringValue,
                elementObjects[i].GetField("group").stringValue,
                elementObjects[i].GetField("period").stringValue,
                elementObjects[i].GetField("metal").stringValue,
                elementObjects[i].GetField("description").stringValue
            );
            elements.Add(element);
        }
    }

    public struct ElementForm
    {
        public string code;
        public string number;
        public string name;
        public string group;
        public string period;
        public string metal;
        public string description;
        
        public ElementForm(string code, string number, string name, string group, string period, string metal, string description)
        {
            this.code = code;
            this.number = number;
            this.name = name;
            this.group = group;
            this.period = period;
            this.metal = metal;
            this.description = description;
        }
    }
}