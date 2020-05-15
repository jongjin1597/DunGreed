using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAttack : MonoBehaviour
{
    Animator _Ani;
    RuntimeAnimatorController _SwardAni;
    RuntimeAnimatorController _SpearAni;

    public delegate void _AttackStart();
    public _AttackStart _Attack;

    void Awake()
    {
        _Attack += Attack;
        _Attack += cCameramanager.GetInstance.VibrateForTime;
        _Ani = GetComponent<Animator>();
        _SwardAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Sward/Swing");
       _SpearAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Spear/SpearAttack");
    }
    public void SetItemMotion(Item item)
    {
        if(item._Type == ItemType.Sword)
        {
            _Ani.runtimeAnimatorController = _SwardAni;
        }

        else if (item._Type == ItemType.Spear)
        {
            _Ani.runtimeAnimatorController = _SpearAni;
        }
        _Ani.speed = item._AttackSpeed;

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
