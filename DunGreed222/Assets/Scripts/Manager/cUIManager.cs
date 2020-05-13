﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cUIManager : cSingleton<cUIManager>
{
    private cDash _Dash;
    private Text _Gold;
    private Text _Food;
    private Image _Foodgauge;
    private cWeaPonUI _WeaPonSlot;

    protected override void Awake()
    {
        base.Awake();
        _Dash = transform.GetChild(0).GetComponent<cDash>();
        _Gold = transform.GetChild(3).GetChild(0).GetComponent<Text>();
        _Food =transform.GetChild(4).GetChild(0).GetComponent<Text>();
         _Foodgauge =transform.GetChild(4).GetChild(1).GetComponent<Image>();
        _WeaPonSlot = transform.GetChild(5).GetComponent<cWeaPonUI>();
        cGameManager.GetInstance._DeleGateGold += SetGold;

    }
    //대쉬게이지 리턴
    public cDash GetDash()
    {
        return _Dash;
    }
    //골드UI셋팅
    public void SetGold()
    {
        _Gold.text = cGameManager.GetInstance.Gold.ToString();
    }

    //무기슬롯UI리턴
    public cWeaPonUI GetWeaPonSlot()
    {
        return _WeaPonSlot;
    }

}