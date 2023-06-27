using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBall : MonoBehaviour
{
    [SerializeField] Define.Fall _type;
    Rigidbody _rigid;
    float _power = 5f;

    public void SetInfo(Define.Fall type)
    {
        _type = type;
    }

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _rigid.isKinematic = false;

        switch(_type)
        {
            case Define.Fall.FreeFall:
                OnFreeFall();
                break;
            case Define.Fall.VerticalFall:
                OnVerticalFall();
                break;
        }
    }

    float _t = 0;

    private void Update()
    {
        _t += Time.deltaTime;

        if(transform.position.y <= -12f)
        {
            _rigid.isKinematic = true;
            Debug.Log($"Stop {_t}");
            enabled = false;
        }
    }

    void OnFreeFall()
    {
        _rigid.AddForce(Vector3.down * _power, ForceMode.Impulse);
    }

    void OnVerticalFall()
    {
        _rigid.AddForce((Vector3.down + Vector3.right) * _power, ForceMode.Impulse);
    }
}
