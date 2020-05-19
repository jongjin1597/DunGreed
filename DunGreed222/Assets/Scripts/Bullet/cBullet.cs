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
        _Anim = GetComponent<Animator>();
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
            if (collision.CompareTag("Monster"))
            {
                cMonsterBase Monster;
                Monster = collision.GetComponent<cMonsterBase>();
               int dam = _Damage - Monster._Defense;
                Monster.HIT(dam);
            }
        }
        else if (_BulletState == BulletState.Monster)
        {
            if (collision.CompareTag("Player"))
            {
               int dam =_Damage - Player.GetInstance._Defense;
                Player.GetInstance.HIT(dam);
            }
        }
        else if (_BulletState == BulletState.Boss)
        {

        }
    }
}
