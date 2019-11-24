using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnim : MonoBehaviour
{

    float posY;
    float posX;
    // Start is called before the first frame update
    void Start()
    {
        posY = transform.position.y;
        posX = transform.position.x;
    }
    float xOffset;
    [SerializeField]
    float yOffset;
    float xSpeed;
    [SerializeField]
    float ySpeed;

    // Update is called once per frame
    void Update()
    {

       transform.position = new Vector3(transform.position.x, posY + Mathf.PingPong(Time.time * ySpeed, yOffset), transform.position.z);
        if(Input.anyKeyDown){
            Destroy(gameObject);
        }
    }
}
