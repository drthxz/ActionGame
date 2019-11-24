using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using GlobalParameter;

public class PlayerControl : MonoBehaviour
{
    private State _state;
    public enum State {
        think,
        GetItem,
        InWater,
        GamePause,
    }
    private GameObject item;
    private Rigidbody rd;
    private float _speed=5f;
    private float _resetSpeed;
    Global global;
    private Animator _anim;
    Damage _damage;
    private SoundControl soundControl = new SoundControl();
    public PlayerBar playerBar;
    public bool death{get;set;}
    public List<GameObject> attackObject=new List<GameObject>();
    private CameraShake cameraShake;

    //[SerializeField]protected AudioSource sound;
    

    // Start is called before the first frame update
    void Start()
    {
        //base.Start();
        cameraShake=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        _resetSpeed=_speed;
        //animation 
        _anim=gameObject.GetComponent<Animator>();

        global=Global.GetInstance();
        _state=State.think;
        soundControl.Init(GetComponent<AudioSource>(), "Sound/Player0");
        //sound=GetComponent<AudioSource>();
        rd =GetComponentInChildren<Rigidbody>();
        _damage=GetComponentInChildren<Damage>();
        if(playerBar==null){
            playerBar=GetComponentInChildren<PlayerBar>();
        }
        foreach(var index in gameObject.GetComponentsInChildren<PlayerDamage>()){
            attackObject.Add(index.gameObject);
        }
        for(int i=0;i<attackObject.Count;i++){
            attackObject[i].SetActive(false);
        }
    }

    // Update is called once per frame
    public bool timerOn;
    float timer;
    void Update()
    {
        if(_state!=State.GamePause)
        //death
        if(death){
            if (!timerOn)
            {
                _anim.SetTrigger("Death");
                soundControl.PlayAudio(SoundControl.SoundType.death);
            }
            death=false;
            timerOn=true;
            
        }
        if(timerOn){
            timer+=Time.deltaTime;
            if(timer>3f){
                playerBar.hp=1;
                playerBar.skill = 1;
                timer=0;
                _anim.ResetTrigger("Attack");
                _anim.ResetTrigger("Hit");
                _anim.ResetTrigger("Kick");
                _anim.ResetTrigger("HeavyAttack");
                _anim.ResetTrigger("Jump");
                StartCoroutine(Wait(false, (value) => timerOn = value));
                //timerOn=false;
            }
        }
        if (!timerOn) {
            if (!global.gameStop)
            {
                Move();
                _anim.speed = 1;
            }
            else
            {
                _anim.SetBool("Run", false);
            }
        }
        // if(Input.GetKeyDown(KeyCode.P)){
        //     playerBar.hp=1;
        // }
    }
    public void Deathing(){
        _anim.speed = 0;
    }

