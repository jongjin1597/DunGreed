using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cNPC : MonoBehaviour
{
    //보일 F버튼
    protected GameObject _ButtonF;
    //플레이어랑 충돌중인지
    protected bool _isPlayer=false;

    protected virtual void Awake()
    {
        _ButtonF = transform.GetChild(0).gameObject;
    }
}
