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

    void Awake()
    {
          _Ani=transform.GetComponent<Animator>();
          _SpriteRend = transform.GetComponent<SpriteRenderer>();
        _SwardAni =Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Sward/Sward");
        _SpearAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Spear/Spear");
    }
    //현제 무기 세팅(무기장착시 세팅)
    public void SetWeaPon(Item _WeaPonNum)
    {
        _NowWeaPon = _WeaPonNum;
        transform.eulerAngles = new Vector3(0, 0, 0);
        if (_WeaPonNum._Type == ItemType.Sword)
        {
            _Ani.runtimeAnimatorController = _SwardAni;
        }
        else if (_WeaPonNum._Type == ItemType.Spear)
        {
            _Ani.runtimeAnimatorController = _SpearAni;
        }
        else if (_WeaPonNum._Type == ItemType.Gun)
        {
            _Ani.runtimeAnimatorController = null;
        }
        _Ani.speed = _NowWeaPon._AttackSpeed;
        _SpriteRend.sprite = _NowWeaPon._ItemIcon;
        Player.GetInstance._MinDamage = 0;
        Player.GetInstance._MaxDamage = 0;


        Player.GetInstance._MinDamage += _NowWeaPon._MinAttackDamage;
        Player.GetInstance._MaxDamage += _NowWeaPon._MaxAttackDamage;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
       
            _Ani.SetTrigger("AttackCheck");
        

        }

       
        //WeaPon.transform.position = rotateCenter + mousePos;
    }

}
