using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadGiantBat : cCharacter
{
    AnemyBullet anemybullet;
<<<<<<< HEAD
  
=======
    SpriteRenderer _Renderer;
    Animator Anim;

>>>>>>> 2253c268ee9a7502bab3cad140033721bb27d7fe
    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머

    protected override void Awake()
    {
        base.Awake();

        anemybullet = GetComponentInChildren<AnemyBullet>();
    }

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
