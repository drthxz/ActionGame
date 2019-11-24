using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfKingDamage : Damage
{
    // Start is called before the first frame update
    protected override void Start()
    {
        self=gameObject.GetComponentInParent<CapsuleCollider>().transform;
        damage = self.GetComponentInParent<Animal>().damage;
        
        base.Start();
        
        soundControl.Init(self.gameObject.GetComponentInParent<AudioSource>(), "Sound/wolf0");
        gameObject.GetComponent<WolfKingDamage>().enabled=false;
    }

    // Update is called once per frame
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
        self.GetComponentInParent<BossControl>().attackCollider[0].enabled = false;
        self.GetComponentInParent<BossControl>().attackCollider[1].enabled = false;
    }
    public override void Attack(bool attack){
        base.Attack(attack);
        soundControl.PlayAudio(SoundControl.SoundType.attack);
    }
}
