using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextArrow : MonoBehaviour
{
    float z;
    float x;
    // Start is called before the first frame update
    void Start()
    {
        z=transform.localPosition.z;
        x=transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.localRotation.eulerAngles.y==0f || transform.localRotation.eulerAngles.y==210f){
            transform.localPosition=new Vector3(x+Mathf.PingPong(Time.time, 2), transform.localPosition.y ,transform.localPosition.z);
            
        }else{
            transform.localPosition=new Vector3(transform.localPosition.x, transform.localPosition.y ,z+Mathf.PingPong(Time.time, 2));
        }
        
    }
}
