using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour,ICameraPos
{
    public CameraFollow mainCamera;
    [SerializeField]private float posY;
    [SerializeField] private float posZ;
    public void PosChange(PlayerControl player)
    {
        mainCamera.Change = !mainCamera.Change;
        mainCamera.posZ = posZ;
        mainCamera.posY = posY;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("CameraFollow").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
