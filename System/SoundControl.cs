using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl
{
    public enum SoundType
    {
        hit,
        attack,
        death,
    }
    private AudioSource _audio;
    public List<AudioClip> se = new List<AudioClip>();
    public void Init(AudioSource audio, string name) {
        this._audio = audio;
        for (int i = 1; i < 4; i++)
        {
            se.Add(Resources.Load<AudioClip>(name +i));
            //Debug.Log(se[0]);
        }
    }
    public void PlayAudio(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.death:
                _audio.clip = se[2];
                _audio.Play();
                break;

            case SoundType.hit:
                _audio.clip = se[1];
                _audio.Play();
                break;

            case SoundType.attack:
                _audio.clip = se[0];
                _audio.Play();
                break;
        }
    }
}
