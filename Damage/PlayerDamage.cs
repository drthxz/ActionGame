using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : Damage
{
    ParticleSystem effect;
    private CameraShake cameraShake;
    private Camera m_Camera;
    private int _damageText;
    public PlayerBar playerBar;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        m_Camera = Camera.main;
        soundControl.Init(self.gameObject.GetComponentInParent<AudioSource>(), "Sound/Player0");
        //for(int i=1;i<5;i++){
        //    SE.Add(Resources.Load<AudioClip>("Sound/Player0"+i));
        //}
        effect =gameObject.GetComponentInChildren<ParticleSystem>();
        cameraShake=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        playerBar=gameObject.GetComponentInParent<PlayerBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(isAttack){
            //audio.PlayOneShot(attackSE);
            if(other.gameObject.tag=="Wolf"|| other.gameObject.tag=="Boar"){
                bool isCriticalHit = Random.Range(0, 100) < 30;
                DamagePopup.Create(other.transform.position+Vector3.up,m_Camera.transform.rotation, _damageText, isCriticalHit);
                other.transform.parent.SendMessageUpwards("Damage",true,SendMessageOptions.DontRequireReceiver);
                //other.transform.parent.SendMessage("enemyBar",true,SendMessageOptions.DontRequireReceiver);
                other.transform.parent.SendMessage("HpBar",damage*0.1f,SendMessageOptions.DontRequireReceiver);
                playerBar.skill+=0.02f;
                effect.Play();
                soundControl.PlayAudio(SoundControl.SoundType.attack);
                cameraShake.IsShake=true;
            }
        }
    }
    public override void Attack(bool attack){
        base.Attack(attack);
        if(attack){
            //audio.clip=SE[0];
            //audio.Play();
            
            _damageText= Random.Range(100, 150);
        }
    }
    public override void HeavyAttack(bool heavy){
        base.HeavyAttack(heavy);
        if(heavy){
            //audio.clip=SE[1];
            //audio.Play();
            //soundControl.PlayAudio(SoundControl.SoundType.attack);
            _damageText = Random.Range(120, 170);
        }
        
    }
    public override void Kick(bool kick){
        base.Kick(kick);
        if(kick){
            //audio.clip=SE[2];
            //audio.Play();
            //soundControl.PlayAudio(SoundControl.SoundType.attack);
            _damageText = Random.Range(180, 200);
        }
        
    }
}
