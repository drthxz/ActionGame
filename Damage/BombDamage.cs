using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour
{
    private GameObject boom;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        boom=Resources.Load<GameObject>("Prefab/boom");
        //if()
        enemy=GameObject.FindGameObjectWithTag("Wolf");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other!=null){
            GameObject temp= Instantiate(boom,transform.position,transform.rotation);
            Destroy(temp,2f);
            Destroy(gameObject);
        }else{
            Destroy(gameObject,3f);
        }        
    }
}
