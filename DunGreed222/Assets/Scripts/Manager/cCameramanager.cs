﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCameramanager : cSingleton<cCameramanager>
{

    //따라갈 타겟
    public GameObject _Target;
    //이동속도
    private float _MoveSpeed=5;
    //타겟위치
    private Vector3 _TargetPosition;
    //카메라가 나가지못할 영역
    private Collider2D Bound;
    //최소 영역
    private Vector3 _minBound;
    //최대 영역
    private Vector3 _maxBound;
    //카메라의 반너비,반높이 값을 지닐변수
    private float halfWidth;
    private float halfHeight;
    //카메라 의 반높이 값을 구할 속성을 이용하기 위한변수
    private Camera theCamera;
    
   protected override void Awake()
    {
        base.Awake();
            theCamera = GetComponent<Camera>();
            Bound = GameObject.Find("Bound").GetComponent<Collider2D>();
            _minBound = Bound.bounds.min;
            _maxBound = Bound.bounds.max;
            halfHeight = theCamera.orthographicSize;
            halfWidth = halfHeight * Screen.width / Screen.height;
    }
  
    void Update()
    {
        if(_Target.gameObject != null)
        {
            _TargetPosition.Set(_Target.transform.position.x, _Target.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, _TargetPosition, _MoveSpeed * Time.deltaTime);
            //카메라 영역조절
            float clampedX = Mathf.Clamp(this.transform.position.x, _minBound.x + halfWidth, _maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, _minBound.y + halfHeight, _maxBound.y - halfHeight);
            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
        
    }
    //카메라영역설정
    public void SetBound(Collider2D newBound)
    {
        Bound = newBound;
        _minBound = Bound.bounds.min;
        _maxBound = Bound.bounds.max;
    }
}