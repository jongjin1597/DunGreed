using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet; //총알프리팹
    public bool canShoot = true; //쏠수 있는 상태
    const float shootDelay = 0.5f; //총알 딜레이
    float shootTimer = 0; //총알 타이머
    Vector2 dir; // 마우스 커서 방향
    float angle;
    


    void Update()
    {
        ShootControl();
    }

    void ShootControl()
    {
        if (canShoot) // 쏠 수 있는 상태인지 검사
        {
            if (shootTimer > shootDelay && Input.GetMouseButtonDown(0)) //쿨타임이 지났는지, 공격키 검사
            {
                dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position);
                angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
                Instantiate(bullet, transform.position, transform.rotation); //총알생성 생성해줍니다.
                shootTimer = 0; //쿨타임 초기화
            }
            shootTimer += Time.deltaTime;
        }
    }
}
