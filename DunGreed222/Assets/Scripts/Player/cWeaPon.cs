using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//현제 장착중인무기
public class cWeaPon : MonoBehaviour
{
    public Image _Cooltime;
    Text _BulletText;
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
    //총 애니메이션
    private RuntimeAnimatorController _GunAni;
    //공격모션
    private cAttack _AttackMotion;
    public delegate void _AttackSpeed(float AttackSpeed);
    public _AttackSpeed _Speed;
    private bool _OneSkillCheck =true;

    void Awake()
    {

        _AttackMotion = FindObjectOfType<cAttack>();
          _Ani =transform.GetComponent<Animator>();
          _SpriteRend = transform.GetComponent<SpriteRenderer>();
        _SwardAni =Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Sward/Sward");
        _SpearAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Spear/Spear");
        _GunAni = Resources.Load<RuntimeAnimatorController>("Animaition/Weapon/Gun/Gun");
        _Speed += _AttackMotion.SetAttackSpeed;
        _Speed += SetAttackSpeed;
        _BulletText = cUIManager.GetInstance.GetWeaPonSlot().transform.GetChild(1).GetChild(1).GetComponent<Text>();
    }
    //현제 무기 세팅(무기장착시 세팅)
    public void SetWeaPon(Item _WeaPon)
    {
        if (_NowWeaPon != null)
        {
            if (!_NowWeaPon._Skill)
            {              
                _NowWeaPon._Skill = true;
                _Cooltime.fillAmount = 0;
                Player.GetInstance._Buff.SetTrigger("BuffOff");
            }
        }

        _NowWeaPon = _WeaPon;
        if (_NowWeaPon._Type == ItemType.Sword)
        {
            _Ani.runtimeAnimatorController = _SwardAni;
            _SpriteRend.sortingOrder = 4;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        else if (_NowWeaPon._Type == ItemType.Spear)
        {
            _Ani.runtimeAnimatorController = _SpearAni;
            _SpriteRend.sortingOrder = 10;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));

        }
        else if (_NowWeaPon._Type == ItemType.Gun|| _NowWeaPon._Type ==ItemType.OneShot)
        {
            _Ani.runtimeAnimatorController = _GunAni;
            _SpriteRend.sortingOrder = 10;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0,0));

            _BulletText.text = ((Longrange)_NowWeaPon)._MaxBullet.ToString() + " / " + ((Longrange)_NowWeaPon)._MaxBullet.ToString();

        }
        _Ani.speed = _NowWeaPon._AttackSpeed;
        _SpriteRend.sprite = _NowWeaPon._ItemIcon;

        Player.GetInstance._MinDamage = 0;
        Player.GetInstance._MaxDamage = 0;


        Player.GetInstance._MinDamage += _NowWeaPon._MinAttackDamage;
        Player.GetInstance._MaxDamage += _NowWeaPon._MaxAttackDamage;
        _AttackMotion.SetItemMotion(_NowWeaPon);
       
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (_NowWeaPon._Type != ItemType.Gun)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _Ani.SetTrigger("AttackCheck");
                    StartCoroutine("Attackmotion");

                }
            }
            else if (_NowWeaPon._Type == ItemType.Gun)
            {
                if (Input.GetMouseButton(0))
                {
                    _Ani.SetTrigger("AttackCheck");

                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (_NowWeaPon._Skill)
                {
                    if (_OneSkillCheck)
                    {

                        _NowWeaPon.Skill();
                        StartCoroutine(Cooltime(_NowWeaPon._SkillCoolTime));
                    }
                }
            }
           if (_NowWeaPon._Type == ItemType.Gun||_NowWeaPon._Type ==ItemType.OneShot)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    ((Longrange)_NowWeaPon).Reload();
   
                }
            }
        }

       
        //WeaPon.transform.position = rotateCenter + mousePos;
    }
    void AnimationEvent()
    {
       
        StartCoroutine("Attack");
    }
    IEnumerator Attack()
    {
         yield return new WaitForSeconds(((Longrange)_NowWeaPon)._Delay);
        Vector3 _mousePos = Input.mousePosition; //마우스 좌표 저장
        Vector3 _oPosition = transform.position;
        Vector3 target = Camera.main.ScreenToWorldPoint(_mousePos);
        Vector2 dir = (target - _oPosition);
        float rotateDegree = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;

        ((Longrange)_NowWeaPon).FireBulet(_oPosition, rotateDegree);
    }
    public void SetAttackSpeed(float AttackSpeed)
    {

        _Ani.speed = _Ani.speed + (_Ani.speed*(AttackSpeed/100));
        
    }

 
    IEnumerator Attackmotion()
    {
        if (_NowWeaPon._Type == ItemType.Sword)
        {
            if (!_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                yield return new WaitForSeconds(0.1f);
                _AttackMotion._Attack();
            }
            else if (!_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            {
                yield return new WaitForSeconds(0.1f);
                _AttackMotion._Attack();
            }
        }
        else if (_NowWeaPon._Type == ItemType.Spear)
        {
            if (!_Ani.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                _AttackMotion._Attack();
                yield return null;
            }
        }
        else if (_NowWeaPon._Type == ItemType.Gun|| _NowWeaPon._Type == ItemType.OneShot)
        {
            yield return null;
        }
    }
    IEnumerator Cooltime(float CoolTime)
    {
        _OneSkillCheck = false;
        _Cooltime.color = new Color(0, 0, 0, 0.6f);
            while (_Cooltime.fillAmount > 0)
            {
                _Cooltime.fillAmount -= 1 * Time.deltaTime / CoolTime;

                yield return null;

            }
       
        yield return new WaitForFixedUpdate();
        _NowWeaPon._Skill = true;
        _OneSkillCheck = true;
    }
}
