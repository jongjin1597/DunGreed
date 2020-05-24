using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellBossLaser : MonoBehaviour
{
    Rigidbody2D _rigid;
    float Speed = 5.0f;
    public bool Fire = false;
    Vector3 targetPosition;

    public Animator[] _anim;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

        if (Fire)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);

            if (this.transform.position == targetPosition)
            {
                _anim[0].SetTrigger("Fire");
                for (int i = 1; i < 3; i++)
                {
                    StartCoroutine("StartAnimation", i);
                }
                Fire = false;
            }
        }
        else if (!Fire)
        {
            targetPosition = new Vector3(transform.position.x, Player.GetInstance.transform.position.y, 0);
        }
    }

    IEnumerator StartAnimation(int i)
    {
        yield return new WaitForSeconds(0.8f);
        _anim[i].SetTrigger("Fire");

    }
}