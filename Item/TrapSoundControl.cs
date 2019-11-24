using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSoundControl : MonoBehaviour
{
    private AudioSource se;
    void Start()
    {
        se=gameObject.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player"){
            se.Play();
        }        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player"){
            se.Stop();
        }  
    }
}
