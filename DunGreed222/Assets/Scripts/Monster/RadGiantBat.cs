using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadGiantBat : MonoBehaviour
{
    AnemyBullet anemybullet;
    SpriteRenderer _Renderer;
    Animator Anim;

    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머
 
    void Awake()
    {
        _Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anemybullet = GetComponentInChildren<AnemyBullet>();
        Anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer > shootDelay) //쿨타임이 지났는지
        {
            Anim.SetTrigger("Fire");
            
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
        for(int i = 0; i < 10; ++i)
        {
            Vector3 dirVec = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / 10), Mathf.Sin(Mathf.PI * 2 * i / 10));
            dirVec += this.transform.position;
            Vector2 dir = (Player.GetInstance.transform.position - this.transform.position);
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            anemybullet.ShootControl(dirVec, angle);
        }
       
    }
}
