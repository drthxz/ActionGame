using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGet : MonoBehaviour, IItem
{
    public void TouchItem(PlayerControl player) { }
    public void GetItem(PlayerControl player)
    {
        if(gameObject.tag== "Emergency")
        {
            player.playerBar.hp = 1f;
        }
        else
        {
            Image modeluf = GameObject.Find("modelufImage").GetComponent<Image>();
            modeluf.CrossFadeAlpha(0, 0f, true);
            modeluf.enabled = true;
            modeluf.CrossFadeAlpha(1, 0.5f, true);
        }
        
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0f, 0.5f, 0f));
    }
}
