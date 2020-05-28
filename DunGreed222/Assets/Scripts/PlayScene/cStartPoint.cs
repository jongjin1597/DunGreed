using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//씬이동시 플레이어 시작지점
public class cStartPoint : MonoBehaviour
{
    public int _startPoint;// 맵이동시 캐릭터 이동위치
    Texture2D _CursorTexture;
    Texture2D _ClickTexture;
    
    void Start()
    {
        _CursorTexture = Resources.Load<Texture2D>("UI/ShootingCursor1");
        _ClickTexture = Resources.Load<Texture2D>("UI/ShootingCursor2");
        _startPoint = 1;
        if (_startPoint == Player.GetInstance._CurrentMapNum)
        {
            cCameramanager.GetInstance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, cCameramanager.GetInstance.transform.position.z);
            Player.GetInstance.transform.position = this.transform.position;
            Player.GetInstance.MoveMap = false;
            cGameManager.GetInstance.SetBackGruond("Sound/1.JailField");
            cGameManager.GetInstance.SetCursor(_CursorTexture, _ClickTexture);
        }
        _startPoint = 0;
    }

}
