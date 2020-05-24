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
    public List<Transform> _MonsterList = new List<Transform>();
    //문리스트
    private List<cDoor> _DoorList = new List<cDoor>(); 
    //문여닫이용 델리게이트, 열린맵 리스트에 집어넣기용
    private delegate void _Door();
    //문닫기
    private _Door _DoorClose;
    //문열기
    private _Door _DoorOpen;
    //박스 생성될 위치
    private GameObject _Box;
    //노말박스
    private GameObject _NormalBox;
    //레어박스
    private GameObject _RareBox;
    //유니크박스
    private GameObject _UniqueBox;
    //박스 생성안됬을때
    private GameObject _NoonBox;
    //몬스터들 관리할 상위 오브젝트
    private Transform _Monster;
    //페어리위치
    private GameObject _Fairy;
    //작은 페어리
    private GameObject _SmallFairy;
    //큰페어리
    private GameObject _BigFairy;
    //박스 관리용 리스트
    public List<GameObject> _BoxList=new List<GameObject>();
    private void Awake()
    {
        _OpenMapList.Add(transform.GetChild(0));
        for(int i =1; i < transform.childCount; ++i)
        {
            _CloseMapList.Add(transform.GetChild(i));
        }
        SetNowMap(_OpenMapList[0]);

        _NormalBox = Resources.Load("Prefabs/DunguenBox/NormalBox") as GameObject;
        _RareBox = Resources.Load("Prefabs/DunguenBox/RareBox") as GameObject;
        _UniqueBox = Resources.Load("Prefabs/DunguenBox/UniqueBox") as GameObject;
        _NoonBox = Resources.Load("Prefabs/DunguenBox/NoonBox") as GameObject;
        _SmallFairy = Resources.Load("Prefabs/Fairy/FairyS") as GameObject;
        _BigFairy = Resources.Load("Prefabs/Fairy/FairyM") as GameObject;
      
    }
    void Update()
    {
        if (_NowMap != null) 
        {
            if (_MonsterList.Count == 0)
            {
                _isMonster = false;
           
                if (_BoxList.Count==0)
                    SetBox();
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
    //문세팅
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
    //현제 맵세팅
    public void SetNowMap(Transform NowMap)
    {
     
       if  (NowMap != null)
        {
            DoorSetting(NowMap);
            _NowMap = NowMap;
            _MonsterList.Clear();
            _BoxList.Clear();
            if (_NowMap.gameObject.CompareTag("MonsterMap"))
            {
                if (_NowMap.transform.Find("Monster") != null)
                {
                    _Monster = _NowMap.transform.Find("Monster");
                    for (int i = 0; i < _Monster.childCount; ++i)
                    {
                        _MonsterList.Add(_Monster.transform.GetChild(i));
                        _Monster.transform.GetChild(i).GetComponent<cMonsterBase>().DIE += ReMoveMonster;
                        _Monster.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    _isMonster = true;
                }
                if (_NowMap.transform.Find("Box") != null)
                {
                    _Box = _NowMap.transform.Find("Box").gameObject;
                    for (int i = 0; i < _Box.transform.childCount; ++i)
                    {
                        _BoxList.Add(_Box.transform.GetChild(0).gameObject);
                    }

                }
                if (_NowMap.transform.Find("Fairy") != null)
                {
                    _Fairy = _NowMap.transform.Find("Fairy").gameObject;
                }
            }
            else if (_NowMap.gameObject.CompareTag("NoMonsterMap"))
            {
                _Fairy = null;
                _Box = null;
                _Monster = null;
            }
            }
    }
    //박스 세팅
    void SetBox()
    {
        if (_NowMap.gameObject.CompareTag("MonsterMap"))
        {
            int RandomIndex = Random.Range(1, 101);
            
            if(RandomIndex >= 1 && RandomIndex <= 50)
            {
                GameObject obj = Instantiate(_NoonBox) as GameObject;
                obj.transform.position = _Box.transform.position;
                obj.transform.SetParent(_Box.transform);
                _BoxList.Add(obj);
            }
           else if (RandomIndex >= 51 && RandomIndex <= 80)
            {
                GameObject obj = Instantiate(_NormalBox) as GameObject;
                obj.transform.position = _Box.transform.position;
                obj.transform.SetParent(_Box.transform);
                _BoxList.Add(obj);

            }
            else if (RandomIndex >= 81 && RandomIndex <= 95)
            {
                GameObject obj = Instantiate(_RareBox) as GameObject;
                obj.transform.position = _Box.transform.position;
                obj.transform.SetParent(_Box.transform);
                _BoxList.Add(obj);

            }
            else if (RandomIndex >= 95 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_UniqueBox) as GameObject;
                obj.transform.position = _Box.transform.position;
                obj.transform.SetParent(_Box.transform);
                _BoxList.Add(obj);

            }

            SetFairy();
        }

    }
    //페어리세팅
    void SetFairy()
    {
        int RandomIndex = Random.Range(1, 101);
        if (RandomIndex >= 1 && RandomIndex <= 90)
        {
            GameObject obj = Instantiate(_SmallFairy) as GameObject;

            obj.transform.position = _Fairy.transform.position;
              obj.transform.SetParent(_Fairy.transform);
        }
        else if (RandomIndex >= 91 && RandomIndex <= 100)
        {
            GameObject obj = Instantiate(_BigFairy) as GameObject;
            obj.transform.SetParent(_Fairy.transform);
            obj.transform.position = _Fairy.transform.position;
        }
      

    }

    void ReMoveMonster(GameObject gameObject) {
        _MonsterList.Remove(gameObject.transform);
    }
}
