using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]private GameObject start;
    [SerializeField]private GameObject quit;
    private AudioSource se;
    // Start is called before the first frame update
    void Start()
    {
        start.SetActive(false);
        quit.SetActive(false);
        se=gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            start.SetActive(true);
            quit.SetActive(true);
            se.Play();
        }
    }

    public void GameStart(){
        SceneManager.LoadScene("SampleScene");
        se.Play();
    }
    public void GameEnd(){
        Application.Quit();
    }
}
