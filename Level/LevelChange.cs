using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameter;

public class LevelChange : MonoBehaviour, INextLevel
{
    Global global;
    public new int name;

    public void Next(PlayerControl player)
    {
        global.gameManager.state = GameManager.State.FadeOut;
        StartCoroutine(wait(name));
    }
    IEnumerator wait(int index)
    {
        yield return new WaitForSeconds(2);
        global.levelname = index;
    }
    // Start is called before the first frame update
    void Start()
    {
        global = Global.GetInstance();
    }

}
