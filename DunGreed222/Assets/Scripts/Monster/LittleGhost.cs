using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGhost : cShortMonster
{

    Rigidbody2D _rigid;

    public float attackDelay = 4f; //어택 딜레이
    float attackTimer = 0; //어택 타이머
    float speed = 0.5f;
    BoxCollider2D _AttackBox;
    protected override void Awake()
    {
        base.Awake();
        _AttackBox = transform.GetChild(1).GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("LittleGhostAttack"))
        {
            speed = 3f;
        }
        else
        {
            speed = 1.5f;
        }
        Vector2 dir = (Player.GetInstance.transform.position - this.transform.position);

        _rigid.velocity = new Vector2(dir.normalized.x * speed, dir.y * speed);

        attackTimer += Time.deltaTime;

        if (attackTimer > attackDelay) //쿨타임이 지났는지
        {
            _AttackBox.enabled = true;
            _Anim.SetTrigger("Attack");

            StartCoroutine(BoxEnabled());
            attackTimer = 0; //쿨타임 초기화
        }

        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
        }
        if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
        }
    }
    IEnumerator BoxEnabled()
    {
        yield return new WaitForSeconds(0.3f);
        _AttackBox.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);

    }
    public override void DropGold()
    {
        for (int i = 0; i <= 3; ++i)
        {
            int RandomIndex = Random.Range(1, 101);
            if (RandomIndex >= 60 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
          
        }
    }
}