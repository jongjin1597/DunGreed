using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//게임매니저
public class cGameManager : cSingleton<cGameManager>
{
    //현제 장착중인무기
    public cWeaPon _WeaPon;
    public cOptionPanel _Option;
    public GameObject _StopPanel;
    //골드
    private static float _Gold = 0;
   public float Gold { set{ _Gold = value; } get { return _Gold; }}
    private int _WeaponNum;
    //골드 딜리게이트
    public delegate void Gold_Del();
    public Gold_Del _DeleGateGold;
    AudioSource _BackGroundSound;
    AudioClip _BackGroundClip;

    //마우스커서
    public Texture2D _CursorTexture;
    public Texture2D _ClickTexture;
    public CursorMode _CursorMode = CursorMode.Auto;
    protected override void Awake()
    {
        base.Awake();
        _BackGroundSound = GetComponent<AudioSource>();
        _BackGroundClip = Resources.Load<AudioClip>("Sound/0.Town");
        _BackGroundSound.clip = _BackGroundClip;
        _BackGroundSound.Play();
        _WeaponNum = 0;
        _CursorTexture = Resources.Load<Texture2D>("UI/BasicCursor");
        _ClickTexture = Resources.Load<Texture2D>("UI/BasicCursor");
        Cursor.SetCursor(_CursorTexture, Vector2.zero, _CursorMode);
        _StopPanel.SetActive(false);
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
 
            if(_WeaponNum == 0)
            {
                _WeaponNum = 1;
            }
            else if (_WeaponNum == 1)
            {
                _WeaponNum = 0;
            }
            cUIManager.GetInstance.GetWeaPonSlot().ChangeSlot();
            
            _WeaPon.SetWeaPon(cInventory.GetInstance.GetWeaponSlot(_WeaponNum)._item);
        }
        //인벤토리 열기
        if(Input.GetKeyDown(KeyCode.I))
        {
            cInventory.GetInstance.SetActive();
        }
        //ESC누를시 일시정지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _StopPanel.SetActive(true);
            Time.timeScale = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(_ClickTexture, Vector2.zero, _CursorMode);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(_CursorTexture, Vector2.zero, _CursorMode);
        }
    }

    public void SetBackGruond(string path)
    {
        _BackGroundClip = Resources.Load<AudioClip>(path);
        _BackGroundSound.clip = _BackGroundClip;
        _BackGroundSound.Play();
    }
    public void SetCursor(Texture2D Cuser, Texture2D Click)
    {
        _CursorTexture = Cuser;
        _ClickTexture = Click;
        Cursor.SetCursor(_CursorTexture, Vector2.zero, _CursorMode);
    }
    public void CloseOption()
    {
        Time.timeScale = 1;
        _StopPanel.SetActive(false);
    }
    public void SetOptionPanel()
    {
        
        _StopPanel.SetActive(false);
        _Option.gameObject.SetActive(true);
    }
}
