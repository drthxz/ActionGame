using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalParameter;

public class CoinManage : MonoBehaviour
{
    private Text coinIndex;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        coinIndex = GameObject.FindGameObjectWithTag("Coin").GetComponent<Text>();
        anim=gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0f, 0.5f, 0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Global.coinCound++;
            coinIndex.text = Global.coinCound.ToString();
            anim.enabled=true;
            Destroy(gameObject,2f);
        }
    }
}
