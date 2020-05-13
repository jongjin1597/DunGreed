using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//씬이동시 플레이어 시작지점
public class cStartPoint : MonoBehaviour
{
    public string _startPoint;// 맵이동시 캐릭터 이동위치
    private Player _Player;
    private cCameramanager _Camera;

    void Start()
    {
        _startPoint = "Start";
        _Player = FindObjectOfType<Player>();
        _Camera = FindObjectOfType<cCameramanager>();
        if (_startPoint == _Player._CurrentMapName)
        {
            _Camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, _Camera.transform.position.z);
            _Player.transform.position = this.transform.position;
        }
    }

}
