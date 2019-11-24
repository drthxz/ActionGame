using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalParameter;

public class GameManager : MonoBehaviour
{
    public enum State{
        Level1,
        Level2,
        Level3,
        Level4,
        FadeIn,
        FadeOut,
    }
    [SerializeField]
    public State state;
    Global global;
    private FadeManager fadeManager;
    public GameObject gameEnd;
    public GameObject boss;
    private GameObject[] map =new GameObject[4];
    private GameObject[] enemyGroup =new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        global=Global.GetInstance();
        fadeManager =GetComponent<FadeManager>();
        fadeManager.FadeInComplete += EnableInput;
        fadeManager.FadeIn();
        gameEnd=GameObject.Find("GameEnding").gameObject;
        gameEnd.SetActive(false);
        for(int i = 1; i < 5; i++)
        {
            map[i-1] = GameObject.Find("Map_0"+i);
            enemyGroup[i-1] = map[i-1].transform.GetChild(3).gameObject;
            if(i-1!=0){
                map[i-1].SetActive(false);
                enemyGroup[i-1].SetActive(false);
            }
        }
        mainCamera=Camera.main.gameObject;
        subCamera=GameObject.Find("SubCamera").gameObject;
        subCamera.SetActive(false);
        timeline=GameObject.Find("TimeLine").gameObject;
        bomb=Resources.Load<GameObject>("Prefab/nuclearBomb");
        timeline.SetActive(false);
    }
void EnableInput()
    {
        fadeManager.FadeInComplete -= EnableInput;
        Global.coinCound=0;
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.FadeIn:
                
                StartCoroutine(waitFadeIn());
                break;
            case State.FadeOut:
                
                StartCoroutine(waitFadeOut());
                break;
            case State.Level1:
                Open_Level1();
                break;
            case State.Level2:
                Open_Level2();
                break;
            case State.Level3:
                Open_Level3();
                break;
            case State.Level4:
                Open_Level4();
                break;
        }
        
    }
    IEnumerator waitFadeOut(){
        fadeManager.FadeOut();
        yield return new WaitForSeconds(2f);
        state=State.FadeIn;
    }
    IEnumerator waitFadeIn(){
        fadeManager.FadeIn();
        yield return new WaitForSeconds(1f);
        if(global.levelname==2){
            state=State.Level2;
        }
        if (global.levelname == 3)
        {
            state = State.Level3;
        }
        if (global.levelname == 4)
        {
            state = State.Level4;
        }
    }
    void Open_Level1()
    {
        //boss display
        if (enemyGroup[0].transform.childCount == 0)
        {
            boss.SetActive(true);
            Destroy(map[0].transform.GetChild(2).transform.GetChild(1).gameObject);
            Destroy(map[0].transform.GetChild(2).transform.GetChild(0).gameObject);
            map[1].SetActive(true);
            state=State.FadeIn;
        }
    }
    void Open_Level2()
    {
        BoxCollider levelOpen=map[1].transform.GetChild(2).transform.GetChild(0).GetComponent<BoxCollider>();
        if(levelOpen){
            levelOpen.isTrigger=false;
            map[2].SetActive(true);
            enemyGroup[1].SetActive(true);
            Destroy(map[0].gameObject,10f);
            //enemyGroup = nextStep[1].transform.Find("EnemyGroup").gameObject;
        }

    }
    void Open_Level3()
    {
        BoxCollider levelOpen = map[2].transform.GetChild(2).transform.GetChild(0).GetComponent<BoxCollider>();
        if (levelOpen)
        {
            levelOpen.isTrigger = false;
            map[3].SetActive(true);
            enemyGroup[2].SetActive(true);
            Destroy(map[1].gameObject,10f);
            //enemyGroup = nextStep[1].transform.Find("EnemyGroup").gameObject;
        }
    }

    GameObject mainCamera;
    GameObject subCamera;
    float timer;
    GameObject timeline;
    GameObject bomb;
    void Open_Level4()
    {
        enemyGroup[3].SetActive(true);
        if(global.gameStop){
            timer+=Time.deltaTime;
            mainCamera.SetActive(false);
            subCamera.SetActive(true);
            timeline.SetActive(true);
            if(timer>=5){
                mainCamera.SetActive(true);
                subCamera.SetActive(false);
                Destroy(timeline,0.5f);
                Instantiate(bomb,global.GetPlayer.gameObject.transform.position+Vector3.forward*2f,new Quaternion(0,-90,0,0));
                global.gameStop=false;
            }
        }
        
        BoxCollider levelOpen = map[3].transform.GetChild(2).transform.GetChild(0).GetComponent<BoxCollider>();
        if (levelOpen)
        {
            levelOpen.isTrigger = false;
            map[3].SetActive(true);
            Destroy(map[2].gameObject,10f);
        }
        if (enemyGroup[3].transform.childCount == 0)
        {
            gameEnd.SetActive(true);
        }
    }
}
