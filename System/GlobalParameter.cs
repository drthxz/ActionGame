using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GlobalParameter
{
    class Global{
    private static Global instance;
    
    public static Global GetInstance(){
        if(instance==null){
            instance=new Global();
        }
        return instance;
    }
    private PlayerControl _player;

    public int levelname{get;set;}
    public bool change { get; set; }
    public bool gameStop { get; set; }
    public GameManager gameManager=GameObject.Find("GameManage").GetComponent<GameManager>();
    public PlayerControl GetPlayer{get{return _player=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();}}
    public static GameObject DamagePopup = Resources.Load<GameObject>("Prefab/DamagePopup");
    public static int coinCound=0;

    }
}
