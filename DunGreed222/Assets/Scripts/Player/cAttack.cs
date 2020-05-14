using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAttack : MonoBehaviour
{
    Animator _Ani;
    RuntimeAnimatorController _SwardAni;
    private Vector3 rotateCenter;
    private Vector3 mousePos;

    public float _Radius;
    public delegate void _AttackStart();
    public _AttackStart _Attack;
    void Awake()
    {
        _Attack += Attack;
        _Attack += cCameramanager.GetInstance.VibrateForTime;
        _Ani = GetComponent<Animator>();
        _SwardAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Sward/Swing");
    }
    public void SetItemMotion(Item item)
    {
        if(item._Type == ItemType.Sword)
        {
            _Ani.runtimeAnimatorController = _SwardAni;
        }

        else if (item._Type == ItemType.Sword)
        {

        }
        _Ani.speed = item._AttackSpeed;

    }
    void Update()
    {
        rotateCenter = Player.GetInstance.transform.position;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos = (mousePos - rotateCenter);

        mousePos = Vector2.ClampMagnitude(mousePos, _Radius);
        this.transform.position = rotateCenter + mousePos;
    }
    private void Attack()
    {
        _Ani.SetTrigger("AttackCheck");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Monster"))
        {


        }
    }
}
