using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터, 플레이어 최상단
public class cCharacter : MonoBehaviour
{
    protected float _initHP;//체력
    protected float _MinAtteckDamage;//최소공격력
    protected float _MaxAttackDamage;//최대공격력
    protected float _MoveSpeed;//이동속도
    protected float _Defense;//방어력
    protected SpriteRenderer _Renderer;
    protected Animator _Anim;
    protected virtual void Awake()
    {

        _Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        _Anim = gameObject.GetComponentInChildren<Animator>();
    }
}
