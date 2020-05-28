using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellBossLaser : MonoBehaviour
{

    float Speed = 5.0f;
    public bool Fire = false;
    Vector3 targetPosition;
    BoxCollider2D _HitBox;
    public int count;
    public int _Damage;
    public Animator[] _anim;
    [HideInInspector]
    public SkellBossLaser _SkellLaser;
    private void Awake()
    {
        _HitBox = GetComponent<BoxCollider2D>();
        _HitBox.enabled = false;
        _Damage = 9;
    }
    private void FixedUpdate()
    {
        
        if (Fire)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);

            if (this.transform.position == targetPosition)
            {
                _anim[0].SetTrigger("Fire");
                for (int i = 1; i < 3; i++)
                {
                    _HitBox.enabled = true;
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

    void AnimationEvent()
    {
    
   
            count += 2;
        if (count < 3)
        {
            _SkellLaser.Fire = true;

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.GetInstance.HIT(_Damage);
            _HitBox.enabled = false;
        }
    }
}
