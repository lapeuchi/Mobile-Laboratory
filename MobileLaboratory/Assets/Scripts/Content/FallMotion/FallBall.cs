using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBall : MonoBehaviour
{
    [SerializeField] Define.Fall _type;
    Rigidbody _rigid;
    Transform _root;
    Material _mat;
    
    public bool IsArrive { get { return _isArrive; } }
    
    float _power = 5f;
    float _currentTime = 0;
    float _exitTime = 0.25f;
    int _count = 0;
    int _maxCount = 6;
    bool _isFire = false;
    bool _isArrive = false;

    public void SetInfo(Define.Fall type, Transform root)
    {
        _type = type;
        _root = root;
    }

    private void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _rigid.isKinematic = true;

        //OnFire();
        _currentTime = _exitTime;
        _mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (_count == _maxCount || _isFire == false)
            return;

        _currentTime += Time.deltaTime;

        if(_currentTime >= _exitTime)
        {
            _count++;
            
            if (_count == _maxCount)
            {
                Destroy(_rigid);
                enabled = false;
                _isArrive = true;
            }

            GameObject go = Managers.Resource.Instantiate("Contents/Content_FallMotion/AfterImage");
            go.transform.position = transform.position;
            go.transform.parent = _root;

            Renderer renderer = go.GetComponent<Renderer>();
            Color color = _mat.color;
            color.a = 0.5f;
            renderer.material.color = color;

            _currentTime = 0;
        }
    }
    
    public void OnFire()
    {
        _rigid.isKinematic = false;
        _isFire = true;
        if (_type == Define.Fall.FreeDrop)
            _rigid.AddForce(Vector3.right * _power, ForceMode.Impulse);
    }
}
