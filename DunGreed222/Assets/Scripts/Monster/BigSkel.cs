﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSkel : cShortMonster
{
    Rigidbody2D _rigid;
    Vector2 dir;

    //float runDelay = 2f; //런 딜레이
    //float runTimer = 0; //런 타이머
    float speed = 2f;

    float Chack = 0f;
    BoxCollider2D _AttackBox;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        _AttackBox = transform.GetChild(2).GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _Anim = GetComponent<Animator>();

        _Clip.Add(Resources.Load<AudioClip>("Sound/swing3")); 
        _Anim.SetBool("Run", true);
        _MaxHP = 75;
        _currnetHP = 75;
        _Defense = 1;
        _AttackDamage = 7;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Chack += Time.deltaTime;
        dir = (Player.GetInstance.transform.position - this.transform.position);

        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("BigWhiteSkelMove"))
        {
            _rigid.velocity = new Vector2(dir.normalized.x * speed,0);
        }
        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("BigWhiteSkelAttack"))
        {
            _rigid.velocity = Vector2.zero;
        }

        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _HPBarBackGround.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            transform.rotation = Quaternion.identity;
            _HPBarBackGround.transform.rotation = Quaternion.identity;
        }
        if (_currnetHP < 1)
        {
            if (!_isDie)
            {
                Die(this.gameObject);
                _isDie = true;
            }
        }

    }
    void Attack()
    {
        if (Chack >= 4f)
        {
            _AttackBox.enabled = true;
            StartCoroutine(BoxEnabled());
                _Anim.SetTrigger("Attack");
            Chack = 0;
        }
    }
    IEnumerator BoxEnabled()
    {
        yield return new WaitForSeconds(0.5f);
        _AttackBox.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_AttackBox.enabled)
            {
                _Audio.clip=_Clip[2];
                _Audio.Play();
        
                int _dam = _AttackDamage - Player.GetInstance._Defense;
                Player.GetInstance.HIT(_dam);
                _AttackBox.enabled = false;
            }


        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Attack();
        }
        }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);

    }
    public override void DropGold()
    {
        for (int i = 0; i <= 10; ++i)
        {
            int RandomIndex = Random.Range(1, 101);
            if (RandomIndex >= 30 && RandomIndex <= 80)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                obj.transform.position = this.transform.position;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
            else if (RandomIndex >= 80 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_BigGold) as GameObject;
                obj.transform.position = this.transform.position;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
        }
    }
}
