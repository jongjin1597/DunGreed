using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMapManager : MonoBehaviour
{
    //현제 맵
    public Transform _NowMap;
    //열린맵 리스트
    public List<Transform> _OpenMapList = new List<Transform>();
    //닫힌맵리스트
    public List<Transform> _CloseMapList = new List<Transform>();
    //현제맵에 몬스터가 있나여부
    public bool _isMonster = true;
    //맵에있는 몬스터
    private List<Transform> _MonsterList = new List<Transform>();
    //문리스트
    public List<cDoor> _DoorList = new List<cDoor>(); 
    //문여닫이용 델리게이트, 열린맵 리스트에 집어넣기용
    public delegate void _Door();
    //문닫기
    public _Door _DoorClose;
    //문열기
    public _Door _DoorOpen;
      private void Awake()
    {
        _NowMap = transform.GetChild(0);
        _OpenMapList.Add(transform.GetChild(1));
        for(int i =2; i < transform.childCount; ++i)
        {
            _CloseMapList.Add(transform.GetChild(i));
        }
        SetNowMap(_OpenMapList[0]);

    }
    void Update()
    {
        if (_NowMap != null) 
        {
            if (_MonsterList.Count == 0)
            {
                _isMonster = false;
            }
            if (!_isMonster)
            {
                _DoorOpen?.Invoke();
            }
            else if (_isMonster)
            {
                _DoorClose?.Invoke();
            }
        }
    }
    public void ClearDoor(Transform NowMap)
    {
        if (NowMap != null)
        {
            Transform Door = NowMap.Find("Door");
            _DoorList.Clear();
            for (int i = 0; i < Door.transform.childCount; ++i)
            {
                _DoorOpen -= Door.transform.GetChild(i).GetComponent<cDoor>().Open;
                _DoorClose -= Door.transform.GetChild(i).GetComponent<cDoor>().Close;
            }
        }
    }
    public void SetNowMap(Transform NowMap)
    {
        if (NowMap != null)
        {
            ClearDoor(NowMap);
            _NowMap = NowMap;

            _MonsterList.Clear();
            //GameObject Monster = transform.GetChild(0).Find("Monster").GetComponent<GameObject>();
            //for (int i = 0; i < Monster.transform.childCount; ++i)
            //{
            //    _MonsterList.Add(Monster.transform.GetChild(i).gameObject);
            //}
            //if(_MonsterList.Count == 0)
            //{
            //    _isMonster = false;
            //}
            //else if (_MonsterList.Count != 0)
            //{
            //    _isMonster = true;
            //}
            Transform Door = _NowMap.Find("Door");
            for (int i = 0; i < Door.transform.childCount; ++i)
            {
                _DoorList.Add(Door.transform.GetChild(i).GetComponent<cDoor>());

            }
            for (int i = 0; i < Door.transform.childCount; ++i)
            {
                _DoorOpen += Door.transform.GetChild(i).GetComponent<cDoor>().Open;
                _DoorClose += Door.transform.GetChild(i).GetComponent<cDoor>().Close;
            }
        }
    }
}
