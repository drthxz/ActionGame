using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position,Quaternion rotation, int damageAmout,bool isCriticalHit)
    {
        Transform damageTransform = Instantiate(GlobalParameter.Global.DamagePopup.transform, position, rotation);
        DamagePopup damagePopup = damageTransform.GetComponent<DamagePopup>();
       // int damage = Random.Range(250, 300);
        damagePopup.Setup(damageAmout, isCriticalHit);
        return damagePopup;
    }

    private TextMeshPro _textMesh;
    private Color _textColor;
    float timer = 1f;
    float speed = 0.01f;
    private static int sortindOrder;
    // Start is called before the first frame update
    void Awake()
    {
        _textMesh = transform.GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        transform.position += Vector3.up * speed;
        if (timer > 0.5f)
        {
            float increaseScaleAmout = 1f;
            transform.localScale += Vector3.one * increaseScaleAmout * Time.deltaTime;
        }
        else
        {
            float increaseScaleAmout = 1f;
            transform.localScale -= Vector3.one * increaseScaleAmout * Time.deltaTime;
        }
        if (timer < 0)
        {
            _textColor.a -= speed * Time.deltaTime;
            _textMesh.color = _textColor;
            Destroy(gameObject,0.5f);
        }
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        _textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {//Normal hit
            _textMesh.fontSize = 3;
            _textColor = new Color32(255, 207, 119, 255);
        }
        else
        {//Critical hit
            _textMesh.fontSize = 3.5f;
            _textColor = new Color32(255, 0, 0, 255);
        }
        sortindOrder++;
        _textMesh.sortingOrder = sortindOrder;
        _textMesh.color = _textColor;
        timer = 1f;
    }
}
