using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMapManager : cSingleton<cMapManager>
{
    //전체 맵리스트
    public List<cMap> _MapList = new List<cMap>();
    //열린맵 리스트
    public List<cMap> _OpenMapList = new List<cMap>();
    //닫힌맵리스트
    public List<cMap> _CloseMapList = new List<cMap>();

    void Start()
    {
        for ( int i =0; i<20; ++i)
        {
            _MapList.Add(transform.GetChild(i).GetComponent<cMap>());
            if (transform.GetChild(i + 1) == null)
            {
                return;
            }
        }
        _OpenMapList.Add(transform.GetChild(0).GetComponent<cMap>());
        for(int i =1; i < 20; ++i)
        {
            _CloseMapList.Add(transform.GetChild(i).GetComponent<cMap>());
            if (transform.GetChild(i + 1) == null)
            {
                return;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
