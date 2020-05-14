using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelBow : MonoBehaviour
{
    AnemyBullet anemybullet;
    public SpriteRenderer _Renderer;

    Animator _Anim;

    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머


    public Transform Skel;
    public float _Radius;

    void Awake()
    {
        anemybullet = GetComponentInChildren<AnemyBullet>();
        _Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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
             

        Vector3 dir = (Player.GetInstance.transform.position - Skel.transform.position);
        this.transform.position = Skel.transform.position + (dir.normalized*_Radius);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    public void AnimationEvent()
    {
        Vector2 dir = (Player.GetInstance.transform.position - this.transform.position);
        float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        anemybullet.ShootControl(this.transform.position, angle);
    }
}
