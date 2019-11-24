using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfControl : Animal
{

    public List<WolfDamage> attackCollider=new List<WolfDamage>();
    protected override void Start()
    {
        base.Start();
        foreach(WolfDamage index in GetComponentsInChildren<WolfDamage>())
        {
            attackCollider.Add(index);
            //index.enabled=false;
        }
        
        //enemyHp=transform.Find("wolf_collider/Canvas/Bar").gameObject;
        // foreach(var index in GameObject.FindGameObjectsWithTag("BoarEnemy")){
        //     boar.Add(index);            
        // }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    private int random;
    private bool _search;
    protected override void Move(){
        distTarget= Vector3.Distance(transform.position, player.transform.position);
        if(!_isRandom && time>=2f){
            x=Random.Range(-5,2);
            z=Random.Range(-5,2);
            _isRandom=true;
            random=Random.Range(1,4);
        }
        base.Move();
        if(playerControl.timerOn){
            StartCoroutine(noSearch());
        }else{
            searchRange=10f;
        }
        if (hit)
        {
            wolfAnim.PlayAnim(WolfAnim.AnimType.hit);
            _isRandom = false;
            StartCoroutine(Wait(false,(value)=>hit=value));
            return; 
        }
        if (distTarget < searchRange){//see
            
            // if(random==2 && bar.hp<=0.5f){//run away
            //     Vector3 run=pos+new Vector3(55,transform.position.y,-15);
            //     transform.LookAt(run);
            //     transform.Translate(Vector3.forward * Time.deltaTime * 3.5f);
            //     wolfAnim.PlayAnim(WolfAnim.AnimType.run);
            //     return;
            // }
			transform.LookAt(player.transform);
			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            //attack Range
			if (distTarget < attackRange)
			{
                if(!damageCom.isAttack){
                    for(int i = 0; i < 1; i++)
                    {
                        attackCollider[i].enabled = true;
                        attackCollider[i].damage = damage;
                    }
                    wolfAnim.PlayAnim(WolfAnim.AnimType.attack);
                    BroadcastMessage("Attack",true,SendMessageOptions.DontRequireReceiver);
                }							
			}
			else
			{	
                if(!damageCom.isAttack){
                    transform.Translate(Vector3.forward * Time.deltaTime * 3);
                    wolfAnim.PlayAnim(WolfAnim.AnimType.run);
                }
				
            }	
		}
        
    }
    public override void Damage(bool value){
        base.Damage(damageCom);
        HittedMatEffect sc = gameObject.GetComponent<HittedMatEffect>();
            if (null == sc)
                sc = gameObject.AddComponent<HittedMatEffect>();
        sc.Active();
        sc.SetColor(Color.red);
    }
    protected override IEnumerator Wait<T>(T value, System.Action<T> changValue)
    {
        yield return StartCoroutine(base.Wait(value,changValue));
    }


    IEnumerator noSearch(){
        yield return new WaitForSeconds(0.5f);
        searchRange=0;
    }

    protected override void OnDrawGizmosSelected () {
		base.OnDrawGizmosSelected();		
	}
}
