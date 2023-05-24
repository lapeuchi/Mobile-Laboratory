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
    
    // JSON name
    public enum DataCodes
    {
        BookData,
        BookCode,
        Elements
    }

    public enum UIEvent
    {
        Click
    }
}