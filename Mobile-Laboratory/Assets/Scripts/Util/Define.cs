using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Sound
    {
        SFX,
        BGM,
        MaxCount
    }

    public enum Scene
    {
        Unknow,
        Menu,
        Game
    }
    
    public enum DataCodes
    {
        BookData,
        BookCode,
        TrackableImageData,

        Elements
    }

    public enum UIEvent
    {
        Click
    }
}