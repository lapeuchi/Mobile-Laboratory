using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBalloon : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField] float mass;
    [SerializeField] float startHeight;
    [SerializeField] float sizeFactor = 1.2f;

    [SerializeField] float hp = 100;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        
    }

    void SetBalloon()
    {
        rigid.mass = mass;
        startHeight = 0.5f;

    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("ProjectileTarget"))
        {

        }
    }
}
