using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDamage : Damage
{
    // Start is called before the first frame update
    protected override void Start()
    {
        self=gameObject.GetComponentInParent<CapsuleCollider>().transform;
        damage = self.GetComponentInParent<Animal>().damage;
        base.Start();
        
        soundControl.Init(self.gameObject.GetComponentInParent<AudioSource>(), "Sound/wolf0");
        gameObject.GetComponent<WolfDamage>().enabled=false;
        //for(int i=1;i<4;i++){
        //    SE.Add(Resources.Load<AudioClip>("Sound/wolf0"+i));
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        
        if(isAttack){
            if (other.gameObject.tag == "Player") {
                other.transform.parent.SendMessageUpwards("Damage",true,SendMessageOptions.DontRequireReceiver);
                other.transform.parent.SendMessage("HpBar",damage*0.1f,SendMessageOptions.DontRequireReceiver);
                StartCoroutine(EnterAttack());
            }
            
        }
    }
    IEnumerator EnterAttack(){
        yield return new WaitForSeconds(1.5f);
        //audio.Pause();
        isAttack=false;
        self.GetComponentInParent<WolfControl>().attackCollider[0].enabled = false;
        self.GetComponentInParent<WolfControl>().attackCollider[1].enabled = false;
    }
    public override void Attack(bool attack){
        base.Attack(attack);
        //damage = self.GetComponentInParent<WolfControl>().damage;
        soundControl.PlayAudio(SoundControl.SoundType.attack);
    }

}
