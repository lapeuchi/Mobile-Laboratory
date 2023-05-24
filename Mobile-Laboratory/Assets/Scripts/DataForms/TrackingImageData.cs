using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Defective.JSON;

namespace Data
{
    public struct TrackableImage
    {
        public string id;
        public string name;
        public int page;

        public TrackableImage(string id, string name, int page)
        {
            this.id = id;
            this.name = name;
            this.page = page;
        }
    }

    public class TrackableImageData : ILoader<string, TrackableImage>, IDataContent
    {
        public TrackableImageData()
        {
            LoadData();
        }

        public void LoadData()
        {
            
        }

        public Dictionary<string, TrackableImage> MakeDict()
        {
            JSONObject jsonObj = Managers.Data.LoadJsonObject(Define.DataCodes.TrackableImageData);
            List<JSONObject> jsonList = jsonObj.GetField("trackableImages").list;
            Dictionary<string, TrackableImage> trackableDict = new Dictionary<string, TrackableImage>();

            foreach (JSONObject data in jsonList)
            {
                TrackableImage trackableImage = new TrackableImage
                (
                    data.GetField("id").stringValue,
                    data.GetField("name").stringValue,
                    data.GetField("page").intValue
                );

                trackableDict.Add(trackableImage.id, trackableImage);
            }

            return trackableDict;
        }
    }
}