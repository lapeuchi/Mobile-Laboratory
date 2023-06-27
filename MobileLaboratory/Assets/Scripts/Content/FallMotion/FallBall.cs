using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBall : MonoBehaviour
{
    Define.Fall _type;

    public void SetInfo(Define.Fall type)
    {
        _type = type;
    }

    void Update()
    {
        switch(_type)
        {
            case Define.Fall.FreeFall:
                UpdateFreeFall();
                break;
            case Define.Fall.VerticalFall:
                UpdateVerticalFall();
                break;
        }
    }

    void UpdateFreeFall()
    {

    }

    void UpdateVerticalFall()
    {

    }
}
