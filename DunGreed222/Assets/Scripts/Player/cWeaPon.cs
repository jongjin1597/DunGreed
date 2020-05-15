using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//현제 장착중인무기
public class cWeaPon : MonoBehaviour
{

    //무기 이미지 그리기용 랜더러
    private SpriteRenderer _SpriteRend;
    //현제 아이템
    private Item _NowWeaPon;
    //무기 애니매이션 재생용 애니메이터
    private Animator _Ani;
    //검 애니메이션
    private RuntimeAnimatorController _SwardAni;
    //창 애니매이션
    private RuntimeAnimatorController _SpearAni;
    private cAttack _AttackMotion;
    void Awake()
    {
        _AttackMotion = FindObjectOfType<cAttack>();
    
          _Ani =transform.GetComponent<Animator>();
          _SpriteRend = transform.GetComponent<SpriteRenderer>();
        _SwardAni =Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Sward/Sward");
        _SpearAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Spear/Spear");
     
    }
   

    //현제 무기 세팅(무기장착시 세팅)
    public void SetWeaPon(Item _WeaPonNum)
    {
        _NowWeaPon = _WeaPonNum;
        if (_WeaPonNum._Type == ItemType.Sword)
        {
            _Ani.runtimeAnimatorController = _SwardAni;
            _SpriteRend.sortingOrder = 4;
            this.transform.parent.localPosition = new Vector3(0.455f,-0.014f,0);
        }
        else if (_WeaPonNum._Type == ItemType.Spear)
        {
            _Ani.runtimeAnimatorController = _SpearAni;
            _SpriteRend.sortingOrder = 10;
            this.transform.parent.localPosition = Vector3.zero;
        }
        else if (_WeaPonNum._Type == ItemType.Gun)
        {
            _Ani.runtimeAnimatorController = null;
            _SpriteRend.sortingOrder = 10;
            this.transform.parent.localPosition = Vector3.zero;
        }
        _Ani.speed = _NowWeaPon._AttackSpeed;
        _SpriteRend.sprite = _NowWeaPon._ItemIcon;
        Player.GetInstance._MinDamage = 0;
        Player.GetInstance._MaxDamage = 0;


        Player.GetInstance._MinDamage += _NowWeaPon._MinAttackDamage;
        Player.GetInstance._MaxDamage += _NowWeaPon._MaxAttackDamage;
        _AttackMotion.SetItemMotion(_NowWeaPon);
    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _Ani.SetTrigger("AttackCheck");
                StartCoroutine("CheckAnimationState");
            }
        }

       
    }

    IEnumerator CheckAnimationState()
    {

        if (!_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack1") /*&&_Ani.GetCurrentAnimatorStateInfo(0).normalizedTime>0.2f*/)
        {
            _AttackMotion._Attack();
            yield return null;
        }

        else if (!_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack2") /*&& _Ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.2f*/)
        {
            _AttackMotion._Attack();
            yield return null;
        }


    }


}
