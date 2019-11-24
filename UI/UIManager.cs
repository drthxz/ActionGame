using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalParameter;
using System;

public class UIManager : MonoBehaviour
{
    private GameObject[] enemyHp;
    private GameObject player;
    [SerializeField]private Slider playerBar;
    [SerializeField]private GameObject _characterImg;
    Global global;
    
    //PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        global=Global.GetInstance();
        enemyHp=GameObject.FindGameObjectsWithTag("UI");
        _characterImg.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player==null){
            player=GameObject.FindGameObjectWithTag("Player");
        }
        if(!global.change){
            if(playerBar.value<=0.05f){
                _characterImg.SetActive(true);
                Time.timeScale=0;
                global.change=true;
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button5)|| Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Joystick1Button4)){
                    Time.timeScale=1;
                    //playerBar.value=0.05f;
                    
                    _characterImg.SetActive(false);
                }
                
            }
        }

    }

    
}
