using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GlobalParameter;

public class Animal : MonoBehaviour
{
    Global global;
    protected GameObject enemyHp;
    protected GameObject player;
    protected PlayerControl playerControl;
    protected WolfAnim wolfAnim = new WolfAnim();
    //protected Animation anim;
    protected SoundControl soundControl = new SoundControl();
    protected Damage damageCom;
    protected float angle;
    protected BarManager bar;
    protected float dissolveThreshold;
    protected new Renderer renderer;
    private float _time;
    public float damage;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        wolfAnim.Init(GetComponentInChildren<Animation>());
        soundControl.Init(GetComponent<AudioSource>(), "Sound/wolf0");
        damageCom =GetComponentInChildren<Damage>();
        angle=Mathf.Cos(Mathf.PI/4f);
        bar=gameObject.GetComponentInChildren<BarManager>();
        player=GameObject.Find("Player");
        playerControl=player.GetComponent<PlayerControl>();
        renderer = GetComponentInChildren<Renderer>();
        global = Global.GetInstance();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(player==null){
            player=GameObject.Find("Player");
            playerControl=player.GetComponent<PlayerControl>();
        }
        //death
        if(death && !dieAnim){
            wolfAnim.PlayAnim(WolfAnim.AnimType.death);
            soundControl.PlayAudio(SoundControl.SoundType.death);
            //damage.audio.clip=damage.SE[1];
           // damage.audio.Play();
            death=false;
            dieAnim=true;
        }
        if (dieAnim)
        {
            _time +=0.5f* Time.deltaTime;
            renderer.material.SetFloat("_Threshold", dissolveThreshold+ _time);
            Destroy(gameObject, 2f);
        }
        if(bar.hp>0){
            if (!global.gameStop)
            {
                Move();
            }
        }
    }
    protected int x;
    protected int z;
    [SerializeField]protected GameObject target;
    protected float distTarget;
    protected bool _isRandom;
    protected float time;
    public bool death{get;set;}
    private bool dieAnim;
    protected Vector3 pos;
    protected virtual void Move(){
        
        if(distTarget > searchRange){
            if(!_isRandom){
                wolfAnim.PlayAnim(WolfAnim.AnimType.Idle);
                time +=Time.deltaTime;
                pos=transform.position;
            }
            if(_isRandom){
                time-=Time.deltaTime;
                Vector3 nextPos=pos+new Vector3(x,0,z);
                transform.LookAt(nextPos);
                transform.Translate(Vector3.forward * Time.deltaTime * 2);
                wolfAnim.PlayAnim(WolfAnim.AnimType.walk);
                float dist= Vector3.Distance(transform.position, nextPos);
                if(dist<0.1f || time<=0){
                    _isRandom=false;
                    time=0;
                }
            }
            transform.rotation=Quaternion.Euler(Mathf.Clamp(transform.eulerAngles.x,-10,10), transform.eulerAngles.y, Mathf.Clamp(transform.eulerAngles.z,-10,10));
        }
    }

    protected bool hit;
    public virtual void Damage(bool value){
        hit=value;
        soundControl.PlayAudio(SoundControl.SoundType.hit);
    }
    protected virtual IEnumerator Wait<T>(T value,System.Action<T>changValue){
        yield return new WaitForSeconds(0.5f);
        changValue(value);

    }

    [Header("DrawGizmos")]
    public float searchRange;
	public float attackRange;
    protected virtual void OnDrawGizmosSelected () {
		Gizmos.color = Color.green;
		//searchRange
		Gizmos.DrawWireSphere (transform.position, searchRange);
		Gizmos.color = Color.red;
		//attackRange
		Gizmos.DrawWireSphere (transform.position, attackRange);
		
	}
}
