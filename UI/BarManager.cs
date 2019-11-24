using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BarManager : MonoBehaviour
{
    [SerializeField]protected GameObject bar;
    [SerializeField]protected Slider[] _bars;
    GameObject player;
    Animal self;
    protected PlayerControl pSelf;
    [SerializeField] protected float defense = 1f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        if(gameObject.tag=="Player"){
            bar=GameObject.Find("Canvas/PlayerBar");
            pSelf=GetComponentInParent<PlayerControl>();
        }else
        {
            self=GetComponentInParent<Animal>();
            bar=transform.Find("wolf_collider/Canvas/Bar").gameObject;
            bar.SetActive(false);
        }
        
        _bars=bar.gameObject.GetComponentsInChildren<Slider>();
        hp=_bars[0].value;
        PlayerPrefs.GetFloat("hp",hp);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _bars[0].value=Mathf.Lerp(_bars[0].value,hp,Time.deltaTime);
        if(player==null){
            player=GameObject.FindGameObjectWithTag("Player");
        }
    }
    public void enemyBar(bool value){
        //bar.SetActive(value);
        bar.transform.LookAt(player.transform.position+Vector3.up*2);
    }
    public float hp{get;set;}
    public virtual void HpBar(float damage){
        hp-=damage/defense;
        if(hp<0){
            self.death=true;
            //StartCoroutine(cameraShake.Shake());
            
            return;
        }
    }

}
