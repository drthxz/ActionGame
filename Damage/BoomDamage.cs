using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomDamage : MonoBehaviour
{
    public float damage;
    void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.tag == "Player")
        // {
        //     other.transform.parent.SendMessageUpwards("Damage", true, SendMessageOptions.DontRequireReceiver);
        //     other.transform.parent.SendMessage("HpBar", damage * 0.1f, SendMessageOptions.DontRequireReceiver);
        // }
        if (other.gameObject.tag == "Wolf")
        {
            other.transform.parent.SendMessageUpwards("Damage", true, SendMessageOptions.DontRequireReceiver);
            //other.transform.parent.SendMessage("enemyBar", true, SendMessageOptions.DontRequireReceiver);
            other.transform.parent.SendMessage("HpBar", damage * 0.5f, SendMessageOptions.DontRequireReceiver);
        }
    }
}
