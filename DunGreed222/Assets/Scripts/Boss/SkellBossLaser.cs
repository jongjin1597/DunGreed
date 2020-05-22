using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellBossLaser : MonoBehaviour
{
    Rigidbody2D _rigid;

    public Animator[] _anim;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

    }

    public void laserPosition()
    {
       _rigid.MovePosition(transform.position + new Vector3(_rigid.velocity.x , Player.GetInstance.transform.position.y) * Time.deltaTime);
        _anim[0].SetTrigger("Fire");
        for (int i = 1; i < 3; i++)
        {

            StartCoroutine("StartAnimation", i);
        }
    }

    IEnumerator StartAnimation(int i)
    {
        yield return new WaitForSeconds(0.8f);
        _anim[i].SetTrigger("Fire");
    }
}
