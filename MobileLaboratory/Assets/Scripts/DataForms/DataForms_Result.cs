using Defective.JSON;
using UnityEngine;
using System.Collections.Generic;
using Data;

public class DataForms_Result
{
    public ExperimentResultForm frm = new ExperimentResultForm();
    
    public DataForms_Result(Define.DataCodes code)
    {
        LoadData(code);
    }

    public void LoadData(Define.DataCodes code)
    {
        ExperimentResultForm frm = new ExperimentResultForm();
        
        JSONObject jsonObj = Managers.Data.LoadJsonObject(code);
        List<JSONObject> resultObj = jsonObj.GetField("result").list;
        List<JSONObject> keyObj = jsonObj.GetField("keypoints").list;
        
        List<ResultForm> resultForms = new List<ResultForm>();
        for(int i = 0; i < resultObj.Count; i++)
        {
            ResultForm resultForm = new ResultForm();
            resultForm.title = resultObj[i].GetField("title").stringValue;
            resultForm.answer = resultObj[i].GetField("answer").stringValue;
            resultForms.Add(resultForm);
        }
        List<KeypointForm> keypointForms = new List<KeypointForm>();
        for(int i = 0; i < keyObj.Count; i++)
        {
            KeypointForm keypointForm = new KeypointForm();
            keypointForm.title = keyObj[i].GetField("title").stringValue;
            keypointForm.answer = keyObj[i].GetField("answer").stringValue;
            keypointForm.p = keyObj[i].GetField("p").stringValue;
            keypointForms.Add(keypointForm);
        }
        frm.result = resultForms;
        frm.keypoint = keypointForms;
        frm.comment = jsonObj.GetField("comment").stringValue;
        this.frm = frm;
    }

    public struct ExperimentResultForm
    {
        public string comment;
        public List<ResultForm> result;
        public List<KeypointForm> keypoint;
    }

    public struct ResultForm
    {
        public string title;
        public string answer;
    }

    public struct KeypointForm
    {
        public string title;
        public string answer;
        public string p;
    }
}