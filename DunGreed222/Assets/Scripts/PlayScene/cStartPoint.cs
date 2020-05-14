using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//씬이동시 플레이어 시작지점
public class cStartPoint : MonoBehaviour
{
    public int _startPoint;// 맵이동시 캐릭터 이동위치

    void Start()
    {
        _startPoint = 1;
        if (_startPoint == Player.GetInstance._CurrentMapNum)
        {
            cCameramanager.GetInstance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, cCameramanager.GetInstance.transform.position.z);
            Player.GetInstance.transform.position = this.transform.position;
        }
    }

}
