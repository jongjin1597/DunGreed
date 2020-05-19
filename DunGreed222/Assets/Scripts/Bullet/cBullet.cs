using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletState
{
    Player,
    Monster,
    Boss
}
public class cBullet : MonoBehaviour
{


    public int _Damage;
    public float _Speed;

    Animator _Anim;
   public Transform _ChildBullet;
    //이총알이 플레이어용인지 몬스터용인지
    public BulletState _BulletState;
    public bool _Start = true;

    private void Awake()
    {
        if (transform.childCount == 0)
        {
            _Anim = GetComponent<Animator>();

        }
        else if (transform.childCount >= 1)
        {
            _ChildBullet = transform.GetChild(0);
            _Anim = transform.GetChild(0).GetComponent<Animator>();
        }
    }
    void Update()
    {
        if (_Start)
        {
            transform.position += transform.up * _Speed * Time.deltaTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (_BulletState == BulletState.Player)
        {
            if (collision.CompareTag("MonsterHitBox"))
            {
                cMonsterBase Monster;
                Monster = collision.GetComponent<cMonsterBase>();
                int dam = _Damage - Monster._Defense;
                if (Player.GetInstance.isCritical())
                {
                    dam = (dam - Monster._Defense) + (dam * (Player.GetInstance._CriticalDamage / 100));
                }
                else if (!Player.GetInstance.isCritical())
                {
                    dam = (dam - Monster._Defense);
                }
       
                Monster.HIT(dam);
                _Anim.SetTrigger("Fire");
                _Start = false;

            }
        }
        else if (_BulletState == BulletState.Monster)
        {
            if (collision.CompareTag("Player"))
            {
                int dam = _Damage - Player.GetInstance._Defense;
                Player.GetInstance.HIT(dam);
                _Anim.SetTrigger("Fire");
                _Start = false;
           
            }
        }
        else if (_BulletState == BulletState.Boss)
        {

        }
    }
    public void CrashBullet()
    {
        this.gameObject.SetActive(false);
    }
}
  