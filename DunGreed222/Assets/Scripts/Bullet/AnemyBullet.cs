using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnemyBullet : MonoBehaviour
{
    public GameObject bullet; //총알프리팹
    
    

    public void ShootControl(Vector3 _position, float _angle)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        Instantiate(bullet, _position, transform.rotation); //총알생성 생성해줍니다.
    }
}
