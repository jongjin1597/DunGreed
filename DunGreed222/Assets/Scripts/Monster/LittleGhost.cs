using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleGhost : MonoBehaviour
{
    AnemyBullet anemybullet;
    SpriteRenderer _Renderer;
    Animator Anim;
    Rigidbody2D _rigid;

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
            Anim.SetTrigger("Attack");

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
}
