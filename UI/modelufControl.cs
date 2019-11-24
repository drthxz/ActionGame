using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalParameter;

public class modelufControl : MonoBehaviour
{
    private Image _modelufImage;
    [SerializeField]private GameObject bomb;
    private PlayerControl _player;
    private bool used=true;
    Global global;
    private AudioSource se;

    // Start is called before the first frame update
    void Start()
    {
        _modelufImage = gameObject.GetComponent<Image>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        global = Global.GetInstance();
        se=GetComponent<AudioSource>();
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        #region //colorChange
        //if (flash)
        //{
        //    if (isShow)
        //    {
        //        timer += Time.deltaTime;
        //        image.color = Color.Lerp(Color.red, Color.white, Time.deltaTime * timer);
        //        if (timer>1f-i)
        //        {
        //            isShow = false;
        //            timer = 0;
        //        }
        //    }
        //    else
        //    {

        //            timer += Time.deltaTime;
        //            image.color = Color.Lerp(Color.white, Color.red, Time.deltaTime * timer);
        //            if (timer > 1f - i)
        //            {
        //                isShow = true;
        //                timer = 0;
        //                i += 0.25f;
        //            }


        //    }

        // }
        #endregion
        if (_modelufImage.enabled && (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Joystick1Button5)) && used )
        {
            //isShow = true;
            //if (isShow)
            //{
            //    flash = true;
            //}
            used=false;
            se.Play();
            StartCoroutine(Flash());
        }
    }

    [SerializeField]float MaxFlashInterval = 1f;
    [SerializeField]float step = 0.25f;
    [SerializeField]int flashCntInterval = 5;

    IEnumerator Flash()
    {
        int cnt = 0;
        float i=0;
        bool isFlash=false;
        float timer = 0f;
        bool flag = true;
        while (flag)
        {
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime;
            _modelufImage.color = Color.Lerp(Color.red, Color.white, (isFlash?timer:MaxFlashInterval-i-timer)/(MaxFlashInterval-i));
            if (timer > MaxFlashInterval - i)
            {
                isFlash = !isFlash;
                timer = 0;
                if (isFlash)
                {
                    cnt++;
                    if (cnt >= flashCntInterval)
                    {
                        cnt = 0;
                        i += step;
                        if (i >= MaxFlashInterval)
                        {
                            //bomb start & modeluf del
                            bomb.SetActive(true);
                            flag = false;
                            _player.SendMessageUpwards("Damage", true, SendMessageOptions.DontRequireReceiver);
                            _player.playerBar.hp -= 0.2f;
                            yield return new WaitForSeconds(0.5f);
                            _modelufImage.enabled = false;
                            global.gameStop = true;
                        }
                    }
                }
            }
        }
    }
}
