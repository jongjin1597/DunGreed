using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//무기 회전용 스크립트
public class cWeaPonMove : MonoBehaviour
{
    private Vector3 rotateCenter;

    public float _Radius;

    void Update()
    {
        Vector3 _mousePos = Input.mousePosition; //마우스 좌표 저장
        Vector3 _oPosition = transform.position;
        Vector3 target = Camera.main.ScreenToWorldPoint(_mousePos);
        float dy = target.y - _oPosition.y;
        float dx = target.x - _oPosition.x;
        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
        
        rotateCenter = Player.GetInstance.transform.position;
        Vector3 _Position = (target - rotateCenter);
        //Debug.Log("로테이션 센터 좌표: " + rotateCenter);

        _Position = Vector2.ClampMagnitude(_Position, _Radius);
        //Debug.Log("마우스 수정 좌표: " + mousePos);
        this.transform.position = rotateCenter + _Position;
        //WeaPon.transform.position = rotateCenter + mousePos;



        // WeaPon.Attack();
    }
}
