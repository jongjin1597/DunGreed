using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cAttack : MonoBehaviour
{
    Animator _Ani;
    RuntimeAnimatorController _SwardAni;
    RuntimeAnimatorController _SpearAni;

    BoxCollider2D _HitBox;
    public delegate void _AttackStart();
    public _AttackStart _Attack;
    int dam;
    void Awake()
    {
        _Attack += Attack;
      
        _Ani = GetComponent<Animator>();
        _HitBox = GetComponent<BoxCollider2D>();
        _SwardAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Sward/Swing");
       _SpearAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Spear/SpearAttack");
    }
    public void SetItemMotion(Item item)
    {
        if(item._Type == ItemType.Sword)
        {
            _Ani.runtimeAnimatorController = _SwardAni;
            _Attack += cCameramanager.GetInstance.VibrateForTime;
        }

        else if (item._Type == ItemType.Spear)
        {
            _Ani.runtimeAnimatorController = _SpearAni;
            _Attack += cCameramanager.GetInstance.VibrateForTime;
        }
        else if (item._Type == ItemType.Gun||item._Type ==ItemType.OneShot)
        {
            _Ani.runtimeAnimatorController = null;
            _Attack -= cCameramanager.GetInstance.VibrateForTime;
            _HitBox.enabled = false;
        }
        _Ani.speed = item._AttackSpeed;

    }

    private void Attack()
    {

        _Ani.SetTrigger("AttackCheck");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MonsterHitBox"))
        {
            cMonsterBase Monster = collision.GetComponent<cMonsterBase>();
            int randomDamage = Random.Range((int)Player.GetInstance._MinDamage, (int)Player.GetInstance._MaxDamage);
          
            if (Player.GetInstance.isCritical())
            {
                dam = (randomDamage - Monster._Defense) + (int)((float)randomDamage*((float)Player.GetInstance._CriticalDamage/100.0f));
                Monster.MonsterHIT(dam,true);
            }
            else
            {
                dam = (randomDamage - Monster._Defense);
                Monster.MonsterHIT(dam, false);
            }
            
        }
    }
}
