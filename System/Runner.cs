using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HittedMatEffect sc = gameObject.GetComponent<HittedMatEffect>();
            if (null == sc)
                sc = gameObject.AddComponent<HittedMatEffect>();
            sc.Active();
            sc.SetColor(Color.red);
        }
    }

}
