using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float power=0.01f;
    public float duration=0.1f;
    public float slowDownAmount=1f;
    [SerializeField]private bool isShake;
    private float initialDuration;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos=transform.localPosition;
        initialDuration=duration;
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
    }
    void Shake(){
        if(isShake){
            if(duration>0){
                transform.localPosition=pos+new Vector3(Random.Range(-10*power, 10*power), 0, Random.Range(-10*power, 10*power));
                //transform.localPosition=pos+Random.insideUnitSphere*power;
                duration-=Time.deltaTime*slowDownAmount;
            }else
            {
                isShake=false;
                duration=initialDuration;
                transform.localPosition=pos;
            }
        }
    }
    public bool IsShake{get {return isShake;}set{isShake=value;}}
}
