using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameter;

public class OiltankAnim : MonoBehaviour
{
    private List<GameObject> oil=new List<GameObject>();
    [SerializeField]private List<ParticleSystem> _oileffect=new List<ParticleSystem>();
    private float _time;
    private bool _isPlay;
    Global global;
    [SerializeField]private int level;
    // Start is called before the first frame update
    void Start()
    {
        global = Global.GetInstance();
        //Get GameObject
        for (int i=0;i<transform.childCount;i++){
            oil.Add(transform.GetChild(i).gameObject);
            _oileffect.Add(oil[i].transform.GetChild(0).GetChild(2).gameObject.GetComponent<ParticleSystem>());
            _oileffect[i].Stop();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (global.levelname== level)
        {
            AnimChange();
        }
        
    }

    private void AnimChange()
    {
        _time += Time.deltaTime;

        for (int i = 0; i < _oileffect.Count; i++)
        {
            _oileffect[i].Play();
            if (!_isPlay)
            {
                if (i % 2 == 1)
                {
                    _oileffect[i].Stop();
                }
                else
                {
                    _oileffect[i].Play();
                }
                if (_time >= 2)
                {
                    _isPlay = true;
                }
            }
            else
            {
                if (i % 2 == 0)
                {
                    _oileffect[i].Stop();
                }
                else
                {
                    _oileffect[i].Play();
                }
                if (_time >= 4)
                {
                    _isPlay = false;
                    _time = 0;
                }
            }
        }
    }
}
