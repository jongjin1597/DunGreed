using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banshee : cLongLangeMonster
{
  

    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머

   protected override void Awake()
    {
        base.Awake();
        _MaxBullet = 12;
        for (int i = 0; i < _MaxBullet; ++i)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/BansheeBullet")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 5.0f;
            _Bullet._Damage = Random.Range(11, 14);
            _Bullet._Player = false;
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.transform.SetParent(transform);
            _Bullet.gameObject.SetActive(false);

            _BulletPoll.Add(_Bullet);
        }

    }

    // Update is called once per frame
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
        for (int i = 0; i < _MaxBullet; ++i)
        {
           // Vector3 dirVec = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / 12), Mathf.Sin(Mathf.PI * 2 * i / 12));
            Vector3 dirVec = this.transform.position;
            float angle = 30 * i;
            FireBulet(dirVec,angle);
        }
    }
        public  void FireBulet(Vector3 Dir, float _angle)
    {
      
            //발사되어야할 순번의 총알이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
            if (_BulletPoll[_CurBulletIndex].gameObject.activeSelf)
            {
                return;
            }


            _BulletPoll[_CurBulletIndex].transform.position = Dir;

            _BulletPoll[_CurBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);
            _BulletPoll[_CurBulletIndex]._transform.rotation = Quaternion.Euler(0f, 0f, 0);
        _BulletPoll[_CurBulletIndex].gameObject.SetActive(true);

          StartCoroutine("ActiveBullet", _BulletPoll[_CurBulletIndex]);
            if (_CurBulletIndex >= _MaxBullet - 1)
            {
                _CurBulletIndex = 0;
            }
            else
            {
                _CurBulletIndex++;
            }

    }

    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.gameObject.SetActive(false);
    }
}