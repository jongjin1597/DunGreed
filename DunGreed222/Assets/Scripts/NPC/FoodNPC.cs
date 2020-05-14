using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FoodNPC : cNPC
{

    //음식점
    public GameObject _FoodTable;
    protected override void Awake()
    {
        base.Awake();
    }
    
    private void Update()
    {
        if (_isPlayer)
        {

            if (Input.GetKeyDown(KeyCode.F)) {

                SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetActive(false);
            }
        }
    }
    //인벤토리 껏다켰다하는 함수
    public void SetActive(bool Active)
    {
        if (Active) {
            _FoodTable.gameObject.SetActive(true);
        }
       else if (!Active)
        {
            _FoodTable.gameObject.SetActive(false);
        }
    }

    //플레이어 충돌여부 및 F버튼 활성화 여부체크
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
            if (collision.gameObject.tag == "Player")
            {
              _ButtonF.SetActive(true);
                _isPlayer = true;
            }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    
            if (collision.gameObject.tag == "Player")
            {
                _ButtonF.SetActive(false);
                _isPlayer = false;
            }
        
    }
}
