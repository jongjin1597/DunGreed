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
    //회전하는총알
    public Transform _transform;
    //이총알이 플레이어용인지 몬스터용인지
    public BulletState _BulletState;
    public bool _Start = true;

    private void Awake()
    {
       
        if (transform.childCount==0)
        {
            _Anim = GetComponent<Animator>();
   
        }
        else if(transform.childCount >= 1)
        {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (_BulletState==BulletState.Player) 
        {
            if (collision.CompareTag("MonsterHitBox"))
            {
                cMonsterBase Monster;
                Monster = collision.GetComponent<cMonsterBase>();
                int dam = _Damage - Monster._Defense;
                Monster.HIT(dam);
                _Anim.SetTrigger("Fire");
                _Start = false;
                if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerBulletCrash") && _Anim.GetCurrentAnimatorStateInfo(0).length>=1)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
        else if (_BulletState == BulletState.Monster)
        {
            if (collision.CompareTag("Player"))
            {
               int dam =_Damage - Player.GetInstance._Defense;
                Player.GetInstance.HIT(dam);
                _Anim.SetTrigger("Fire");
                //if (_Anim.GetCurrentAnimatorStateInfo(0).length >= 1)
                //{
                    this.gameObject.SetActive(false);
                //}
            }
        }
        else if (_BulletState == BulletState.Boss)
        {

        }
    }
}
