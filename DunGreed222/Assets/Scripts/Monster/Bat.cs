﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    Rigidbody2D _rigid;
    SpriteRenderer _Renderer;
    public int _moveRangeX;
    public int _moveRangeY;
    //public GameObject Player;
    void Awake()
    {
        _Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        _rigid = GetComponent<Rigidbody2D>();
        Invoke("MoveRange", 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigid.velocity = new Vector2(_moveRangeX, _moveRangeY);

        //if (Player.transform.position.x < this.transform.position.x)
        //{
        //    _Renderer.flipX = true;
        //}
        //if (Player.transform.position.x > this.transform.position.x)
        //{
        //    _Renderer.flipX = false;
        //}
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "floor" || other.gameObject.tag == "Wall")
        {
            _moveRangeX *= -1 ;
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
}