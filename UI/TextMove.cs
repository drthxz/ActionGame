using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextMove : MonoBehaviour
{
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find ("Text");
    }

    // Update is called once per frame
    void Update()
    {

        text.GetComponent<RectTransform>().anchoredPosition += new Vector2(0,1);
        //Destroy(text,15f);
    }

}
