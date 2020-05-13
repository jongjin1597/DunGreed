﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBat : MonoBehaviour
{
    AnemyBullet anemybullet;
    SpriteRenderer _Renderer;
    Animator Anim;
    public GameObject Player;

    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머
    Vector2 dir;

    void Awake()
    {
        _Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anemybullet = GetComponentInChildren<AnemyBullet>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer > shootDelay) //쿨타임이 지났는지
        {
            Anim.SetTrigger("Fire");

            shootTimer = 0; //쿨타임 초기화
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

    public void AnimationEvent()
    {
        dir = (Player.transform.position - this.transform.position);
        for (float i = 0f; i <= 0.8f; i += 0.4f)
        {
            Invoke("Attack", i);
        }
    }

    public void Attack()
    {
        for (int i = -1; i < 2; ++i)
        {
           
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            angle += 25 * i;
            anemybullet.ShootControl(this.transform.position, angle);
        }
    }
}
