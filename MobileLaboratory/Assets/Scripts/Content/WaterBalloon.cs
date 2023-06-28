using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBalloon : MonoBehaviour
{
    public bool isDroped = false;
    Rigidbody rigid;
    [SerializeField] float mass;
    [SerializeField] float startHeight;
    [SerializeField] float sizeFactor = 1.2f;

    [SerializeField] float hp = 100;

    float maxSpeed = 0.0f;
    float P; // 운동량
    float I; // 충격량

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        isDroped = false;
    }

    void SetBalloon()
    {
        rigid.mass = mass;
        startHeight = 0.5f;

    }

    void Update()
    {
        maxSpeed = rigid.velocity.y;
        if(rigid.velocity.y >= maxSpeed)
        {
            maxSpeed = rigid.velocity.y;
        }

    }

    private void OnCollisionEnter(Collision other) 
    {
        if(isDroped) return;

        if(other.gameObject.CompareTag("ProjectileTarget"))
        {
            P = rigid.mass * maxSpeed;
            I = other.impulse.y;

            isDroped = true;

        }
    }
}
