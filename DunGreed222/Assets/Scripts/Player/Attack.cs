using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
  


   // private Vector3 dir;


    private Vector3 rotateCenter;
    private Vector3 mousePos;

    private Camera camera;

    public float _Radius;

    private void Awake()
    {
     
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

   

    void Update()
    {
        rotateCenter = Player.GetInstance.transform.position;
        //Debug.Log("로테이션 센터 좌표: " + rotateCenter);
        mousePos = Input.mousePosition;
        //Debug.Log("마우스 좌표: " + mousePos);
        mousePos = camera.ScreenToWorldPoint(mousePos);
        mousePos = (mousePos - rotateCenter);

        mousePos = Vector2.ClampMagnitude(mousePos, _Radius);
        //Debug.Log("마우스 수정 좌표: " + mousePos);
        this.transform.position = rotateCenter + mousePos;
        //WeaPon.transform.position = rotateCenter + mousePos;
       


       // WeaPon.Attack();
    }
}
