using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshipControl : MonoBehaviour
{
    private float timer;
    private GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        
        bomb= Resources.Load<GameObject>("Prefab/nuclearBomb");
        StartCoroutine(Clone());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= 5f)
        {
            gameObject.transform.Translate(new Vector3(0, 0, 0.5f));

        }
    }
    IEnumerator Clone()
    {
        bool clone = true;
        while (clone)
        {
            yield return new WaitForSeconds(0.5f);
            //Instantiate(bomb, transform.position, Quaternion.Euler(0,0,-90));
            
            if (timer > 5f)
            {
                clone = false;
                //Destroy(gameObject);
            }
            
        }
    }
}
