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
        public string contentPath;

        public TrackableImage(string id, string name, int page, string contentPath)
        {
            this.id = id;
            this.name = name;
            this.page = page;
            this.contentPath = contentPath;
        }
    }

    public class TrackableImageData : ILoader<string, TrackableImage>
    {
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
                    data.GetField("page").intValue,
                    data.GetField("contentPath").stringValue
                );

                trackableDict.Add(trackableImage.id, trackableImage);
            }

            return trackableDict;
        }
    }
}