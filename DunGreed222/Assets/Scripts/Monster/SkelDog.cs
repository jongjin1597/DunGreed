using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelDog : MonoBehaviour
{
    Rigidbody2D _rigid;
    Animator _Anim;
    Vector2 dir;
    SpriteRenderer _Renderer;

    float runDelay = 2f; //런 딜레이
    float runTimer = 0; //런 타이머
    float speed = 4f;
    float attackSpeed = 3.0f;

    // Start is called before the first frame update
    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _Anim = GetComponent<Animator>();
        _Renderer = GetComponentInChildren<SpriteRenderer>();
        //Footbox = gameObject.GetComponentInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = (Player.GetInstance.transform.position - this.transform.position);

        runTimer += Time.deltaTime;

        if (runTimer > runDelay) //쿨타임이 지났는지
        {
            _Anim.SetBool("Run", true);
            runTimer = 0; //쿨타임 초기화
        }
        else
        {
            _Anim.SetBool("Run", false);
        }

        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("SkelDogRun"))
        {
            _rigid.velocity = new Vector2(dir.normalized.x * speed, _rigid.position.y);
        }
       

        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
            //attackSpeed *= 1;
        }
        if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
            //attackSpeed *= -1;
        }

        if (_rigid.velocity.y < 0)
        {
            Debug.DrawRay(_rigid.position, dir.normalized * 2.5f, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(_rigid.position, dir.normalized * 2.5f, 1, LayerMask.GetMask("Player"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.0f)
                {
                    //Footbox.enabled = false;
                    _Anim.SetBool("Attack", true);
                    _rigid.velocity = new Vector2(attackSpeed, 3f);
                }
            }
        }
    }

}
