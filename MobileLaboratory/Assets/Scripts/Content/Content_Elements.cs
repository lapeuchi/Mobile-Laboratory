using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Content_Elements : Content_Base
{   
    public Data_Element elementData;
    public const int MaxElement = 118; // 118
    private int focusElementIndex;
    MeshRenderer cubeMeshRenderer;
    string materialPath = "Materials/Contents/Content_Elements/CubeMat_";

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
        Transform cubeSet = transform.Find("Content");
        cubeSet.SetParent(transform);
        
        cubeSet.position = GameObject.Find("Lab").transform.position + new Vector3(0, 2, -0.2f);
        
        elementData = new Data_Element();
        FocusElementIndex = 0;
        Managers.UI.ShowPopupUI<UI_PeriodicTablePopup>().transform.SetParent(transform);
        
        cubeMeshRenderer = GameObject.Find("ElementCube").GetComponent<MeshRenderer>();

        cubeMeshRenderer.material = Managers.Resource.Load<Material>($"{materialPath}{FocusElementIndex + 1}");
        base.Init();
    }

    public override void Clear()
    {
        base.Clear();
    }

}
