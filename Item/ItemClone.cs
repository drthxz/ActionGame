using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClone : MonoBehaviour,IItem
{
    [SerializeField] private GameObject emergency;
    private bool _isTouch;
    [SerializeField] private string _name;
    // Start is called before the first frame update
    public void TouchItem(PlayerControl player)
    {
        if (!_isTouch)
        {
            Instantiate(emergency, transform.position, transform.rotation);
            _isTouch = true;
        }
    }

    public void GetItem(PlayerControl player) { }
    void Start()
    {
        emergency = Resources.Load<GameObject>("Prefab/"+ _name);
    }
}
