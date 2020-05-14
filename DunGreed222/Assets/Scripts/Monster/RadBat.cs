using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadBat : MonoBehaviour
{
    AnemyBullet anemybullet;
    SpriteRenderer _Renderer;

    Animator Anim;
    Rigidbody2D _rigid;
    public int _moveRangeX;
    public int _moveRangeY;

    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머
    void Awake()
    {
        _Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        anemybullet = GetComponentInChildren<AnemyBullet>();
        Anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        Invoke("MoveRange", 1);
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
        

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Bat_Rad"))
        {
            _rigid.velocity = new Vector2(_moveRangeX, _moveRangeY);
        }
        else if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Bat_RadAttack"))
        {
            _rigid.velocity = Vector2.zero;
        }

        if(Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
        }
        if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "floor" || other.gameObject.tag == "BaseLine")
        {
            _moveRangeX *= -1;
            _moveRangeY *= -1;
            CancelInvoke();
            Invoke("MoveRange", 0.1f);
        }
    }

    //재귀 함수
    void MoveRange()
    {
        _moveRangeX = Random.Range(-2, 3);
        _moveRangeY = Random.Range(-2, 3);

        float naxtMoveRange = Random.Range(2f, 4f);
        Invoke("MoveRange", naxtMoveRange);
    }

    public void AnimationEvent()
    {
        for (int i = 0; i < 10; ++i)
        {
            Vector2 dir = (Player.GetInstance.transform.position - this.transform.position);
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            anemybullet.ShootControl(this.transform.position, angle);
        }

    }
}
