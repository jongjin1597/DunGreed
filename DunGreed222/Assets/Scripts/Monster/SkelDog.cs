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
    BoxCollider2D _AttackBox;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        _AttackDamage = 5;
               _AttackBox = transform.GetChild(2).GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _Anim = GetComponent<Animator>();
        _Renderer = GetComponentInChildren<SpriteRenderer>();
        //Footbox = gameObject.GetComponentInChildren<GameObject>();
        _Anim.SetBool("Run", false);
        _MaxHP = 20;
        _currnetHP = 20;
        _Defense = 0;
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
            _rigid.velocity = new Vector2(dir.normalized.x * speed, 0);
        }

        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
        }
        else if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
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

    public void Attack()
    {
        if (Chack >= 4f)
        {
            _AttackBox.enabled = true;
            StartCoroutine(BoxEnabled());
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

    IEnumerator BoxEnabled()
    {
        yield return new WaitForSeconds(0.5f);
        _AttackBox.enabled = false;
    }
    // 플레이어랑 충돌했을때 플레이어한테 데미지 입히기위함
    //공격용박스
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_AttackBox.enabled)
            {

                int _dam = _AttackDamage - Player.GetInstance._Defense;
                Player.GetInstance.HIT(_dam);
                _AttackBox.enabled = false;
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
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);

    }
    public override void DropGold()
    {
        for (int i = 0; i <= 3; ++i)
        {
            int RandomIndex = Random.Range(1, 101);
            if (RandomIndex >= 50 && RandomIndex <= 98)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                obj.transform.position = this.transform.position;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
            else if (RandomIndex >= 98 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_BigGold) as GameObject;
                obj.transform.position = this.transform.position;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
        }
    }
}
