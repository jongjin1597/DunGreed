using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banshee : cCharacter
{
    AnemyBullet anemybullet;

    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머

   protected override void Awake()
    {
        base.Awake();
        anemybullet = GetComponentInChildren<AnemyBullet>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer > shootDelay) //쿨타임이 지났는지
        {
            _Anim.SetTrigger("Fire");

            shootTimer = 0; //쿨타임 초기화
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

    public void AnimationEvent()
    {
        for (int i = 0; i < 12; ++i)
        {
            Vector3 dirVec = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / 12), Mathf.Sin(Mathf.PI * 2 * i / 12));
            dirVec += this.transform.position;
            float angle = 30 * i;
            anemybullet.ShootControl(dirVec, angle);
        }
    }
}