    private bool _run;
    private bool _walk;
    private float t1;
    private float t2;
    private void Move()
    {
        MoveControl();
        Attack();
    }
    private bool hit;
    private bool hitting;
    public void Damage(bool value){
        if(!hit && !hitting){
            soundControl.PlayAudio(SoundControl.SoundType.hit);
            hit =value;
        }
    }
    public void HitEnd(){
        hitting=false;
    }
    private bool attack=false;
    private bool jump=false;
    public void AttackEnd(){
        
        for(int i=0;i<attackObject.Count;i++){
            attackObject[i].SetActive(false);
        }
        //attack change
        StartCoroutine(Wait(false, (value) => attack = value));
    }
    public void Jump(){
        rd.AddForce(transform.up * 200);
    }
    public void JumpEnd(){
        jump=false;
    }
    private void Attack()
    {

        if(hit){
            _anim.SetTrigger("Hit");
            cameraShake.IsShake=true;
            hit=false;
            hitting=true;
            return;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            
            //attack
            if(!attack){
                _anim.SetTrigger("Attack");
                attack=true;
                attackObject[1].SetActive(true);
            }
            
        }
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            //key
            if (Input.GetKey(KeyCode.E))
            {
                if (playerBar.skill > 0.3f)
                {
                    if (!attack)
                    {
                        _anim.SetTrigger("Kick");
                        gameObject.SendMessage("SkillBar", 0.3f, SendMessageOptions.DontRequireReceiver);
                        attack = true;
                        attackObject[0].SetActive(true);
                    }
                }
                
            }
            else
            {
                //skill
                if (playerBar.skill > 0.1f)
                {
                    if (!attack)
                    {
                        _anim.SetTrigger("HeavyAttack");
                        gameObject.SendMessage("SkillBar", 0.1f, SendMessageOptions.DontRequireReceiver);
                        attack = true;
                        attackObject[1].SetActive(true);
                    }
                }
                
                    
                // if (!_damage.isAttack)
                // {
                //     //skill
                //     _anim.SetTrigger("HeavyAttack");
                    
                //     //coillder.SkillBar=0.1f;
                //     //GameObject temp= Instantiate(_slash,transform);
                //    // Destroy(temp,1f);
                    
                // }
            }
        }
        if(Input.GetKeyDown(KeyCode.Joystick1Button1)){
            if (playerBar.skill > 0.3f)
            {
                if (!attack)
                {
                    _anim.SetTrigger("Kick");
                    gameObject.SendMessage("SkillBar", 0.3f, SendMessageOptions.DontRequireReceiver);
                    attackObject[0].SetActive(true);
                    attack = true;
                }
            }
            
        }
    }

    private void MoveControl()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if(transform.position.y>3f){
            //don't fly
            transform.position=new Vector3(transform.position.x,0,transform.position.z);
        }
        if((h != 0 || v != 0)){
            attack = false;
            for(int i=0;i<attackObject.Count;i++){
                attackObject[i].SetActive(false);
            }
            jump=false;
            _anim.SetBool("Run",true);
            _anim.ResetTrigger("Attack");
            _anim.ResetTrigger("Hit");
            _anim.ResetTrigger("Kick");
            _anim.ResetTrigger("HeavyAttack");
            _anim.ResetTrigger("Jump");
            hitting=false;
            Vector3 direction = new Vector3(h, 0, v).normalized;
            float y = Camera.main.transform.rotation.eulerAngles.y;
            direction = Quaternion.Euler(0, y, 0) * direction;
            Vector3 target = Vector3.Lerp(transform.forward, direction, 0.2f);
            transform.LookAt(transform.position + target);
            transform.Translate(target * Time.deltaTime * _speed, Space.World);
        }else
        {
            _anim.SetBool("Run",false);
        }
        
        
        //jump
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)){
            if(!jump){
                _anim.SetTrigger("Jump");
                jump=true;
            }
            
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        //look at
        if(other.tag=="Wolf"){
            transform.LookAt(other.transform);
            transform.rotation=Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
        }

        ICameraPos cameraPos = other.gameObject.GetComponent<ICameraPos>();
        if (other.tag=="Trigger"){
            //cameraPos change
            cameraPos.PosChange(this);

        }

        INextLevel nextLevel = other.gameObject.GetComponent<INextLevel>();
        if(other.name == "LevelOpen")
        {
            //nextLevel
            nextLevel.Next(this);
        }
        
        //Touch Item
        IItem temp = other.gameObject.GetComponent<IItem>();
        if (temp != null && attack)
        {
            if (other.tag == "Barrels") {
                Rigidbody rd = other.GetComponent<Rigidbody>();
                rd.isKinematic = false;
                rd.velocity = transform.forward;
                StartCoroutine(Wait(true, (value) => takeItem = value));
            }
            
            temp.TouchItem(this);
        }
        
    }
    bool takeItem;
    IEnumerator Wait(bool value,System.Action<bool> changeValue){
        yield return new WaitForSeconds(0.5f);
        changeValue(value);  
    }
    private void OnTriggerStay(Collider other)
    {
        IItem temp = other.gameObject.GetComponent<IItem>();
        if (temp != null)
        {
            //Get Item
            if (takeItem && (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Joystick1Button5)))
            {
                temp.GetItem(this);
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        //touch effect
        if(other.name=="Flames"){
            gameObject.SendMessageUpwards("Damage",true,SendMessageOptions.DontRequireReceiver);
            gameObject.SendMessage("HpBar",0.005f,SendMessageOptions.DontRequireReceiver);
            hit=true;
        }
        
    }

    public State GetState{get{return _state;}}
    public State SetState{set{ _state=value;}}
}
