using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameter;

public class PlayerBar : BarManager
{
    // Start is called before the first frame update
    Global global;
    public float hpValue{get;private set;}
    public float skill { get; set; }
    protected override void Start()
    {
        global=global=Global.GetInstance();
        base.Start();
        hp=_bars[0].value;
        skill=_bars[1].value;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        _bars[1].value=Mathf.Lerp(_bars[1].value,skill,Time.deltaTime);
        _bars[0].value=Mathf.Lerp(_bars[0].value,hp,Time.deltaTime);
    }

    public override void HpBar(float damage){
        if(hp<=0){
            pSelf.death=true;
            return;
        }
        hp-=damage;
    }
    public void SkillBar(float value)
    {
            skill-= value;
    }
        
}
