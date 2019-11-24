using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : Damage
{
    // Start is called before the first frame update
    protected override void Start()
    {
        self = gameObject.transform;
        base.Start();
        self=gameObject.GetComponentInParent<Transform>();
        //soundControl.Init(self.gameObject.GetComponentInParent<AudioSource>(), "Sound/wolf0");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent.SendMessageUpwards("Damage", true, SendMessageOptions.DontRequireReceiver);
            other.transform.parent.SendMessage("HpBar", damage * 0.1f, SendMessageOptions.DontRequireReceiver);
        }
        // if (other.gameObject.tag == "Wolf")
        // {
        //     other.transform.parent.SendMessageUpwards("Damage", true, SendMessageOptions.DontRequireReceiver);
        //     //other.transform.parent.SendMessage("enemyBar", true, SendMessageOptions.DontRequireReceiver);
        //     other.transform.parent.SendMessage("HpBar", damage * 0.5f, SendMessageOptions.DontRequireReceiver);
        // }
    }

    public override void Attack(bool attack)
    {
        base.Attack(attack);
        //soundControl.PlayAudio(SoundControl.SoundType.attack);
    }
}
