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
        BookSelectOption,
    }

    public enum UIEvent
    {
        Click
    }
}