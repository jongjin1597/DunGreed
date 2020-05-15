using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//게임매니저
public class cGameManager : cSingleton<cGameManager>
{
    //현제 장착중인무기
    public cWeaPon _WeaPon;
    // public cWeaPonUI _UISlot;
    //public Text
    //골드
    private static float _Gold = 40000;
   public float Gold { set{ _Gold = value; } get { return _Gold; }}

    //골드 딜리게이트
    public delegate void Gold_Del();
    public Gold_Del _DeleGateGold;


    protected override void Awake()
    {
        base.Awake();
      
    }
    private void Start()
    {
        _DeleGateGold();
    }

    private void Update()
    {
        //탭누를시 무기 1번 슬롯 2번슬롯 변경
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            cInventory.GetInstance.ChangeWeapon();
            cUIManager.GetInstance.GetWeaPonSlot().ChangeSlot();
            
            _WeaPon.SetWeaPon(cInventory.GetInstance.GetWeaponSlot(0)._item);
        }
        //인벤토리 열기
        if(Input.GetKeyDown(KeyCode.I))
        {
            cInventory.GetInstance.SetActive();
        }
    }



}
