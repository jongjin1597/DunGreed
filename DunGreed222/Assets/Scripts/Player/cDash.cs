using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//대쉬 슬롯
public class cDash :MonoBehaviour
{
   
    //대쉬 슬롯 이미지 배열
    private GameObject[] _DashSlot =new GameObject[3];
   

    private void Start()
    {
        for(int i=0; i < 3; ++i)
        {
            _DashSlot[i]=transform.GetChild(i).gameObject;
        }
    }
    //활성화
    public void SetEnabled(int DashCount)
    {
        if (DashCount <0)
        {
            return;
        }
        _DashSlot[DashCount].SetActive(true);

    }
    //비활성화
    public void SetEnabledfasle(int DashCount)
    {
        if (DashCount >2)
        {
            return;
        }
        _DashSlot[DashCount].SetActive(false);

    }
  
}
