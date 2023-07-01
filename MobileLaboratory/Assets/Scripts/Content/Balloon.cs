using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public Rigidbody rigid;
    Content_DropBalloon content;
    
    public float i;
    public float p;
    public bool isChecked;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        content = GetComponentInParent<Content_DropBalloon>();
        rigid.isKinematic = true;
        isChecked = false;
        content.SetBalloonHeight(false);
        content.SetBalloonMass(false);
        
        p = 0;
        i = 0;

    }  
    void Update()
    {
        float pt = rigid.mass * rigid.velocity.magnitude;
        if (pt > p && isChecked == false)
        {   
            Debug.Log(pt);
            p = pt;
        } 
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(isChecked) return;
        isChecked = true;
        i = other.impulse.magnitude;

        if(content.cusion.activeSelf == false)
            i += i/3;
        

        content.IsComplete = true;

        Debug.Log(i);
        Debug.Log(p);
        if(i >= 12)
        {
            gameObject.SetActive(false);
        }
    }
}
