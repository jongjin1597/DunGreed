using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelDog : cShortMonster
{
    Rigidbody2D _rigid;
    Vector2 dir;

    float runDelay = 2f; //런 딜레이
    float runTimer = 0; //런 타이머
    float speed = 4f;

    float Chack = 0f;
    BoxCollider2D _Box;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        _MinAtteckDamage = 10;
         _MaxAttackDamage= 12;
        _Box = GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _Anim = GetComponent<Animator>();
        _Renderer = GetComponentInChildren<SpriteRenderer>();
        //Footbox = gameObject.GetComponentInChildren<GameObject>();
        _Anim.SetBool("Run", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir = (Player.GetInstance.transform.position - this.transform.position);

        if (_rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(_rigid.position, Vector3.down * 2.5f, 1, LayerMask.GetMask("floor"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.2f)
                {
                    _Anim.SetBool("Attack", false);
                }
            }
        }

        runTimer += Time.deltaTime;
        if (runTimer > runDelay) //쿨타임이 지났는지
        {
            _Anim.SetBool("Run", true);
            runTimer = 0; //쿨타임 초기화
        }

        Chack += Time.deltaTime;

        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("SkelDogRun"))
        {
            _rigid.velocity = new Vector2(dir.normalized.x * speed, _rigid.position.y);
        }

        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
        }
        else if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
        }

    }

    public void Attack()
    {
        if (Chack >= 4f)
        {
     
                _Anim.SetBool("Attack", true);
                _rigid.velocity = Vector2.zero;
                float attackSpeed = 3.0f;
                if (Player.GetInstance.transform.position.x < this.transform.position.x)
                {
                    attackSpeed *= -1;
                }
                _rigid.velocity = new Vector2(attackSpeed, 4f);
            Chack = 0;
        }
    }
    // 플레이어랑 충돌했을때 플레이어한테 데미지 입히기위함
    //공격용박스
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_Box.enabled)
            {
                int Attack = Random.Range(_MinAtteckDamage, _MaxAttackDamage);
                int _dam = Attack - Player.GetInstance._Defense;
                Player.GetInstance._health.MyCurrentValue -= _dam;
            }
            }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        //피격판정및 공격판정 스타트용
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }
}
