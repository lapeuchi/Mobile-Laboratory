using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content_Elements : Content_Base
{   
    public Data_Element elementData;
    public const int MaxElement = 118; // 118

    private int focusElementIndex;
    public int FocusElementIndex
    {
        get { return focusElementIndex; }
        set
        {
            if (value < 0) focusElementIndex = MaxElement - 1;
            else if (value >= MaxElement) focusElementIndex = 0;
            else focusElementIndex = value;    
        }
    }

    protected override void Init()
    {
        elementData = new Data_Element();
        FocusElementIndex = 0;

        Managers.UI.ShowPopupUI<UI_PeriodicTablePopup>().transform.SetParent(transform);
        
        base.Init();
    }

    public override void Clear()
    {
        base.Clear();
    }

}
