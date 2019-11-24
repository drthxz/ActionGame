using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public Transform self;
    public float damage;
    private float tempDamage;
    public bool isAttack{get ;protected set;}
    private new Collider collider;
    //public new AudioSource audio;
    //public List<AudioClip> SE=new List<AudioClip>();
    protected SoundControl soundControl = new SoundControl();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        collider=GetComponent<Collider>();
        
        //audio=self.gameObject.GetComponentInParent<AudioSource>();
        GameObject[] damageCollider=GameObject.FindGameObjectsWithTag("damage");
        GameObject[] background=GameObject.FindGameObjectsWithTag("Background");
        foreach(var da in damageCollider){
            if(da !=transform.gameObject){
                Physics.IgnoreCollision(collider,da.GetComponent<Collider>());
            }
        }
        foreach(var go in background){
            if(go !=transform.gameObject){
                Physics.IgnoreCollision(collider,go.GetComponent<Collider>());
            }
        }
        Physics.IgnoreCollision(collider,self.GetComponent<Collider>());
        tempDamage=damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual void Attack(bool attack){
        isAttack=attack;
        if(attack){
            damage=tempDamage;
        }
        
    }
    public virtual void HeavyAttack(bool heavy){
        isAttack=heavy;
        damage=tempDamage*1.5f;
    }
    public virtual void Kick(bool kick){
        isAttack=kick;
        damage=tempDamage*2.5f;
    }
}
