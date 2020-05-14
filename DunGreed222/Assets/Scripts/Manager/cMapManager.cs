using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMapManager : MonoBehaviour
{
    //현제 맵
    private Transform _NowMap =null;
    //열린맵 리스트
    public List<Transform> _OpenMapList = new List<Transform>();
    //닫힌맵리스트
    public List<Transform> _CloseMapList = new List<Transform>();
    //현제맵에 몬스터가 있나여부
    private bool _isMonster = true;
    //맵에있는 몬스터
    private List<Transform> _MonsterList = new List<Transform>();
    //문리스트
    public List<cDoor> _DoorList = new List<cDoor>(); 
    //문여닫이용 델리게이트, 열린맵 리스트에 집어넣기용
    private delegate void _Door();
    //문닫기
    private _Door _DoorClose;
    //문열기
    private _Door _DoorOpen;
      private void Awake()
    {

        _OpenMapList.Add(transform.GetChild(0));
        for(int i =1; i < transform.childCount; ++i)
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
    public void DoorSetting(Transform NowMap)
    {
        //현제맵에있는 문 딜리게이트에서 제거
        if (_NowMap != null) 
        {
            Transform _NowMapDoor = _NowMap.Find("Door");
            if (_NowMapDoor.transform.childCount != 0)
            {
                for (int i = 0; i < _NowMapDoor.transform.childCount; ++i)
                {
                    _DoorOpen -= _NowMapDoor.transform.GetChild(i).GetComponent<cDoor>().Open;
                    _DoorClose -= _NowMapDoor.transform.GetChild(i).GetComponent<cDoor>().Close;
                }
            }
        }
        //이동한 맵 문 델리게이트 추가 및 문리스트에 추가
        Transform Door = NowMap.Find("Door");
        _DoorList.Clear();
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
    public void SetNowMap(Transform NowMap)
    {
        if (NowMap != null)
        {
            DoorSetting(NowMap);
            _NowMap = NowMap;
            _MonsterList.Clear();
            //Transform Monster = _NowMap.GetChild(0).Find("Monster");
            //if (Monster.transform.childCount != 0)
            //{
            //    for (int i = 0; i < Monster.transform.childCount; ++i)
            //    {
            //        _MonsterList.Add(Monster.transform.GetChild(i));
            //    }
            //}
            //if (_MonsterList.Count == 0)
            //{
            //    _isMonster = false;
            //}
            //else if (_MonsterList.Count != 0)
            //{
            //    _isMonster = true;
            //}
          
        }
    }
}
