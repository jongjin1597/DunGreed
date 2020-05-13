using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGhost : MonoBehaviour
{
    AnemyBullet anemybullet;
    SpriteRenderer _Renderer;
    Animator Anim;
    Rigidbody2D _rigid;
    public GameObject Player;

    public float attackDelay = 4f; //어택 딜레이
    float attackTimer = 0; //어택 타이머
    float speed = 0.5f;
    void Awake()
    {
        _Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anemybullet = GetComponentInChildren<AnemyBullet>();
        Anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("LittleGhostAttack"))
        {
            speed = 1.4f;
        }
        else
        {
            speed = 0.5f;
        }
        Vector2 dir = (Player.transform.position - this.transform.position);
        _rigid.velocity = new Vector2(dir.x * speed, dir.y * speed);

        attackTimer += Time.deltaTime;

        if (attackTimer > attackDelay) //쿨타임이 지났는지
        {
            Anim.SetTrigger("Attack");

            attackTimer = 0; //쿨타임 초기화
        }

        if (Player.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
        }
        if (Player.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
        }
    }
}
