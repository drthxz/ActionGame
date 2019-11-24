using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : Animal
{
    public GameObject clone;
    private bool _cloneWolf;
    public List<WolfKingDamage> attackCollider=new List<WolfKingDamage>();
    private Transform enemyGroup;
    protected override void Start()
    {
        enemyGroup = GameObject.Find("Map_01").transform.GetChild(3);
        base.Start();
        
        foreach (WolfKingDamage index in GetComponentsInChildren<WolfKingDamage>())
        {
            attackCollider.Add(index);
            //index.enabled=false;
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    private bool _search;
    float cloneTime;
    private bool cloneAnim;
    protected override void Move()
    {
        distTarget = Vector3.Distance(transform.position, player.transform.position);
        if (!_isRandom && time >= 2f)
        {
            x = Random.Range(-5, 2);
            z = Random.Range(-5, 2);
        }
        base.Move();
        if (playerControl.timerOn)
        {
            StartCoroutine(noSearch());
        }
        else
        {
            searchRange = 8f;
        }

        if (distTarget < searchRange)
        {//see
            cloneTime += Time.deltaTime;
            if (cloneTime > 5f)
            {
                if (enemyGroup.transform.childCount == 0)
                {
                    _cloneWolf = !_cloneWolf;
                    cloneTime = 0;
                }
            }
            transform.LookAt(player.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            //attack Range
            if (hit)
            {
                wolfAnim.PlayAnim(WolfAnim.AnimType.hit);
                StartCoroutine(Wait(false,(value)=>hit=value));
                return;
            }
            if (distTarget < attackRange)
            {
                if (!damageCom.isAttack)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        attackCollider[i].enabled = true;
                        attackCollider[i].damage = damage;
                    }
                    wolfAnim.PlayAnim(WolfAnim.AnimType.attack);
                    BroadcastMessage("Attack", true, SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                if (_cloneWolf)
                {
                    for(int i=0; i < 3; i++)
                    {
                        GameObject temp= Instantiate(clone,gameObject.transform.position+Vector3.forward*3f*i,transform.rotation);
                        temp.transform.parent = enemyGroup;
                    }
                    wolfAnim.PlayAnim(WolfAnim.AnimType.clone);
                    cloneAnim = true;
                    _cloneWolf = false;
                    StartCoroutine(Wait(false, (value) => cloneAnim = value));
                    
                        
                }
                else
                {
                    if (!cloneAnim)
                    {
                        if(!damageCom.isAttack){
                            transform.Translate(Vector3.forward * Time.deltaTime * 3);
                            wolfAnim.PlayAnim(WolfAnim.AnimType.run);
                        }
                    }
                    
                }
            }
        }

    }
    public override void Damage(bool value)
    {
        base.Damage(damageCom);
        HittedMatEffect sc = gameObject.GetComponent<HittedMatEffect>();
        if (null == sc)
            sc = gameObject.AddComponent<HittedMatEffect>();
        sc.Active();
        sc.SetColor(Color.red);
    }
    protected override IEnumerator Wait<T>(T value, System.Action<T> changValue)
    {
        yield return StartCoroutine(base.Wait(value, changValue));
    }
    IEnumerator noSearch()
    {
        yield return new WaitForSeconds(0.5f);
        searchRange = 0;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}

