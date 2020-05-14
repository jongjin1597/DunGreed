using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovibos : MonoBehaviour
{
    Rigidbody2D _rigid;
    Animator _Anim;
    Vector2 dir;
    SpriteRenderer _Renderer;

    float speed = 6f;

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
       

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _Anim.SetBool("Run", true);
            dir = (Player.GetInstance.transform.position - this.transform.position);
            _rigid.velocity = new Vector2(dir.normalized.x * speed, _rigid.position.y);
        }
    }

}
