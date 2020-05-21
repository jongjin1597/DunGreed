using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터, 플레이어 최상단
public class cCharacter : MonoBehaviour
{
    public int _currnetHP;//체력
    public int _MaxHP;//최대체력
    public int _MinAtteckDamage;//최소공격력
    public int _MaxAttackDamage;//최대공격력
    public float _MoveSpeed;//이동속도
    public int _Defense=0;//방어력
    protected SpriteRenderer _Renderer;
    protected Animator _Anim;
    protected virtual void Awake()
    {

        _Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        _Anim = gameObject.GetComponentInChildren<Animator>();
    }
    public virtual void  HIT(int dam)
    {

    }
}
