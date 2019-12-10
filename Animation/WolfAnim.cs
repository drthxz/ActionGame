using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WolfAnim
{
    public enum AnimType
    {
        death,
        Idle,
        walk,
        run,
        hit,
        attack,
        clone,
    }
    private Animation _anim;
    public void Init(Animation anim) { this._anim = anim; }
    public void PlayAnim(AnimType animType)
    {
        switch (animType)
        {
            case AnimType.death:
                _anim.Play("death");
                break;
            case AnimType.Idle:
                _anim.Play("Standby");
                break;
            case AnimType.walk:
                _anim.Play("walk");
                break;
            case AnimType.run:
                _anim.Play("run");
                break;
            case AnimType.hit:
                _anim.Play("Beaten");
                break;
            case AnimType.attack:
                _anim.Play("Attack1");
                break;
            case AnimType.clone:
                _anim.Play("Roar");
                break;
        }
    }
}

