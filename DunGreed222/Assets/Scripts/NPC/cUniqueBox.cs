﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cUniqueBox : cNPC
{
    SpriteRenderer _Renderer;
    int _ItemCount=0;
    protected override void Awake()
    {

        base.Awake();
        _Renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isPlayer)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                ItemDrop();
            }
    
        } 
    }
     void ItemDrop()
    {
        if (_ItemCount == 0)
        {
            int _RandNum = Random.Range(6, 10);         // 유니크등급아이템뽑기
            if (_RandNum >= 6 && _RandNum < 10)
            {
                GameObject _DropNode = null;
                _DropNode = (GameObject)Instantiate(Resources.Load("Prefabs/Item/DropNode"));
                _DropNode.transform.position = this.transform.position;

                Item _ItemValue = cDataBaseManager.GetInstance._ItemList[_RandNum];
                ItemDrop a_RefItemInfo = _DropNode.GetComponent<ItemDrop>();
                if (a_RefItemInfo != null)
                {

                    a_RefItemInfo.SetItem(_ItemValue);
                    a_RefItemInfo.transform.Rotate(new Vector3(0, 0, 90));
                    //a_RefItemInfo.StartItem();
                }
                // 동적으로 텍스쳐 이미지 바꾸기
                SpriteRenderer a_RefRender = _DropNode.GetComponent<SpriteRenderer>();
                a_RefRender.sprite = _ItemValue._ItemIcon;

            }
            _ItemCount += 1;
            _Renderer.sprite = Resources.Load<Sprite>("Itemp/YellowTresureOpened");
        }
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
