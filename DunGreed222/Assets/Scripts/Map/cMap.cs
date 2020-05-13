using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMap : MonoBehaviour
{
    //플레이어가 있나여부
    private bool _isPlayer=false;
    //현제맵에 몬스터가 있나여부
    private bool _isMonster=true;

    public int _MapNumber;

    //문여닫이용 델리게이트, 열린맵 리스트에 집어넣기용
    public delegate void _DoorOpen();
    public _DoorOpen _Door;

    // Update is called once per frame
    void Update()
    {
        if (_isPlayer)
        {
            if (!_isMonster)
            {
                _Door();
            }
        }

    }

    public void SetPlayer(bool _SetPlayer)
    {
        _isPlayer = _SetPlayer;
    }
}
