using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaurs : cShortMonster
{
    Rigidbody2D _rigid;
    Vector2 dir;

    float speed = 8f;
    bool dash = false;
    float Chack = 0f;

    float attackDelay = 2f; //런 딜레이
    float attackTimer = 0; //런 타이머

    BoxCollider2D _AttackBox;
    protected override void Awake()
    {
        base.Awake();
        _AttackBox = transform.GetChild(2).GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Chack += Time.deltaTime;

        attackTimer += Time.deltaTime;
        if (attackTimer > attackDelay && dash == false) //쿨타임이 지났는지
        {
            _Anim.SetTrigger("Attack");
            attackTimer = 0; //쿨타임 초기화
        }


        if (_rigid.velocity == Vector2.zero)
        {
            _Anim.SetBool("Run", false);
        }

        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("MinotaursCharge"))
        {
            dash = true;
        }
        else
        {
            dash = false;
        }

        if (!dash)
        {
            if (Player.GetInstance.transform.position.x < this.transform.position.x)
            {
                _Renderer.flipX = true;
            }
            if (Player.GetInstance.transform.position.x > this.transform.position.x)
            {
                _Renderer.flipX = false;
            }
        }
    }
    public void Dash()
    {
        if (Chack >= 8f)
        {
            //if (other.gameObject.CompareTag("Player"))
            //{
            _AttackBox.enabled = true;
            StartCoroutine(BoxEnabled());
                _Anim.SetBool("Run", true);
                dir = (Player.GetInstance.transform.position - this.transform.position);
                float dashSpeed = 1.5f;
                if (Player.GetInstance.transform.position.x < this.transform.position.x)
                {
                    dashSpeed *= -1;
                }
                _rigid.velocity = new Vector2((dir.normalized.x * speed) + dashSpeed, _rigid.position.y);
           // }
            Chack = 0;
        }
    }
    IEnumerator BoxEnabled()
    {
        yield return new WaitForSeconds(1f);
        _AttackBox.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_AttackBox.enabled)
            {
                int Attack = Random.Range(_MinAtteckDamage, _MaxAttackDamage);
                int _dam = Attack - Player.GetInstance._Defense;
                Player.GetInstance.HIT(_dam);
                _AttackBox.enabled = false;
            }


        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //공격판정 스타트용
        if (other.gameObject.CompareTag("Player"))
        {
            Dash();
        }
    }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);
    
    }
}
