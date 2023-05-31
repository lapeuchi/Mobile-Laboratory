using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content_Elements : Content
{   
    public static Content_Elements instance;
    public Data_Element elementData;
    public int focusElementIndex;

    protected override void Init()
    {
        if(instance != null) Destroy(gameObject);
        else instance = this;

        elementData = new Data_Element();
        focusElementIndex = 0;

        Managers.UI.ShowSceneUI<UI_PeriodicTableScene>();
    }
}
