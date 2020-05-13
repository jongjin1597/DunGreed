using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMapManager : MonoBehaviour
{
    //현제 맵
    private GameObject _NowMap;
    //열린맵 리스트
    public List<GameObject> _OpenMapList = new List<GameObject>();
    //닫힌맵리스트
    public List<GameObject> _CloseMapList = new List<GameObject>();
    //현제맵에 몬스터가 있나여부
    private bool _isMonster = true;
    //맵에있는 몬스터
    private List<GameObject> _MonsterList = new List<GameObject>();
    //문리스트
    private List<GameObject> _DoorList = new List<GameObject>(); 
    //문여닫이용 델리게이트, 열린맵 리스트에 집어넣기용
    public delegate void _Door();
    //문닫기
    public _Door _DoorClose;
    //문열기
    public _Door _DoorOpen;
      private void Awake()
    {
 
        _NowMap = transform.GetChild(0).GetComponent<GameObject>();
        _OpenMapList.Add(transform.GetChild(1).GetComponent<GameObject>());
        for(int i =2; i < transform.childCount; ++i)
        {
            _CloseMapList.Add(transform.GetChild(i).GetComponent<GameObject>());
        }
        _NowMap = _OpenMapList[0];
        //GameObject Door = transform.GetChild(0).Find("Door").GetComponent<GameObject>();
        //for (int i =0; i< Door.transform.childCount; ++i)
        //{
        //    _DoorList.Add(Door.transform.GetChild(i).gameObject);
        //}

    }
    void Update()
    {
        if (_NowMap != null) 
        {
            if(_MonsterList.Count == 0)
            {
                _isMonster = false;
            }
            if (!_isMonster)
            {
                _DoorOpen();
            }
            else if (_isMonster)
            {
                _DoorClose();
            }
        }
    }
    public void SetNowMap(GameObject NowMap)
    {
        _NowMap = NowMap;
        if (_NowMap != null)
        {
            _MonsterList.Clear();
            GameObject Monster = transform.GetChild(0).Find("Monster").GetComponent<GameObject>();
            for (int i = 0; i < Monster.transform.childCount; ++i)
            {
                _MonsterList.Add(Monster.transform.GetChild(i).gameObject);
            }
            if(_MonsterList.Count == 0)
            {
                _isMonster = false;
            }
            else if (_MonsterList.Count != 0)
            {
                _isMonster = true;
            }
            _DoorList.Clear();
            GameObject Door = transform.GetChild(0).Find("Door").GetComponent<GameObject>();
            for (int i = 0; i < Door.transform.childCount; ++i)
            {
                _DoorList.Add(Door.transform.GetChild(i).gameObject);
            }
        }
    }
}
