
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool autoRotation = false;
    public float autoRotationSpeed = 0.1f;
    public bool canRotation = false;
    private GameObject _target;
    private Camera _camera;
    [SerializeField]private float _distance=7f;
    private bool posChange;
    public bool Change{get {return posChange;}set{posChange=value;}}
    public float posZ=-30f;
    public float posY = -90f;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _camera = Camera.main;
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, posY, posZ), Time.deltaTime);
    }
    private void LateUpdate()
    {
        if(_target==null){
            _target=GameObject.FindGameObjectWithTag("Player");
        }
        var cameraPos=_target.transform.position-transform.right*_distance+transform.up;
        transform.position=_target.transform.position+transform.up*2;
        _camera.transform.LookAt(transform.position);
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, cameraPos, Time.deltaTime);
    }

    
}
